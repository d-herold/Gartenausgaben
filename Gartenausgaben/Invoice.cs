using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class Invoice : Form
    {
        public Invoice()
        {
            InitializeComponent();
            SetMyCustomFormat();

        }
        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "ddMMMM yyyy"; //MMMM dd, yyyy";
        }

        private void cmd_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
