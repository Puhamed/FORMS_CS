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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        dbcon con = new dbcon();

        private void rjButton1_Click(object sender, EventArgs e)
        {
            dbcon cc = new dbcon();
            cc.disconnect();
            string sql = "select password from users where Username =N'" + namebox.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cc.connect());
            var pass = cmd.ExecuteScalar();

            try
            {
                if (passbox.Text == null || passbox.Text == "")
                {
                    MessageBox.Show("كلمة المرور أو إسم المستخدم غير صحيح", "يرجى إعادة المحاولة");
                    return;
                }
                /*else if (con.userIdentityforLabs(namebox.Text) == true)
                {
                    cc.Disconnect();
                    con.login(DateTime.Now.ToShortDateString().ToString(), namebox.Text);
                    labForm fr = new labForm();
                    fr.currentUser = namebox.Text;
                    fr.Show();
                    this.Hide();
                }*/
                else if (passbox.Text == pass.ToString())
                {
                    cc.disconnect();
                    con.login(DateTime.Now.ToShortDateString().ToString(), namebox.Text);
                    Form1 fr = new Form1();
                    fr.currentUser = namebox.Text;
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("كلمة المرور أو إسم المستخدم غير صحيح", "يرجى إعادة المحاولة");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("حدث خطأ غير متوقع, يرجى التأكد من الاتصال بقاعدة البيانات", "مشكلة في الاتصال!");
                return;
            }
        }
    }
}
