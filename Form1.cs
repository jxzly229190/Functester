using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace BLLFuncTest
{
    using Newtonsoft.Json;

    public partial class Form1 : Form
    {
        private Assembly[] assemblies;

        public Form1()
        {
            InitializeComponent();

            ReadAssemblies();
            
            if (assemblies == null)
            {
                MessageBox.Show("反射没有获得程序集");
                return;
            }

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    if (type.IsClass)
                    {
                        cbbClass.Items.Add(type.FullName);
                    }
                }
            }
        }

        private void ReadAssemblies()
        {
            var path = Utility.GetAppConfig("TestBinPath");
            if (!Directory.Exists(path))
            {
                MessageBox.Show("程序集路径不存在");
                return;
            }

            var files = Directory.GetFiles(path).Where(f => f.EndsWith(".dll")).ToArray();

            if (files.Length <= 0)
            {
                return;
            }

            //var dlls = files.Select(f => f.Contains(".dll"));

            assemblies = new Assembly[files.Length];

            for (var i = 0; i < files.Length; i++)
            {
                assemblies[i] = Assembly.LoadFile(files[i]);
            }
        }

        private void lbFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;

            var method = list.SelectedItem as MethodInfo;

            if (method == null)
            {
                MessageBox.Show("获取方法信息失败");
                return;
            }

            var paras = method.GetParameters();
            tbInfo.Clear();
            pnlOperate.Controls.Clear();
            int index = 0;
            foreach (var parameterInfo in paras)
            {
                index++;

                if (parameterInfo.ParameterType.FullName.Contains("System"))
                {
                    this.SetParameterControl(parameterInfo.Name, GetTypeName(parameterInfo.ParameterType.Name), index);
                    tbInfo.AppendText(parameterInfo.Name + "\t" + parameterInfo.ParameterType.Name + "\n");
                }
                else
                {
                    var ptype = this.GetTypeFromAssembly(parameterInfo.ParameterType.FullName);
                    
                    if (ptype != null)
                    {
                        var properties = ptype.GetProperties();

                        int indexY = index;
                        foreach (var propertyInfo in properties)
                        {
                            this.SetParameterControl(propertyInfo.Name, GetTypeName(propertyInfo.ToString()), indexY);
                            tbInfo.AppendText(propertyInfo.Name + "\t" + propertyInfo.ToString() + "\n");
                            indexY++;
                        }
                    }
                }
            }
        }

        private void SetParameterControl(string name, string typeName, int index)
        {
            var paractrl = new ParaControl(name, typeName);

            int x = (index + 1) % 2 * 300;
            paractrl.Location = new Point(x, ((index - 1) / 2) * 25);

            this.pnlOperate.Controls.Add(paractrl);
        }

        private string GetTypeName(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("type");
            }

            if (type.ToLower().Contains("system.int") || type.ToLower().Contains("int16")
                || type.ToLower().Contains("int32") || type.ToLower().Contains("int64"))
            {
                return "int";
            }

            if (type.ToLower().Contains("system.string") || type.ToLower().Contains("string"))
            {
                return "string";
            }

            if (type.ToLower().Contains("boolean") || type.ToLower().Contains("bool"))
            {
                return "bool";
            }

            if (type.ToLower().Contains("system.byte") || type.ToLower().Contains("byte"))
            {
                return "byte";
            }

            if (type.ToLower().Contains("system.datetime") || type.ToLower().Contains("datetime"))
            {
                return "datetime";
            }

            return "";
        }

        private void cbbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFunction.Items.Clear();
            ComboBox cb = sender as ComboBox;
            
            if (cb != null)
            {
                var type = GetTypeFromAssembly(cb.Text);

                if (type != null)
                {
                    var methods = type.GetMethods();

                    foreach (var methodInfo in methods)
                    {
                        lbFunction.Items.Add(methodInfo);
                        this.lbFunction.DisplayMember = "Name";
                    }
                }
                else
                {
                    MessageBox.Show("没有获取到此方法对象：" + cb.Text);
                }
            }
        }

        //获取类型
        private Type GetTypeFromAssembly(string name)
        {
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(name);
                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        private object CreateInstanceFromAssembly(string name)
        {
            object instance;
            foreach (var assembly in assemblies)
            {
                instance = assembly.CreateInstance(name);
                if (instance != null)
                {
                    return instance;
                }
            }
            return null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //执行程序
            var obj = CreateInstanceFromAssembly(cbbClass.Text);
            var mi = lbFunction.SelectedItem as MethodInfo;

            if (mi != null)
            {
                var paraValue = new Dictionary<string, object>();

                foreach (ParaControl inputCtrl in pnlOperate.Controls)
                {
                    paraValue.Add(inputCtrl.paraName, inputCtrl.InputValue);
                }

                var paras = mi.GetParameters();
                var objs = new object[paras.Length];
                var boxedParas = new List<ParameterInfo>();

                int i = 0;
                //简单参数
                foreach (var parameterInfo in paras)
                {
                    if (paraValue.ContainsKey(parameterInfo.Name))
                    {
                        objs.SetValue(paraValue[parameterInfo.Name], i);
                        paraValue.Remove(parameterInfo.Name);
                        i++;
                    }
                    else
                    {
                        boxedParas.Add(parameterInfo);
                    }
                }

                //非简单参数
                #region 拆分非简单参数
                if (boxedParas.Count > 0) //存在非简单参数对象
                {
                    foreach (var parameterInfo in boxedParas)
                    {
                        var pobj = CreateInstanceFromAssembly(parameterInfo.ParameterType.FullName);

                        if (pobj != null)
                        {
                            Type ptype = pobj.GetType();
                            var pinfos = ptype.GetProperties();

                            foreach (var pi in pinfos)
                            {
                                pi.SetValue(pobj, paraValue[pi.Name], null);
                            }
                            objs.SetValue(pobj, i++);
                        }
                    }
                }
                #endregion

                try
                {
                    var result = mi.Invoke(obj, objs);
                    tbInfo.Clear();
                    if (result != null)
                    {
                        tbInfo.Text = JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        tbInfo.Text = "执行成功，返回空值。";
                    }
                }
                catch (Exception ex)
                {
                    tbInfo.AppendText("出错了，错误信息：" + ex.Message + "(" + ex.InnerException + ")\n");
                    tbInfo.AppendText(ex.StackTrace);
                }
            }
        }
    }
}
