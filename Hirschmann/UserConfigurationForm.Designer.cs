namespace Hirschmann
{
    partial class UserConfigurationForm
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
            this.labelRegisteredUsers = new System.Windows.Forms.Label();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.buttonAddNewUser = new System.Windows.Forms.Button();
            this.buttonDeleteSelectedUser = new System.Windows.Forms.Button();
            this.columnBadgeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnrank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRegisteredUsers
            // 
            this.labelRegisteredUsers.AutoSize = true;
            this.labelRegisteredUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegisteredUsers.Location = new System.Drawing.Point(12, 9);
            this.labelRegisteredUsers.Name = "labelRegisteredUsers";
            this.labelRegisteredUsers.Size = new System.Drawing.Size(220, 31);
            this.labelRegisteredUsers.TabIndex = 0;
            this.labelRegisteredUsers.Text = "Registered users";
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnBadgeId,
            this.columnrank});
            this.dataGridViewUsers.Location = new System.Drawing.Point(12, 43);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUsers.Size = new System.Drawing.Size(352, 251);
            this.dataGridViewUsers.TabIndex = 1;
            // 
            // buttonAddNewUser
            // 
            this.buttonAddNewUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddNewUser.Location = new System.Drawing.Point(370, 43);
            this.buttonAddNewUser.Name = "buttonAddNewUser";
            this.buttonAddNewUser.Size = new System.Drawing.Size(172, 35);
            this.buttonAddNewUser.TabIndex = 2;
            this.buttonAddNewUser.Text = "Add New User";
            this.buttonAddNewUser.UseVisualStyleBackColor = true;
            this.buttonAddNewUser.Click += new System.EventHandler(this.ButtonAddNewUserClick);
            // 
            // buttonDeleteSelectedUser
            // 
            this.buttonDeleteSelectedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteSelectedUser.Location = new System.Drawing.Point(370, 85);
            this.buttonDeleteSelectedUser.Name = "buttonDeleteSelectedUser";
            this.buttonDeleteSelectedUser.Size = new System.Drawing.Size(172, 35);
            this.buttonDeleteSelectedUser.TabIndex = 3;
            this.buttonDeleteSelectedUser.Text = "Delete Selected User";
            this.buttonDeleteSelectedUser.UseVisualStyleBackColor = true;
            this.buttonDeleteSelectedUser.Click += new System.EventHandler(this.ButtonDeleteSelectedUserClick);
            // 
            // columnBadgeId
            // 
            this.columnBadgeId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnBadgeId.HeaderText = "Badge ID";
            this.columnBadgeId.Name = "columnBadgeId";
            this.columnBadgeId.ReadOnly = true;
            // 
            // columnrank
            // 
            this.columnrank.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnrank.HeaderText = "Rank";
            this.columnrank.Name = "columnrank";
            this.columnrank.ReadOnly = true;
            // 
            // UserConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 311);
            this.Controls.Add(this.buttonDeleteSelectedUser);
            this.Controls.Add(this.buttonAddNewUser);
            this.Controls.Add(this.dataGridViewUsers);
            this.Controls.Add(this.labelRegisteredUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UserConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRegisteredUsers;
        private System.Windows.Forms.DataGridView dataGridViewUsers;
        private System.Windows.Forms.Button buttonAddNewUser;
        private System.Windows.Forms.Button buttonDeleteSelectedUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnBadgeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnrank;
    }
}