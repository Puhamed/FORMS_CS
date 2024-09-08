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
    public partial class Settings : Form
    {
        Users users = new Users();  
        public Settings()
        {
            InitializeComponent();
        }
        void Forms(Form pr)
        {
            pr.TopLevel = false;

            mainpanel.Controls.Add(pr);
            pr.BringToFront();
            pr.AutoSize = true;
            pr.Dock = DockStyle.Fill;
            pr.Show();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(144, 179, 204);
            Forms(users);
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.FromArgb(197, 225, 244);
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = SystemColors.ControlLightLight;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = SystemColors.ControlLightLight;

        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            //pictureBox4.BackColor = Color.FromArgb(144, 179, 204);
            pictureBox4.BackColor = Color.FromArgb(197, 225, 244);

        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.FromArgb(197, 225, 244);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
