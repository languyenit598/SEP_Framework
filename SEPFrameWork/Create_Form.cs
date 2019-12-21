using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPFrameWork
{
    public partial class Create_Form : Form
    {
        public Create_Form(System.Collections.ArrayList fields)
        {
            InitializeComponent();
            this.InitFields(fields);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string abc = "";
            foreach(var element in arr)
            {
                TextBox temp = (TextBox)element;
                abc += temp.Text+" ";
            }
            MessageBox.Show(abc);
        }
    }
}
