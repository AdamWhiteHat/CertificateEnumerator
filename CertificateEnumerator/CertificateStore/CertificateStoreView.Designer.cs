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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewCertificates = new System.Windows.Forms.DataGridView();
            contextMenuDataGridRow = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemExport = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            btnSaveCellsAs = new System.Windows.Forms.Button();
            saveFileDialogSelectedCells = new System.Windows.Forms.SaveFileDialog();
            btnSearch = new System.Windows.Forms.Button();
            tbSearch = new System.Windows.Forms.TextBox();
            btnGetPublicKey = new System.Windows.Forms.Button();
            btnSearchFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCertificates).BeginInit();
            contextMenuDataGridRow.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewCertificates
            // 
            dataGridViewCertificates.AllowUserToAddRows = false;
            dataGridViewCertificates.AllowUserToDeleteRows = false;
            dataGridViewCertificates.AllowUserToOrderColumns = true;
            dataGridViewCertificates.AllowUserToResizeRows = false;
            dataGridViewCertificates.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCertificates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCertificates.ContextMenuStrip = contextMenuDataGridRow;
            dataGridViewCertificates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridViewCertificates.Location = new System.Drawing.Point(2, 28);
            dataGridViewCertificates.Margin = new System.Windows.Forms.Padding(0);
            dataGridViewCertificates.Name = "dataGridViewCertificates";
            dataGridViewCertificates.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCertificates.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCertificates.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCertificates.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCertificates.Size = new System.Drawing.Size(930, 413);
            dataGridViewCertificates.TabIndex = 4;
            dataGridViewCertificates.CellContextMenuStripNeeded += dataGridViewCertificates_CellContextMenuStripNeeded;
            dataGridViewCertificates.Sorted += dataGridViewCertificates_Sorted;
            dataGridViewCertificates.KeyUp += dataGridViewCertificates_KeyUp;
            // 
            // contextMenuDataGridRow
            // 
            contextMenuDataGridRow.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuDataGridRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemOpen, toolStripMenuItemExport, toolStripSeparator1, toolStripMenuItemCopy, toolStripMenuItemDelete, toolStripSeparator2, toolStripMenuItemSelectAll });
            contextMenuDataGridRow.Name = "contextMenuDataGridRow";
            contextMenuDataGridRow.Size = new System.Drawing.Size(141, 136);
            // 
            // toolStripMenuItemOpen
            // 
            toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            toolStripMenuItemOpen.Size = new System.Drawing.Size(140, 24);
            toolStripMenuItemOpen.Text = "Open";
            toolStripMenuItemOpen.Click += toolStripMenuItemOpen_Click;
            // 
            // toolStripMenuItemExport
            // 
            toolStripMenuItemExport.Name = "toolStripMenuItemExport";
            toolStripMenuItemExport.Size = new System.Drawing.Size(140, 24);
            toolStripMenuItemExport.Text = "Export...";
            toolStripMenuItemExport.Click += toolStripMenuItemExport_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(137, 6);
            // 
            // toolStripMenuItemCopy
            // 
            toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            toolStripMenuItemCopy.Size = new System.Drawing.Size(140, 24);
            toolStripMenuItemCopy.Text = "Copy";
            toolStripMenuItemCopy.Click += toolStripMenuItemCopy_Click;
            // 
            // toolStripMenuItemDelete
            // 
            toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            toolStripMenuItemDelete.Size = new System.Drawing.Size(140, 24);
            toolStripMenuItemDelete.Text = "Delete";
            toolStripMenuItemDelete.Click += toolStripMenuItemDelete_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(137, 6);
            // 
            // toolStripMenuItemSelectAll
            // 
            toolStripMenuItemSelectAll.Name = "toolStripMenuItemSelectAll";
            toolStripMenuItemSelectAll.Size = new System.Drawing.Size(140, 24);
            toolStripMenuItemSelectAll.Text = "Select All";
            toolStripMenuItemSelectAll.Click += toolStripMenuItemSelectAll_Click;
            // 
            // btnSaveCellsAs
            // 
            btnSaveCellsAs.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSaveCellsAs.Location = new System.Drawing.Point(841, 2);
            btnSaveCellsAs.Name = "btnSaveCellsAs";
            btnSaveCellsAs.Size = new System.Drawing.Size(91, 23);
            btnSaveCellsAs.TabIndex = 7;
            btnSaveCellsAs.Text = "Save list as...";
            btnSaveCellsAs.UseVisualStyleBackColor = true;
            btnSaveCellsAs.Click += btnSaveCellsAs_Click;
            // 
            // saveFileDialogSelectedCells
            // 
            saveFileDialogSelectedCells.DefaultExt = "txt";
            saveFileDialogSelectedCells.Filter = "Text files|*.txt|Html files|*.html|CSV File|*.csv|Excel File|*.xlsx|All Files|*.*";
            saveFileDialogSelectedCells.FilterIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSearch.Location = new System.Drawing.Point(251, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(86, 23);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // tbSearch
            // 
            tbSearch.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbSearch.Location = new System.Drawing.Point(2, 2);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new System.Drawing.Size(248, 27);
            tbSearch.TabIndex = 9;
            tbSearch.KeyUp += tbSearch_KeyUp;
            // 
            // btnGetPublicKey
            // 
            btnGetPublicKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnGetPublicKey.Location = new System.Drawing.Point(694, 2);
            btnGetPublicKey.Name = "btnGetPublicKey";
            btnGetPublicKey.Size = new System.Drawing.Size(148, 23);
            btnGetPublicKey.TabIndex = 10;
            btnGetPublicKey.Text = "Save public keys to...";
            btnGetPublicKey.UseVisualStyleBackColor = true;
            btnGetPublicKey.Click += btnGetPublicKey_Click;
            // 
            // btnSearchFolder
            // 
            btnSearchFolder.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSearchFolder.Location = new System.Drawing.Point(336, 2);
            btnSearchFolder.Name = "btnSearchFolder";
            btnSearchFolder.Size = new System.Drawing.Size(158, 23);
            btnSearchFolder.TabIndex = 12;
            btnSearchFolder.Text = "Search folder...";
            btnSearchFolder.UseVisualStyleBackColor = true;
            btnSearchFolder.Click += btnSearchFolder_Click;
            // 
            // CertificateStoreView
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(933, 444);
            Controls.Add(btnSearchFolder);
            Controls.Add(btnGetPublicKey);
            Controls.Add(tbSearch);
            Controls.Add(btnSearch);
            Controls.Add(btnSaveCellsAs);
            Controls.Add(dataGridViewCertificates);
            MinimumSize = new System.Drawing.Size(850, 300);
            Name = "CertificateStoreView";
            Text = "Certificate Enumerator";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewCertificates).EndInit();
            contextMenuDataGridRow.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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

