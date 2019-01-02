namespace msmqReceiver
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.接受到的内容 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.retextbox = new System.Windows.Forms.TextBox();
            this.接受到的内容.SuspendLayout();
            this.SuspendLayout();
            // 
            // 接受到的内容
            // 
            this.接受到的内容.Controls.Add(this.button1);
            this.接受到的内容.Controls.Add(this.retextbox);
            this.接受到的内容.Location = new System.Drawing.Point(12, 8);
            this.接受到的内容.Name = "接受到的内容";
            this.接受到的内容.Size = new System.Drawing.Size(776, 426);
            this.接受到的内容.TabIndex = 1;
            this.接受到的内容.TabStop = false;
            this.接受到的内容.Text = "接收内容";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(696, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // retextbox
            // 
            this.retextbox.Location = new System.Drawing.Point(6, 20);
            this.retextbox.Multiline = true;
            this.retextbox.Name = "retextbox";
            this.retextbox.Size = new System.Drawing.Size(764, 371);
            this.retextbox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.接受到的内容);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.接受到的内容.ResumeLayout(false);
            this.接受到的内容.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox 接受到的内容;
        private System.Windows.Forms.TextBox retextbox;
        private System.Windows.Forms.Button button1;
    }
}

