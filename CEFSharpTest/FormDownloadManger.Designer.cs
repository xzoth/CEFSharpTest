namespace CEFSharpTest
{
    partial class FormDownloadManger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDownloadSetting = new System.Windows.Forms.Button();
            this.itemPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnDownloadSetting
            // 
            this.btnDownloadSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadSetting.Location = new System.Drawing.Point(198, 3);
            this.btnDownloadSetting.Name = "btnDownloadSetting";
            this.btnDownloadSetting.Size = new System.Drawing.Size(68, 23);
            this.btnDownloadSetting.TabIndex = 1;
            this.btnDownloadSetting.Text = "设置";
            this.btnDownloadSetting.UseVisualStyleBackColor = true;
            // 
            // itemPanel
            // 
            this.itemPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemPanel.AutoScroll = true;
            this.itemPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.itemPanel.Location = new System.Drawing.Point(0, 32);
            this.itemPanel.Name = "itemPanel";
            this.itemPanel.Size = new System.Drawing.Size(279, 275);
            this.itemPanel.TabIndex = 2;
            // 
            // FormDownloadManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 307);
            this.Controls.Add(this.itemPanel);
            this.Controls.Add(this.btnDownloadSetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDownloadManger";
            this.Text = "下载";
            this.Load += new System.EventHandler(this.FormDownloadManger_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDownloadSetting;
        private System.Windows.Forms.FlowLayoutPanel itemPanel;

    }
}