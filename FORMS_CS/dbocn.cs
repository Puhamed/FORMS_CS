﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FORMS_CS
{
    public class Dbcon
    {
        public SqlConnection con;
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
        
        public DataTable Fillsenfs()
        {
            DataTable dt = new DataTable();
           Disconnect();
            SqlDataAdapter sa = new SqlDataAdapter("select item_name as[الأصناف] from items", con);
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
            SqlDataAdapter sa = new SqlDataAdapter("select * from suppliers",con)
                ; sa.Fill(dt);
            return dt;
        }
        public DataTable Data_tem(string num)//جدول يتم تخزين الاصناف فيه بشكل مؤقت يستعمل في فورم البيع فقط  
        {
        DataTable dt =new DataTable();
            string str = "select * from items where item_id = @num";

            SqlDataAdapter da=new SqlDataAdapter(str,con) ;
            da.SelectCommand.Parameters.AddWithValue("@num", num);

            da.Fill(dt);
            //لتحديد العنصر المختار 
            if (dt.Rows.Count > 1)// للتحقق من انه يوجد عنصر ب الرقم المختار
            {
            }
            else if (dt.Rows.Count==0)
            {
                MessageBox.Show("خطا في الرقم", "لا يوجد عنصر بهاذا الخطا");
            }



            return dt; 
        }
     
    }
}
