
namespace dictionary
{
    partial class top
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.titleLabel = new System.Windows.Forms.Label();
			this.manualLabel = new System.Windows.Forms.LinkLabel();
			this.indexLabel = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// titleLabel
			// 
			this.titleLabel.AutoSize = true;
			this.titleLabel.Location = new System.Drawing.Point(22, 33);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new System.Drawing.Size(86, 12);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "マニュアル作成中";
			// 
			// manualLabel
			// 
			this.manualLabel.AutoSize = true;
			this.manualLabel.Location = new System.Drawing.Point(22, 103);
			this.manualLabel.Name = "manualLabel";
			this.manualLabel.Size = new System.Drawing.Size(50, 12);
			this.manualLabel.TabIndex = 1;
			this.manualLabel.TabStop = true;
			this.manualLabel.Text = "マニュアル";
			this.manualLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.contents_Clicked);
			// 
			// indexLabel
			// 
			this.indexLabel.AutoSize = true;
			this.indexLabel.Location = new System.Drawing.Point(22, 147);
			this.indexLabel.Name = "indexLabel";
			this.indexLabel.Size = new System.Drawing.Size(29, 12);
			this.indexLabel.TabIndex = 2;
			this.indexLabel.TabStop = true;
			this.indexLabel.Text = "索引";
			this.indexLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.index_Clicked);
			// 
			// top
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.indexLabel);
			this.Controls.Add(this.manualLabel);
			this.Controls.Add(this.titleLabel);
			this.Name = "top";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.top_FormClosing);
			this.Load += new System.EventHandler(this.top_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.LinkLabel manualLabel;
        private System.Windows.Forms.LinkLabel indexLabel;
    }
}

