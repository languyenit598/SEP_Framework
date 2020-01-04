using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPFrameWork.Forms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public BaseForm(string dbName, string tabName)
        {
            InitializeComponent();
            tableName = tabName;
            databaseName = dbName;
        }

        private string databaseName, tableName;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
