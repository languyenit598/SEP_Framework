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
    public partial class AddForm : BaseForm
    {
        public AddForm()
        {
            InitializeComponent();
        }

        public AddForm(IConnector dbConn, string dbName, string tabName, string windowsName, Object[] obj) : base(dbConn, dbName, tabName, windowsName, obj)
        {
            InitializeComponent();
            base.databaseConnection = dbConn;
            base.tableName = tabName;
            base.databaseName = dbName;
            base.windowsName = windowsName;
            base.obj = obj;
            this.Text = base.windowsName; // override lại tên
            // set dài rộng cửa sổ
            int sz = base.fields.Count;
            this.Size = new Size(713, 100 + sz * 50 + 100);
        }

        protected override void doSomething()
        {
            // code here
            int idx = 0;
            int sz = base.fields.Count;
            Object[] obj = null;
            if (base.fieldsAuto.Count > 0) // có 1 dòng tự tăng (auto increment) => bỏ qua dòng đó k truyền
            {
                obj = new Object[sz - 1]; // bỏ qua cột auto
                idx++; // lấy từ txt1.Text
            }
            else
            {
                obj = new Object[sz]; // bỏ qua cột auto
                // Lấy từ txt0.Text
            }

            //int idxObj = 0;
            
            for(int i=0; i<obj.Length;i++)
            {
                obj[i] = getDataTextBox("txt" + idx.ToString()); // object[0]=txt0.Text ......
                //i++;
                idx++;
            }
            if (base.databaseConnection.CreateData(base.tableName, obj))
            {
                MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
