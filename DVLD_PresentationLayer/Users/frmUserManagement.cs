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
    public partial class frmUserManagement : Form
    {
        public frmUserManagement()
        {
            InitializeComponent();
        }
        private static DataTable _dtAllUsers;

        
        private void frmUsersManagement_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[7].HeaderText = "Person ID";
                dgvUsers.Columns[7].Width = 120;

                dgvUsers.Columns[8].HeaderText = "Full Name";
                dgvUsers.Columns[8].Width = 350;

                dgvUsers.Columns[9].HeaderText = "Username";
                dgvUsers.Columns[9].Width = 120;

                dgvUsers.Columns[10].HeaderText = "Is Active";
                dgvUsers.Columns[10].Width = 120;
            }
        }
        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {          
            string FilterName = "";

            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterName = "UserID";
                    break;
                case "Person ID":
                    FilterName = "PersonID";
                    break;
                case "Full Name":
                    FilterName = "FullName";
                    break;
                case "Username":
                    FilterName = "SecondName";
                    break;
                case "Is Active":
                    FilterName = "Is Active";
                    break;
                default:
                    FilterName = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterName == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }
            

            if (FilterName != "FullName" || FilterName != "Username")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterName, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterName, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddOrEditUser();
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

        private void tsmShowDetails_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddOrEditUser frm = new frmAddOrEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

        private void tsmAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddOrEditUser frm = new frmAddOrEditUser();
            frm.ShowDialog();
            frmUsersManagement_Load(null, null);
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to delete User [" + (int)dgvUsers.CurrentRow.Cells[0].Value + "]", "Delete User", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (clsUser.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User Deleted Successfully", "Delete User", MessageBoxButtons.OK);
                    frmUsersManagement_Load(null, null);
                }
                else
                {
                    MessageBox.Show("User Deleted Faild", "Delete User", MessageBoxButtons.OK);
                }

            }
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

               
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
                
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "UserID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = "";

            switch (cbFilter.Text)
            {
                case "All":
                    FilterValue = "UserID";
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }
            }

        private void cmsChangePassword_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmChangePassword Frm1 = new frmChangePassword(UserID);
            Frm1.ShowDialog();
        }
    }
}
