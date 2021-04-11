namespace Hirschmann
{
    partial class DummyTestingForm
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
            this.panelPrograms = new System.Windows.Forms.Panel();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.labelPrograms = new System.Windows.Forms.Label();
            this.comboBoxPrograms = new System.Windows.Forms.ComboBox();
            this.panelCamera1 = new System.Windows.Forms.Panel();
            this.hWindowControlCamera1 = new HalconDotNet.HWindowControl();
            this.labelCamera1 = new System.Windows.Forms.Label();
            this.panelCamera2 = new System.Windows.Forms.Panel();
            this.hWindowControlCamera2 = new HalconDotNet.HWindowControl();
            this.labelCamera2 = new System.Windows.Forms.Label();
            this.panelPrograms.SuspendLayout();
            this.panelCamera1.SuspendLayout();
            this.panelCamera2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPrograms
            // 
            this.panelPrograms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPrograms.Controls.Add(this.buttonExecute);
            this.panelPrograms.Controls.Add(this.labelPrograms);
            this.panelPrograms.Controls.Add(this.comboBoxPrograms);
            this.panelPrograms.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPrograms.Location = new System.Drawing.Point(0, 0);
            this.panelPrograms.Name = "panelPrograms";
            this.panelPrograms.Size = new System.Drawing.Size(1484, 66);
            this.panelPrograms.TabIndex = 3;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExecute.Location = new System.Drawing.Point(1221, 3);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(258, 58);
            this.buttonExecute.TabIndex = 3;
            this.buttonExecute.Text = "EXECUTE";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.ButtonExecuteClick);
            // 
            // labelPrograms
            // 
            this.labelPrograms.AutoSize = true;
            this.labelPrograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrograms.Location = new System.Drawing.Point(12, 15);
            this.labelPrograms.Name = "labelPrograms";
            this.labelPrograms.Size = new System.Drawing.Size(134, 25);
            this.labelPrograms.TabIndex = 0;
            this.labelPrograms.Text = "PROGRAMS";
            // 
            // comboBoxPrograms
            // 
            this.comboBoxPrograms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrograms.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPrograms.FormattingEnabled = true;
            this.comboBoxPrograms.Location = new System.Drawing.Point(152, 12);
            this.comboBoxPrograms.Name = "comboBoxPrograms";
            this.comboBoxPrograms.Size = new System.Drawing.Size(189, 28);
            this.comboBoxPrograms.TabIndex = 1;
            // 
            // panelCamera1
            // 
            this.panelCamera1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera1.Controls.Add(this.hWindowControlCamera1);
            this.panelCamera1.Controls.Add(this.labelCamera1);
            this.panelCamera1.Location = new System.Drawing.Point(0, 72);
            this.panelCamera1.Name = "panelCamera1";
            this.panelCamera1.Size = new System.Drawing.Size(725, 877);
            this.panelCamera1.TabIndex = 4;
            // 
            // hWindowControlCamera1
            // 
            this.hWindowControlCamera1.BackColor = System.Drawing.Color.Black;
            this.hWindowControlCamera1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlCamera1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControlCamera1.ImagePart = new System.Drawing.Rectangle(0, 0, 1200, 500);
            this.hWindowControlCamera1.Location = new System.Drawing.Point(0, 45);
            this.hWindowControlCamera1.Name = "hWindowControlCamera1";
            this.hWindowControlCamera1.Size = new System.Drawing.Size(723, 830);
            this.hWindowControlCamera1.TabIndex = 2;
            this.hWindowControlCamera1.WindowSize = new System.Drawing.Size(723, 830);
            // 
            // labelCamera1
            // 
            this.labelCamera1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCamera1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera1.Location = new System.Drawing.Point(0, 0);
            this.labelCamera1.Name = "labelCamera1";
            this.labelCamera1.Size = new System.Drawing.Size(723, 45);
            this.labelCamera1.TabIndex = 1;
            this.labelCamera1.Text = "CAMERA 1";
            this.labelCamera1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelCamera2
            // 
            this.panelCamera2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCamera2.Controls.Add(this.hWindowControlCamera2);
            this.panelCamera2.Controls.Add(this.labelCamera2);
            this.panelCamera2.Location = new System.Drawing.Point(759, 72);
            this.panelCamera2.Name = "panelCamera2";
            this.panelCamera2.Size = new System.Drawing.Size(725, 876);
            this.panelCamera2.TabIndex = 5;
            // 
            // hWindowControlCamera2
            // 
            this.hWindowControlCamera2.BackColor = System.Drawing.Color.Black;
            this.hWindowControlCamera2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControlCamera2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControlCamera2.ImagePart = new System.Drawing.Rectangle(0, 0, 1200, 500);
            this.hWindowControlCamera2.Location = new System.Drawing.Point(0, 45);
            this.hWindowControlCamera2.Name = "hWindowControlCamera2";
            this.hWindowControlCamera2.Size = new System.Drawing.Size(723, 829);
            this.hWindowControlCamera2.TabIndex = 3;
            this.hWindowControlCamera2.WindowSize = new System.Drawing.Size(723, 829);
            // 
            // labelCamera2
            // 
            this.labelCamera2.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCamera2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera2.Location = new System.Drawing.Point(0, 0);
            this.labelCamera2.Name = "labelCamera2";
            this.labelCamera2.Size = new System.Drawing.Size(723, 45);
            this.labelCamera2.TabIndex = 1;
            this.labelCamera2.Text = "CAMERA 2";
            this.labelCamera2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DummyTestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 961);
            this.Controls.Add(this.panelCamera2);
            this.Controls.Add(this.panelCamera1);
            this.Controls.Add(this.panelPrograms);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DummyTestingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dummy Testing";
            this.panelPrograms.ResumeLayout(false);
            this.panelPrograms.PerformLayout();
            this.panelCamera1.ResumeLayout(false);
            this.panelCamera2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPrograms;
        private System.Windows.Forms.Label labelPrograms;
        private System.Windows.Forms.ComboBox comboBoxPrograms;
        private System.Windows.Forms.Panel panelCamera1;
        private System.Windows.Forms.Panel panelCamera2;
        private System.Windows.Forms.Label labelCamera1;
        private System.Windows.Forms.Label labelCamera2;
        private System.Windows.Forms.Button buttonExecute;
        private HalconDotNet.HWindowControl hWindowControlCamera1;
        private HalconDotNet.HWindowControl hWindowControlCamera2;
    }
}