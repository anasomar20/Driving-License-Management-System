using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmLocalDrivingLicenseApplicationsManagement : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications;
        public frmLocalDrivingLicenseApplicationsManagement()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicenseApplicationsManagement_Load(object sender, EventArgs e)
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvAllLocalDrivingLicenseApp.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblRecordsCount.Text = dgvAllLocalDrivingLicenseApp.Rows.Count.ToString();

            if (dgvAllLocalDrivingLicenseApp.Rows.Count > 0)
            {
                dgvAllLocalDrivingLicenseApp.Columns[0].HeaderText = "L.D.L.AppID";
                dgvAllLocalDrivingLicenseApp.Columns[0].Width = 120;

                dgvAllLocalDrivingLicenseApp.Columns[1].HeaderText = "Driving Class";
                dgvAllLocalDrivingLicenseApp.Columns[1].Width = 300;

                dgvAllLocalDrivingLicenseApp.Columns[2].HeaderText = "National No.";
                dgvAllLocalDrivingLicenseApp.Columns[2].Width = 150;

                dgvAllLocalDrivingLicenseApp.Columns[3].HeaderText = "Full Name";
                dgvAllLocalDrivingLicenseApp.Columns[3].Width = 350;

                dgvAllLocalDrivingLicenseApp.Columns[4].HeaderText = "Application Date";
                dgvAllLocalDrivingLicenseApp.Columns[4].Width = 170;

                dgvAllLocalDrivingLicenseApp.Columns[5].HeaderText = "Passed Tests";
                dgvAllLocalDrivingLicenseApp.Columns[5].Width = 150;
            }
            cbFilter.SelectedIndex = 0;
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

            _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
            lblRecordsCount.Text = dgvAllLocalDrivingLicenseApp.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Local Driving Application ID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvAllLocalDrivingLicenseApp.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvAllLocalDrivingLicenseApp.Rows.Count.ToString();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateNewLocalLicenseApplication frm = new frmAddUpdateNewLocalLicenseApplication();
            frm.ShowDialog();
            frmLocalDrivingLicenseApplicationsManagement_Load(null, null);
        }

     
        private void schdulerVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void _ScheduleTest(clsTestType.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            frmTestAppointment frm = new frmTestAppointment(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            //refresh
            frmLocalDrivingLicenseApplicationsManagement_Load(null, null);

        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure toy want to delete this application", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication != null)
            {
                if (localDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    frmLocalDrivingLicenseApplicationsManagement_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not Delete Application", "Not Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void ShowApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingApplicationInfo frm = new frmLocalDrivingApplicationInfo((int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void scheduleWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }
        private void scheduleDrivingTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestType.enTestType.VisionTest);
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateNewLocalLicenseApplication frm = new frmAddUpdateNewLocalLicenseApplication((int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmLocalDrivingLicenseApplicationsManagement_Load(null, null);
        }

        private void cancleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure toy want to Cancle this application", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                return;

            int LocalDrivingLicenseApplicationID = (int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication localDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication != null)
            {
                if (localDrivingLicenseApplication.Cancle())
                {
                    MessageBox.Show("Application Canceled Successfully", "Cancle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    frmLocalDrivingLicenseApplicationsManagement_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Could not Cancle Application", "Not Cancle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            
            int TotalPassedTests = (int)dgvAllLocalDrivingLicenseApp.CurrentRow.Cells[5].Value;
            bool LicenseExists = false;//LocalDrivingLicenseApplication.IsLicenseIssued();

            issueToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editApplicationToolStripMenuItem.Enabled = !LicenseExists && LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;
            SechduleTestMenu.Enabled = !LicenseExists;

            cancleToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            deleteToolStripMenuItem.Enabled = LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New;

            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.VisionTest);
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestType.enTestType.StreetTest);

            SechduleTestMenu.Enabled = (!PassedVisionTest || !PassedWrittenTest ||!PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (SechduleTestMenu.Enabled)
            {
                schdulerVisionTestToolStripMenuItem.Enabled = !PassedStreetTest;

                scheduleWriteToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                scheduleDrivingTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Local Driving Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
