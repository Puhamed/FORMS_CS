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
// حفيظ مش شورك الفورم
// عدل استعلام اختيار المستخدمين 
// مازالك الحذف وداتا جريد فيو لعرض السجلات 


namespace FORMS_CS
{
    public partial class Users : Form
    {
        readonly Dbcon con = new Dbcon();
        DataTable dt;
        SqlDataAdapter da;
        SqlCommandBuilder cmd;
        int rowselect = -1;
        public Users()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Users_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt = con.Fill_users();
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count==0)
            { return; }
            nameBox.Text = dataGridView1.Rows[e.RowIndex].Cells["user_name"].Value.ToString();
            passbox.Text = dataGridView1.Rows[e.RowIndex].Cells["user_pass"].Value.ToString();
            comboBox1.DataSource = con.Chose_level();
            comboBox1.DisplayMember = "level_name";
            comboBox1.SelectedIndex = -1;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (dt.GetChanges() != null)
            {
                da = new SqlDataAdapter();
                cmd = new SqlCommandBuilder(da);
                da.Update(dt);

            }
            else
                MessageBox.Show(" الحفظ", "لا يوجد تغيرات");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rowselect == -1)
            {
                MessageBox.Show("الرجاء اختيار صف ليتم التعديل عليه");
                return;
            }
            dt.Rows[rowselect]["user_name"] = nameBox.Text;
            dt.Rows[rowselect]["user_pass"] = passbox.Text;
            dt.Rows[rowselect]["user_level"] = comboBox1.SelectedIndex;
        }
    }
}
