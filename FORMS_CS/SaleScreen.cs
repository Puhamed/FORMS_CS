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
// الصفحه تحتاج الي تعديل في اضافة الكمية لو كنت حفيظ متحطش خشمك ف الموضوع توا نعدلها نا 

namespace FORMS_CS
{
    public partial class SaleScreen : Form
    {
        readonly Dbcon con = new Dbcon();
        int i = 2;// متغير مساعد ل تحديد عدد العناصر المكرره يستخدم في دالة chose
        string valued;//متغير مساعد ل حفظ قيمة التاريخ في حالة تكرار العناصر
        double valuep;//متغير مساعد لحفظ قيمة السعر في حالة تكرار العناصر
        DataTable dt = new DataTable();//جدول مساعد ل تحديد العناصر المكرره فيه
        List<DateTime> dates= new List<DateTime>();// يخزن تواريخ العناصر
        DateTime date_co;//متغير مساعد لتخزين تاريخ المختار 
        string str;
        SqlCommand cmd;

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
            TextBox10.Text = "";
            quantbox.Value = 0;
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
        public void addtogrid()//اضافة العنصر الي الداتا جريد فيو
        {
             date_co = Convert.ToDateTime(valued);
     
                foreach (DataRow row in dt.Rows)
                {
                    if (Convert.ToDateTime(row["item_date"]) == date_co && Convert.ToDouble(row["item_prizes"]) == valuep)
                    {

                        foreach (DataGridViewRow row1 in dvg.Rows)
                        {
                            if (dvg.Rows.Count == 0)
                                break;
                            if ((row1.Cells[0]).Value.Equals(row["item_id"]) && row1.Cells[2].Value.Equals(row["item_prizes"]) && Convert.ToDateTime(row["item_date"]) == dates[row1.Index])
                            {
                            if (quantbox.Value == 1)
                            {
                                dvg.Rows[row1.Index].Cells[3].Value = Convert.ToInt32(dvg.Rows[row1.Index].Cells[3].Value) + 1;
                            }
                            else
                            {
                                dvg.Rows[row1.Index].Cells[3].Value = Convert.ToInt32(dvg.Rows[row1.Index].Cells[3].Value) + quantbox.Value;
                            }
                            dvg.Rows[row1.Index].Cells[4].Value = Convert.ToInt32(dvg.Rows[row1.Index].Cells[3].Value) * Convert.ToInt32(dvg.Rows[row1.Index].Cells[2].Value);
                            dvg.Refresh();
                                return;
                            }
                        }
                    addoneitem(row);
                    break;
                    }
                }
            

            dvg.Refresh();
        }
        private void addoneitem(DataRow roww)//اضافة ليس متكرر
        {
            if (quantbox.Value == 1)
            {
                dvg.Rows.Add(roww["item_id"], roww["item_name"], roww["item_prizes"], 1, roww["item_prizes"]);
                dates.Add(date_co);
            }
            else
            {
                dvg.Rows.Add(roww["item_id"], roww["item_name"], roww["item_prizes"], quantbox.Value, Convert.ToInt32(roww["item_prizes"])*quantbox.Value);
                dates.Add(date_co);
            }

        }
        private void SaleScreen_Load(object sender, EventArgs e)
        {
            senfsgridview.DataSource = con.Fillsenfs();
        ComboBox1.DataSource=  con.Fill_cos();
        ComboBox1.DisplayMember= "cus_name";
        ComboBox1.SelectedIndex= 0;
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
                    quantbox.Value = 1;
                    dt = con.Data_tem(TextBox10.Text);
                    if (dt.Rows.Count > 1)// للتحقق من انه العنصر مخزن اكثر من مره
                    {
                        Chose();
                    }
                    else
                    addoneitem(dt.Rows[0]);

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

        private void button10_Click(object sender, EventArgs e)
        {
        }

        private void Button7_Click(object sender, EventArgs e)
        {
         dvg.Rows.Clear();
            dt.Rows.Clear();
           dates.Clear();
            Restchose();
            dvg.Refresh(); 
        }

        private void button9_Click(object sender, EventArgs e)
        {
            supfromitem();
            addtoinvo();
            addtoinvodet();
        }
        private void supfromitem()//لتنقيص عدد الكمية من قاعدة البيانات
        {
            foreach (DataGridViewRow dataRow in dvg.Rows)
           {//يحتاج الي تعديل القيمه لا تطبق
      

                str = "update items set item_countity = item_countity-@sup where item_id=@id and item_prizes=@pr and item_date=@date";
                cmd = new SqlCommand(str, con.con);
                cmd.Parameters.AddWithValue("@sup",Convert.ToInt32 (dataRow.Cells[3].Value));
                cmd.Parameters.AddWithValue("@id", dataRow.Cells[0].Value);
                cmd.Parameters.AddWithValue("@pr", dataRow.Cells[2].Value);
                cmd.Parameters.AddWithValue("@date", dates[dataRow.Index]);
                con.Connect();
                cmd.ExecuteNonQuery();
                con.Disconnect();
            }
        }
        private void addtoinvo()//لحفظ الفاتورة
        {

        }
        private void addtoinvodet()//لحفظ تفاصيل الفاتورة
        {

        }

    }
    } 
