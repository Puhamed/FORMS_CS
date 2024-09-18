using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORMS_CS
{
    public partial class Store : Form
    {
        dbcon con = new dbcon();
        public Store()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            newBill nb = new newBill();
            nb.ShowDialog();
            load();
        }
        void load()
        {
            dataGridView1.DataSource = con.storeView();
        }
        private void Store_Load(object sender, EventArgs e)
        {
            load();
        }

        // عند تغيير قيمة معينة في الداتا قريد فيو يظهر عمود يحتوي على بتون لتأكيد التغييرات
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewButtonColumn btnSaveChanges = new DataGridViewButtonColumn();
                btnSaveChanges.HeaderText = "حفظ التغييرات";
                btnSaveChanges.Text = "تأكيد";
                btnSaveChanges.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnSaveChanges);
            }
            catch
            {
                return;
            }
        }
        // دالة لحفظ التغييرات عن طريق البتون 
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns[7].Index)
                {
                    int rowIndex = e.RowIndex;
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];
                    // UpdateDatabase(row);
                }
            }
            catch
            {
                return ;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == false)
            {
                panel5.Visible = true;
            }
            else
            {
                panel5.Visible=false;
            }
        }
    }
}
