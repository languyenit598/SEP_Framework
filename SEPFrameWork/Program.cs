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

            System.Collections.ArrayList abc = new System.Collections.ArrayList();
            abc.Add("MSSV");
            abc.Add("Họ tên");
            abc.Add("Điểm");
            Application.Run(new Create_Form(abc));
        }
    }
}
