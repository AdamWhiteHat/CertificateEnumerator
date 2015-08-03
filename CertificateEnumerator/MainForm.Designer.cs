namespace CertificateEnumeratorGUI
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
			this.dataGridViewCertificates = new System.Windows.Forms.DataGridView();
			this.btnVerifyCerts = new System.Windows.Forms.Button();
			this.btnSaveTextAs = new System.Windows.Forms.Button();
			this.saveFileDialogText = new System.Windows.Forms.SaveFileDialog();
			this.btnSaveCellsAs = new System.Windows.Forms.Button();
			this.saveFileDialogSelectedCells = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).BeginInit();
			this.SuspendLayout();
			// 
			// tbOutput
			// 
			this.tbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbOutput.Location = new System.Drawing.Point(2, 29);
			this.tbOutput.Multiline = true;
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbOutput.Size = new System.Drawing.Size(668, 172);
			this.tbOutput.TabIndex = 0;
			this.tbOutput.WordWrap = false;
			// 
			// btnEnumerate
			// 
			this.btnEnumerate.Location = new System.Drawing.Point(2, 2);
			this.btnEnumerate.Name = "btnEnumerate";
			this.btnEnumerate.Size = new System.Drawing.Size(75, 23);
			this.btnEnumerate.TabIndex = 1;
			this.btnEnumerate.Text = "Enumerate";
			this.btnEnumerate.UseVisualStyleBackColor = true;
			this.btnEnumerate.Click += new System.EventHandler(this.btnEnumerate_Click);
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
			this.dataGridViewCertificates.Location = new System.Drawing.Point(2, 205);
			this.dataGridViewCertificates.Name = "dataGridViewCertificates";
			this.dataGridViewCertificates.ReadOnly = true;
			this.dataGridViewCertificates.RowHeadersVisible = false;
			this.dataGridViewCertificates.RowHeadersWidth = 5;
			this.dataGridViewCertificates.Size = new System.Drawing.Size(668, 271);
			this.dataGridViewCertificates.TabIndex = 4;
			// 
			// btnVerifyCerts
			// 
			this.btnVerifyCerts.Location = new System.Drawing.Point(83, 2);
			this.btnVerifyCerts.Name = "btnVerifyCerts";
			this.btnVerifyCerts.Size = new System.Drawing.Size(75, 23);
			this.btnVerifyCerts.TabIndex = 5;
			this.btnVerifyCerts.Text = "Verify Certs";
			this.btnVerifyCerts.UseVisualStyleBackColor = true;
			this.btnVerifyCerts.Click += new System.EventHandler(this.btnVerifyCerts_Click);
			// 
			// btnSaveTextAs
			// 
			this.btnSaveTextAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveTextAs.Location = new System.Drawing.Point(423, 2);
			this.btnSaveTextAs.Name = "btnSaveTextAs";
			this.btnSaveTextAs.Size = new System.Drawing.Size(100, 23);
			this.btnSaveTextAs.TabIndex = 6;
			this.btnSaveTextAs.Text = "Save text as...";
			this.btnSaveTextAs.UseVisualStyleBackColor = true;
			this.btnSaveTextAs.Click += new System.EventHandler(this.btnSaveTextAs_Click);
			// 
			// btnSaveCellsAs
			// 
			this.btnSaveCellsAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveCellsAs.Location = new System.Drawing.Point(529, 2);
			this.btnSaveCellsAs.Name = "btnSaveCellsAs";
			this.btnSaveCellsAs.Size = new System.Drawing.Size(141, 23);
			this.btnSaveCellsAs.TabIndex = 7;
			this.btnSaveCellsAs.Text = "Save datagrid cells as...";
			this.btnSaveCellsAs.UseVisualStyleBackColor = true;
			this.btnSaveCellsAs.Click += new System.EventHandler(this.btnSaveCellsAs_Click);
			// 
			// saveFileDialogSelectedCells
			// 
			this.saveFileDialogSelectedCells.DefaultExt = "txt";
			this.saveFileDialogSelectedCells.Filter = "Text files|*.txt|Html files|*.html|CSV File|*.csv|All Files|*.*";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(672, 479);
			this.Controls.Add(this.btnSaveCellsAs);
			this.Controls.Add(this.btnSaveTextAs);
			this.Controls.Add(this.btnVerifyCerts);
			this.Controls.Add(this.dataGridViewCertificates);
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
		private System.Windows.Forms.DataGridView dataGridViewCertificates;
		private System.Windows.Forms.Button btnVerifyCerts;
		private System.Windows.Forms.Button btnSaveTextAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialogText;
		private System.Windows.Forms.Button btnSaveCellsAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialogSelectedCells;
	}
}

