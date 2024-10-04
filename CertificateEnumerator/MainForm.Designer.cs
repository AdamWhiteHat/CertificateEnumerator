
namespace CertificateManagement
{
	partial class MainForm
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
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnShowCertStore = new System.Windows.Forms.Button();
			this.btnShowCertRevocationList = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.flowLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.btnShowCertStore);
			this.flowLayoutPanel1.Controls.Add(this.btnShowCertRevocationList);
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(29, 172);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(9, 9, 9, 9);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 9, 0, 9);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(243, 273);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btnShowCertStore
			// 
			this.btnShowCertStore.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnShowCertStore.AutoEllipsis = true;
			this.btnShowCertStore.AutoSize = true;
			this.btnShowCertStore.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnShowCertStore.BackColor = System.Drawing.Color.Gainsboro;
			this.btnShowCertStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowCertStore.Location = new System.Drawing.Point(17, 16);
			this.btnShowCertStore.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.btnShowCertStore.Name = "btnShowCertStore";
			this.btnShowCertStore.Size = new System.Drawing.Size(209, 26);
			this.btnShowCertStore.TabIndex = 0;
			this.btnShowCertStore.Text = "View Certificates in Certificate Store";
			this.btnShowCertStore.UseVisualStyleBackColor = false;
			this.btnShowCertStore.Click += new System.EventHandler(this.btnShowCertStore_Click);
			// 
			// btnShowCertRevocationList
			// 
			this.btnShowCertRevocationList.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnShowCertRevocationList.AutoSize = true;
			this.btnShowCertRevocationList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnShowCertRevocationList.BackColor = System.Drawing.Color.Gainsboro;
			this.btnShowCertRevocationList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnShowCertRevocationList.Location = new System.Drawing.Point(6, 56);
			this.btnShowCertRevocationList.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
			this.btnShowCertRevocationList.Name = "btnShowCertRevocationList";
			this.btnShowCertRevocationList.Size = new System.Drawing.Size(231, 26);
			this.btnShowCertRevocationList.TabIndex = 1;
			this.btnShowCertRevocationList.Text = "Certificate Revocation List Management";
			this.btnShowCertRevocationList.UseVisualStyleBackColor = false;
			this.btnShowCertRevocationList.Click += new System.EventHandler(this.btnShowCertRevocationList_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.10223F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.89777F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(301, 454);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox1.Image = global::CertificateEnumerator.Properties.Resources.Certificate;
			this.pictureBox1.Location = new System.Drawing.Point(86, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(129, 157);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = global::CertificateEnumerator.Properties.Resources.GuillocheHalfSeal_25Percent_GreenBlue_Transparent;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(307, 460);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Text = "Certificate Manager";
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnShowCertStore;
		private System.Windows.Forms.Button btnShowCertRevocationList;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}