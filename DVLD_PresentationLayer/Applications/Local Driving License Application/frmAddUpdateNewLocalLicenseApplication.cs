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
    public partial class frmAddUpdateNewLocalLicenseApplication : Form
    {
        public enum enMode {AddNew = 0,Update = 1};
        private enMode _Mode;
        private int _LocalDrivingLicenseAppllicationID = -1;
        private int _SelectedPersonID = -1;

        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public frmAddUpdateNewLocalLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateNewLocalLicenseApplication(int LocalDrivingLicenseAppllicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LocalDrivingLicenseAppllicationID = LocalDrivingLicenseAppllicationID;
        }

        private void _FillLicenseClassInComoboBox()
        {
            DataTable dtLicenseClass = clsLicenseClass.GetAllLicenseClass();
            foreach (DataRow row in dtLicenseClass.Rows)
            {
                cbLicenseClasses.Items.Add(row["ClassName"]);
            }
        }
        private void _ResetDefaultValues()
        {
            _FillLicenseClassInComoboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Local Driving License Application";
                this.Text = "Add New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                ctrlPersonInformationWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbLicenseClasses.SelectedIndex = 2;
                lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedbyUserID.Text = clsGlobal.CurrentUser.Username;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = false;
                btnSave.Enabled = true;

            }
        }
    
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
                return;
            }


            //incase of add new mode.
            if (ctrlPersonInformationWithFilter1.PersonID != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];

            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonInformationWithFilter1.FilterFocus();
            }
        }

        private void _LoadData()
        {
            ctrlPersonInformationWithFilter1.FilterEnabled = false;
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseAppllicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseAppllicationID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrlPersonInformationWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            cbLicenseClasses.SelectedIndex = cbLicenseClasses.FindString(clsLicenseClass.Find(_LocalDrivingLicenseApplication.LicenseClassID).ClassName);
            lblCreatedbyUserID.Text = clsUser.FindByUserID(_LocalDrivingLicenseApplication.CreatedByUserID).Username;

        }

        private void frmAddNewLocalLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseClassID = clsLicenseClass.Find(cbLicenseClasses.Text).LicenseClassID;

            int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an Active Application", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ctrlPersonInformationWithFilter1._SelectedPersonID, LicenseClassID))
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonInformationWithFilter1._SelectedPersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.ApplicationTypeID = 1;
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = LicenseClassID;
            clsApplication application = new clsApplication();

            if (application.Save())
            {
                lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;

                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                if (_LocalDrivingLicenseApplication.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Errro : Data Is not saved Sucessfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonInformationWithFilter1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmAddUpdateNewLocalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonInformationWithFilter1.FilterFocus();
        }
    }
}
