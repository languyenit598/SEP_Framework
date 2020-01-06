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
    public interface FactoryForm
    {
        //Form createForm();

        //Form createForm(IConnector dbConn, string dbName, string tabName);
        Form createForm(IConnector dbConn, string dbName, string tabName, string windowsName, Object[] obj);
    }

    
}
