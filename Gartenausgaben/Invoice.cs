using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
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

        //private void CalculateQuantity()
        //{
        //    if (tb_GesamtBetrag.Text != "")
        //    {
        //        try
        //        {
        //            menge = Double.Parse(tb_GesamtBetrag.Text) / Double.Parse(tb_Einzelpreis.Text);
        //            tb_Menge.Text = menge.ToString();
        //        }
        //        catch
        //        {
        //            if (tb_GesamtBetrag.Text == "")
        //                tb_Menge.Text = "";
        //            else
        //            {
        //                tb_Menge.Text = "";
        //                MessageBox.Show("Bitte geben sie eine Zahl ein!");
        //            }
        //        }
        //    }
        //}

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

        //Sortierung nach Name ASC Default
        public void LadeHaendler()
        {
            // Connection String aus der App.config 
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql = "SELECT Name FROM Haendler ORDER BY Name ASC";

            //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
            SqlDataAdapter sql_adapt = new SqlDataAdapter(querySql, sql_con);

            //Erstellt eine neue Tabelle im Arbeitsspeicher
            DataTable tblData = new DataTable();
            //Befüllt die DataTable
            sql_adapt.Fill(tblData);

            //Anzeige in der ComboBox alle Namen der vorhanden Ids
            cb_Haendler.DisplayMember = "Name";
            cb_Haendler.ValueMember = "[Haendler_Id]";

            //Zuweisen der Datentabelle zur Datenquelle
            cb_Haendler.DataSource = tblData;
        }

        //Sortierung nach ID Absteigend
        public void LadeHaendler(string sort)
        {
            // Connection String aus der App.config 
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql = "SELECT Name FROM Haendler ORDER BY " + sort + " DESC";

            //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
            SqlDataAdapter sql_adapt = new SqlDataAdapter(querySql, sql_con);

            //Erstellt eine neue Tabelle im Arbeitsspeicher
            DataTable tblData = new DataTable();
            //Befüllt die DataTable
            sql_adapt.Fill(tblData);

            //Anzeige in der ComboBox alle Namen der vorhanden Ids
            cb_Haendler.DisplayMember = "Name";
            cb_Haendler.ValueMember = "[Haendler_Id]";

            //Zuweisen der Datentabelle zur Datenquelle
            cb_Haendler.DataSource = tblData;
        }

        private DataTable Db_Get_Table(string sqlInsert)
        {
            //Anzeigen von DAten
            //< init >
            //via app-Setting
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;
            //</ init >

            SqlConnection sql_conn = new SqlConnection(conn);
            if (sql_conn.State != ConnectionState.Open)
                sql_conn.Open();
            SqlDataAdapter sql_adapt = new SqlDataAdapter(sqlInsert, sql_conn);

            DataTable tblData = new DataTable();
            sql_adapt.Fill(tblData);
            sql_conn.Close();

            return tblData;
        }

        private int Db_execute(string sql_Insert)
        {
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            SqlConnection sql_conn = new SqlConnection(conn);
            if (sql_conn.State != ConnectionState.Open) sql_conn.Open();
            SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

            int nResult = sql_com.ExecuteNonQuery();
            sql_conn.Close();
            return nResult;
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            LadeHaendler();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //NeuenHaendlerHinzufügen();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNeuerHaendler_Click(object sender, EventArgs e)
        {
            var neuerHaendler = new NeuerHaendlerPlus();
            neuerHaendler.FormClosed += new FormClosedEventHandler(f2_FormClosed);
            neuerHaendler.Show();
        }

        void f2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.LadeHaendler("Haendler_Id");
        }
    }
}
