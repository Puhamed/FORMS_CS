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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
// الصفحه تحتاج الي تعديل في اضافة الكمية لو كنت حفيظ متحطش خشمك ف الموضوع توا نعدلها نا 

namespace FORMS_CS
{
    public partial class SaleScreen : Form
    {
        string sel;
        readonly dbcon con = new dbcon();
        int i = 2;// متغير مساعد ل تحديد عدد العناصر المكرره يستخدم في دالة chose
        string valued;//متغير مساعد ل حفظ قيمة التاريخ في حالة تكرار العناصر
        double valuep;//متغير مساعد لحفظ قيمة السعر في حالة تكرار العناصر
        DataTable dt = new DataTable();//جدول مساعد ل تحديد العناصر المكرره فيه
        List<DateTime> dates= new List<DateTime>();// يخزن تواريخ العناصر
        DateTime date_co;//متغير مساعد لتخزين تاريخ المختار 
        string str;
        SqlCommand cmd;
        SqlDataAdapter da = new SqlDataAdapter();
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
        public void Addtogrid(string id)//اضافة العنصر الي الداتا جريد فيو
        {
             date_co = Convert.ToDateTime(valued);
                bool found = false;
            dt = con.Data_tem(id);
            if (dvg.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dvg.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == id)
                        {
                            dvg.AllowUserToAddRows = false;
                            row.Cells[3].Value = Convert.ToUInt16(row.Cells[3].Value) + quantbox.Value;
                            found = true;
                        }

                    }
                    if (!found)
                    {
                        Addoneitem(dt.Rows[0]);
                    }
                }
                catch
                {
                    return;
                }

            }
            else
            {
                Addoneitem(dt.Rows[0]);
            }
            quantbox.Value = 1;
            /* foreach (DataRow row in dt.Rows)
             {
                 if (Convert.ToDateTime(row["item_date"]) == date_co && Convert.ToDouble(row["item_prizes"]) == valuep)
                 {

                     foreach (DataGridViewRow row1 in dvg.Rows)
                     {
                     if (dvg.Rows.Count == 0)
                     {
                         Addoneitem(row);
                     }
                     else if ((row1.Cells[0]).Value.ToString() == (row["item_id"]).ToString() && row1.Cells[2].Value.ToString() == (row["item_prizes"]).ToString() && Convert.ToDateTime(row["item_date"]).ToString() == dates[row1.Index].ToString())
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
                             totalp.Text= Convert.ToInt32( dvg.Rows[row1.Index].Cells[4].Value) + totalp.Text;
                             dvg.Refresh();
                             return;
                         }
                         else
                         {
                             Addoneitem(row);
                         }
                     }


                 }
             }


         dvg.Refresh();*/
        }
        public void Maxinvo()//اضافة فاتورة جديده
        {
          //  comboBox2.Items.Add(con.Max_invo(con.Fill_invo(), "invo_id") + 1);
           // comboBox2.SelectedIndex = 0;
        }
        private void Addoneitem(DataRow roww)//اضافة ليس متكرر
        {
            //bool found = false;

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
            comboBox3.DataSource = con.getcat();
            comboBox3.DisplayMember = "cat_Name";
            comboBox3.ValueMember = "id";
        }
        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (TextBox10.Text == "" || TextBox10.Text == null)
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
                            Addtogrid(TextBox10.Text);

                    }
                }
                catch { return; }
            
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
            //Addtogrid();
            Restchose();
        }

        private void chose2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (chose2.LinkColor == System.Drawing.Color.Gray)//لضمان عدم الرجوع الي خانه غير موجوده
            return;
            valued = date2.Text;
            valuep = Convert.ToDouble(prize2.Text);
            //Addtogrid();
            Restchose();
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
            comboBox2.DataSource=null;
            Maxinvo();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            supfromitem();
        }
        private void supfromitem()//لتنقيص عدد الكمية من قاعدة البيانات
        {
            // بداية المعاملة
            str = " Update items set item_countity = 5 Where item_id = 153154";
            da = new SqlDataAdapter(str, con.con);
            dt = new DataTable();
            da.Fill(dt);
            da.Update(dt);
            //SqlTransaction transaction = con.con.BeginTransaction();
            //try
            //{
            //    foreach (DataGridViewRow dataRow in dvg.Rows)
            //    {
            //        string str = "UPDATE items SET item_countity = item_countity - @sup WHERE item_id = @id AND item_prizes = @pr AND item_date = @date";
            //         cmd = new SqlCommand(str, con.con);
            //        cmd.Transaction = transaction; // استخدم المعاملة المخزنة في متغير transaction
            //        cmd.Parameters.AddWithValue("@sup", Convert.ToInt32(dataRow.Cells[3].Value));
            //        cmd.Parameters.AddWithValue("@id", dataRow.Cells[0].Value.ToString());
            //        cmd.Parameters.AddWithValue("@pr", dataRow.Cells[2].Value.ToString());
            //        cmd.Parameters.AddWithValue("@date", dates[dataRow.Index]);
            //        int rowsAffected = cmd.ExecuteNonQuery();
            //        // التحقق من أن الأمر نفذ بنجاح
            //        if (rowsAffected > 0)
            //        {
            //            MessageBox.Show("0000");
            //            // الأمر نفذ بنجاح
            //        }
            //        else
            //        {
            //            // لم يتم تحديث أي صف
            //            throw new Exception("لم يتم تنفيذ الأمر بنجاح.");
            //        }
            //    }
            //    // إذا تم تنفيذ جميع الأوامر بنجاح، قم بتثبيت المعاملة
            //    transaction.Commit();
            //}
            //catch (Exception ex)
            //{
            //    // إذا حدث خطأ، قم بإلغاء المعاملة
            //    transaction.Rollback();
            //    throw ex;
            //}
            //con.Disconnect();
        }
        private void addtoinvo()//لحفظ الفاتورة
        {///يحتاج الي تعديل
            str = "update invoice_sell set invo_id=@1 , invo_date=@2 , invo_cus=@3 , invo_total=@4";
         cmd=new SqlCommand(str, con.con);
            cmd.Parameters.AddWithValue("@1", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@2", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@3", comboBox2.SelectedValue);
            cmd.Parameters.AddWithValue("@4", comboBox2.SelectedValue);
            con.connect();
            cmd.ExecuteNonQuery();
            con.disconnect();
        }
        private void addtoinvodet()//لحفظ تفاصيل الفاتورة
        {

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.DataSource = con.Fill_invo();
            comboBox2.DisplayMember = "invo_id";
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
         
        }

        private void comboBox2_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                return;
            str = "select * from invoice_sell_det where invo_id ='" + comboBox2.Text + "'";
            da = new SqlDataAdapter(str, con.con);
            dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                dvg.Rows.Add(row["item_id"], row["item_name"], row["item_countity"], row["item_prize"]);
            }
            dt.Rows.Clear();
            //غيرها بعدين بالهون
            ComboBox1.DataSource = null;
            ComboBox1.Text = con.Fill_invo_where(comboBox2.Text).Rows[0]["invo_cus"].ToString();
        }

        private void senfsgridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            quantbox.Value = 1;
            dt = con.Data_tem(senfsgridview.Rows[e.RowIndex].Cells[0].Value.ToString());
            if (dt.Rows.Count > 1)// للتحقق من انه العنصر مخزن اكثر من مره
            {
                Chose();
            }
            else
                Addoneitem(dt.Rows[0]);
        }

        private void Panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TextBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            senfsgridview.DataSource = con.Fillsenfs(comboBox3.Text);

        }

        private void senfsgridview_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
      
        }

        private void senfsgridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                sel = con.getcode(senfsgridview.Rows[e.RowIndex].Cells[0].Value.ToString()).Rows[0][0].ToString();
                Restchose();

                quantbox.Value = 1;
                dt = con.Data_tem(sel);
                if (dt.Rows.Count > 1)// للتحقق من انه العنصر مخزن اكثر من مره
                {
                    Chose();
                }
                else
                Addtogrid(sel);
            }
            catch
            {
                return;
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {

        }
    }
    } 
