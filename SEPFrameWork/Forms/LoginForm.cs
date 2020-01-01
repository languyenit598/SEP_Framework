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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            LoadComboboxData();
            //HideShowComponent(0);
        }

        private int selTypeDatabase = -1, selDatabase = -1, selTable = -1;
        private IConnector databaseConnection;

        #region init
        private void LoadComboboxData()
        {
            List<string> dat = new List<string>() { "SQLServer", "MySQL" };
            cbTypeDatabase.DataSource = dat;
            cbTypeDatabase.SelectedIndex = -1;
        }

        private void HideShowComponent(int type) // 0:hide, 1:show
        {
            if (type == 0)
            {
                cbDatabase.Hide();
                cbTable.Hide();
                lblChooseTable.Hide();
            }
            else
            {
                cbDatabase.Show();
                cbTable.Show();
                lblChooseTable.Show();
            }
        }
        #endregion


        #region Action
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // dosomething....
            var user = txtUsername.Text;
            var pass = txtPassword.Text;

            if (selTypeDatabase == -1)
            {
                MessageBox.Show("Vui lòng chọn loại cơ sở dữ liệu","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (selTypeDatabase == 0) // SQLServer
            {
                //cbDatabase.DataSource = databaseConnection.DanhSachCacDatabase; <------------

                //databaseConnection = new SQLServerConnection(); //ngoài username & password đoạn code này yêu cầu thêm tên database, vậy cái này lấy ở đâu ra?
            }
            else if (selTypeDatabase == 1) // MySQL
            {

            }

            // then
            var frm = new BaseForm();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTypeDatabase_SelectionIndexChanged(object sender, EventArgs e)
        {
            selTypeDatabase = (int)cbTypeDatabase.SelectedIndex;
            //MessageBox.Show(selTypeDatabase.ToString());

            List<string> datDB = new List<string>(); // chứa tên các database. ex: QLCafe, QLNhaSach, ...


            switch (selTypeDatabase) // chọn loại DB, 0: SQLServer, 1:MySQl
            {
                case 0:
                    // dosomething

                    // truyền datasource cho combobox
                    cbDatabase.DataSource = datDB;
                    break;
                case 1:
                    // dosomething

                    // truyền datasource cho combobox
                    cbDatabase.DataSource = datDB;
                    break;
                default:
                    break;
            }
        }

        private void cbDatabase_SelectionIndexChanged(object sender, EventArgs e)
        {
            selDatabase = (int)cbDatabase.SelectedIndex;

            List<string> datTable = new List<string>(); // chứa tên các table

            // get tất cả tên các table
            // ...
            // ...

            // gán cho combobox
            cbTable.DataSource = datTable;
            
        }

        private void cbTable_SelectionIndexChanged(object sender, EventArgs e)
        {
            selTable = (int)cbTable.SelectedIndex;
        }
        #endregion


    }
}
