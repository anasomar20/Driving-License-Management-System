using DVLD_BusinessLayer;
using DVLD_Project.Global_Classes;
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
    public partial class frmEditTestType : Form
    {

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();
            this._TestTypeID = _TestTypeID;
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
                return;
            }
            _TestType.Title = txtTitle.Text;
            _TestType.Description = txtDescriotion.Text;
            _TestType.Fees = Convert.ToSingle(txtFees.Text.Trim());

            if (_TestType.Save())
            {
                MessageBox.Show("Test Type Updated Successfully", "Update", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Test Type Updated Faild", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {

            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType != null)
            {
                lblTestTypeID.Text = ((int)_TestTypeID).ToString();
                txtTitle.Text = _TestType.Title;
                txtDescriotion.Text = _TestType.Description;
                txtFees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
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
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
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

        private void txtDescriotion_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescriotion.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescriotion, "Description connat be empty!");
            }
            else
            {
                errorProvider1.SetError(txtDescriotion, null);
            }
        }
    }
}
