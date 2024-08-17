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
        

        public newBill()
        {
            InitializeComponent();
        }
  


        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            str = "select * from items where item_id=@1";
            da = new SqlDataAdapter(str, Li.con);
            da.SelectCommand.Parameters.AddWithValue("@1", Idbox.Text);
            da.Fill(items);
            foreach (DataRow row in items.Rows)
            { 
             if (row["item_id"].ToString() ==Idbox.Text)
             ShowNotification("هاذا العنصر موجود");
            }
            Empty();

          
        }

        private void newBill_Load_1(object sender, EventArgs e)
        {
            dt = Li.Fill_sup();
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember= "sup_name";
        }
    }
}
