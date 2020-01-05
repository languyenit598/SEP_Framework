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
    public partial class UpdateForm : BaseForm
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public UpdateForm(IConnector dbConn, string dbName, string tabName, string windowsName, Object[] obj) : base(dbConn, dbName, tabName, windowsName, obj)
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
            // Object[] newdata = null;
            //if (base.fieldsAuto.Count > 0) // có 1 dòng tự tăng (auto increment) => bỏ qua dòng đó k truyền
            //{
            //    newdata = new Object[sz - 1]; // bỏ qua cột auto
            //    idx++; // lấy từ txt1.Text
            //}
            //else
            //{
            //    newdata = new Object[sz]; // bỏ qua cột auto
            //    // Lấy từ txt0.Text
            //}
            Object[] newdata = new Object[sz];
            int idxObj = 0;

            foreach (var field in base.fields)
            {
                var checkdata = base.databaseConnection.GetTypeofFields(base.tableName, field).Name;
                if (checkdata.Equals("Int32"))
                {
                    newdata[idxObj] = Int32.Parse(getDataTextBox("txt" + idx.ToString())); // object[0]=txt0.Text ......
                }
                else if (checkdata.Equals("String"))
                {
                    newdata[idxObj] = getDataTextBox("txt" + idx.ToString());
                }
                else if (checkdata.Equals("DateTime"))
                {
                    newdata[idxObj] = DateTime.Parse(getDataTextBox("txt" + idx.ToString()));
                }
                else if (checkdata.Equals("Boolean"))
                {
                    newdata[idxObj] = Boolean.Parse(getDataTextBox("txt" + idx.ToString()));
                }
                else if (checkdata.Equals("Double"))
                {
                    newdata[idxObj] = Double.Parse(getDataTextBox("txt" + idx.ToString()));
                }
                else if (checkdata.Equals("Byte[]"))
                {
                    newdata[idxObj] = Encoding.ASCII.GetBytes(getDataTextBox("txt" + idx.ToString()));
                }
                else
                {
                    newdata[idxObj] = getDataTextBox("txt" + idx.ToString());
                }
                idxObj++;
                idx++;
            }
            if (base.databaseConnection.UpdateData(base.tableName, base.obj, newdata))
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
