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
    public partial class frmDriversManagement : Form
    {
        private DataTable _dtAllDrivers;

        public frmDriversManagement()
        {
            InitializeComponent();
        }
      

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (cbFilter.Text == "None")
            {
                txtFilterValue.Enabled = false;
            }
            else
                txtFilterValue.Enabled = false;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void frmDriversManagement_Load(object sender, EventArgs e)
        {
            _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvAllDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = _dtAllDrivers.Rows.Count.ToString();
            if (_dtAllDrivers.Rows.Count > 0)
            {
                dgvAllDrivers.Columns[0].HeaderText = "Driver ID";
                dgvAllDrivers.Columns[0].Width = 120;

                dgvAllDrivers.Columns[1].HeaderText = "Person ID";
                dgvAllDrivers.Columns[1].Width = 120;

                dgvAllDrivers.Columns[2].HeaderText = "National No";
                dgvAllDrivers.Columns[2].Width = 140;

                dgvAllDrivers.Columns[3].HeaderText = "Full Name";
                dgvAllDrivers.Columns[3].Width = 320;

                dgvAllDrivers.Columns[4].HeaderText = "Date";
                dgvAllDrivers.Columns[4].Width = 170;

                dgvAllDrivers.Columns[5].HeaderText = "Active License";
                dgvAllDrivers.Columns[5].Width = 100;

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() != "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = _dtAllDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = _dtAllDrivers.Rows.Count.ToString();


        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Driver ID" || cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvAllDrivers.CurrentRow.Cells[1].Value;
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();
            frmDriversManagement_Load(null, null);
        }
    }
}
