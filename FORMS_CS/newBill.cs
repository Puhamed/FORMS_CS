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
      readonly  Dbcon Li = new Dbcon();
        int count = 0;
        public newBill()
        {
            InitializeComponent();
        }
        private void NewBill_Load(object sender, EventArgs e)
        {// درتها لك لوطة شوفها
            MessageBox.Show("sss");

        }


        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var index = this.dvg.Rows.Add();
            bool found = false;
            if (dvg.Rows.Count > 0)
            {
                try
                {
                    foreach (DataGridViewRow row in dvg.Rows)
                    {
                        try
                        {
                            if (row.Cells[1].Value.ToString() == codebox.Text)
                            {

                                dvg.AllowUserToAddRows = false;
                                row.Cells[4].Value = Convert.ToUInt16(row.Cells[4].Value) + quantbox.Value;
                                row.Cells[5].Value = Convert.ToDouble(row.Cells[4].Value) * Convert.ToDouble(row.Cells[3].Value);
                                found = true;
                                return;
                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                    if (!found)
                    {
                        this.dvg.Rows.Add(++count, codebox.Text,senfBox.Text, priceBox.Text, quantbox.Text, Convert.ToDouble(quantbox.Value) * Convert.ToDouble(priceBox.Text));
                        /*this.dvg.Rows[index].Cells[0].Value = ++count;
                        this.dvg.Rows[index].Cells[1].Value = senfBox.Text;
                        this.dvg.Rows[index].Cells[2].Value = priceBox.Text;
                        this.dvg.Rows[index].Cells[3].Value = quantbox.Text;
                        this.dvg.Rows[index].Cells[4].Value = Convert.ToDouble(quantbox.Value) * Convert.ToDouble(priceBox.Text);*/
                    }
                }
                catch
                {
                    return;
                }
            }
        }

        private void newBill_Load_1(object sender, EventArgs e)
        {
            // اهياه يا يوسف
        }
    }
}
