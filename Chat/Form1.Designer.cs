namespace Chat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.charContentRichText = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.settingBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(12, 340);
            this.sendTextBox.Multiline = true;
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(776, 60);
            this.sendTextBox.TabIndex = 2;
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(528, 409);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(108, 32);
            this.selectFileBtn.TabIndex = 4;
            this.selectFileBtn.Text = "选择文件";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // charContentRichText
            // 
            this.charContentRichText.BackColor = System.Drawing.SystemColors.Window;
            this.charContentRichText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.charContentRichText.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.charContentRichText.Location = new System.Drawing.Point(12, 12);
            this.charContentRichText.Name = "charContentRichText";
            this.charContentRichText.ReadOnly = true;
            this.charContentRichText.Size = new System.Drawing.Size(776, 322);
            this.charContentRichText.TabIndex = 5;
            this.charContentRichText.Text = "";
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(660, 409);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(108, 31);
            this.sendBtn.TabIndex = 7;
            this.sendBtn.Text = "发送信息";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // settingBtn
            // 
            this.settingBtn.Location = new System.Drawing.Point(436, 409);
            this.settingBtn.Name = "settingBtn";
            this.settingBtn.Size = new System.Drawing.Size(62, 32);
            this.settingBtn.TabIndex = 8;
            this.settingBtn.Text = "设置";
            this.settingBtn.UseVisualStyleBackColor = true;
            this.settingBtn.Click += new System.EventHandler(this.settingBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.settingBtn);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.charContentRichText);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.sendTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.RichTextBox charContentRichText;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button settingBtn;
    }
}

