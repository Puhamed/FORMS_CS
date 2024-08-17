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
        double valuep;//متغير مساعد لحفظ قيمة السعر في حالة تكرار العناصر
        DataTable dt = new DataTable();//جدول مساعد ل تحديد العناصر المكرره فيه
        List<DateTime> dates= new List<DateTime>();

        public SaleScreen()
        {

            InitializeComponent();
        }
        private void Restchose()//لاعادة الوان لينك الاختيار الي حالتها الرئيسيه
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
        private void Chose()//في حالة كان هناك اكثر من عنصر بنفس الباركود
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
        public void addtogrid()
        {
            DateTime date_co = Convert.ToDateTime(valued);
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToDateTime(row["item_date"]) == date_co && Convert.ToDouble(row["item_prizes"]) == valuep) 
                {

                    foreach (DataGridViewRow row1 in dvg.Rows)
                    {
                        if (dvg.Rows.Count == 0)
                            break;
                        if ((row1.Cells[0]).Value.Equals( row["item_id"]) && row1.Cells[2].Value.Equals (row["item_prizes"])&& Convert.ToDateTime( row["item_date"]) == dates[row1.Index])
                        {
                            if (quantbox.Value == 1)
                                dvg.Rows[row1.Index].Cells[3].Value =Convert.ToInt32( dvg.Rows[row1.Index].Cells[3].Value)+1;
                            else
                                dvg.Rows[row1.Index].Cells[3].Value = Convert.ToInt32 ( dvg.Rows[row1.Index].Cells[3].Value)+quantbox.Value;

                            dvg.Refresh();
                            return;
                        }


                    }
                    if (quantbox.Value == 1)
                    {
                        dvg.Rows.Add(row["item_id"], row["item_name"], row["item_prizes"], 1, row["item_prizes"]);
                        dates.Add(date_co);
                    }
                    else 
                    {
                        dvg.Rows.Add(row["item_id"], row["item_name"], row["item_prizes"], quantbox.Value, row["item_prizes"]);
                        dates.Add(date_co);
                    }
                }
            }
            dvg.Refresh();
        }
      
  
      

        private void SaleScreen_Load(object sender, EventArgs e)
        {
            senfsgridview.DataSource = con.Fillsenfs();
        }
        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TextBox10.Text == ""|| TextBox10.Text == null)
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
                quantbox.Focus();
                quantbox.Value = 1;
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
            valuep = Convert.ToDouble(prize1.Text);
            addtogrid();
            Restchose();
        }

        private void chose2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chose2.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
             valued = date2.Text;
             valuep = Convert.ToDouble(prize2.Text);
            addtogrid();
            Restchose();
        }

        private void dvg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    } 
