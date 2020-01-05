using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEPFrameWork.Databases;

namespace SEPFrameWork.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IConnector dbConn, string dbName, string tabName)
        {
            InitializeComponent();
            // init
            databaseConnection = dbConn;
            tableName = tabName;
            databaseName = dbName;

            // load data
            MainLoad(); // load dữ liệu cho màn hình
            LoadTable(); // load bảng
        }

        private string databaseName, tableName;
        private IConnector databaseConnection = null;
        private List<Dictionary<string, string>> dataTable = null;
        private List<string> fields = null;

        #region Load
        private void MainLoad()
        {
            fields = databaseConnection.GetNameFieldsOfTable(tableName);
        }

        private void LoadTable()
        {
            // clear dữ liệu
            grvData.Rows.Clear();
            grvData.Columns.Clear();
            grvData.Refresh();
            // Load bảng
            dataTable = databaseConnection.ReadData(tableName);

            //Load cột
            foreach (var feild in fields)
            {
                grvData.Columns.Add(feild, feild);
            }

            //List<DataGridViewRow> listRow = new List<DataGridViewRow>();
            ////Load dòng
            //foreach (var row in dataTable) // mỗi row là 1 dictionary
            //{
            //    DataGridViewRow r = new DataGridViewRow();
            //    int idx = 0;
            //    foreach (var item in row) // môi item là 1 cặp (key,values)
            //    {
            //        //MessageBox.Show(item.Key + "---" + item.Value);
            //        r.Cells[0].Value = item.Value;
            //        idx++;
            //    }
            //    listRow.Add(r);
            //}
            //grvData.DataSource = listRow;


            //Load dòng
            foreach (var row in dataTable) // mỗi row là 1 dictionary
            {
                //Create the new row first and get the index of the new row
                int rowIndex = this.grvData.Rows.Add();
                //Obtain a reference to the newly created DataGridViewRow 
                var r = this.grvData.Rows[rowIndex];
                int idx = 0;

                foreach (var item in row) // môi item là 1 cặp (key,values)
                {
                    //MessageBox.Show(item.Key + "---" + item.Value);
                    r.Cells[idx].Value = item.Value;
                    idx++;
                }
            }

            //Orther properties
            grvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //autosize
            grvData.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold); // font
            grvData.ColumnHeadersDefaultCellStyle.BackColor = Color.Black; // column
            grvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grvData.EnableHeadersVisualStyles = false;
        }
        #endregion

        #region Event
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Add clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            //BaseForm frm = new AddForm(databaseConnection, databaseName, tableName, "THÊM", getCurrentRow());
            //frm.ShowDialog();
            new FormAddFactory().createForm(databaseConnection, databaseName, tableName, "THÊM", getCurrentRow()); // factory method


            // Refresh
            LoadTable();
            this.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Update clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            //BaseForm frm = new UpdateForm(databaseConnection, databaseName, tableName, "CẬP NHẬT", getCurrentRow());
            //frm.ShowDialog();
            new FormUpdateFactory().createForm(databaseConnection, databaseName, tableName, "CẬP NHẬT", getCurrentRow()); // factory method


            // Refresh
            LoadTable();
            this.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Delete clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var obj = getCurrentRow();
            var res = MessageBox.Show("Bạn có muốn xóa thông tin dòng hiện tại không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                if (databaseConnection.DeleteData(tableName, obj))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa Thất Bại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Refresh
                LoadTable();
            }
        }
        #endregion

        #region Processer
        private Object[] getCurrentRow()
        {
            var selRow = grvData.CurrentCell.RowIndex;
            //MessageBox.Show(selRow.ToString());
            var r = this.grvData.Rows[selRow];
            var sz = fields.Count();
            Object[] obj = new Object[sz];
            for (int i = 0; i < sz; i++)
            {
                obj[i] = r.Cells[i].Value;
            }
            return obj;
        }
        #endregion
    }
}
