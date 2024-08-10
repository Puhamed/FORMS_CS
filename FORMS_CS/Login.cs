using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FORMS_CS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbcon cc = new dbcon();
            cc.disconnect();
            string sql = "select password from users where Username =N'" + nameBox.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, cc.connect());
            var pass = cmd.ExecuteScalar();
            try
            {
                if (passbox.Text == null || passbox.Text == "")
                {
                    MessageBox.Show("كلمة المرور أو إسم المستخدم غير صحيح", "يرجى إعادة المحاولة");
                    return;
                }

                else if (passbox.Text == pass.ToString())
                {
                    cc.login(DateTime.Now.ToShortDateString().ToString(), nameBox.Text);
                    Form1 fr = new Form1();
                    fr.currentUser = nameBox.Text;
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
                  MessageBox.Show("كلمة المرور أو إسم المستخدم غير صحيح", "يرجى إعادة المحاولة");
                return;
                }
            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
