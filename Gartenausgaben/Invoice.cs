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
using System.Threading;

namespace Gartenausgaben
{
    public partial class Invoice : Form
    {
        List<string> artikel = new List<string>();
        List<string> projekt = new List<string>();
        List<string> haendler = new List<string>();
        List<int> menge = new List<int>();
        List<decimal> einzelpreis = new List<decimal>();
        List<decimal> gesamtpreis = new List<decimal>();
        List<DateTime> datum = new List<DateTime>();
        decimal einzelPreis;
        decimal artikelMenge;
        decimal gesamtBetrag;
        int position;
        string conn = Properties.Settings.Default.GartenProjekteConnectionString; // Connection String aus der App.config 

        public Invoice()
        {
            InitializeComponent();
            SetMyCustomFormat();
            position = 1;
            LadeStartDaten();
            
        }
        
        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePickerDatum.Value = DateTime.Now;
            dateTimePickerDatum.Format = DateTimePickerFormat.Custom;
            dateTimePickerDatum.CustomFormat = "ddMMMM yyyy"; //MMMM dd, yyyy";
        }

        public decimal GetEinzelPreis
        {
            get 
            { 
                return einzelPreis; 
            }
            set 
            {
                this.einzelPreis = Convert.ToDecimal(tb_Einzelpreis.Text);
                einzelPreis = value; 
            }
        }

        public decimal GetMenge
        {
            get
            {
                return artikelMenge;
            }
            set
            {
                artikelMenge = Convert.ToDecimal(tb_Menge.Text);

            }
        }

