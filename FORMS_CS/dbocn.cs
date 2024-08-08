using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FORMS_CS
{
    public class dbcon
    {
        public SqlConnection con;
        public dbcon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\supertmarket.mdf;Integrated Security=True");
        }
        public SqlConnection connect()
        {
            if (con.State == ConnectionState.Closed == true)
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
        public DataTable fillsenfs()
        {
            DataTable dt = new DataTable();
            disconnect();
            SqlDataAdapter sa = new SqlDataAdapter("select item_name as[الأصناف] from items", con);
            sa.Fill(dt);
            return dt;
        }
        public void Custom_Add(string Nation,string qaid,string name,string phone_no,string city,string id,string Nation_pic,string city_pic,string id_pic,string contract)
        {
            disconnect();
            string command = "insert into Custom_data (Nation,qaid, name, phone_no, city, id,Nation_pic, city_pic, id_pic, contract, date_) values('" + Nation + "','" + qaid + "', N'" + name + "',N'" + phone_no + "', N'" + city + "',N'" + id + "',N'" + Nation_pic + "',N'" + city_pic + "',N'" + id_pic + "', N'" + contract + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
            SqlCommand cmd = new SqlCommand(command, connect());
            cmd.ExecuteNonQuery();
            disconnect();
        }
   
    }
}
