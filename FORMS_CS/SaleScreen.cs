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
    public partial class SaleScreen : Form
    {
        dbcon con = new dbcon();
        public SaleScreen()
        {
            InitializeComponent();
        }

        private void SaleScreen_Load(object sender, EventArgs e)
        {
            senfsgridview.DataSource = con.fillsenfs();
        }

        private void quantbox_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
