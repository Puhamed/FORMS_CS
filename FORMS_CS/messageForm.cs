using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace FORMS_CS
{


    public partial class messageForm : Form
    {
        public string headlabel;
        public string message;
        public int pic;
        public messageForm()
        {
            InitializeComponent();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void messageForm_Load(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            label1.Text = message;  
            label2.Text = headlabel;
            if (pic == 0)
            {
                pictureBox1.Image = Properties.Resources.correctmark;

            }
            else if (pic == 1)
            {
                pictureBox1.Image = Properties.Resources.attentionRed;
            }
        }
    }
}
