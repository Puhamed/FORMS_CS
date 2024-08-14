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
        DataTable newtable = new DataTable();

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
        private void Datagrid()//لضبط اتجاهات النص
        {
            dvg.RightToLeft = RightToLeft.Yes;
            dvg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }
        private void Newbell()//لتهيئة اول عنصر في الفاتورة


        {
            newtable = dt.Clone();
            dvg.DataSource = newtable.DefaultView.ToTable(false, "item_id", "item_name", "item_prizes");
            Datagrid();
            DataGridViewColumn quntityc = new DataGridViewTextBoxColumn();
            DataGridViewColumn totalc = new DataGridViewTextBoxColumn();
            dvg.Columns.Add(quntityc);
            dvg.Columns.Add(totalc);
            dvg.Columns["item_id"].HeaderText = "رقم العنصر";
            dvg.Columns["item_name"].HeaderText = "اسم العنصر";
            dvg.Columns["item_prizes"].HeaderText = "السعر";
            quntityc.HeaderText = "الكمية";
            quntityc.Name = "cc";
            quntityc.ValueType = typeof(double);
            totalc.HeaderText = "الاجمالي";
            totalc.Name = "tt";
            dvg.AllowUserToAddRows = false;

        }
        private void Add1(DataRow row, out bool cheack)//
        {
             cheack = false;
            for (int s = 0; s <= newtable.Rows.Count - 1; s++)
            {
                if (row.ItemArray.SequenceEqual (newtable.Rows[s].ItemArray))
                {
                    cheack = true;
                    if (quantbox.Value == 1)
                        dvg.Rows[s].Cells["cc"].Value = (double)dvg.Rows[s].Cells["cc"].Value +1;
                    else
                        dvg.Rows[s].Cells["cc"].Value = +quantbox.Value;

                    break;
                }
            }
        }
        private void Addtogrid()//لاضافة العنصر الي الداتا جريد فيو
      
        { 
            if (newtable.Rows.Count == 0)//  اول عنصر في الفاتورة
            {
              Newbell();
            }


            if (dt.Rows.Count > 1)//  عنصر ف الفاتورة متكرر
                {
                    foreach (DataRow row in dt.Rows)
                    {
                    DateTime date_co = Convert.ToDateTime(valued); 
                    if ( Convert.ToDateTime (row["item_date"]) == date_co && Convert.ToDouble(row["item_prizes"]) == valuep)
                    {
                        Add1(row, out bool ceake) ;
                        if (ceake == false)
                        {
                            newtable.ImportRow(row);

                            if (quantbox.Value == 1)
                            {
                                dvg.DataSource = newtable.DefaultView.ToTable(false, "item_id", "item_name", "item_prizes");//لحجب بعض العناصر

                                // Convert.ToDouble(dvg.Rows[dvg.Rows.Count - 1].Cells["cc"].Value = 1);//مشكلة هنا
                                dvg.Rows[0].Cells["cc"].Value = 1;
                              //  double value = Convert.ToDouble(dvg.Rows[dvg.Rows.Count - 1].Cells["cc"].Value);
                            }
                            else
                                dvg.Rows[dvg.Rows.Count - 1].Cells["cc"].Value = quantbox.Value;//يحتاج الي تعديل 

                        }
                        break;
                    }
                    }
                }
                else// عنصر ف الفاتورة ليس متكرر
                {
                    dt.Rows.Add(newtable);// يحتاج الي تعديل 
                }

            dvg.DataSource = newtable.DefaultView.ToTable(false, "item_id", "item_name", "item_prizes");//لحجب بعض العناصر
            Datagrid();
            Result();
            dt.Clear();
        }
        private void Result()// لاحساب الكمية و السعر الاجمالي
        {
        //    if (quantbox.Value == 1)
        //    {
        //        for (int i = 0; i <= newtable.Rows.Count; i++)
        //        {
        //            if (newtable.Rows[i]["item_id"].ToString() == TextBox10.Text)
        //            {
        //                dvg.Rows[dvg.Rows.Count-2].Cells["cc"].Value = 1;
        //              //  dvg.Rows[i].Cells["tt"].Value = (int)dvg.Rows[i].Cells["cc"].Value * (float)newtable.Rows[i]["item_prizes"];//مشكلة هنا
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i <= newtable.Rows.Count; i++)
        //        {
        //            if (newtable.Rows[i]["item_id"].ToString() == TextBox10.Text)
        //            {
        //                dvg.Rows[i].Cells["cc"].Value = +quantbox.Value;
        //                //dvg.Rows[i].Cells["tt"].Value = (int)dvg.Rows[i].Cells["cc"].Value * (int)newtable.Rows[i]["item_prizes"];//مشكلة هنا

        //                break;
        //            }
        //        }

        //    }
        //    for (int i =0;i<= dvg.Rows.Count;i++)
        //    {
        //        //totalp.Text +=dvg.Rows[i].Cells["tt"].Value;
        //    }
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
            Addtogrid();
            Restchose();
        }

        private void chose2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chose2.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
                return;
            valued = date2.Text;
             valuep = Convert.ToDouble(prize2.Text);
            Addtogrid();
            Restchose();
        }

      
    }
    } 
