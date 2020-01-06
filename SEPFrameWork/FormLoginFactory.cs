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
    class FormLoginFactory : FactoryForm
    {
        //public Form createForm()
        //{
        //    return new LoginForm();
        //}

        //public Form createForm(IConnector dbConn, string dbName, string tabName)
        //{
        //    throw new NotImplementedException();
        //}

        //public Form createForm(IConnector dbConn, string dbName, string tabName, string windowsName, object[] obj)
        //{
        //    throw new NotImplementedException();
        //}
        public Form createForm(IConnector dbConn, string dbName, string tabName, string windowsName, object[] obj)
        {
            return new LoginForm();
        }
    }
}