        public decimal GetGesamtBetrag
        {
            get
            {
                return gesamtBetrag;
            }
            set
            {
                this.gesamtBetrag = Convert.ToDecimal(tb_GesamtBetrag.Text);
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
                gesamtBetrag = Decimal.Parse(tb_Menge.Text) * Decimal.Parse(tb_Einzelpreis.Text);
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

        /// <summary>
        /// Lädt beim Start alle verfügbaren Händler und Artikel. Sortierung ASC
        /// </summary>
        public void LadeStartDaten()
        {
            // Connection String aus der App.config 
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql_Haendler = "SELECT Name FROM Haendler ORDER BY Name ASC";
            string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY Artikelbezeichnung ASC";

            //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
            SqlDataAdapter sql_adapt_Haendler = new SqlDataAdapter(querySql_Haendler, sql_con);
            SqlDataAdapter sql_adapt_Artikel = new SqlDataAdapter(querySql_Artikel, sql_con);


            //Erstellt eine neue Tabelle im Arbeitsspeicher
            DataTable tblData_Haendler = new DataTable();
            //Befüllt die DataTable
            sql_adapt_Haendler.Fill(tblData_Haendler);

            //Anzeige in der ComboBox alle Namen der vorhanden Ids
            cb_Haendler.DisplayMember = "Name";
            cb_Haendler.ValueMember = "[Haendler_Id]";

            //Zuweisen der Datentabelle zur Datenquelle
            cb_Haendler.DataSource = tblData_Haendler;


            DataTable tblData_Artikel = new DataTable();
            sql_adapt_Artikel.Fill(tblData_Artikel);

            cb_Artikel.DisplayMember = "Artikelbezeichnung";
            cb_Artikel.ValueMember = "[Artikel_Id]";
            cb_Artikel.DataSource = tblData_Artikel;

        }

        /// <summary>
        /// Lädt nach einem Update die neue Händlerliste. Hinzugefügter Händler steht oben
        /// </summary>
        /// <param name="sort"></param>
        public void LadeHaendler(string sortierung)
        {
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(this.conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql = "SELECT Name FROM Haendler ORDER BY " + sortierung + " DESC";

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
        public void LadeArtikelNeu(string sort)
        {
            // Connection String aus der App.config 
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY " + sort + " DESC";

            //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
            SqlDataAdapter sql_adapt_Artikel = new SqlDataAdapter(querySql_Artikel, sql_con);

            DataTable tblData_Artikel = new DataTable();
            sql_adapt_Artikel.Fill(tblData_Artikel);

            cb_Artikel.DisplayMember = "Artikelbezeichnung";
            cb_Artikel.ValueMember = "[Artikel_Id]";
            cb_Artikel.DataSource = tblData_Artikel;
        }

        //private DataTable Db_Get_Table(string sqlInsert)
        //{
        //    //Anzeigen von DAten
        //    //< init >
        //    //via app-Setting
        //    string conn = Properties.Settings.Default.GartenProjekteConnectionString;
        //    //</ init >

        //    SqlConnection sql_conn = new SqlConnection(conn);
        //    if (sql_conn.State != ConnectionState.Open)
        //        sql_conn.Open();
        //    SqlDataAdapter sql_adapt = new SqlDataAdapter(sqlInsert, sql_conn);

        //    DataTable tblData = new DataTable();
        //    sql_adapt.Fill(tblData);
        //    sql_conn.Close();

        //    return tblData;
        //}

        //private int Db_execute(string sql_Insert)
        //{
        //    string conn = Properties.Settings.Default.GartenProjekteConnectionString;

        //    SqlConnection sql_conn = new SqlConnection(conn);
        //    if (sql_conn.State != ConnectionState.Open) sql_conn.Open();
        //    SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

        //    int nResult = sql_com.ExecuteNonQuery();
        //    sql_conn.Close();
        //    return nResult;
        //}

        private void Invoice_Load(object sender, EventArgs e)
        {
            LadeStartDaten();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            position = 0;
            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNeuerHaendler_Click(object sender, EventArgs e)
        {
            var neuerHaendler = new NeuerHaendler();
            neuerHaendler.FormClosed += new FormClosedEventHandler(f2_FormClosed);
            neuerHaendler.Show();
        }

        void f2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.LadeHaendler("Haendler_Id");
        }

        private void SetNeuerEinkauf()
        {
            string artikel = cb_Artikel.Text;
            string haendler = cb_Haendler.Text;
            string einzelpreis = tb_Einzelpreis.Text;
            string menge = tb_Menge.Text;
            string gesamtpreis = tb_GesamtBetrag.Text;

            //Abfrage-String um einen neuen Händler aus der TextBox zur Händler Tabelle hinzuzufügen
            //string sql_Insert = "INSERT INTO Haendler (Name , Anschrift , PLZ , Ort , Telefon ) " +
            //    "VALUES ('" + name + "' , '" + strasse + "' , '" + plz + "' , '" + ort + "' , '" + telefon + "') ";

            // Insert Into EINKAUF
        }

        private void btnEintragen_Click(object sender, EventArgs e)
        {
            List<string[]> liste = new List<string[]>();

            menge.Add(Convert.ToInt32(tb_Menge.Text));
            artikel.Add(cb_Artikel.Text);
            einzelpreis.Add(Convert.ToDecimal(tb_Einzelpreis.Text));
            gesamtpreis.Add(Convert.ToDecimal(tb_GesamtBetrag.Text));
            datum.Add(dateTimePickerDatum.Value);

            lbListe.Items.Add(cb_Artikel.Text + " " + tb_Menge.Text + " " + tb_GesamtBetrag.Text);

            position++;

            cb_Artikel.Text = "";
            tb_Einzelpreis.Clear();
            tb_GesamtBetrag.Clear();
            tb_Menge.Clear();

        }

        private void btnNeuerArtikel_Click(object sender, EventArgs e)
        {
            if (!DbConnect.EqualsArtikel(tb_NeuerArtikel.Text))
                DbConnect.AddNeuerArtikel(tb_NeuerArtikel.Text, conn);
            LadeArtikelNeu("Artikel_Id");
            tb_NeuerArtikel.Clear();
        }
    }
}
