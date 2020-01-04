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

            List<string[]> listTemp = new List<string[]>();
            var sz = fields.Count; // đếm số lượng phần tử của mỗi dòng (bằng luôn số lượng cột)

            //Load dòng
            foreach (var row in dataTable) // mỗi row là 1 dictionary
            {
                int idx = 0;
                string[] temp = new string[sz];

                foreach (var item in row) // môi item là 1 cặp (key,values)
                {
                    //MessageBox.Show(item.Key + "---" + item.Value);
                    temp[idx] = item.Value;
                    idx++;
                }

                listTemp.Add(temp);
            }
            grvData.DataSource = listTemp;

            //Orther properties
            grvData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            grvData.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.75F, FontStyle.Bold);
            grvData.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            grvData.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grvData.EnableHeadersVisualStyles = false;
        }
        #endregion

        #region Event
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
        #endregion
    }
}
