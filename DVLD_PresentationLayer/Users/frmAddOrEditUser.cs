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
    public partial class frmAddOrEditUser : Form
    {
        public enum enMode { Update = 0, AddNew = 1 };
        public enMode _Mode = enMode.Update;

        private int _UserID = -1;
        clsUser _User;

        public frmAddOrEditUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddOrEditUser(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tbLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tbLoginInfo"];
                return;
            }

            if (ctrlPersonInformationWithFilter1.PersonID != -1)
            {
                if (clsUser.IsUserExistForPersonID(ctrlPersonInformationWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user,Choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonInformationWithFilter1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tbLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tbLoginInfo"];

                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                tbLoginInfo.Enabled = false;

                ctrlPersonInformationWithFilter1.FilterFocus();
            }
            else
            {
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                tbLoginInfo.Enabled = true;

                btnSave.Enabled = false;
            }

            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            ctrlPersonInformationWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User With ID = " + _User, "User not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
   
            lblUserID.Text = _User.PersonID.ToString();
            txtUsername.Text = _User.Username;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonInformationWithFilter1.LoadPersonInfo(_User.PersonID);
            
        }
       
            
        private bool CheckPassword()
        {
            if (txtPassword.Text == txtConfirmPassword.Text)
                return true;
            else
                return false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Of Fileds are not vaild!, put the mouse over the red icon",
                    "Vaildation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _User.PersonID = ctrlPersonInformationWithFilter1.PersonID;
            _User.Username = txtUsername.Text;
            _User.Password = txtPassword.Text;
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";
                this.Text = "Update User";
                MessageBox.Show("Data Saved Successfully.","Saved",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Error : Data Is not Saved Successfully.","Not Saved",MessageBoxButtons.OK,MessageBoxIcon.Error);

        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUsername, "Username cannot be blank");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUsername, null);
            };
            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExist(txtUsername.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUsername, "Username is used Choose another User");
                }
                else
                {
                    errorProvider1.SetError(txtUsername, null);
                }
            }
            else
            {
                //incase update make sure not to use anothers user name
                if (_User.Username != txtUsername.Text.Trim())
                {
                    if (clsUser.IsUserExist(txtUsername.Text.Trim()))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUsername, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUsername, null);
                    }
                    ;
                }
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider2.SetError(txtConfirmPassword, "Password is not match");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void frmAddOrEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider2.SetError(txtPassword, "Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }
    }
}
