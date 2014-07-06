using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLLFuncTest
{
    public partial class ParaControl : UserControl
    {
        public string paraName { get; private set; }

        public string paraDataType { get; private set; }

        public object InputValue { get; private set; }

        public ParaControl(string paraName,string paraDataType)
        {
            InitializeComponent();

            lblName.Text = paraName+"：";
            this.paraName = paraName;
            this.paraDataType = paraDataType;
            lblType.Text = paraDataType;
        }

        private void tbValue_Leave(object sender, EventArgs e)
        {
            //MessageBox.Show(tbValue.Text);
            switch (this.paraDataType)
            {
                case "int":
                    int intVal = 0;
                    if (int.TryParse(tbValue.Text, out intVal))
                    {
                        InputValue = intVal;
                    }
                    else
                    {
                        MessageBox.Show("错误参数值");
                        //this.tbValue.Focus();
                    }
                    break;

                case "string":
                    if (string.IsNullOrEmpty(tbValue.Text))
                    {
                        MessageBox.Show("参数不能为空");
                        //this.tbValue.Focus();
                    }
                    else
                    {
                        InputValue = tbValue.Text;
                    }
                    break;

                case "datetime":
                    DateTime dtValue;
                    if (DateTime.TryParse(tbValue.Text, out dtValue))
                    {
                        InputValue = dtValue;
                    }
                    else
                    {
                        MessageBox.Show("错误参数值");
                        //this.tbValue.Focus();
                    }
                    break;

                case "bool":
                    if (tbValue.Text.ToLower() == "true" || tbValue.Text.ToLower() == "false")
                    {
                        InputValue = tbValue.Text.ToLower() == "true";
                    }
                    else
                    {
                        MessageBox.Show("请输入”true“或”false“。");
                        //this.tbValue.Focus();
                    }
                    break;

                case "byte":
                    byte byteVal;
                    if (Byte.TryParse(tbValue.Text, out byteVal))
                    {
                        InputValue = byteVal;
                    }
                    else
                    {
                        MessageBox.Show("请输入0~255范围的整数。");
                        //this.tbValue.Focus();
                    }
                    break;
                default:
                    InputValue = "";
                    break;
            }
        }
    }
}
