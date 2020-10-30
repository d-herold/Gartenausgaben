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
        double gesamtBetrag = 0.00; double menge; double einzelpreis;

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

        public double GetEinzelPreis
        {
            get 
            { 
                return einzelpreis; 
            }
            set 
            {
                this.einzelpreis = Convert.ToDouble(tb_Einzelpreis.Text);
                einzelpreis = value; 
            }
        }

        public double GetMenge
        {
            get
            {
                return menge;
            }
            set
            {
                menge = Convert.ToDouble(tb_Menge.Text);
            }
        }

        public double GetGesamtBetrag
        {
            get
            {
                return gesamtBetrag;
            }
            set
            {
                this.gesamtBetrag = Convert.ToDouble(tb_GesamtBetrag.Text);
                gesamtBetrag = value;
            }
        }

        private void CalculateQuantity()
        {
            if (tb_GesamtBetrag.Text != "")
            {
                try
                {
                    menge = Double.Parse(tb_GesamtBetrag.Text) / Double.Parse(tb_Einzelpreis.Text);
                    tb_Menge.Text = menge.ToString();
                }
                catch
                {
                    if (tb_GesamtBetrag.Text == "")
                        tb_Menge.Text = "";
                    else
                    {
                        tb_Menge.Text = "";
                        MessageBox.Show("Bitte geben sie eine Zahl ein!");
                    }
                }
            }
        }

        private void CalculateAmount()
        {
            if(tb_Menge.Text != "")
            try
            {
                gesamtBetrag = Double.Parse(tb_Menge.Text) * Double.Parse(tb_Einzelpreis.Text);
                tb_GesamtBetrag.Text = gesamtBetrag.ToString("0.00");
            }
            catch
            {
                if (tb_Einzelpreis.Text == "")
                    tb_GesamtBetrag.Text = "";
                else
                {
                    tb_GesamtBetrag.Text = "";
                    MessageBox.Show("Bitte geben sie eine Zahl ein!");
                }  
            }
            
        }

        private void tb_Einzelpreis_TextChanged(object sender, EventArgs e)
        {
            if (tb_Menge != null)
            {
                CalculateAmount();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
