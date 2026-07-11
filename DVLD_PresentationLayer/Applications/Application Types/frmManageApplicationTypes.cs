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
    public partial class frmManageApplicationTypes : Form
    {
        private DataTable _dtAllApplicationTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtAllApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();
            dgvApplicationType.DataSource = _dtAllApplicationTypes;
            lblNumOfRecords.Text = dgvApplicationType.Rows.Count.ToString();
            if (dgvApplicationType.Rows.Count > 0 )
            {
                dgvApplicationType.Columns[0].HeaderText = "ID";
                dgvApplicationType.Columns[0].Width = 110;

                dgvApplicationType.Columns[0].HeaderText = "Title";
                dgvApplicationType.Columns[0].Width = 400;

                dgvApplicationType.Columns[0].HeaderText = "Fees";
                dgvApplicationType.Columns[0].Width = 100;
            }
            

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvApplicationType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmManageApplicationTypes_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
