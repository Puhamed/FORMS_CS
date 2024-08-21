using System;
using System.Windows.Forms;

namespace FORMS_CS
{
    public partial class Form1 : Form
    {
        public string currentUser;
        bool maxmin = false;
       readonly SaleScreen Sc = new SaleScreen();
     readonly   Store st = new Store();
        // دالة عرض الفورمات داخل الفورم الرئيسي
        void Forms(Form pr)
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
            //تكبير وتصغير الفورم
            this.WindowState = (maxmin) ? FormWindowState.Normal : FormWindowState.Maximized;
            maxmin = !maxmin;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
        }

        private void Button8_Click(object sender, EventArgs e)
        {

            mainpanel.Controls.Clear();
            Forms(Sc);
    
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

        private void button9_Click(object sender, EventArgs e)
        {
            newBill bill = new newBill();
            bill.Show();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
      
                mainpanel.Controls.Clear();
                Forms(st);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            current.Text = currentUser;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من أنك تريد إغلاق التطبيق؟ ", "تنبيه الإغلاق!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Users users = new Users();
            users.Show();
        }
    }
}
