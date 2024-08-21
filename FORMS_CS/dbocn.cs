using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FORMS_CS
{
    public class Dbcon
    {
        public SqlConnection con;
        public SqlDataAdapter sa;
        public Dbcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\supertmarket.mdf;Integrated Security=True");
        }
        public SqlConnection Connect()
        {
            if (con.State == ConnectionState.Closed == true)
            {
                con.Open();
            }
            return con;

        }
        public void Disconnect()
        {
            if (con.State == ConnectionState.Open == true)
            {
                con.Close();
            }
        }
        public DataTable Fill_users()
        { 
        DataTable dt = new DataTable();
            sa=new SqlDataAdapter("select u.user_id ,u.user_name,u.user_pass , l.level_name from users u join levels l on u.user_level = l.level_id ",con);
            dt=new DataTable();
            sa.Fill(dt);
        return dt;
        }
        public DataTable Fillsenfs()
        {
            DataTable dt = new DataTable();
           Disconnect();
             sa = new SqlDataAdapter("select item_name as[الأصناف] from items", con);
            sa.Fill(dt);
            return dt;
        }
        public void Custom_Add(string Nation, string qaid, string name, string phone_no, string city, string id, string Nation_pic, string city_pic, string id_pic, string contract)
        {
            Disconnect();
            string command = "insert into Custom_data (Nation,qaid, name, phone_no, city, id,Nation_pic, city_pic, id_pic, contract, date_) values('" + Nation + "','" + qaid + "', N'" + name + "',N'" + phone_no + "', N'" + city + "',N'" + id + "',N'" + Nation_pic + "',N'" + city_pic + "',N'" + id_pic + "', N'" + contract + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            SqlCommand cmd = new SqlCommand(command, Connect());
            cmd.ExecuteNonQuery();
            Disconnect();
        }

        public DataTable Fill_sup()//لتعبة كوبو بوكس الموردين
        {
            DataTable dt = new DataTable();
            Disconnect();
             sa = new SqlDataAdapter("select * from suppliers", con);
                sa.Fill(dt);
            return dt;
        }
        public DataTable Chose_level() // لاختيار مستوي المستخدم
        { DataTable dt= new DataTable();
            sa = new SqlDataAdapter("select * from levels", con);
            sa.Fill(dt);
            return dt;
        }
        public DataTable Fill_cos()//لتعبة كوبو بوكس الزبائن
        {
            DataTable dt = new DataTable();
            Disconnect();
            sa = new SqlDataAdapter("select * from customers", con);
            sa.Fill(dt);
            return dt;
        }
        public DataTable Fill_invo()//لتعبة كوبو بوكس الزبائن
        {
            DataTable dt = new DataTable();
            Disconnect();
            sa = new SqlDataAdapter("select * from invoice_sell", con);
            sa.Fill(dt);
            return dt;
        }
        public DataTable Fill_invo_where(string _name)//لتعبة كوبو بوكس الزبائن
        {
            DataTable dt = new DataTable();
            Disconnect();
             sa = new SqlDataAdapter("select * from invoice_sell where invo_id = '"+ _name + "' ", con);
            sa.Fill(dt);
            return dt;
        }
        public int max_invo(DataTable dataTable,string name)
        { 
            int max = dataTable.AsEnumerable().Max(row => row.Field<int>(name));

            return max;
        }
        public DataTable Data_tem(string num)//جدول يتم تخزين الاصناف فيه بشكل مؤقت يستعمل في فورم البيع فقط  
        {
        DataTable dt =new DataTable();
            string str = "select * from items where item_id = @num";

             sa=new SqlDataAdapter(str,con) ;
            sa.SelectCommand.Parameters.AddWithValue("@num", num);

            sa.Fill(dt);
            return dt; 
        }
    }
   
}
