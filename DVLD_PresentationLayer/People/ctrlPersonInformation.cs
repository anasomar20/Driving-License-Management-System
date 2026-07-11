using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BusinessLayer;

namespace DVLD_Project
{
    public partial class ctrlPersonInformation : UserControl
    {
        private clsPerson _Person;
        private int __SelectedPersonID = -1;

        public int _SelectedPersonID
        {
            get { return __SelectedPersonID; }
        }
        public clsPerson SelectPersonInfo
        {
            get { return _Person; }
        }
        public ctrlPersonInformation()
        {            
            InitializeComponent();
        }

        private void ctrl_PersonInformation_Load(object sender, EventArgs e)
        {
            

        }
        public void LoadPersonInfo(int _SelectedPersonID)
        {
            _Person = clsPerson.Find(_SelectedPersonID);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("Person With ID[" + _SelectedPersonID + "] Does not Exist", "Error", MessageBoxButtons.OK);
                return;
            }

            _FillPersonInfo();
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("Person With NationalNo [" + NationalNo + "] Does not Exist", "Error", MessageBoxButtons.OK);
                return;
            }

            _FillPersonInfo();
        }

        private void _ResetPersonInfo()
        {
            lbl_SelectedPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblAddress.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblNationalityCountryID.Text = "[????]";
            lblGendor.Text = "[????]";
            lblEditPersonInfo.Enabled = false;
            pbPersonImage.Image = Properties.Resources.Male_512;
        }
        public void _LoadPersonImage()
        {
            if(_Person.Gendor == 0)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void _FillPersonInfo()
        {
            lblEditPersonInfo.Enabled = true;
            __SelectedPersonID = _Person.PersonID;
            lbl_SelectedPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = _Person.FullName();
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblNationalityCountryID.Text = clsCountry.Find(_Person.NationalityCountryID).CountryName;
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            _LoadPersonImage();
        }
        
        private void lblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson(__SelectedPersonID);
            frm.ShowDialog();

            LoadPersonInfo(__SelectedPersonID);
        }
    }
}
