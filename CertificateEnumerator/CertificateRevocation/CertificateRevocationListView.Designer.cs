namespace CertificateManagement
{
    partial class CertificateRevocationListView
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
            dataGridViewCertificates = new System.Windows.Forms.DataGridView();
            contextMenuDataGridRow = new System.Windows.Forms.ContextMenuStrip(components);
            downloadInstallCRLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            downloadCRLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            installCRLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            dataGridViewCertificates.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCertificates.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCertificates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCertificates.ContextMenuStrip = contextMenuDataGridRow;
            dataGridViewCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewCertificates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridViewCertificates.EnableHeadersVisualStyles = false;
            dataGridViewCertificates.Location = new System.Drawing.Point(0, 0);
            dataGridViewCertificates.Margin = new System.Windows.Forms.Padding(0);
            dataGridViewCertificates.Name = "dataGridViewCertificates";
            dataGridViewCertificates.ReadOnly = true;
            dataGridViewCertificates.RowHeadersVisible = false;
            dataGridViewCertificates.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridViewCertificates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCertificates.ShowEditingIcon = false;
            dataGridViewCertificates.Size = new System.Drawing.Size(1182, 459);
            dataGridViewCertificates.TabIndex = 0;
            dataGridViewCertificates.KeyUp += dataGridViewCertificates_KeyUp;
            // 
            // contextMenuDataGridRow
            // 
            contextMenuDataGridRow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { downloadInstallCRLToolStripMenuItem, downloadCRLToolStripMenuItem, installCRLToolStripMenuItem });
            contextMenuDataGridRow.Name = "contextMenuDataGridRow";
            contextMenuDataGridRow.Size = new System.Drawing.Size(213, 70);
            contextMenuDataGridRow.Opening += contextMenuDataGridRow_Opening;
            // 
            // downloadInstallCRLToolStripMenuItem
            // 
            downloadInstallCRLToolStripMenuItem.Name = "downloadInstallCRLToolStripMenuItem";
            downloadInstallCRLToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            downloadInstallCRLToolStripMenuItem.Text = "&Download && Install CRL";
            downloadInstallCRLToolStripMenuItem.Click += downloadInstallCRLToolStripMenuItem_Click;
            // 
            // downloadCRLToolStripMenuItem
            // 
            downloadCRLToolStripMenuItem.Name = "downloadCRLToolStripMenuItem";
            downloadCRLToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            downloadCRLToolStripMenuItem.Text = "Download CRL";
            downloadCRLToolStripMenuItem.Click += downloadCRLToolStripMenuItem_Click;
            // 
            // installCRLToolStripMenuItem
            // 
            installCRLToolStripMenuItem.Name = "installCRLToolStripMenuItem";
            installCRLToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            installCRLToolStripMenuItem.Text = "Install CRL";
            installCRLToolStripMenuItem.Click += installCRLToolStripMenuItem_Click;
            // 
            // CertificateRevocationListView
            // 
            ClientSize = new System.Drawing.Size(1182, 459);
            Controls.Add(dataGridViewCertificates);
            Name = "CertificateRevocationListView";
            Text = "Certificate Revocation Lists (CRLs)";
            Load += CertificateRevocationListView_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewCertificates).EndInit();
            contextMenuDataGridRow.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCertificates;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataGridRow;
        private System.Windows.Forms.ToolStripMenuItem downloadInstallCRLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadCRLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installCRLToolStripMenuItem;
    }
}