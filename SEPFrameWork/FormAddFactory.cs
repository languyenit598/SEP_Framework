using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPFrameWork.Forms;
using System.Windows.Forms;
using SEPFrameWork.Databases;

namespace SEPFrameWork
{
    class FormAddFactory : FactoryForm
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
            BaseForm frm = new AddForm(dbConn, dbName, tabName, windowsName, obj);
            frm.ShowDialog();
            return frm;
        }

        
    }
}
