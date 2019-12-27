using SEPFrameWork.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            IConnector databaseConnection = new SQLServerConnection("DataTest", @"DESKTOP-2BKS2GH\SQLEXPRESS");
            //List<String> test = databaseConnection.GetNameTables();
            //List<String> test2 = databaseConnection.getColumnsName();

            //List<String> test3 = databaseConnection.GetNameFieldsOfTable("DEPARTMENT");
            //Type type = databaseConnection.GetTypeofFields("DEPARTMENT", "DEPT_ID");
            //foreach (String i in test3)
            //{
            //    Console.WriteLine(i);
            //}
            object[] data = { 70, "ANDROID DEV", "D70", "HCM CITY" };
            bool a = databaseConnection.CreateData("DEPARTMENT", data);
            Console.WriteLine(a);
            List<Dictionary<String, String>> test4 = databaseConnection.ReadData("DEPARTMENT");
            foreach(Dictionary<String, String> i in test4)
            {
                String b = i.Keys.ToString();
            }

            List<String> test5 = databaseConnection.GetNameFieldsNotNullOfTable("DEPARTMENT");
            string test6 = databaseConnection.GetPrimaryKeyOfTable("DEPARTMENT");
            Console.WriteLine("A");
        }
    }
}
