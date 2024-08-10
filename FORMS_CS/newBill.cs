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
    public partial class newBill : Form

    {
        dbcon li = new dbcon();
  
        public newBill()
        {
            InitializeComponent();
        }
        private void newBill_Load(object sender, EventArgs e)
        {// ياحفيظ هاذي مبتش تخدم الدالة هذي كتبتها نا مقدرتش انطلعها من الديزاين شوفها كنها عشان الموردين
            MessageBox.Show("sss");

        }


        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
          /*  var index = this.dvg.Rows.Add();

            bool found = false;
            if (dvg.Rows.Count > 1)
            {
                try
                {
                    foreach (DataGridViewRow row in dvg.Rows)
                    {
                        if (row.Cells[1].Value == senfBox.Text)
                        {

                            row.Cells[4].Value = Convert.ToUInt16(row.Cells[4].Value) + quantbox.Value;


                            found = true;
                            return;
                        }

                    }
                    if (!found)
                    {
                        this.dvg.Rows[index].Cells[0].Value = ++count;
                        this.dvg.Rows[index].Cells[1].Value = senfBox.Text;
                       // this.dvg.Rows[index].Cells[2].Value = listBox1.Text;
                       // this.dvg.Rows[index].Cells[3].Value = unitBox.Text;
                       // this.dvg.Rows[index].Cells[4].Value = quantity.Value;
                    }
                }
                catch
                {
                    return;
                }
            }*/
        }


    }
}
