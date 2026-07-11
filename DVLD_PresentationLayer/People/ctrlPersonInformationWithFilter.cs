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
    public partial class ctrlPersonInformationWithFilter : UserControl
    {
        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int _SelectedPersonID)
        {
            Action<int> handle = OnPersonSelected;

            if (handle != null)
            {
                handle(_SelectedPersonID);
            }
        }

        private bool _ShowAddPerson = true;

        public int PersonID
        {
            get { return ctrlPersonInformation1._SelectedPersonID; }
        }

        public bool ShowAddPerson 
        {
            get 
            {
                return _ShowAddPerson; 
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled 
        { 
            get 
            {
                return _FilterEnabled;
            } 
            set
            {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;
            }
        }

        public ctrlPersonInformationWithFilter()
        {
            InitializeComponent();
        }

        private int __SelectedPersonID = -1;

        public int _SelectedPersonID
        {
            get
            {
                return ctrlPersonInformation1._SelectedPersonID;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonInformation1.SelectPersonInfo; }
        }

        public void LoadPersonInfo(int _SelectedPersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = _SelectedPersonID.ToString();
            FindNow();
        }
        private void FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    ctrlPersonInformation1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonInformation1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;
                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrlPersonInformation1._SelectedPersonID);  
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddOrEditPerson frm = new frmAddOrEditPerson();
            frm.DataBack += frmAddOrEditPerson_DataBack;
            frm.ShowDialog();
        }
        private void frmAddOrEditPerson_DataBack(object sender, int _SelectedPersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = _SelectedPersonID.ToString();
            ctrlPersonInformation1.LoadPersonInfo(_SelectedPersonID);
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }
        private void ctrlPersonInformationWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }

            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not vailde!, put the mouse over the red icon to show", "Not Vaild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FindNow();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }
    }
}
