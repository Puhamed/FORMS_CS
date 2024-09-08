using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FORMS_CS
{
    public class dbcon
    {
        public SqlConnection con;
        public SqlDataAdapter sa;
        public dbcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\supertmarket.mdf;Integrated Security=True");
        }
        public SqlConnection connect()
        {
            if (con.State ==  ConnectionState.Closed == true)
            {
                con.Open();
            }
            return con;

        }
        public void disconnect()
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
            sa.Fill(dt);
        return dt;
        }
        public DataTable Fillsenfs()
        {
            DataTable dt = new DataTable();
           disconnect();
             sa = new SqlDataAdapter("select item_name as[الأصناف] from items", con);
            sa.Fill(dt);
            return dt;
        }
        public void Custom_Add(string Nation, string qaid, string name, string phone_no, string city, string id, string Nation_pic, string city_pic, string id_pic, string contract)
        {
            disconnect();
            string command = "insert into Custom_data (Nation,qaid, name, phone_no, city, id,Nation_pic, city_pic, id_pic, contract, date_) values('" + Nation + "','" + qaid + "', N'" + name + "',N'" + phone_no + "', N'" + city + "',N'" + id + "',N'" + Nation_pic + "',N'" + city_pic + "',N'" + id_pic + "', N'" + contract + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            SqlCommand cmd = new SqlCommand(command, connect());
            cmd.ExecuteNonQuery();
            disconnect();
        }

        public DataTable Fill_sup()//لتعبة كوبو بوكس الموردين
        {
            DataTable dt = new DataTable();
            disconnect();
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
            disconnect();
            sa = new SqlDataAdapter("select * from customers", con);
            sa.Fill(dt);
            return dt;
        }
        public DataTable Fill_invo()//لتعبة كوبو بوكس الزبائن
        {
            DataTable dt = new DataTable();
            disconnect();
            sa = new SqlDataAdapter("select * from invoice_sell", con);
            sa.Fill(dt);
            return dt;
        }
        public DataTable Fill_invo_where(string _name)//لتعبة كوبو بوكس الزبائن
        {
            DataTable dt = new DataTable();
            disconnect();
             sa = new SqlDataAdapter("select * from invoice_sell where invo_id = '"+ _name + "' ", con);
            sa.Fill(dt);
            return dt;
        }

        public int Max_invo(DataTable dataTable,string name)
        { 
            int max = dataTable.AsEnumerable().Max(row => row.Field<int>(name));

            return max;
        }
        public DataTable Fill_hes(int user)
        { 
        DataTable dt=new DataTable();
            sa = new SqlDataAdapter("SELECT h.hes_id, u.user_name, k.kind_name, h.hes_date FROM hestory h JOIN kind_hes k ON h.hes_kind = k.kind_id JOIN users u ON h.hes_actor = u.user_id WHERE h.hes_actor =@id ", con);
            sa.SelectCommand.Parameters.AddWithValue("@id", user);
            sa.Fill(dt);
            return dt;
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
        public void add_newItems(string id, string it_name , string it_price ,string it_exdate, string it_quant,string sup, string total,string code )
        {
            disconnect();
            string command = "INSERT INTO invoice_det (invo_id,item_name,item_prize,item_date,item_countity,item_code)VALUES('"+ id + "', '"+ it_name + "','"+ it_price + "', '"+ it_exdate + "','"+ it_quant + "','"+ code +"')";
            SqlCommand cmd = new SqlCommand(command, connect());
            cmd.ExecuteNonQuery();
            disconnect();
            string command2 = "INSERT INTO invoice [item_date],[item_sup],[item_total])VALUES( '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + sup + "', '" + total + "')";
            SqlCommand cmd2 = new SqlCommand(command2, connect());
            cmd2.ExecuteNonQuery();
            disconnect();
        }
        public void login(string date, string n)
        {
            disconnect();
            string sql1 = "update users set LastLogged = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "'  where UserName = N'" + n + "' ";
            string sql2 = "update users set active = 1  where UserName = N'" + n + "' ";
            SqlCommand cmd1 = new SqlCommand(sql1, connect());
            disconnect();
            SqlCommand cmd2 = new SqlCommand(sql2, connect());
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
        }
        public DataTable top()
        {
            DataTable dt = new DataTable();
            sa = new SqlDataAdapter("SELECT max(invo_id)+1 from invoice ", con);
            sa.Fill(dt);
            return dt;
        }
    }
   
}
