using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEPFrameWork.Forms
{
    public partial class BaseForm : Form
    {
        private Panel pn1;

        public BaseForm()
        {
            InitializeComponent();
            pn1 = new Panel();
            pn1.Size = new Size(200, 100);
            pn1.BackColor = Color.Red;
            pn1.Location = new Point(0, 0);
            this.Controls.Add(pn1);
        }
    }
}
