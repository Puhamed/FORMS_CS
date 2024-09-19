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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FORMS_CS
{
    public partial class newBill : Form
       
        
    {
        messageForm msf = new messageForm();
        tools tls = new tools();
        int count = 0;
        readonly dbcon Li = new dbcon();
        DataTable dt = new DataTable();
        DataTable items = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        dbcon con = new dbcon();
        string str;
        public void Empty()
        {
            //if (Idbox.Text || Namebox.Text || PricebBox.Text || Prizesbox.Text == "")
            //{ 

            //}

        }
        // فرضاً أن لديك Label اسمه notificationLabel
        private void ShowNotification(string message)
        {
            label11.Text = message;
            label11.Visible = true;

            Timer timer = new Timer();
            timer.Interval = 5000; // 5000 milliseconds = 5 seconds
            timer.Tick += (s, e) =>
            {
                label11.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
        // جمع قيمة عمود في الداتا قريد
        private double SumColumn(DataGridView dgv, int columnIndex)
        {
            double sum = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[columnIndex].Value != null)
                    {
                        sum += Convert.ToDouble(row.Cells[columnIndex].Value);
                    }
                }
            }
            return sum;
        }
       /* public void Addnewitem()
        {
            str = "insert into items (item_id,item_name,item_date,item_countity,item_kind,item_prizes,item_prizeb)values  (@1,@2,@3,@4,@5,@6,@7)";
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\supertmarket.mdf;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@1", Idbox.Text);
                    cmd.Parameters.AddWithValue("@2", Namebox.Text);
                    cmd.Parameters.AddWithValue("@3", Datebox.Value);
                    cmd.Parameters.AddWithValue("@4", Quantbox.Value);
                    //cmd.Parameters.AddWithValue("@5", comboBox2.SelectedIndex);
                    cmd.Parameters.AddWithValue("@6", Prizesbox.Text);
                    cmd.Parameters.AddWithValue("@7", PricebBox.Text);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }*/
        void Clear()
        {
            Namebox.Clear();
            Idbox.Clear();
            Datebox.Value = DateTime.Now;
            Quantbox.Value = 1;
            PricebBox.Text = "1";
            Prizesbox.Text = "1";
        }

        public newBill()
        {
            InitializeComponent();
        }




        private void Button4_Click(object sender, EventArgs e)
        {
            bool found = false;
            if (PricebBox.Text == "" || Prizesbox.Text == "" || Quantbox.Value < 1 || Namebox.Text == "" || Idbox.Text == "")
            {
                tls.messageOK("إدخال خاطئ", "يرجى إدخال قيم جميع الحقول من أجل المتابعة!!", 1);
            }
            else
            {
                if (dvg.Rows.Count > 0)
                {
                    try
                    {
                        foreach (DataGridViewRow row in dvg.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == Idbox.Text && Datebox.Text == row.Cells[3].Value.ToString())
                            {
                                dvg.AllowUserToAddRows = false;
                                row.Cells[5].Value = Convert.ToUInt16(row.Cells[5].Value) + Quantbox.Value;
                                row.Cells[7].Value = Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[5].Value);
                                totalBox.Text = SumColumn(dvg, 7).ToString();
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            dvg.Rows.Add(++count, Idbox.Text, Namebox.Text, Datebox.Text, PricebBox.Text, Quantbox.Value, Prizesbox.Text, Convert.ToDouble(PricebBox.Text) * Convert.ToDouble(Quantbox.Value), comboBox2.Text, "حذف");
                            totalBox.Text = SumColumn(dvg, 7).ToString();
                            Clear();
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                {
                    dvg.Rows.Add(++count, Idbox.Text, Namebox.Text, Datebox.Text, PricebBox.Text, Quantbox.Value, Prizesbox.Text, Convert.ToDouble(PricebBox.Text) * Convert.ToDouble(Quantbox.Value), "حذف");
                    totalBox.Text = SumColumn(dvg, 7).ToString();
                    Clear();
                }
                Quantbox.Value = 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        
            //تشطيب
            for (int i = 0; i < dvg.Rows.Count; i++)
                {
                    con.add_newItems(Bellbox.Text, dvg.Rows[i].Cells[2].Value.ToString(), dvg.Rows[i].Cells[4].Value.ToString(), dvg.Rows[i].Cells[3].Value.ToString(), dvg.Rows[i].Cells[5].Value.ToString(),comboBox1.Text, totalBox.Text, dvg.Rows[i].Cells[1].Value.ToString(), dvg.Rows[i].Cells[6].Value.ToString());
                    Bellbox.Text = con.top().Rows[0][0].ToString();
                }
            dvg.Rows.Clear();
            tls.messageOK("تم الحفظ", "تم إدراج الفاتورة بنجاح",0);
            
        
        }

        private void newBill_Load(object sender, EventArgs e)
        {
            Bellbox.Text = con.top().Rows[0][0].ToString();
            comboBox2.DataSource = con.getcat();
            comboBox2.DisplayMember = "cat_Name";
            comboBox2.ValueMember = "id";
            comboBox1.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == null || comboBox1.Text == "")
            {
                MessageBox.Show("يرجى إدخال إسم أي مورد من اجل المتابعة","تنبيه هام",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (panel2.Enabled == false)
                {
                    panel2.Enabled = true;
                    Idbox.Focus();
                    button6.Text = "إلغاء";
                    comboBox1.Enabled = false;
                }
                else
                {
                    panel2.Enabled = false;
                    comboBox1.Focus();
                    button6.Text = "تأكيد";
                    comboBox1.Enabled = true;
                }
            }
           
        }

        private void newBill_KeyDown(object sender, KeyEventArgs e)
        {
            /*if(panel2.Enabled== true || )
            if (e.KeyCode == Keys.Enter)
        */
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(dvg.Rows.Count > 0)
            {
                if(MessageBox.Show("هل أنت متأكد من إلغاء هذه الفاتورة","رسالة تأكيد", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    this.Hide();
                }
                else
                {
                    return;
                }
            }
            else
            {
                this.Hide();
            }
        }

        private void Idbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Idbox.Text == "" || Idbox.Text == null)
                {
                    tls.messageOK(" باركود", "لرجاء ادخال باركود",1);
                }
                else
                {
                   if (con.getItemName(Idbox.Text).Rows.Count < 1)
                    {
                        tls.messageOK("باركود غير معروف", "  لا يوجد عنصر مدخل بهذا الكود ", 1);
                        return;
                    }
                    else
                    {
                        Namebox.Text = con.getItemName(Idbox.Text).Rows[0][0].ToString();
                    }
                }

            }
        }

        private void dvg_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}