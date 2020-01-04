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
            dataTable = databaseConnection.ReadData(tableName);
            fields = databaseConnection.GetNameFieldsOfTable(tableName);
        }

        private void LoadTable()
        {
            grvData.DataSource = null;
            //Load cột
            foreach (var feild in fields)
            {
                grvData.Columns.Add(feild,feild);
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
            BaseForm frm = new BaseForm(databaseConnection, databaseName, tableName);
            frm.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete clicked!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
