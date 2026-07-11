using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DVLD_BusinessLayer;

namespace DVLD_Project
{
    public partial class frmPeopleManagement : Form
    {
        public frmPeopleManagement()
        {
            InitializeComponent();
        }

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                    "FirstName", "SecondName", "ThirdName", "LastName"
                                                , "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");

        public void _RefreshPeopleList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                    "FirstName", "SecondName", "ThirdName", "LastName"
                                                , "GendorCaption", "DateOfBirth", "CountryName", "Phone", "Email");
            dvgPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dvgPeople.Rows.Count.ToString();
        }
        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            dvgPeople.DataSource = _dtPeople;
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dvgPeople.Rows.Count.ToString();

            if (dvgPeople.Rows.Count > 0)
            {
                dvgPeople.Columns[0].HeaderText = "Person ID";
                dvgPeople.Columns[0].Width = 110;

                dvgPeople.Columns[1].HeaderText = "National No";
                dvgPeople.Columns[1].Width = 120;

                dvgPeople.Columns[2].HeaderText = "First Name";
                dvgPeople.Columns[2].Width = 120;

                dvgPeople.Columns[3].HeaderText = "Second Name";
                dvgPeople.Columns[3].Width = 140;

                dvgPeople.Columns[4].HeaderText = "Third Name";
                dvgPeople.Columns[4].Width = 120;

                dvgPeople.Columns[5].HeaderText = "Last Name";
                dvgPeople.Columns[5].Width = 120;

                dvgPeople.Columns[6].HeaderText = "Gendor";
                dvgPeople.Columns[6].Width = 120;

                dvgPeople.Columns[7].HeaderText = "Date Of Birth";
                dvgPeople.Columns[7].Width = 120;

                dvgPeople.Columns[8].HeaderText = "Nationality";
                dvgPeople.Columns[8].Width = 120;

                dvgPeople.Columns[9].HeaderText = "Phone";
                dvgPeople.Columns[9].Width = 120;

                dvgPeople.Columns[10].HeaderText = "Email";
                dvgPeople.Columns[10].Width = 170;
            }
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterName = "";

            switch (cbFilter.Text)
            {
                case "PersonID":
                    FilterName = "PersonID";
                    break;
                case "NationalNo":
                    FilterName = "NationalNo";
                    break;
                case "FirstName":
                    FilterName = "FirstName";
                    break;
                case "SecondName":
                    FilterName = "SecondName";
                    break;
                case "ThirdName":
                    FilterName = "ThirdName";
                    break;
                case "LastName":
                    FilterName = "LastName";
                    break;
                case "Nationality":
                    FilterName = "Nationality";
                    break;
                case "Gendor":
                    FilterName = "Gendor";
                    break;
                case "Phone":
                    FilterName = "Phone";
                    break;
                case "Email":
                    FilterName = "Email";
                    break;
                default:
                    FilterName = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterName == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dvgPeople.Rows.Count.ToString();
                return;
            }

            if (FilterName == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterName, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterName, txtFilterValue.Text.Trim());

        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditPerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails((int)dvgPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson((int)dvgPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to delete Person [" +(int)dvgPeople.CurrentRow.Cells[0].Value +"]" ,"Delete Person",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson((int)dvgPeople.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully", "Delete Person", MessageBoxButtons.OK);
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Person Deleted Faild", "Delete Person", MessageBoxButtons.OK);
                }

            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilter.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "PersonID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
