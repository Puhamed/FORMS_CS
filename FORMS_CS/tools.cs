using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace FORMS_CS
{
    public class tools
    {
        messageForm msf = new messageForm();

        public void messageOK(string head, string message, int picNo)
        {
            msf.headlabel = head;
            msf.message = message;
            msf.pic = picNo;
            msf.ShowDialog();
        }
    }
}
