namespace Hirschmann
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
            this.panelCamera1 = new System.Windows.Forms.Panel();
            this.hWindowControlCamera1 = new HalconDotNet.HWindowControl();
            this.labelCamera1 = new System.Windows.Forms.Label();
            this.panelCamera2 = new System.Windows.Forms.Panel();
            this.hWindowControlCamera2 = new HalconDotNet.HWindowControl();
            this.pictureBoxCamera2 = new System.Windows.Forms.PictureBox();
            this.labelCamera2 = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.labelNokRate = new System.Windows.Forms.Label();
            this.labelNokParts = new System.Windows.Forms.Label();
            this.labelOkParts = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userAdministrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageProgramsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonDummyTesting = new System.Windows.Forms.Button();
            this.buttonDisableSystem = new System.Windows.Forms.Button();
            this.buttonSelecProgram = new System.Windows.Forms.Button();
            this.buttonResetInspectionData = new System.Windows.Forms.Button();
            this.labelSelectedProgram = new System.Windows.Forms.Label();
            this.labelSelectedProgramName = new System.Windows.Forms.Label();
            this.panelSelectedProgram = new System.Windows.Forms.Panel();
            this.buttonAlarms = new System.Windows.Forms.Button();
            this.reconnectCamerasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectCamera1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reconnectCamera2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCamera1.SuspendLayout();
            this.panelCamera2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera2)).BeginInit();
            this.panelStats.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelSelectedProgram.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCamera1
            // 
            this.panelCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera1.Controls.Add(this.hWindowControlCamera1);
            this.panelCamera1.Controls.Add(this.labelCamera1);
            this.panelCamera1.Location = new System.Drawing.Point(50, 100);
            this.panelCamera1.Name = "panelCamera1";
            this.panelCamera1.Size = new System.Drawing.Size(850, 800);
            this.panelCamera1.TabIndex = 0;
            // 
            // hWindowControlCamera1
            // 
            this.hWindowControlCamera1.BackColor = System.Drawing.Color.Black;
            this.hWindowControlCamera1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControlCamera1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControlCamera1.Location = new System.Drawing.Point(0, 45);
            this.hWindowControlCamera1.Name = "hWindowControlCamera1";
            this.hWindowControlCamera1.Size = new System.Drawing.Size(848, 753);
            this.hWindowControlCamera1.TabIndex = 1;
            this.hWindowControlCamera1.WindowSize = new System.Drawing.Size(848, 753);
            // 
            // labelCamera1
            // 
            this.labelCamera1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCamera1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera1.Location = new System.Drawing.Point(0, 0);
            this.labelCamera1.Name = "labelCamera1";
            this.labelCamera1.Size = new System.Drawing.Size(848, 45);
            this.labelCamera1.TabIndex = 0;
            this.labelCamera1.Text = "CAMERA 1";
            this.labelCamera1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelCamera2
            // 
            this.panelCamera2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera2.Controls.Add(this.hWindowControlCamera2);
            this.panelCamera2.Controls.Add(this.pictureBoxCamera2);
            this.panelCamera2.Controls.Add(this.labelCamera2);
            this.panelCamera2.Location = new System.Drawing.Point(1020, 100);
            this.panelCamera2.Name = "panelCamera2";
            this.panelCamera2.Size = new System.Drawing.Size(850, 799);
            this.panelCamera2.TabIndex = 1;
            // 
            // hWindowControlCamera2
            // 
            this.hWindowControlCamera2.BackColor = System.Drawing.Color.Black;
            this.hWindowControlCamera2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlCamera2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControlCamera2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControlCamera2.Location = new System.Drawing.Point(0, 45);
            this.hWindowControlCamera2.Name = "hWindowControlCamera2";
            this.hWindowControlCamera2.Size = new System.Drawing.Size(848, 752);
            this.hWindowControlCamera2.TabIndex = 3;
            this.hWindowControlCamera2.WindowSize = new System.Drawing.Size(848, 752);
            // 
            // pictureBoxCamera2
            // 
            this.pictureBoxCamera2.Location = new System.Drawing.Point(3, 47);
            this.pictureBoxCamera2.Name = "pictureBoxCamera2";
            this.pictureBoxCamera2.Size = new System.Drawing.Size(842, 747);
            this.pictureBoxCamera2.TabIndex = 2;
            this.pictureBoxCamera2.TabStop = false;
            // 
            // labelCamera2
            // 
            this.labelCamera2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCamera2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCamera2.Location = new System.Drawing.Point(0, 0);
            this.labelCamera2.Name = "labelCamera2";
            this.labelCamera2.Size = new System.Drawing.Size(848, 45);
            this.labelCamera2.TabIndex = 1;
            this.labelCamera2.Text = "CAMERA 2";
            this.labelCamera2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelStats
            // 
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.labelNokRate);
            this.panelStats.Controls.Add(this.labelNokParts);
            this.panelStats.Controls.Add(this.labelOkParts);
            this.panelStats.Controls.Add(this.labelTotal);
            this.panelStats.Location = new System.Drawing.Point(1021, 905);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(850, 74);
            this.panelStats.TabIndex = 4;
            // 
            // labelNokRate
            // 
            this.labelNokRate.AutoSize = true;
            this.labelNokRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNokRate.ForeColor = System.Drawing.Color.Gold;
            this.labelNokRate.Location = new System.Drawing.Point(630, 19);
            this.labelNokRate.Name = "labelNokRate";
            this.labelNokRate.Size = new System.Drawing.Size(215, 29);
            this.labelNokRate.TabIndex = 3;
            this.labelNokRate.Text = "NOK RATE: 92.7%";
            this.labelNokRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNokParts
            // 
            this.labelNokParts.AutoSize = true;
            this.labelNokParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNokParts.ForeColor = System.Drawing.Color.Red;
            this.labelNokParts.Location = new System.Drawing.Point(395, 18);
            this.labelNokParts.Name = "labelNokParts";
            this.labelNokParts.Size = new System.Drawing.Size(190, 29);
            this.labelNokParts.TabIndex = 2;
            this.labelNokParts.Text = "NOK PARTS: 73";
            this.labelNokParts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOkParts
            // 
            this.labelOkParts.AutoSize = true;
            this.labelOkParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOkParts.ForeColor = System.Drawing.Color.Lime;
            this.labelOkParts.Location = new System.Drawing.Point(178, 18);
            this.labelOkParts.Name = "labelOkParts";
            this.labelOkParts.Size = new System.Drawing.Size(185, 29);
            this.labelOkParts.TabIndex = 1;
            this.labelOkParts.Text = "OK PARTS: 927";
            this.labelOkParts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotal.Location = new System.Drawing.Point(3, 18);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(156, 29);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "TOTAL: 1000";
            this.labelTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userAdministrationToolStripMenuItem,
            this.manageProgramsToolStripMenuItem,
            this.debugMenuToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.reconnectCamerasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1904, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userAdministrationToolStripMenuItem
            // 
            this.userAdministrationToolStripMenuItem.Name = "userAdministrationToolStripMenuItem";
            this.userAdministrationToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.userAdministrationToolStripMenuItem.Text = "User Administration";
            this.userAdministrationToolStripMenuItem.Click += new System.EventHandler(this.UserAdministrationToolStripMenuItemClick);
            // 
            // manageProgramsToolStripMenuItem
            // 
            this.manageProgramsToolStripMenuItem.Name = "manageProgramsToolStripMenuItem";
            this.manageProgramsToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
            this.manageProgramsToolStripMenuItem.Text = "Manage Programs";
            this.manageProgramsToolStripMenuItem.Click += new System.EventHandler(this.ManageProgramsToolStripMenuItemClick);
            // 
            // debugMenuToolStripMenuItem
            // 
            this.debugMenuToolStripMenuItem.Name = "debugMenuToolStripMenuItem";
            this.debugMenuToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.debugMenuToolStripMenuItem.Text = "Debug Menu";
            this.debugMenuToolStripMenuItem.Click += new System.EventHandler(this.DebugMenuToolStripMenuItemClick);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.logsToolStripMenuItem.Text = "Logs";
            this.logsToolStripMenuItem.Click += new System.EventHandler(this.LogsToolStripMenuItemClick);
            // 
            // panelButtons
            // 
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.buttonDummyTesting);
            this.panelButtons.Controls.Add(this.buttonDisableSystem);
            this.panelButtons.Controls.Add(this.buttonSelecProgram);
            this.panelButtons.Controls.Add(this.buttonResetInspectionData);
            this.panelButtons.Location = new System.Drawing.Point(50, 906);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(850, 74);
            this.panelButtons.TabIndex = 5;
            // 
            // buttonDummyTesting
            // 
            this.buttonDummyTesting.BackColor = System.Drawing.Color.Yellow;
            this.buttonDummyTesting.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDummyTesting.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDummyTesting.Location = new System.Drawing.Point(449, 3);
            this.buttonDummyTesting.Name = "buttonDummyTesting";
            this.buttonDummyTesting.Size = new System.Drawing.Size(195, 66);
            this.buttonDummyTesting.TabIndex = 15;
            this.buttonDummyTesting.Text = "DUMMY TESTING";
            this.buttonDummyTesting.UseVisualStyleBackColor = false;
            this.buttonDummyTesting.Click += new System.EventHandler(this.ButtonDummyTestingClick);
            // 
            // buttonDisableSystem
            // 
            this.buttonDisableSystem.BackColor = System.Drawing.Color.Crimson;
            this.buttonDisableSystem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDisableSystem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDisableSystem.Location = new System.Drawing.Point(204, 3);
            this.buttonDisableSystem.Name = "buttonDisableSystem";
            this.buttonDisableSystem.Size = new System.Drawing.Size(195, 66);
            this.buttonDisableSystem.TabIndex = 14;
            this.buttonDisableSystem.Text = "DISABLE SYSTEM";
            this.buttonDisableSystem.UseVisualStyleBackColor = false;
            this.buttonDisableSystem.Click += new System.EventHandler(this.ButtonDisableSystemClick);
            // 
            // buttonSelecProgram
            // 
            this.buttonSelecProgram.BackColor = System.Drawing.Color.Lime;
            this.buttonSelecProgram.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSelecProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelecProgram.Location = new System.Drawing.Point(3, 3);
            this.buttonSelecProgram.Name = "buttonSelecProgram";
            this.buttonSelecProgram.Size = new System.Drawing.Size(195, 66);
            this.buttonSelecProgram.TabIndex = 13;
            this.buttonSelecProgram.Text = "SELECT PROGRAM";
            this.buttonSelecProgram.UseVisualStyleBackColor = false;
            this.buttonSelecProgram.Click += new System.EventHandler(this.ButtonSelectProgramClick);
            // 
            // buttonResetInspectionData
            // 
            this.buttonResetInspectionData.BackColor = System.Drawing.Color.Cyan;
            this.buttonResetInspectionData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonResetInspectionData.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonResetInspectionData.Location = new System.Drawing.Point(650, 3);
            this.buttonResetInspectionData.Name = "buttonResetInspectionData";
            this.buttonResetInspectionData.Size = new System.Drawing.Size(195, 66);
            this.buttonResetInspectionData.TabIndex = 12;
            this.buttonResetInspectionData.Text = "RESET INSPECTION DATA";
            this.buttonResetInspectionData.UseVisualStyleBackColor = false;
            this.buttonResetInspectionData.Click += new System.EventHandler(this.ButtonResetInspectionDataClick);
            // 
            // labelSelectedProgram
            // 
            this.labelSelectedProgram.AutoSize = true;
            this.labelSelectedProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedProgram.Location = new System.Drawing.Point(3, 18);
            this.labelSelectedProgram.Name = "labelSelectedProgram";
            this.labelSelectedProgram.Size = new System.Drawing.Size(238, 31);
            this.labelSelectedProgram.TabIndex = 12;
            this.labelSelectedProgram.Text = "Selected Program:";
            // 
            // labelSelectedProgramName
            // 
            this.labelSelectedProgramName.AutoSize = true;
            this.labelSelectedProgramName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSelectedProgramName.Location = new System.Drawing.Point(247, 18);
            this.labelSelectedProgramName.Name = "labelSelectedProgramName";
            this.labelSelectedProgramName.Size = new System.Drawing.Size(23, 31);
            this.labelSelectedProgramName.TabIndex = 13;
            this.labelSelectedProgramName.Text = "-";
            // 
            // panelSelectedProgram
            // 
            this.panelSelectedProgram.Controls.Add(this.labelSelectedProgram);
            this.panelSelectedProgram.Controls.Add(this.labelSelectedProgramName);
            this.panelSelectedProgram.Location = new System.Drawing.Point(12, 31);
            this.panelSelectedProgram.Name = "panelSelectedProgram";
            this.panelSelectedProgram.Size = new System.Drawing.Size(888, 67);
            this.panelSelectedProgram.TabIndex = 14;
            // 
            // buttonAlarms
            // 
            this.buttonAlarms.BackColor = System.Drawing.Color.LightSeaGreen;
            this.buttonAlarms.Enabled = false;
            this.buttonAlarms.FlatAppearance.BorderSize = 0;
            this.buttonAlarms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAlarms.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAlarms.Location = new System.Drawing.Point(1770, 31);
            this.buttonAlarms.Name = "buttonAlarms";
            this.buttonAlarms.Size = new System.Drawing.Size(122, 63);
            this.buttonAlarms.TabIndex = 15;
            this.buttonAlarms.Text = "ALARMS";
            this.buttonAlarms.UseVisualStyleBackColor = false;
            // 
            // reconnectCamerasToolStripMenuItem
            // 
            this.reconnectCamerasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reconnectCamera1ToolStripMenuItem,
            this.reconnectCamera2ToolStripMenuItem});
            this.reconnectCamerasToolStripMenuItem.Name = "reconnectCamerasToolStripMenuItem";
            this.reconnectCamerasToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.reconnectCamerasToolStripMenuItem.Text = "Reconnect Cameras";
            // 
            // reconnectCamera1ToolStripMenuItem
            // 
            this.reconnectCamera1ToolStripMenuItem.Name = "reconnectCamera1ToolStripMenuItem";
            this.reconnectCamera1ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.reconnectCamera1ToolStripMenuItem.Text = "Reconnect Camera 1";
            this.reconnectCamera1ToolStripMenuItem.Click += new System.EventHandler(this.ReconnectCamera1ToolStripMenuItemClick);
            // 
            // reconnectCamera2ToolStripMenuItem
            // 
            this.reconnectCamera2ToolStripMenuItem.Name = "reconnectCamera2ToolStripMenuItem";
            this.reconnectCamera2ToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.reconnectCamera2ToolStripMenuItem.Text = "Reconnect Camera 2";
            this.reconnectCamera2ToolStripMenuItem.Click += new System.EventHandler(this.ReconnectCamera2ToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.buttonAlarms);
            this.Controls.Add(this.panelSelectedProgram);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelCamera2);
            this.Controls.Add(this.panelCamera1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " HIRSCHMANN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.panelCamera1.ResumeLayout(false);
            this.panelCamera2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera2)).EndInit();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelSelectedProgram.ResumeLayout(false);
            this.panelSelectedProgram.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCamera1;
        private System.Windows.Forms.Panel panelCamera2;
        private System.Windows.Forms.Label labelCamera1;
        private System.Windows.Forms.Label labelCamera2;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label labelNokRate;
        private System.Windows.Forms.Label labelNokParts;
        private System.Windows.Forms.Label labelOkParts;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem userAdministrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonDummyTesting;
        private System.Windows.Forms.Button buttonDisableSystem;
        private System.Windows.Forms.Button buttonSelecProgram;
        private System.Windows.Forms.Button buttonResetInspectionData;
        private System.Windows.Forms.Label labelSelectedProgram;
        private System.Windows.Forms.Label labelSelectedProgramName;
        private System.Windows.Forms.Panel panelSelectedProgram;
        private System.Windows.Forms.PictureBox pictureBoxCamera2;
        private System.Windows.Forms.ToolStripMenuItem manageProgramsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugMenuToolStripMenuItem;
        private HalconDotNet.HWindowControl hWindowControlCamera1;
        private HalconDotNet.HWindowControl hWindowControlCamera2;
        private System.Windows.Forms.Button buttonAlarms;
        private System.Windows.Forms.ToolStripMenuItem reconnectCamerasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectCamera1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reconnectCamera2ToolStripMenuItem;
    }
}

