﻿using System;
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
        public Store()
        {
            InitializeComponent();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            newBill nb = new newBill();
            nb.ShowDialog(); 
        }
    }
}
