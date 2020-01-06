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
    class FormMainFactory : FactoryForm
    {
        //public Form createForm()
        //{
        //    throw new NotImplementedException();
        //}

        //public Form createForm(IConnector dbConn, string dbName, string tabName)
        //{
        //    MainForm frm = new MainForm(dbConn, dbName, tabName);
        //    frm.ShowDialog();
        //    return frm;
        //}

        //public Form createForm(IConnector dbConn, string dbName, string tabName, string windowsName, object[] obj)
        //{
        //    throw new NotImplementedException();
        //}
        public Form createForm(IConnector dbConn, string dbName, string tabName, string windowsName, object[] obj)
        {
            MainForm frm = new MainForm(dbConn, dbName, tabName);
            frm.ShowDialog();
            return frm;
        }
    }
}
