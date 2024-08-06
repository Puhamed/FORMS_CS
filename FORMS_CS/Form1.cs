using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORMS_CS
{
    public partial class Form1 : Form
    {
        bool maxmin = false;
        bool fr = false;
        SaleScreen sc = new SaleScreen();
        void forms(Form pr)
        {
            pr.TopLevel = false;
            mainpanel.Controls.Clear();
            mainpanel.Controls.Add(pr);
            pr.BringToFront();
            pr.AutoSize = true; 
            pr.Dock = DockStyle.Fill; 
            pr.Show();
        }
        public Form1()
        {
            InitializeComponent();
            this.Resize += Form1_Resize;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = (maxmin) ? FormWindowState.Normal : FormWindowState.Maximized;
            maxmin = !maxmin;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // هكي الكومنت يا غبي
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            if (fr == false) 
            {
                forms(sc);
                fr = true;
            }
            else
            {
                sc.Hide();
                mainpanel.Controls.Clear();
                fr = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (mainpanel.Controls.Count > 0)
            {
                Form childForm = (Form)mainpanel.Controls[0];
                childForm.Size = mainpanel.Size;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {

        }
    }
}
