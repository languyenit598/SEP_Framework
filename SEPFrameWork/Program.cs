using SEPFrameWork.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEPFrameWork.Forms;

namespace SEPFrameWork
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] 
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new LoginForm());
            Application.Run(new FormLoginFactory().createForm(null,null,null,null,null)); // factory method



            //IConnector databaseConnection = new MySQLConnector();



            //List<String> test = databaseConnection.GetNameTables();
            //List<String> test2 = databaseConnection.getColumnsName();
            //List<String> test3 = databaseConnection.GetNameFieldsOfTable("DEPARTMENT");
            //Type type = databaseConnection.GetTypeofFields("DEPARTMENT", "DEPT_ID");
            //foreach (String i in test3)
            //{
            //    Console.WriteLine(i);
            //}


            //object[] oldData = { 80, "BE DEV", "D80", "HCM CITY" };
            //object[] newData = { 80, "BE DEV", "D80", "HANOI" };


            //bool a = databaseConnection.CreateData("DEPARTMENT", oldData);
            //bool a = databaseConnection.UpdateData("DEPARTMENT", oldData, newData);


            //List<Dictionary<String, String>> test4 = databaseConnection.ReadData("DEPARTMENT");
            //foreach (Dictionary<String, String> i in test4)
            //{
            //    Console.WriteLine(i);
            //}
            //var a = databaseConnection.GetFieldsAutoIncrement("salary_grade");
            //bool a = databaseConnection.DeleteData("department",newData);
            //Console.WriteLine(a);
            //List<String> test5 = databaseConnection.GetNameFieldsNotNullOfTable("department");
            //string test6 = databaseConnection.GetPrimaryKeyOfTable("DEPARTMENT");
            //Console.WriteLine("A");
        }
    }
}
