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
    //public class FormAddFactoryBuilder
    //{
    //    IConnector connector;
    //    string databaseName;
    //    string tableName;
    //    string windowName;
    //    Object[] objs;

    //    public FormAddFactoryBuilder SetConnectot(IConnector conn)
    //    {
    //        this.connector = conn;
    //        return this;
    //    }

    //   // FormAddFactoryBuilder()
    //   public AddForm build()
    //    {
    //        return new AddForm(connector, databaseName, tableName, windowName, objs);
            
    //    }
       
    //}



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

            //FormAddFactoryBuilder builder = new FormAddFactoryBuilder();
            //builder.SetConnectot(dbConn);
            //BaseForm frm = builder.build();
            //frm.ShowDialog();
            //return frm;
        }

        
    }
}
