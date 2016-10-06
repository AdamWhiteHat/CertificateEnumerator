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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewCertificates = new System.Windows.Forms.DataGridView();
            this.btnSaveCellsAs = new System.Windows.Forms.Button();
            this.saveFileDialogSelectedCells = new System.Windows.Forms.SaveFileDialog();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnGetPublicKey = new System.Windows.Forms.Button();
            this.btnCertRevocationLists = new System.Windows.Forms.Button();
            this.btnSearchFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewCertificates
            // 
            this.dataGridViewCertificates.AllowUserToAddRows = false;
            this.dataGridViewCertificates.AllowUserToDeleteRows = false;
            this.dataGridViewCertificates.AllowUserToOrderColumns = true;
            this.dataGridViewCertificates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCertificates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCertificates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewCertificates.Location = new System.Drawing.Point(2, 28);
            this.dataGridViewCertificates.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewCertificates.Name = "dataGridViewCertificates";
            this.dataGridViewCertificates.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCertificates.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCertificates.RowHeadersVisible = false;
            this.dataGridViewCertificates.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dataGridViewCertificates.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCertificates.Size = new System.Drawing.Size(861, 382);
            this.dataGridViewCertificates.TabIndex = 4;
            this.dataGridViewCertificates.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewCertificates_KeyUp);
            // 
            // btnSaveCellsAs
            // 
            this.btnSaveCellsAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveCellsAs.Location = new System.Drawing.Point(760, 2);
            this.btnSaveCellsAs.Name = "btnSaveCellsAs";
            this.btnSaveCellsAs.Size = new System.Drawing.Size(100, 23);
            this.btnSaveCellsAs.TabIndex = 7;
            this.btnSaveCellsAs.Text = "Save as...";
            this.btnSaveCellsAs.UseVisualStyleBackColor = true;
            this.btnSaveCellsAs.Click += new System.EventHandler(this.btnSaveCellsAs_Click);
            // 
            // saveFileDialogSelectedCells
            // 
            this.saveFileDialogSelectedCells.DefaultExt = "txt";
            this.saveFileDialogSelectedCells.Filter = "Text files|*.txt|Html files|*.html|CSV File|*.csv|Excel File|*.xlsx|All Files|*.*" +
    "";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(261, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(3, 4);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(252, 20);
            this.tbSearch.TabIndex = 9;
            this.tbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyUp);
            // 
            // btnGetPublicKey
            // 
            this.btnGetPublicKey.Location = new System.Drawing.Point(433, 2);
            this.btnGetPublicKey.Name = "btnGetPublicKey";
            this.btnGetPublicKey.Size = new System.Drawing.Size(100, 23);
            this.btnGetPublicKey.TabIndex = 10;
            this.btnGetPublicKey.Text = "Get public keys";
            this.btnGetPublicKey.UseVisualStyleBackColor = true;
            this.btnGetPublicKey.Click += new System.EventHandler(this.btnGetPublicKey_Click);
            // 
            // btnCertRevocationLists
            // 
            this.btnCertRevocationLists.Location = new System.Drawing.Point(537, 2);
            this.btnCertRevocationLists.Name = "btnCertRevocationLists";
            this.btnCertRevocationLists.Size = new System.Drawing.Size(100, 23);
            this.btnCertRevocationLists.TabIndex = 11;
            this.btnCertRevocationLists.Text = "Get all CRLs";
            this.btnCertRevocationLists.UseVisualStyleBackColor = true;
            this.btnCertRevocationLists.Click += new System.EventHandler(this.btnCertRevocationLists_Click);
            // 
            // btnSearchFolder
            // 
            this.btnSearchFolder.Location = new System.Drawing.Point(643, 2);
            this.btnSearchFolder.Name = "btnSearchFolder";
            this.btnSearchFolder.Size = new System.Drawing.Size(100, 23);
            this.btnSearchFolder.TabIndex = 12;
            this.btnSearchFolder.Text = "Search Folder...";
            this.btnSearchFolder.UseVisualStyleBackColor = true;
            this.btnSearchFolder.Click += new System.EventHandler(this.btnSearchFolder_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 413);
            this.Controls.Add(this.btnSearchFolder);
            this.Controls.Add(this.btnCertRevocationLists);
            this.Controls.Add(this.btnGetPublicKey);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnSaveCellsAs);
            this.Controls.Add(this.dataGridViewCertificates);
            this.MinimumSize = new System.Drawing.Size(563, 300);
            this.Name = "MainForm";
            this.Text = "Certificate Enumerator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewCertificates;
		private System.Windows.Forms.Button btnSaveCellsAs;
		private System.Windows.Forms.SaveFileDialog saveFileDialogSelectedCells;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnGetPublicKey;
        private System.Windows.Forms.Button btnCertRevocationLists;
        private System.Windows.Forms.Button btnSearchFolder;
    }
}

