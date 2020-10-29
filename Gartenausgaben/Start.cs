using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class Start : Form
    {
        Invoice invoice = new Invoice();
        
        public Start()
        {
            InitializeComponent();
        }

        private void CmdLoadNewInvoice_Click(object sender, EventArgs e)
        {
            if ((invoice.IsDisposed) || (null == invoice))
                invoice = new Invoice();
            invoice.Show();
            this.Close();
        }

        private void CmdEvaluation_Click(object sender, EventArgs e)
        {
            var evaluation = new Evaluation();
            evaluation.Show();
        }

    }
}
