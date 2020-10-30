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
                this.einzelpreis = Convert.ToDouble(tb_Einzelpreis);
                value = einzelpreis; 
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
                this.menge = Convert.ToDouble(tb_Menge);
                value = menge;
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
                this.gesamtBetrag = Convert.ToDouble(tb_GesamtBetrag);
                value = gesamtBetrag;
            }
        }

        private double CalculateAmount()
        {
            gesamtBetrag = einzelpreis * menge;
            tb_GesamtBetrag.Text = gesamtBetrag.ToString();

            return gesamtBetrag;
        }

        private void tb_GesamtBetrag_Validated(object sender, EventArgs e)
        {
            
        }

        private void tb_GesamtBetrag_Validating(object sender, CancelEventArgs e)
        {
            if (tb_Einzelpreis != null && tb_Menge.Text != "")
            {
                CalculateAmount();
            }
        }
    }
}
