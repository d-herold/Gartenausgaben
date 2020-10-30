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
    public partial class Gartenausgaben : Form
    {
        public Gartenausgaben()
        {
            InitializeComponent();
        }

        private void CmdLoadNewInvoice_Click(object sender, EventArgs e)
        {
            var invoice = new Invoice();
            invoice.FormClosing += oldForm_FormClosing;
            Hide();
            invoice.Show(this);
        }

        private void CmdEvaluation_Click(object sender, EventArgs e)
        {
            var evaluation = new Evaluation();
            evaluation.FormClosing += oldForm_FormClosing;
            Hide();
            evaluation.Show();
        }
        private void oldForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("Wollen Sie das Fenster wirklich schließen");
            Show();
        }
    }
}