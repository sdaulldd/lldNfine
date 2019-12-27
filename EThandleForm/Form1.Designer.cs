namespace EThandleForm
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
            this.InstallETService = new System.Windows.Forms.Button();
            this.StartETService = new System.Windows.Forms.Button();
            this.StopETService = new System.Windows.Forms.Button();
            this.UnstallETService = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InstallETService
            // 
            this.InstallETService.Location = new System.Drawing.Point(67, 129);
            this.InstallETService.Name = "InstallETService";
            this.InstallETService.Size = new System.Drawing.Size(112, 23);
            this.InstallETService.TabIndex = 0;
            this.InstallETService.Text = "安装服务";
            this.InstallETService.UseVisualStyleBackColor = true;
            this.InstallETService.Click += new System.EventHandler(this.InstallETService_Click);
            // 
            // StartETService
            // 
            this.StartETService.Location = new System.Drawing.Point(219, 129);
            this.StartETService.Name = "StartETService";
            this.StartETService.Size = new System.Drawing.Size(97, 23);
            this.StartETService.TabIndex = 4;
            this.StartETService.Text = "启动服务";
            this.StartETService.Click += new System.EventHandler(this.StartETService_Click);
            // 
            // StopETService
            // 
            this.StopETService.Location = new System.Drawing.Point(338, 129);
            this.StopETService.Name = "StopETService";
            this.StopETService.Size = new System.Drawing.Size(119, 23);
            this.StopETService.TabIndex = 2;
            this.StopETService.Text = "停止服务";
            this.StopETService.UseVisualStyleBackColor = true;
            this.StopETService.Click += new System.EventHandler(this.StopETService_Click);
            // 
            // UnstallETService
            // 
            this.UnstallETService.Location = new System.Drawing.Point(494, 129);
            this.UnstallETService.Name = "UnstallETService";
            this.UnstallETService.Size = new System.Drawing.Size(118, 23);
            this.UnstallETService.TabIndex = 3;
            this.UnstallETService.Text = "卸载服务";
            this.UnstallETService.UseVisualStyleBackColor = true;
            this.UnstallETService.Click += new System.EventHandler(this.UnstallETService_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 275);
            this.Controls.Add(this.UnstallETService);
            this.Controls.Add(this.StopETService);
            this.Controls.Add(this.StartETService);
            this.Controls.Add(this.InstallETService);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button InstallETService;
        private System.Windows.Forms.Button StartETService;
        private System.Windows.Forms.Button StopETService;
        private System.Windows.Forms.Button UnstallETService;
    }
}

