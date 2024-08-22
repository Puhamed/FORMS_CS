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

namespace FORMS_CS
{
    public partial class newBill : Form

    {
        int count = 0;
        readonly Dbcon Li = new Dbcon();
        DataTable dt = new DataTable();
        DataTable items = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
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
        public void Addnewitem()
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
                    cmd.Parameters.AddWithValue("@5", comboBox2.SelectedIndex);
                    cmd.Parameters.AddWithValue("@6", Prizesbox.Text);
                    cmd.Parameters.AddWithValue("@7", PricebBox.Text);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
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
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        dvg.Rows.Add(++count, Idbox.Text, Namebox.Text, Datebox.Text, PricebBox.Text, Quantbox.Value, Prizesbox.Text, Convert.ToDouble(PricebBox.Text) * Convert.ToDouble(Quantbox.Value), "حذف");
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
            }
            Quantbox.Value = 1;
        }
    }
}

