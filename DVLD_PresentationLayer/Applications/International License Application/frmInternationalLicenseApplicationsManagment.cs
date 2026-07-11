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
    public partial class frmInternationalLicenseApplicationsManagment : Form
    {
        private DataTable _AllInternationalLicenseApplications;
        public frmInternationalLicenseApplicationsManagment()
        {
            InitializeComponent();
        }
       
        private void frmInternationalLicenseApplicationsManagment_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            _AllInternationalLicenseApplications = clsInternationalLicense.GetAllInternationalLicenses();
            dgvAllInternationalLicenseApplications.DataSource = _AllInternationalLicenseApplications;
            lblRecordCount.Text = dgvAllInternationalLicenseApplications.Rows.Count.ToString();

            if (dgvAllInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvAllInternationalLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvAllInternationalLicenseApplications.Columns[0].Width = 160;

                dgvAllInternationalLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvAllInternationalLicenseApplications.Columns[1].Width = 150;

                dgvAllInternationalLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvAllInternationalLicenseApplications.Columns[2].Width = 130;

                dgvAllInternationalLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvAllInternationalLicenseApplications.Columns[3].Width = 130;

                dgvAllInternationalLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvAllInternationalLicenseApplications.Columns[4].Width = 180;

                dgvAllInternationalLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvAllInternationalLicenseApplications.Columns[5].Width = 180;

                dgvAllInternationalLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvAllInternationalLicenseApplications.Columns[6].Width = 120;
            }
        }

        private void btnAddNewInternationalLicense_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicenseApplication frm = new frmAddNewInternationalLicenseApplication();
            frm.ShowDialog();
            frmInternationalLicenseApplicationsManagment_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilter.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilter.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }
        

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvAllInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvAllInternationalLicenseApplications.CurrentRow.Cells[0].Value;
            frmDriverInternationalInfo frm = new frmDriverInternationalInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvAllInternationalLicenseApplications.CurrentRow.Cells[2].Value;
            int PersonID = clsDriver.FindByDriverID(DriverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

       
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Local License ID":
                    FilterColumn = "LocalLicenseID";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _AllInternationalLicenseApplications.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _AllInternationalLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordCount.Text = _AllInternationalLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
