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
    public partial class CategortiesForm : Form
    {
        int action = 0;
        dbcon con = new dbcon();
        tools tls = new tools();
        string selc ="";
        public CategortiesForm()
        {
            InitializeComponent();
        }
        void uncheck()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;   
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (panel3.Visible == false)
            {
                panel3.Visible = true;
                action = 1;
                button5.Visible = false;
                textBox1.ReadOnly = false;
                button3.Visible = false;
            }
            else
            {
                panel3.Visible = false;
                action = 0;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (panel3.Visible == false)
            {
                panel3.Visible = true;
                action = 2;
                button5.Visible = true;
                textBox1.ReadOnly = true;
                button3.Visible = true;
            }
            else
            {
                panel3.Visible = false;
                button5.Visible = false;
                action = 0;
                textBox1.ReadOnly=false;
                button3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (action == 1)
            {
                if (textBox1.Text == "" || textBox1.Text == null || (radioButton1.Checked == false && radioButton2.Checked == false))
                {
                    tls.messageOK("بيانات غير مستوفية", "يرجى تعبئة اسم الفئة واختيار إذا كانت خاضعة للصلاحية أو لا", 1);
                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        con.add_cat(textBox1.Text, "1");
                        tls.messageOK("تمت العملية ", "تمت إضافة فئة '" + textBox1.Text + "'بنجاح..", 0);
                        load();

                    }
                    else if (radioButton2.Checked == true)
                    {
                        con.add_cat(textBox1.Text, "0");
                        tls.messageOK("تمت العملية ", "تمت إضافة فئة '" + textBox1.Text + "'بنجاح..", 0);
                        load();
                    }
                }
            }
            else if (action == 2)
            {
                if (textBox1.Text == "" || textBox1.Text == null || (radioButton1.Checked == false && radioButton2.Checked == false))
                {
                    tls.messageOK("بيانات غير مستوفية", "يرجى تعبئة اسم الفئة واختيار إذا كانت خاضعة للصلاحية أو لا", 1);
                }
                else
                {
                    if (selc == "" || selc == null)
                    {
                        tls.messageOK("عملية خاطئة", "يجب اختيار اسم من القائمة قبل عملية التعديل", 1);
                        return;
                    }
                    else
                    {
                        if (radioButton1.Checked == true)
                        {
                            con.update_cat(textBox1.Text, "1",selc);
                            tls.messageOK("تمت العملية ", "تم تعديل الفئة بنجاح", 0);
                            load();
                        }
                        else if (radioButton2.Checked == true)
                        {
                            con.update_cat(textBox1.Text, "0", selc);
                            tls.messageOK("تمت العملية ", "تم تعديل الفئة بنجاح", 0);
                            load();
                        }
                    }
                }



            }
        }

        private void CategortiesForm_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            dvg.DataSource = con.viewcategories();
        }

        private void dvg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dvg.Rows[e.RowIndex].Cells[0].Value.ToString();
            selc = dvg.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selc == "" || selc == null)
            {
                tls.messageOK("عملية خاطئة", "يجب اختيار اسم من القائمة قبل عملية التعديل", 1);
                return;
            }
            else
            {
                if (MessageBox.Show("هل أنت متأكد من أنك تريد حذف هذه الفئة نهائياً؟، ستظل الأصناف تحت هذه الفئة موجودة ولكن دون فئة ", "رسالة تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    con.del_cat(textBox1.Text, selc);
                    tls.messageOK("تمت العملية", "الأصناف الموجودة تحت الفئة المحذوفة موجودة ولكن غير خاضعة للتصنيف", 0);
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;  
        }
    }
}
