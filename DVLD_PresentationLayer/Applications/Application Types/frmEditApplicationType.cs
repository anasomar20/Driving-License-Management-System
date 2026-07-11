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
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID { get; set; }
        private clsApplicationTypes _ApplicationType;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            this._ApplicationTypeID = _ApplicationTypeID;
        }
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds are not vaide!,put the mouse over the red icon", "Empty failds", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _ApplicationType.ApplicationTypeID = _ApplicationTypeID;
            _ApplicationType.Title = txtTitle.Text;
            _ApplicationType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Application Type Updated Successfully", "Update", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Application Type Updated Faild", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title connat be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);    
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees connat be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }

            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {

            lblApplicationType.Text = _ApplicationTypeID.ToString();

            _ApplicationType = clsApplicationTypes.Find(_ApplicationTypeID);

            if (_ApplicationType != null)
            {
                txtTitle.Text = _ApplicationType.Title;
                txtFees.Text = _ApplicationType.Fees.ToString();
            }
        }
    }
}
