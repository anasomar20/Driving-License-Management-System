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
    public partial class ctrlDriverLicenses : UserControl
    {
        private int _DriverID;

        private clsDriver _Driver;
        private DataTable _dtDriverLocalLicenseHistory;
        private DataTable _dtDriverInternationalLicenseHistory;


        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicenseInfo()
        {
            _dtDriverInternationalLicenseHistory = clsDriver.GetLicenses(_DriverID);

            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicenseHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[2].Width = 170;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[4].Width = 110;

            }
        }
        private void _LoadInternationalLicenseInfo()
        {
            _dtDriverInternationalLicenseHistory = clsDriver.GetLicenses(_DriverID);

            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicenseHistory;
            lblInternationalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 110;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[2].Width = 170;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 170;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[4].Width = 110;

            }
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no driver with id = " + _DriverID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }

        public void LoadInfoByPersonID(int PersonID)
        {
            _Driver = clsDriver.FindByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("There is no driver linked with person with id = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;

            _LoadLocalLicenseInfo();
            _LoadInternationalLicenseInfo();
        }
        public void Clear()
        {
            _dtDriverLocalLicenseHistory.Clear();
        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showLicenseInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //int InternationalLicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value;
            //frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            //frm.ShowDialog();
        }
    }
}
