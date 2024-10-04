namespace CertificateManagement
{
	partial class CertificateStoreView
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridViewCertificates = new System.Windows.Forms.DataGridView();
			this.contextMenuDataGridRow = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.btnSaveCellsAs = new System.Windows.Forms.Button();
			this.saveFileDialogSelectedCells = new System.Windows.Forms.SaveFileDialog();
			this.btnSearch = new System.Windows.Forms.Button();
			this.tbSearch = new System.Windows.Forms.TextBox();
			this.btnGetPublicKey = new System.Windows.Forms.Button();
			this.btnSearchFolder = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).BeginInit();
			this.contextMenuDataGridRow.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridViewCertificates
			// 
			this.dataGridViewCertificates.AllowUserToAddRows = false;
			this.dataGridViewCertificates.AllowUserToDeleteRows = false;
			this.dataGridViewCertificates.AllowUserToResizeRows = false;
			this.dataGridViewCertificates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCertificates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCertificates.ContextMenuStrip = this.contextMenuDataGridRow;
			this.dataGridViewCertificates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewCertificates.Location = new System.Drawing.Point(2, 28);
			this.dataGridViewCertificates.Margin = new System.Windows.Forms.Padding(0);
			this.dataGridViewCertificates.Name = "dataGridViewCertificates";
			this.dataGridViewCertificates.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCertificates.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewCertificates.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
			this.dataGridViewCertificates.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCertificates.Size = new System.Drawing.Size(930, 413);
			this.dataGridViewCertificates.TabIndex = 4;
			this.dataGridViewCertificates.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dataGridViewCertificates_CellContextMenuStripNeeded);
			this.dataGridViewCertificates.Sorted += new System.EventHandler(this.dataGridViewCertificates_Sorted);
			this.dataGridViewCertificates.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewCertificates_KeyUp);
			// 
			// contextMenuDataGridRow
			// 
			this.contextMenuDataGridRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen,
            this.toolStripMenuItemExport,
            this.toolStripSeparator1,
            this.toolStripMenuItemCopy,
            this.toolStripMenuItemDelete,
            this.toolStripSeparator2,
            this.toolStripMenuItemSelectAll});
			this.contextMenuDataGridRow.Name = "contextMenuDataGridRow";
			this.contextMenuDataGridRow.Size = new System.Drawing.Size(123, 126);
			// 
			// toolStripMenuItemOpen
			// 
			this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
			this.toolStripMenuItemOpen.Size = new System.Drawing.Size(122, 22);
			this.toolStripMenuItemOpen.Text = "Open";
			this.toolStripMenuItemOpen.Click += new System.EventHandler(this.toolStripMenuItemOpen_Click);
			// 
			// toolStripMenuItemExport
			// 
			this.toolStripMenuItemExport.Name = "toolStripMenuItemExport";
			this.toolStripMenuItemExport.Size = new System.Drawing.Size(122, 22);
			this.toolStripMenuItemExport.Text = "Export...";
			this.toolStripMenuItemExport.Click += new System.EventHandler(this.toolStripMenuItemExport_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
			// 
			// toolStripMenuItemCopy
			// 
			this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
			this.toolStripMenuItemCopy.Size = new System.Drawing.Size(122, 22);
			this.toolStripMenuItemCopy.Text = "Copy";
			this.toolStripMenuItemCopy.Click += new System.EventHandler(this.toolStripMenuItemCopy_Click);
			// 
			// toolStripMenuItemDelete
			// 
			this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
			this.toolStripMenuItemDelete.Size = new System.Drawing.Size(122, 22);
			this.toolStripMenuItemDelete.Text = "Delete";
			this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
			// 
			// toolStripMenuItemSelectAll
			// 
			this.toolStripMenuItemSelectAll.Name = "toolStripMenuItemSelectAll";
			this.toolStripMenuItemSelectAll.Size = new System.Drawing.Size(122, 22);
			this.toolStripMenuItemSelectAll.Text = "Select All";
			this.toolStripMenuItemSelectAll.Click += new System.EventHandler(this.toolStripMenuItemSelectAll_Click);
			// 
			// btnSaveCellsAs
			// 
			this.btnSaveCellsAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveCellsAs.Location = new System.Drawing.Point(841, 2);
			this.btnSaveCellsAs.Name = "btnSaveCellsAs";
			this.btnSaveCellsAs.Size = new System.Drawing.Size(91, 23);
			this.btnSaveCellsAs.TabIndex = 7;
			this.btnSaveCellsAs.Text = "Save list as...";
			this.btnSaveCellsAs.UseVisualStyleBackColor = true;
			this.btnSaveCellsAs.Click += new System.EventHandler(this.btnSaveCellsAs_Click);
			// 
			// saveFileDialogSelectedCells
			// 
			this.saveFileDialogSelectedCells.DefaultExt = "txt";
			this.saveFileDialogSelectedCells.Filter = "Text files|*.txt|Html files|*.html|CSV File|*.csv|Excel File|*.xlsx|All Files|*.*" +
    "";
			this.saveFileDialogSelectedCells.FilterIndex = 3;
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(251, 2);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(86, 23);
			this.btnSearch.TabIndex = 8;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// tbSearch
			// 
			this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbSearch.Location = new System.Drawing.Point(2, 2);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.Size = new System.Drawing.Size(248, 23);
			this.tbSearch.TabIndex = 9;
			this.tbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyUp);
			// 
			// btnGetPublicKey
			// 
			this.btnGetPublicKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGetPublicKey.Location = new System.Drawing.Point(694, 2);
			this.btnGetPublicKey.Name = "btnGetPublicKey";
			this.btnGetPublicKey.Size = new System.Drawing.Size(148, 23);
			this.btnGetPublicKey.TabIndex = 10;
			this.btnGetPublicKey.Text = "Save public keys to...";
			this.btnGetPublicKey.UseVisualStyleBackColor = true;
			this.btnGetPublicKey.Click += new System.EventHandler(this.btnGetPublicKey_Click);
			// 
			// btnSearchFolder
			// 
			this.btnSearchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchFolder.Location = new System.Drawing.Point(336, 2);
			this.btnSearchFolder.Name = "btnSearchFolder";
			this.btnSearchFolder.Size = new System.Drawing.Size(158, 23);
			this.btnSearchFolder.TabIndex = 12;
			this.btnSearchFolder.Text = "Search folder...";
			this.btnSearchFolder.UseVisualStyleBackColor = true;
			this.btnSearchFolder.Click += new System.EventHandler(this.btnSearchFolder_Click);
			// 
			// CertificateStoreView
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(933, 444);
			this.Controls.Add(this.btnSearchFolder);
			this.Controls.Add(this.btnGetPublicKey);
			this.Controls.Add(this.tbSearch);
			this.Controls.Add(this.btnSearch);
			this.Controls.Add(this.btnSaveCellsAs);
			this.Controls.Add(this.dataGridViewCertificates);
			this.MinimumSize = new System.Drawing.Size(850, 300);
			this.Name = "CertificateStoreView";
			this.Text = "Certificate Enumerator";
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCertificates)).EndInit();
			this.contextMenuDataGridRow.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnSearchFolder;
		private System.Windows.Forms.ContextMenuStrip contextMenuDataGridRow;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExport;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSelectAll;
	}
}

