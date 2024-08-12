using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
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
        string valued;//متغير مساعد ل حفظ قيمة التاريخ في حالة تكرار العناصر
        string valuep;//متغير مساعد لحفظ قيمة السعر في حالة تكرار العناصر
        DataTable dt = new DataTable();//جدول مساعد ل تحديد العناصر المكرره فيه
        SqlDataAdapter da;
        DataTable newtable = new DataTable();

        public SaleScreen()
        {

            InitializeComponent();
        }
        private void restchose()
        {
            chose1.LinkColor = System.Drawing.Color.Gray;
            chose2.LinkColor = System.Drawing.Color.Gray;
            next1.LinkColor = System.Drawing.Color.Gray;
            last1.LinkColor = System.Drawing.Color.Gray;
            date1.Text = "****";
            date2.Text = "****";
            prize1.Text = "****";
            prize2.Text = "****";
            count.Text = "****";
        }
        private void Chose()
        {
            chose1.LinkColor = System.Drawing.Color.Blue;
            count.Text = dt.Rows.Count.ToString();

          
                date1.Text = Convert.ToDateTime(dt.Rows[i - 2]["item_date"]).ToString("yyyy-MM-dd");
                prize1.Text = dt.Rows[i - 2]["item_prizes"].ToString();
            

            if (dt.Rows.Count >= i)
            {
                chose2.LinkColor = System.Drawing.Color.Blue;
                date2.Text = Convert.ToDateTime(dt.Rows[i - 1]["item_date"]).ToString("yyyy-MM-dd");
                prize2.Text = dt.Rows[i - 1]["item_prizes"].ToString();
            }
            else
            {
                chose2.LinkColor = System.Drawing.Color.Gray;
                date2.Text = "";
                prize2.Text = "";
            }

            next1.LinkColor = dt.Rows.Count > i ? System.Drawing.Color.Blue : System.Drawing.Color.Gray;

            last1.LinkColor = i == 2 ? System.Drawing.Color.Gray : System.Drawing.Color.Blue;
        }
        private void addtogrid()//لاضافة العنصر الي الداتا جريد فيو
        {
            if (newtable.Rows.Count == 0)//  اول عنصر في الفاتورة
            {
                newtable = dt.Clone();
            }
            //foreach (DataRow data in newtable.Rows)
            //{
            //    if ((string)data["item_id"] == TextBox10.Text)
            //    {
                    
            //    }
            //}
                if (dt.Rows.Count > 1)//  عنصر ف الفاتورة متكرر
                {
                    foreach (DataRow row in dt.Rows)
                    {
                 

                    if ((string)row["item_date"] == valued && (string)row["item_prizes"] == valuep)
                    {
                        newtable.ImportRow(row);
                        dt.Clear();

                        break;
                    }
                    }
                }
                else// عنصر ف الفاتورة ليس متكرر
                {
                    dt.Rows.Add(newtable);
                    dt.Clear();
                }

                dvg.DataSource = newtable.DefaultView.ToTable(false, "item_id", "item_name","item_prizes");
            
  
           
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
     
      

        private void next1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (next1.LinkColor == System.Drawing.Color.Gray)// لضمان عدم التقدم لخانه غير موجوده
                return;
            i += 2;
            Chose();
        }

        private void last1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (last1.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            i -= 2;
            Chose();
        }

        private void chose1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chose1.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            valued = date1.Text;
            valuep = prize1.Text;
            addtogrid();
            restchose();
        }

        private void chose2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chose2.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            valued = date2.Text;
             valuep = prize2.Text;
            addtogrid();
            restchose();
        }
    }
    } 
