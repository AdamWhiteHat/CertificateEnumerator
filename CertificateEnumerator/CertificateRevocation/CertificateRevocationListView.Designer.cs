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
            dataGridViewCertificates = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCertificates).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewCertificates
            // 
            dataGridViewCertificates.AllowUserToAddRows = false;
            dataGridViewCertificates.AllowUserToDeleteRows = false;
            dataGridViewCertificates.AllowUserToOrderColumns = true;
            dataGridViewCertificates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCertificates.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridViewCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridViewCertificates.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            dataGridViewCertificates.Location = new System.Drawing.Point(0, 0);
            dataGridViewCertificates.Name = "dataGridViewCertificates";
            dataGridViewCertificates.RowHeadersVisible = false;
            dataGridViewCertificates.RowHeadersWidth = 51;
            dataGridViewCertificates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            dataGridViewCertificates.ShowEditingIcon = false;
            dataGridViewCertificates.Size = new System.Drawing.Size(800, 450);
            dataGridViewCertificates.StandardTab = true;
            dataGridViewCertificates.TabIndex = 0;
            // 
            // CertificateRevocationListView
            // 
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(dataGridViewCertificates);
            Name = "CertificateRevocationListView";
            Text = "Revoked Certificates";
            Load += CertificateRevocationListView_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewCertificates).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewCertificates;
    }
}