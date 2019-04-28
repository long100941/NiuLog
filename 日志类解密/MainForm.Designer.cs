namespace 日志类解密
{
    partial class MainForm
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
            this.RSAjiemi = new System.Windows.Forms.Button();
            this.jiemixianshi = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AESjiemi = new System.Windows.Forms.Button();
            this.dakai = new System.Windows.Forms.OpenFileDialog();
            this.jindu = new System.Windows.Forms.ProgressBar();
            this.zhuangtai = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RSAjiemi
            // 
            this.RSAjiemi.Location = new System.Drawing.Point(12, 12);
            this.RSAjiemi.Name = "RSAjiemi";
            this.RSAjiemi.Size = new System.Drawing.Size(75, 23);
            this.RSAjiemi.TabIndex = 0;
            this.RSAjiemi.Text = "RSA解密";
            this.RSAjiemi.UseVisualStyleBackColor = true;
            this.RSAjiemi.Click += new System.EventHandler(this.RSAjiemi_Click);
            // 
            // jiemixianshi
            // 
            this.jiemixianshi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jiemixianshi.Location = new System.Drawing.Point(0, 61);
            this.jiemixianshi.Name = "jiemixianshi";
            this.jiemixianshi.Size = new System.Drawing.Size(659, 391);
            this.jiemixianshi.TabIndex = 1;
            this.jiemixianshi.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zhuangtai);
            this.panel1.Controls.Add(this.AESjiemi);
            this.panel1.Controls.Add(this.RSAjiemi);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 51);
            this.panel1.TabIndex = 2;
            // 
            // AESjiemi
            // 
            this.AESjiemi.Location = new System.Drawing.Point(93, 12);
            this.AESjiemi.Name = "AESjiemi";
            this.AESjiemi.Size = new System.Drawing.Size(75, 23);
            this.AESjiemi.TabIndex = 1;
            this.AESjiemi.Text = "AES解密";
            this.AESjiemi.UseVisualStyleBackColor = true;
            this.AESjiemi.Click += new System.EventHandler(this.AESjiemi_Click);
            // 
            // dakai
            // 
            this.dakai.Filter = "日志文本文件|*.txt|所有文件|*.*";
            this.dakai.Title = "选择加密的日志文件";
            // 
            // jindu
            // 
            this.jindu.Dock = System.Windows.Forms.DockStyle.Top;
            this.jindu.Location = new System.Drawing.Point(0, 51);
            this.jindu.Name = "jindu";
            this.jindu.Size = new System.Drawing.Size(659, 10);
            this.jindu.TabIndex = 3;
            // 
            // zhuangtai
            // 
            this.zhuangtai.AutoSize = true;
            this.zhuangtai.Location = new System.Drawing.Point(174, 17);
            this.zhuangtai.Name = "zhuangtai";
            this.zhuangtai.Size = new System.Drawing.Size(53, 12);
            this.zhuangtai.TabIndex = 2;
            this.zhuangtai.Text = "准备就绪";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 452);
            this.Controls.Add(this.jiemixianshi);
            this.Controls.Add(this.jindu);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "日志类解密";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RSAjiemi;
        private System.Windows.Forms.RichTextBox jiemixianshi;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AESjiemi;
        private System.Windows.Forms.OpenFileDialog dakai;
        private System.Windows.Forms.ProgressBar jindu;
        private System.Windows.Forms.Label zhuangtai;
    }
}

