using System.Drawing;
using System.Windows.Forms;

namespace SEPFrameWork
{
    partial class Create_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(350, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Create_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Name = "Create_Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Collections.ArrayList arr;
        private System.Windows.Forms.Button button1;
        private Panel pn1;



        public void InitFields(System.Collections.ArrayList fields)
        {
            arr = new System.Collections.ArrayList();
            pn1 = new Panel();
            pn1.BackColor = Color.Red;
            pn1.Size = new Size(Screen.FromControl(this).Bounds.Width, Screen.FromControl(this).Bounds.Height-20);
            int i = 0;

            foreach (var field in fields)
            {
                System.Windows.Forms.Label lb= new System.Windows.Forms.Label();
                lb.Text = field.ToString();
                lb.Location = new System.Drawing.Point(0, 10 + i * 20);
                lb.Size = new System.Drawing.Size(150, 13);


                System.Windows.Forms.TextBox temp = new System.Windows.Forms.TextBox();
                temp.Location = new System.Drawing.Point(150, 10 + i * 20);
                temp.Size = new System.Drawing.Size(150, 10);
                temp.Text = "";
                arr.Add(temp);
                pn1.Controls.Add(temp);
                pn1.Controls.Add(lb);
                i++;
            }
            this.Controls.Add(pn1);
           


        }
    }



}

