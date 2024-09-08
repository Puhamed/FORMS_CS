using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// حفيظ مش شورك الفورم
// مازالك الحذف وداتا جريد فيو لعرض السجلات 


namespace FORMS_CS
{
    public partial class Users : Form
    {
        AddUser au = new AddUser();
        UsersForm usf = new UsersForm();    
      
        public Users()
        {
            InitializeComponent();
        }
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
        private void button2_Click(object sender, EventArgs e)
        {
            Forms(au);
        }

        private void Users_Load(object sender, EventArgs e)
        {
           

        }

       

        private void Button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {//مشكلة هنا عند الضغط علي مكان لا يوجد به قيمه
        
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Forms(usf);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
