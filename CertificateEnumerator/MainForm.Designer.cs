namespace CertificateEnumerator
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
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.btnEnumerate = new System.Windows.Forms.Button();
			this.selectStoreLocation = new System.Windows.Forms.ComboBox();
			this.selectStoreName = new System.Windows.Forms.ListBox();
			this.dataGridViewCertificates = new System.Windows.Forms.DataGridView();
			this.btnVerifyCerts = new System.Windows.Forms.Button();
			this.btnSaveAs = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).BeginInit();
			this.SuspendLayout();
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbOutput.Location = new System.Drawing.Point(2, 108);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbOutput.Size = new System.Drawing.Size(668, 172);
			this.tbOutput.TabIndex = 0;
			this.tbOutput.WordWrap = false;
			// 
			// btnEnumerate
			// 
			this.btnEnumerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEnumerate.Location = new System.Drawing.Point(504, 12);
			this.btnEnumerate.Name = "btnEnumerate";
			this.btnEnumerate.Size = new System.Drawing.Size(75, 23);
			this.btnEnumerate.TabIndex = 1;
			this.btnEnumerate.Text = "Enumerate";
			this.btnEnumerate.UseVisualStyleBackColor = true;
			this.btnEnumerate.Click += new System.EventHandler(this.btnEnumerate_Click);
			// 
			// selectStoreLocation
			// 
			this.selectStoreLocation.FormattingEnabled = true;
			this.selectStoreLocation.Location = new System.Drawing.Point(12, 7);
			this.selectStoreLocation.Name = "selectStoreLocation";
			this.selectStoreLocation.Size = new System.Drawing.Size(113, 21);
			this.selectStoreLocation.TabIndex = 2;
			// 
			// selectStoreName
			// 
			this.selectStoreName.FormattingEnabled = true;
			this.selectStoreName.Location = new System.Drawing.Point(131, 7);
			this.selectStoreName.Name = "selectStoreName";
			this.selectStoreName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.selectStoreName.Size = new System.Drawing.Size(160, 95);
			this.selectStoreName.TabIndex = 3;
			// 
			// dataGridViewCertificates
			// 
			this.dataGridViewCertificates.AllowUserToAddRows = false;
			this.dataGridViewCertificates.AllowUserToDeleteRows = false;
			this.dataGridViewCertificates.AllowUserToOrderColumns = true;
			this.dataGridViewCertificates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCertificates.Location = new System.Drawing.Point(2, 286);
			this.dataGridViewCertificates.Name = "dataGridViewCertificates";
			this.dataGridViewCertificates.ReadOnly = true;
			this.dataGridViewCertificates.RowHeadersVisible = false;
			this.dataGridViewCertificates.RowHeadersWidth = 5;
			this.dataGridViewCertificates.Size = new System.Drawing.Size(668, 191);
			this.dataGridViewCertificates.TabIndex = 4;
			// 
			// btnVerifyCerts
			// 
			this.btnVerifyCerts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnVerifyCerts.Location = new System.Drawing.Point(585, 41);
			this.btnVerifyCerts.Name = "btnVerifyCerts";
			this.btnVerifyCerts.Size = new System.Drawing.Size(75, 23);
			this.btnVerifyCerts.TabIndex = 5;
			this.btnVerifyCerts.Text = "Verify Certs";
			this.btnVerifyCerts.UseVisualStyleBackColor = true;
			this.btnVerifyCerts.Click += new System.EventHandler(this.btnVerifyCerts_Click);
			// 
			// btnSaveAs
			// 
			this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveAs.Location = new System.Drawing.Point(585, 12);
			this.btnSaveAs.Name = "btnSaveAs";
			this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
			this.btnSaveAs.TabIndex = 6;
			this.btnSaveAs.Text = "Save as...";
			this.btnSaveAs.UseVisualStyleBackColor = true;
			this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(672, 479);
			this.Controls.Add(this.btnSaveAs);
			this.Controls.Add(this.btnVerifyCerts);
			this.Controls.Add(this.dataGridViewCertificates);
			this.Controls.Add(this.selectStoreName);
			this.Controls.Add(this.selectStoreLocation);
			this.Controls.Add(this.btnEnumerate);
			this.Controls.Add(this.tbOutput);
			this.Name = "MainForm";
			this.Text = "Certificate Enumerator";
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.Button btnEnumerate;
		private System.Windows.Forms.ComboBox selectStoreLocation;
		private System.Windows.Forms.ListBox selectStoreName;
		private System.Windows.Forms.DataGridView dataGridViewCertificates;
		private System.Windows.Forms.Button btnVerifyCerts;
		private System.Windows.Forms.Button btnSaveAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
	}
}

