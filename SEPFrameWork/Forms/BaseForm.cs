using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SEPFrameWork.Databases;

namespace SEPFrameWork.Forms
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public BaseForm(IConnector dbConn, string dbName, string tabName)
        {
            InitializeComponent();
            databaseConnection = dbConn;
            tableName = tabName;
            databaseName = dbName;
            Load();
        }

        private string databaseName, tableName;
        private IConnector databaseConnection = null;
        private List<string> fields = null;

        #region Load
        private void Load()
        {
            fields = databaseConnection.GetNameFieldsOfTable(tableName);
            int x = 50, y = 100;
            int idx = 0;
            foreach (var field in fields)
            {
                // Add label
                Label lbl = new Label();
                lbl.Text = field;
                lbl.Size = new Size(200, 30);
                lbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl.Location = new System.Drawing.Point(x , y + idx*50);
                lbl.Name = "lbl"+idx;
                this.Controls.Add(lbl);

                // Add textbox
                TextBox txt = new TextBox();
                txt.Text = "";
                txt.Size = new Size(300, 30);
                txt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                txt.Location = new System.Drawing.Point(x + 250, y + idx * 50);
                txt.Name = "txt" + idx;
                this.Controls.Add(txt);

                idx++;
            }

        }
        #endregion

    }
}
