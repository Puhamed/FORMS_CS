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
       readonly Dbcon con = new Dbcon();
        int i = 2;// متغير مساعد ل تحديد عدد العناصر المكرره يستخدم في دالة chose

        DataTable dt = new DataTable();//جدول مساعد ل تحديد العناصر المكرره فيه

        public SaleScreen()
        {

            InitializeComponent();
        }
        private void Chose()
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
            if (i == 2)
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
            senfsgridview.DataSource = con.Fillsenfs();
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
                    dt = con.Data_tem(TextBox10.Text);
                    if (dt.Rows.Count > 1)// للتحقق من انه العنصر مخزن اكثر من مره
                    {
                        Chose();
                    }

                }
            }
        }



   
   
         

       

        private void Next1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (next1.LinkColor == System.Drawing.Color.Gray)// لضمان عدم التقدم لخانه غير موجوده
                return;


            i +=2;
            Chose();
        }

        private void Last1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (last1.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            i -= 2;
            Chose();
        }
    }
    } 
