namespace Hirschmann
{
    partial class LogsForm
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
            this.dataGridViewLogs = new System.Windows.Forms.DataGridView();
            this.columnIdBadge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelCamera1 = new System.Windows.Forms.Label();
            this.pictureBoxCamera1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxCamera2 = new System.Windows.Forms.PictureBox();
            this.labelCamera2 = new System.Windows.Forms.Label();
            this.panelCamera1 = new System.Windows.Forms.Panel();
            this.panelCamera2 = new System.Windows.Forms.Panel();
            this.buttonExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera2)).BeginInit();
            this.panelCamera1.SuspendLayout();
            this.panelCamera2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewLogs
            // 
            this.dataGridViewLogs.AllowUserToAddRows = false;
            this.dataGridViewLogs.AllowUserToDeleteRows = false;
            this.dataGridViewLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLogs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIdBadge,
            this.columnAction,
            this.columnDate});
            this.dataGridViewLogs.Location = new System.Drawing.Point(12, 62);
            this.dataGridViewLogs.Name = "dataGridViewLogs";
            this.dataGridViewLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLogs.Size = new System.Drawing.Size(500, 537);
            this.dataGridViewLogs.TabIndex = 0;
            this.dataGridViewLogs.SelectionChanged += new System.EventHandler(this.DataGridViewLogsSelectionChanged);
            // 
            // columnIdBadge
            // 
            this.columnIdBadge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnIdBadge.HeaderText = "Badge ID";
            this.columnIdBadge.Name = "columnIdBadge";
            this.columnIdBadge.ReadOnly = true;
            this.columnIdBadge.Width = 77;
            // 
            // columnAction
            // 
            this.columnAction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnAction.HeaderText = "Action";
            this.columnAction.Name = "columnAction";
            this.columnAction.ReadOnly = true;
            // 
            // columnDate
            // 
            this.columnDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnDate.HeaderText = "Date";
            this.columnDate.Name = "columnDate";
            this.columnDate.ReadOnly = true;
            this.columnDate.Width = 55;
            // 
            // labelCamera1
            // 
            this.labelCamera1.AutoSize = true;
            this.labelCamera1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera1.Location = new System.Drawing.Point(3, -1);
            this.labelCamera1.Margin = new System.Windows.Forms.Padding(3);
            this.labelCamera1.Name = "labelCamera1";
            this.labelCamera1.Size = new System.Drawing.Size(132, 31);
            this.labelCamera1.TabIndex = 1;
            this.labelCamera1.Text = "Camera 1";
            // 
            // pictureBoxCamera1
            // 
            this.pictureBoxCamera1.Location = new System.Drawing.Point(3, 34);
            this.pictureBoxCamera1.Name = "pictureBoxCamera1";
            this.pictureBoxCamera1.Size = new System.Drawing.Size(643, 251);
            this.pictureBoxCamera1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCamera1.TabIndex = 2;
            this.pictureBoxCamera1.TabStop = false;
            // 
            // pictureBoxCamera2
            // 
            this.pictureBoxCamera2.Location = new System.Drawing.Point(3, 34);
            this.pictureBoxCamera2.Name = "pictureBoxCamera2";
            this.pictureBoxCamera2.Size = new System.Drawing.Size(643, 251);
            this.pictureBoxCamera2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCamera2.TabIndex = 4;
            this.pictureBoxCamera2.TabStop = false;
            // 
            // labelCamera2
            // 
            this.labelCamera2.AutoSize = true;
            this.labelCamera2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera2.Location = new System.Drawing.Point(3, -1);
            this.labelCamera2.Margin = new System.Windows.Forms.Padding(3);
            this.labelCamera2.Name = "labelCamera2";
            this.labelCamera2.Size = new System.Drawing.Size(132, 31);
            this.labelCamera2.TabIndex = 3;
            this.labelCamera2.Text = "Camera 2";
            // 
            // panelCamera1
            // 
            this.panelCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera1.Controls.Add(this.labelCamera1);
            this.panelCamera1.Controls.Add(this.pictureBoxCamera1);
            this.panelCamera1.Location = new System.Drawing.Point(521, 12);
            this.panelCamera1.Name = "panelCamera1";
            this.panelCamera1.Size = new System.Drawing.Size(651, 290);
            this.panelCamera1.TabIndex = 6;
            // 
            // panelCamera2
            // 
            this.panelCamera2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera2.Controls.Add(this.labelCamera2);
            this.panelCamera2.Controls.Add(this.pictureBoxCamera2);
            this.panelCamera2.Location = new System.Drawing.Point(521, 309);
            this.panelCamera2.Name = "panelCamera2";
            this.panelCamera2.Size = new System.Drawing.Size(654, 290);
            this.panelCamera2.TabIndex = 7;
            // 
            // buttonExport
            // 
            this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExport.Location = new System.Drawing.Point(12, 12);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(228, 44);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "EXPORT";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.ButtonExportClick);
            // 
            // LogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 611);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.panelCamera2);
            this.Controls.Add(this.panelCamera1);
            this.Controls.Add(this.dataGridViewLogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LogsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logs";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera2)).EndInit();
            this.panelCamera1.ResumeLayout(false);
            this.panelCamera1.PerformLayout();
            this.panelCamera2.ResumeLayout(false);
            this.panelCamera2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewLogs;
        private System.Windows.Forms.Label labelCamera1;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnIdBadge;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnAction;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnDate;
        private System.Windows.Forms.PictureBox pictureBoxCamera1;
        private System.Windows.Forms.PictureBox pictureBoxCamera2;
        private System.Windows.Forms.Label labelCamera2;
        private System.Windows.Forms.Panel panelCamera1;
        private System.Windows.Forms.Panel panelCamera2;
        private System.Windows.Forms.Button buttonExport;
    }
}