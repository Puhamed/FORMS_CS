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
    public partial class SaleScreen : Form
    {
        dbcon con = new dbcon();
<<<<<<< HEAD
        int i = 2;// متغير مساعد ل تحديد عدد العناصر المكرره يستخدم في دالة chose
=======
      int i = 2;// متغير مساعد ل تحديد عدد العناصر المكرره يستخدم في دالة chose
>>>>>>> 22a2926245395d05d23a48d870cf8549fad4939a
        DataTable dt=new DataTable();//جدول مساعد ل تحديد العناصر المكرره فيه

        public SaleScreen()
        {

            InitializeComponent();
        }
        private void chose()
        {
            chose1.LinkColor = System.Drawing.Color.Blue;
            count.Text = dt.Rows.Count.ToString();
            date1.Text = Convert.ToDateTime(dt.Rows[i - 2]["item_date"]).ToString("yyyy-MM-dd");
            prize1.Text = dt.Rows[i - 2]["item_prizes"].ToString();
            if (dt.Rows.Count >= i)//لمعرفة هل سنحتاج الي السطر الثاني ام لا 
            {
                chose2.LinkColor = System.Drawing.Color.Blue;
                date2.Text = Convert.ToDateTime(dt.Rows[i - 1]["item_date"]).ToString("yyyy-MM-dd");
                prize2.Text = dt.Rows[i - 1]["item_prizes"].ToString();
                            }
            if (dt.Rows.Count > i) 
            {
                next1.LinkColor = System.Drawing.Color.Blue;
            }
            else
            {
                next1.LinkColor = System.Drawing.Color.Gray;
            }
            if (i==2)
            {
                last1.LinkColor = System.Drawing.Color.Gray;
            }
            else
            {
                last1.LinkColor = System.Drawing.Color.Blue;
            }
        }

        private void SaleScreen_Load(object sender, EventArgs e)
        {
            senfsgridview.DataSource = con.fillsenfs();
        }

        private void quantbox_ValueChanged(object sender, EventArgs e)
        {
            
        }
<<<<<<< HEAD

        private void dvg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TextBox10.Text == "")
                {
                    MessageBox.Show(" باركود", "لرجاء ادخال باركود");
                }
                else
                {
                     dt = con.data_tem(TextBox10.Text);
                    if (dt.Rows.Count > 1)// للتحقق من انه العنصر مخزن اكثر من مره
                    {
                        chose();
                    }

                }
            }
        }

        private void next1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (next1.LinkColor == System.Drawing.Color.Gray)// لضمان عدم التقدم لخانه غير موجوده
                return;
             

            i = i + 2;
            chose();
        }

        private void last1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (last1.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            i = i - 2;
            chose();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
=======
>>>>>>> 22a2926245395d05d23a48d870cf8549fad4939a

        private void dvg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TextBox10.Text == "")
                {
                    MessageBox.Show(" باركود", "لرجاء ادخال باركود");
                }
                else
                {
                     dt = con.data_tem(TextBox10.Text);
                    if (dt.Rows.Count > 1)// للتحقق من انه العنصر مخزن اكثر من مره
                    {
                        chose();
                    }

                }
            }
        }

        private void next1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (next1.LinkColor == System.Drawing.Color.Gray)// لضمان عدم التقدم لخانه غير موجوده
                return;
             

            i = i + 2;
            chose();
        }

        private void last1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (last1.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            i = i - 2;
            chose();
        }
    }
}
