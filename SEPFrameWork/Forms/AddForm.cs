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

        public AddForm(IConnector dbConn, string dbName, string tabName, string windowsName,Object[] obj):base(dbConn, dbName, tabName, windowsName,obj)
        {
            InitializeComponent();
            base.databaseConnection = dbConn;
            base.tableName = tabName;
            base.databaseName = dbName;
            base.windowsName = windowsName;
            base.obj = obj;
            this.Text = base.windowsName; // override lại tên
        }

        protected override void doSomething()
        {
            // code here

        }
    }
}
