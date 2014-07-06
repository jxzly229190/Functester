namespace BLLFuncTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbbClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFunction = new System.Windows.Forms.ListBox();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.pnlOperate = new System.Windows.Forms.Panel();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbbClass
            // 
            this.cbbClass.FormattingEnabled = true;
            this.cbbClass.Location = new System.Drawing.Point(60, 12);
            this.cbbClass.Name = "cbbClass";
            this.cbbClass.Size = new System.Drawing.Size(249, 20);
            this.cbbClass.TabIndex = 0;
            this.cbbClass.SelectedIndexChanged += new System.EventHandler(this.cbbClass_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "类名：";
            // 
            // lbFunction
            // 
            
            this.lbFunction.FormattingEnabled = true;
            this.lbFunction.ItemHeight = 12;
            this.lbFunction.Location = new System.Drawing.Point(15, 38);
            this.lbFunction.Name = "lbFunction";
            this.lbFunction.Size = new System.Drawing.Size(294, 424);
            this.lbFunction.TabIndex = 2;
            this.lbFunction.SelectedIndexChanged += new System.EventHandler(this.lbFunction_SelectedIndexChanged);
            // 
            // tbInfo
            // 
            this.tbInfo.Location = new System.Drawing.Point(324, 210);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(666, 252);
            this.tbInfo.TabIndex = 3;
            // 
            // pnlOperate
            // 
            this.pnlOperate.AutoScroll = true;
            this.pnlOperate.Location = new System.Drawing.Point(324, 22);
            this.pnlOperate.Name = "pnlOperate";
            this.pnlOperate.Size = new System.Drawing.Size(666, 148);
            this.pnlOperate.TabIndex = 4;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(854, 176);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(136, 28);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "执行";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 487);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pnlOperate);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.lbFunction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbClass);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbFunction;
        private System.Windows.Forms.TextBox tbInfo;
        private System.Windows.Forms.Panel pnlOperate;
        private System.Windows.Forms.Button btnOk;
    }
}

