using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEPFrameWork.Databases;
using SEPFrameWork.Forms;

namespace SEPFrameWork
{
    class FormUpdateFactory : FactoryForm
    {
        //public Form createForm()
        //{
        //    throw new NotImplementedException();
        //}

        //public Form createForm(IConnector dbConn, string dbName, string tabName)
        //{
        //    throw new NotImplementedException();
        //}

        public Form createForm(IConnector dbConn, string dbName, string tabName, string windowsName, object[] obj)
        {
            BaseForm frm = new UpdateForm(dbConn, dbName, tabName, windowsName, obj);
            frm.ShowDialog();
            return frm;
        }
    }
}
