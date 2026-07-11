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
using DVLD_Project.Global_Classes;

namespace DVLD_Project
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }        

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string Password = clsDataHelper.ComputeHash(txtPassword.Text.Trim());
            clsUser user = clsUser.FindByUsernameAndPassword(txtUsername.Text.Trim(),Password);
            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RemmemberUsernameAndPassword(txtUsername.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RemmemberUsernameAndPassword("", "");
                }

                if (!user.IsActive)
                {
                    txtUsername.Focus();
                    MessageBox.Show("Your account is not active , Contact Admin.", "In Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Username/password ", "Wrong", MessageBoxButtons.OK);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref Username, ref Password))
            {
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
