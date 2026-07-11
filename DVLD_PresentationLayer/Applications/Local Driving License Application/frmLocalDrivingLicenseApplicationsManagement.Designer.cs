namespace DVLD_Project
{
    partial class frmLocalDrivingLicenseApplicationsManagement
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
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAllLocalDrivingLicenseApp = new System.Windows.Forms.DataGridView();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SechduleTestMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.schdulerVisionTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleWriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleDrivingTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.issueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalDrivingLicenseApp)).BeginInit();
            this.cmsApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(85, 476);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(23, 15);
            this.lblRecordsCount.TabIndex = 24;
            this.lblRecordsCount.Text = "??";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 474);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "# Record:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Filter by:";
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(252, 210);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(175, 20);
            this.txtFilterValue.TabIndex = 19;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // cbFilter
            // 
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None",
            "Local Driving Application ID",
            "National No.",
            "Full Name",
            "Status"});
            this.cbFilter.Location = new System.Drawing.Point(78, 209);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(168, 21);
            this.cbFilter.TabIndex = 18;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(178, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(590, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "Local Driving License Applications Management";
            // 
            // dgvAllLocalDrivingLicenseApp
            // 
            this.dgvAllLocalDrivingLicenseApp.AllowUserToAddRows = false;
            this.dgvAllLocalDrivingLicenseApp.AllowUserToDeleteRows = false;
            this.dgvAllLocalDrivingLicenseApp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAllLocalDrivingLicenseApp.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllLocalDrivingLicenseApp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllLocalDrivingLicenseApp.ContextMenuStrip = this.cmsApplications;
            this.dgvAllLocalDrivingLicenseApp.Location = new System.Drawing.Point(10, 236);
            this.dgvAllLocalDrivingLicenseApp.Name = "dgvAllLocalDrivingLicenseApp";
            this.dgvAllLocalDrivingLicenseApp.ReadOnly = true;
            this.dgvAllLocalDrivingLicenseApp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllLocalDrivingLicenseApp.Size = new System.Drawing.Size(956, 235);
            this.dgvAllLocalDrivingLicenseApp.TabIndex = 16;
            // 
            // cmsApplications
            // 
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowApplicationToolStripMenuItem,
            this.editApplicationToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.cancleToolStripMenuItem,
            this.toolStripSeparator1,
            this.SechduleTestMenu,
            this.toolStripSeparator2,
            this.issueToolStripMenuItem,
            this.showLicenseToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem});
            this.cmsApplications.Name = "cmsMenu";
            this.cmsApplications.Size = new System.Drawing.Size(262, 320);
            this.cmsApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplications_Opening);
            // 
            // ShowApplicationToolStripMenuItem
            // 
            this.ShowApplicationToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.PersonDetails_32;
            this.ShowApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowApplicationToolStripMenuItem.Name = "ShowApplicationToolStripMenuItem";
            this.ShowApplicationToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.ShowApplicationToolStripMenuItem.Text = "Slow Application Details";
            this.ShowApplicationToolStripMenuItem.Click += new System.EventHandler(this.ShowApplicationToolStripMenuItem_Click);
            // 
            // editApplicationToolStripMenuItem
            // 
            this.editApplicationToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.edit_32;
            this.editApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editApplicationToolStripMenuItem.Name = "editApplicationToolStripMenuItem";
            this.editApplicationToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.editApplicationToolStripMenuItem.Text = "Edit Application";
            this.editApplicationToolStripMenuItem.Click += new System.EventHandler(this.editApplicationToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.deleteToolStripMenuItem.Text = "Delete Application";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // cancleToolStripMenuItem
            // 
            this.cancleToolStripMenuItem.Name = "cancleToolStripMenuItem";
            this.cancleToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.cancleToolStripMenuItem.Text = "Cancel Application";
            this.cancleToolStripMenuItem.Click += new System.EventHandler(this.cancleToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(258, 6);
            // 
            // SechduleTestMenu
            // 
            this.SechduleTestMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schdulerVisionTestToolStripMenuItem,
            this.scheduleWriteToolStripMenuItem,
            this.scheduleDrivingTestToolStripMenuItem});
            this.SechduleTestMenu.Name = "SechduleTestMenu";
            this.SechduleTestMenu.Size = new System.Drawing.Size(261, 38);
            this.SechduleTestMenu.Text = "Sechdule Tests";
            // 
            // schdulerVisionTestToolStripMenuItem
            // 
            this.schdulerVisionTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Vision_Test_32;
            this.schdulerVisionTestToolStripMenuItem.Name = "schdulerVisionTestToolStripMenuItem";
            this.schdulerVisionTestToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.schdulerVisionTestToolStripMenuItem.Text = "Schedule Vision Test";
            this.schdulerVisionTestToolStripMenuItem.Click += new System.EventHandler(this.schdulerVisionTestToolStripMenuItem_Click);
            // 
            // scheduleWriteToolStripMenuItem
            // 
            this.scheduleWriteToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Written_Test_32;
            this.scheduleWriteToolStripMenuItem.Name = "scheduleWriteToolStripMenuItem";
            this.scheduleWriteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.scheduleWriteToolStripMenuItem.Text = "Schedule Written Test";
            this.scheduleWriteToolStripMenuItem.Click += new System.EventHandler(this.scheduleWriteToolStripMenuItem_Click);
            // 
            // scheduleDrivingTestToolStripMenuItem
            // 
            this.scheduleDrivingTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.Street_Test_32;
            this.scheduleDrivingTestToolStripMenuItem.Name = "scheduleDrivingTestToolStripMenuItem";
            this.scheduleDrivingTestToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.scheduleDrivingTestToolStripMenuItem.Text = "Schedule Driving Test";
            this.scheduleDrivingTestToolStripMenuItem.Click += new System.EventHandler(this.scheduleDrivingTestToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(258, 6);
            // 
            // issueToolStripMenuItem
            // 
            this.issueToolStripMenuItem.Name = "issueToolStripMenuItem";
            this.issueToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.issueToolStripMenuItem.Text = "Issue Driving License (First Time)";
            // 
            // showLicenseToolStripMenuItem
            // 
            this.showLicenseToolStripMenuItem.Name = "showLicenseToolStripMenuItem";
            this.showLicenseToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.showLicenseToolStripMenuItem.Text = "Show License";
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(261, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(402, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.Image = global::DVLD_Project.Properties.Resources.New_Application_64;
            this.btnAddNewApplication.Location = new System.Drawing.Point(919, 179);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(47, 47);
            this.btnAddNewApplication.TabIndex = 25;
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            this.btnAddNewApplication.Click += new System.EventHandler(this.btnAddNewApplication_Click);
            // 
            // frmLocalDrivingLicenseApplicationsManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 496);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAllLocalDrivingLicenseApp);
            this.Name = "frmLocalDrivingLicenseApplicationsManagement";
            this.Text = "frmLocalDrivingLicenseApplicationsManagement";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationsManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalDrivingLicenseApp)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAllLocalDrivingLicenseApp;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem ShowApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem SechduleTestMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem issueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schdulerVisionTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleWriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleDrivingTestToolStripMenuItem;
    }
}