using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Data.SqlTypes;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using Gartenausgaben.Datenbank;
using Gartenausgaben.Datenbank.GartenProjekteDataSetTableAdapters;

namespace Gartenausgaben
{
    public partial class Invoice : Form
    {
        readonly string conn = Properties.Settings.Default.GartenDB; // Connection String aus der App.config
        //string conn = Properties.Settings.Default.GartenProjekteConnectionString; // Alter Connection String aus der App.config --> obsolete?
        DataTable einkauf;
        bool listeSort = true;
        int positionDataGridView = 1;
        public decimal EinzelPreis { get; set; }
        public int Menge { get; set; }
        public decimal GesamtBetrag { get; set; }
        public int ArtikelID { get; set; }
        public int ProjektID { get; set; }
        public int HaendlerID { get; set; }
        public int ArtikelPreisID { get; set; }
        public int ArtikelHaendlerID { get; set; }
        public int EinkaufID { get; set; }
        public int EinkaufPositionID { get; set; }

        public Invoice()
        {
            InitializeComponent();
            SetMyCustomFormat();
            LadeStartDaten();
            ErstelleDataTable();
            SetDataGrid_Tabelle();
            BtnNewItem.Enabled = false;
            
        }
        
        public void ErstelleDataTable()
        {
            DataSet datasetEinkauf = new DataSet();
            einkauf = new DataTable();

            einkauf.Columns.Add("Haendler", typeof(String));
            einkauf.Columns.Add("Datum", typeof(DateTime));
            einkauf.Columns.Add("Artikel", typeof(String));
            einkauf.Columns.Add("Einzelpreis", typeof(Decimal));
            einkauf.Columns.Add("Menge", typeof(Int32));
            einkauf.Columns.Add("Projekt", typeof(String));

            datasetEinkauf.Tables.Add(einkauf);
        }        

        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePickerDatum.Value = DateTime.Now;
            dateTimePickerDatum.Format = DateTimePickerFormat.Custom;
            dateTimePickerDatum.CustomFormat = "dd. MMMM yyyy"; //MMMM dd, yyyy";
        }

        private void CalculateAmount()
        {
            GesamtBetrag = numericUpDown_Einzelpreis.Value * numericUpDown_Menge.Value;
            tb_GesamtBetrag.Text = GesamtBetrag.ToString("0.00");
        }

        /// <summary>
        /// Lädt beim Start alle verfügbaren Händler und Artikel. Sortierung ASC
        /// </summary>
        public void LadeStartDaten()
        {
            // Connection String aus der App.config 
            //string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(conn))
            {

                //Abfrage-String für alle Namen aus der Händler Tabelle
                string querySql_Haendler = "SELECT Name, Ort FROM Haendler ORDER BY Name ASC";
                string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY Artikelbezeichnung ASC";
                string querySql_Projekt = "SELECT Projektname FROM Projekt ORDER BY Projektname ASC";

                try
                {
                    sql_con.Open();
                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt_Haendler = new SqlDataAdapter(querySql_Haendler, sql_con);
                    SqlDataAdapter sql_adapt_Artikel = new SqlDataAdapter(querySql_Artikel, sql_con);
                    SqlDataAdapter sql_adapt_Projekt = new SqlDataAdapter(querySql_Projekt, sql_con);

                    //Händlerliste laden
                    //Erstellt eine neue Tabelle im Arbeitsspeicher
                    DataTable tblData_Haendler = new DataTable();
                    //Befüllt die DataTable
                    sql_adapt_Haendler.Fill(tblData_Haendler);

                    //Anzeige in der ComboBox alle Namen der vorhanden Ids
                    tblData_Haendler.Columns.Add("NameOrt", typeof(string), "Name + ', ' + Ort");

                    cb_Haendler.DisplayMember = "NameOrt";
                    cb_Haendler.ValueMember = "[Haendler_ID]";

                    //Zuweisen der Datentabelle zur Datenquelle
                    cb_Haendler.DataSource = tblData_Haendler;

                    //Artikelliste laden
                    DataTable tblData_Artikel = new DataTable();
                    sql_adapt_Artikel.Fill(tblData_Artikel);

                    cb_Artikel.DisplayMember = "Artikelbezeichnung";
                    cb_Artikel.ValueMember = "[Artikel_ID]";
                    cb_Artikel.DataSource = tblData_Artikel;

                    //nochmaliges zuweisen, da Artikel am Anfang noch nicht geladen wurden und der Einzelpreis sonst leer bleibt
                    cb_Haendler.DataSource = tblData_Haendler;


                    //Projektdaten laden
                    DataTable tblData_Projekt = new DataTable();
                    sql_adapt_Projekt.Fill(tblData_Projekt);

                    cb_Projekt.DisplayMember = "Projektname";
                    cb_Projekt.ValueMember = "[Projekt_ID]";
                    cb_Projekt.DataSource = tblData_Projekt;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Achtung: " + ex.Message);
                }
                sql_con.Close();
            }
            lbl_Haendler.Text = cb_Haendler.Text;
            lbl_Datum.Text = dateTimePickerDatum.Value.ToString("dd. MMMM yyyy");
        }

        /// <summary>
        /// Lädt nach einem Update die neue Händlerliste. Hinzugefügter Händler steht oben
        /// </summary>
        /// <param name="sort"></param>
        public void LadeHaendler(string sortierung)
        {
            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql = "SELECT Name FROM Haendler ORDER BY " + sortierung + " DESC";

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(conn))
            {
                //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                SqlDataAdapter sql_adapt = new SqlDataAdapter(querySql, sql_con);

                //Erstellt eine neue Tabelle im Arbeitsspeicher
                DataTable tblData = new DataTable();
                //Befüllt die DataTable
                sql_adapt.Fill(tblData);

                //Anzeige in der ComboBox alle Namen der vorhanden Ids
                cb_Haendler.DisplayMember = "Name";
                cb_Haendler.ValueMember = "[Haendler_ID]";

                //Zuweisen der Datentabelle zur Datenquelle
                cb_Haendler.DataSource = tblData;

                sql_con.Close();
            }
        }

        /// <summary>
        /// Sortierung nach ID Absteigend
        /// </summary>
        /// <param name="sort"></param>
        public void LadeArtikelNeu(string sort, string orderBy)
        {
            // Connection String aus der App.config 
            //string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY " + sort + " " + orderBy;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(conn))
            {
                try
                {
                    sql_con.Open();
                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt_Artikel = new SqlDataAdapter(querySql_Artikel, sql_con);

                    DataTable tblData_Artikel = new DataTable();


                    sql_adapt_Artikel.Fill(tblData_Artikel);

                    cb_Artikel.DisplayMember = "Artikelbezeichnung";
                    cb_Artikel.ValueMember = "[Artikel_ID]";
                    cb_Artikel.DataSource = tblData_Artikel;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_con.Close();
            }
        }
        public void LadeProjektNeu(string sort, string orderBy)
        {
            // Connection String aus der App.config 
            //string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            string querySql_Projekt = "SELECT Projektname FROM Projekt ORDER BY " + sort + " " + orderBy;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(conn))
            {
                try
                {
                    sql_con.Open();
                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt_Projekt = new SqlDataAdapter(querySql_Projekt, sql_con);

                    DataTable tblData_Projekt = new DataTable();


                    sql_adapt_Projekt.Fill(tblData_Projekt);

                    cb_Projekt.DisplayMember = "Projektname";
                    cb_Projekt.ValueMember = "[Projekt_ID]";
                    cb_Projekt.DataSource = tblData_Projekt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_con.Close();
            }
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            LadeStartDaten();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //Erstelle DataTable
            var dgvEinkauf = new DataTable();
            var dbEinkaufposition = new DataTable();
            var dbArtikel = new DataTable();
            var dbHaendler = new DataTable();
            var dbArtikelHaendler = new DataTable();
            var dbArtikelPreis = new DataTable();
            var dbProjekt = new DataTable();
            var dbEinkauf = new DataTable();

            //Hinzufügen der Spalten
            foreach (DataGridViewColumn column in dataGridView_Einkauf.Columns)
            {
                dgvEinkauf.Columns.Add(column.HeaderText, column.ValueType);
            }

            //Hinzufügen der Zeilen
            foreach (DataGridViewRow row in dataGridView_Einkauf.Rows)
            {
                dgvEinkauf.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgvEinkauf.Rows[dgvEinkauf.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
            }

            var dataset = new DataSet();
            dataset.Tables.Add(dgvEinkauf);

            //Holen der Daten aus der Datenbank
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                string querySql1 = "SELECT * FROM Einkaufpositionen";
                string querySql2 = "SELECT * FROM Artikel";
                string querySql3 = "SELECT * FROM Haendler";
                string querySql4 = "SELECT * FROM Artikel_Haendler";
                string querySql5 = "SELECT * FROM Artikel_Preis";
                string querySql6 = "SELECT * FROM Projekt";
                string querySql7 = "SELECT * FROM Einkauf";

                try
                {
                    sql_conn.Open();
                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt1 = new SqlDataAdapter(querySql1, sql_conn);
                    SqlDataAdapter sql_adapt2 = new SqlDataAdapter(querySql2, sql_conn);
                    SqlDataAdapter sql_adapt3 = new SqlDataAdapter(querySql3, sql_conn);
                    SqlDataAdapter sql_adapt4 = new SqlDataAdapter(querySql4, sql_conn);
                    SqlDataAdapter sql_adapt5 = new SqlDataAdapter(querySql5, sql_conn);
                    SqlDataAdapter sql_adapt6 = new SqlDataAdapter(querySql6, sql_conn);
                    SqlDataAdapter sql_adapt7 = new SqlDataAdapter(querySql7, sql_conn);

                    sql_adapt1.Fill(dbEinkaufposition);
                    sql_adapt2.Fill(dbArtikel);
                    sql_adapt3.Fill(dbHaendler);
                    sql_adapt4.Fill(dbArtikelHaendler);
                    sql_adapt5.Fill(dbArtikelPreis);
                    sql_adapt6.Fill(dbProjekt);
                    sql_adapt7.Fill(dbEinkauf);

                    dataset.Tables.Add(dbEinkaufposition);
                    dataset.Tables.Add(dbArtikel);
                    dataset.Tables.Add(dbHaendler);
                    dataset.Tables.Add(dbArtikelHaendler);
                    dataset.Tables.Add(dbArtikelPreis);
                    dataset.Tables.Add(dbProjekt);
                    dataset.Tables.Add(dbEinkauf);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Achtung: " + ex.Message);
                }
                sql_conn.Close();
            }

            SetNewEinkaufId();

            //Übertragen der einzelnen Positionen in die Datenbank
            foreach (DataGridViewRow row in dataGridView_Einkauf.Rows)
            {
                SetEinzelpreisUndMenge(row.Index);

                ArtikelID = GetId("Artikel", "Artikelbezeichnung", "Artikel", row.Index);
                ProjektID = GetId("Projekt", "Projektname", "Projekt", row.Index);
                ArtikelHaendlerID = GetId("Artikel_Haendler", ArtikelID, HaendlerID);

                if (ArtikelHaendlerID == 0)
                    InsertArtikelHaendler();

                ArtikelPreisID = GetId("Artikel_Preis", ArtikelHaendlerID, EinzelPreis);

                if (ArtikelPreisID == 0)
                    InsertArtikelPreis();

                SetEinkaufsposition();
            }
        }

        private int SetEinkaufsposition()
        {
            string sql_Insert = "INSERT INTO Einkaufpositionen (Menge, Projekt_ID, Artikel_ID, Einkauf_ID, Preis_ID) " + "VALUES (@Menge,  @ProjektId, @ArtikelId, @EinkaufId, @PreisId); "
                + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@ArtikelId", ArtikelID);
                command.Parameters.AddWithValue("@ProjektId", ProjektID);
                command.Parameters.AddWithValue("@EinkaufId", EinkaufID);
                command.Parameters.AddWithValue("@PreisId", ArtikelPreisID);
                command.Parameters.AddWithValue("@Menge", Menge);
                try
                {
                    sql_conn.Open();
                    EinkaufPositionID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                    return EinkaufPositionID;
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen in der Tabelle \"Einkaufspositionen\" aufgetreten", "Achtung", MessageBoxButtons.OK);
                    return 0;
                }
            }
            
        }

        private int SetNewEinkaufId()
        {
            HaendlerID = GetId("Haendler", "Name", lbl_Haendler.Text.Remove(lbl_Haendler.Text.IndexOf(",")));
            string sql_Insert = "INSERT INTO Einkauf (Datum, Haendler_ID)" + "VALUES (@Datum, @HaendlerID); " + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@Datum", dateTimePickerDatum.Value.Date);
                command.Parameters.AddWithValue("@HaendlerID", HaendlerID);
                try
                {
                    sql_conn.Open();
                    EinkaufID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen einer neuen Einkauf_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
            return EinkaufID;
        }

        /// <summary>
        /// Funktion zum Abrufen und Setzen der jeweiligen Primärschlüssel aus der Datenbank <br></br>
        /// GetId(string DbTable,string DbColumnName, string DgvColumnName, int DgvRowIndex)
        /// </summary>
        /// <param name="list"></param>
        /// <paramref>GetId(DbTable, DbColumnName, DgvColumnName, DgvRowIndex)</paramref>
        /// <returns>ID</returns>
        private int GetId(params object[] list)
        {
            int id = 0;
            char[] charToTrim = { ',', ' ' };
            string ort = lbl_Haendler.Text.Substring(lbl_Haendler.Text.IndexOf(',')).TrimStart(charToTrim);

            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                string sql_Select = "SELECT * FROM " + list[0];
                string sql_Select_Haendler = "SELECT * FROM " + list[0] + " WHERE Ort = @Ort";
                string sql_Select_ArtikelHaendler = "SELECT * FROM " + list[0] + " WHERE Artikel_ID = @Artikel_ID AND Haendler_ID = @Haendler_ID";
                string sql_Select_ArtikelPreis = "SELECT * FROM " + list[0] + " WHERE ArtikelHaendler_ID = @ArtikelHaendler_ID AND Artikelpreis = @Artikelpreis";

                SqlDataAdapter adapter = new SqlDataAdapter(sql_Select, sql_conn);
                SqlDataAdapter adapterHaendler = new SqlDataAdapter(sql_Select_Haendler, sql_conn);
                SqlDataAdapter adapterArtikelHaendler = new SqlDataAdapter(sql_Select_ArtikelHaendler, sql_conn);
                SqlDataAdapter adapterArtikelPreis = new SqlDataAdapter(sql_Select_ArtikelPreis, sql_conn);

                adapterHaendler.SelectCommand.Parameters.AddWithValue("@Ort", ort);
                adapterArtikelHaendler.SelectCommand.Parameters.AddWithValue("@Artikel_ID", ArtikelID);
                adapterArtikelHaendler.SelectCommand.Parameters.AddWithValue("@Haendler_ID", HaendlerID);
                adapterArtikelPreis.SelectCommand.Parameters.AddWithValue("@ArtikelHaendler_ID", ArtikelHaendlerID);
                adapterArtikelPreis.SelectCommand.Parameters.AddWithValue("@Artikelpreis", EinzelPreis);

                DataTable dt = new DataTable();
                sql_conn.Open();

                /// <value>Gibt die Artikel ID oder Projekt ID zuück</value>
                if (list[0].ToString() == "Artikel" || list[0].ToString() == "Projekt")
                {
                    adapter.Fill(dt);
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName == list[1].ToString())
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                for (int j = 0; j < row.ItemArray.Length; j++)
                                {
                                    if (row.ItemArray[j].ToString() == dataGridView_Einkauf.Rows[(int)list[3]].Cells[list[2].ToString()].Value.ToString())
                                    {
                                        id = (int)row.ItemArray[0];
                                        return id;
                                    }
                                } 
                            } 
                        } 
                    } 
                }

                /// <value>Gibt die Händler ID zuück</value> 
                else if (list[0].ToString() == "Haendler")
                {
                    adapterHaendler.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        for (int j = 0; j < row.ItemArray.Length; j++)
                        {
                            if (row.ItemArray[j].ToString() == list[2].ToString())
                            {
                                id = (int)row.ItemArray[0];
                                return id;
                            }
                        }
                    }
                }

                /// <value>Gibt die ArtikelHaendlerID zuück</value> 
                else if (list[0].ToString() == "Artikel_Haendler")
                {
                    adapterArtikelHaendler.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.ItemArray[1].ToString() == list[1].ToString() && row.ItemArray[2].ToString() == list[2].ToString())
                        {
                            id = (int)row.ItemArray[0];
                            return id;
                        }
                        else
                            id = 0;
                    }
                }

                /// <value>Gibt die ArtikelPreisID zuück</value>
                else if (list[0].ToString() == "Artikel_Preis")
                {
                    adapterArtikelPreis.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row.ItemArray[1].ToString() == list[1].ToString() && row.ItemArray[2].ToString() == list[2].ToString())
                        {
                            id = (int)row.ItemArray[0];
                            return id;
                        }
                        else
                            id = 0;
                    }
                }

                sql_conn.Close();
            }
                return id;
        }

        private void SetEinzelpreisUndMenge(int row)
        {
            for (int i = 0; i < dataGridView_Einkauf.Columns.Count; i++) 
            {
                if (dataGridView_Einkauf.Columns[i].Name == "Einzelpreis" || dataGridView_Einkauf.Columns[i].Name == "Menge") 
                {
                    for (int j = 0; j < dataGridView_Einkauf.RowCount; j++) 
                    {
                        if (j == row && dataGridView_Einkauf.Columns[i].Name == "Einzelpreis")
                        {
                            EinzelPreis = Convert.ToDecimal(dataGridView_Einkauf.Rows[j].Cells[i].Value);
                        }
                        else if (j == row && dataGridView_Einkauf.Columns[i].Name == "Menge")
                        {
                            Menge = Convert.ToInt32(dataGridView_Einkauf.Rows[j].Cells[i].Value);
                        }
                    }
                } 
            } 
        }

        private int InsertArtikelHaendler()
        {
            string sql_Insert = "INSERT INTO Artikel_Haendler (Artikel_ID, Haendler_ID) " + "VALUES (@Artikel_ID, @Haendler_ID); " + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@Artikel_ID", ArtikelID);
                command.Parameters.AddWithValue("@Haendler_ID", HaendlerID);
                try
                {
                    sql_conn.Open();
                    ArtikelHaendlerID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen der Artikel_Händler_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
            return ArtikelHaendlerID;
        }

        private void InsertArtikelPreis()
        {
            string sql_Insert = "INSERT INTO Artikel_Preis (ArtikelHaendler_ID, Artikelpreis, Datum) " + "VALUES (@ArtikelHaendler_ID, @Artikelpreis, @Datum); " + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@ArtikelHaendler_ID", ArtikelHaendlerID);
                command.Parameters.AddWithValue("@Artikelpreis", EinzelPreis);
                command.Parameters.AddWithValue("@Datum", dateTimePickerDatum.Value.Date);
                try
                {
                    sql_conn.Open();
                    ArtikelPreisID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen der Artikel_Preis_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnNeuerHaendler_Click(object sender, EventArgs e)
        {
            var neuerHaendler = new NeuerHaendler();
            neuerHaendler.FormClosed += new FormClosedEventHandler(F2_FormClosed);
            neuerHaendler.Show();
        }

        void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.LadeHaendler("Haendler_Id");
        }

        private void BtnEintragen_Click(object sender, EventArgs e)
        {
            if(numericUpDown_Einzelpreis.Value != 0 && numericUpDown_Menge.Value != 0 && cb_Artikel.Text != "")
            {
                DataRow row = einkauf.NewRow();
                row["Haendler"] = cb_Haendler.Text;
                row["Datum"] = dateTimePickerDatum.Value;
                row["Artikel"] = cb_Artikel.Text;
                row["Menge"] = numericUpDown_Menge.Value;
                row["Einzelpreis"] = numericUpDown_Einzelpreis.Value;
                row["Projekt"] = cb_Projekt.Text;

                lbl_Datum.Text = dateTimePickerDatum.Value.ToString("dd. MMMM yyyy");
                lbl_Haendler.Text = cb_Haendler.Text;

                dataGridView_Einkauf.Rows.Add(positionDataGridView, numericUpDown_Menge.Value, cb_Artikel.Text, numericUpDown_Einzelpreis.Value, tb_GesamtBetrag.Text, cb_Projekt.Text);

                //Summe aller Gesamtpreise - Zur Kontrolle des Kassenbons
                var count = dataGridView_Einkauf.Rows.Count;
                decimal x = 0.00m;

                if (count > 1)
                {
                    for (int i = 0; i < count; i++)
                    {
                        try
                        {
                            x += Convert.ToDecimal(dataGridView_Einkauf.Rows[i].Cells["Gesamtpreis"].Value.ToString());
                        }
                        catch
                        {
                            MessageBox.Show("Bitte überprüfen Sie Ihre Eingaben. Die Felder Menge und Einzelpreis müssen ausgefüllt sein", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    x = Convert.ToDecimal(tb_GesamtBetrag.Text);

                lbl_SummeBetrag.Text = x.ToString() + " €";

                positionDataGridView++;

                cb_Artikel.Text = "";
                numericUpDown_Einzelpreis.Value = 0;
                tb_GesamtBetrag.Clear();
                numericUpDown_Menge.Value = 0;
            }
            else
            {
                MessageBox.Show("Bitte überprüfen Sie Ihre Eingaben. Die Felder Menge, Einzelpreis und Artikel müssen ausgefüllt sein", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!listeSort)
            {
                LadeArtikelNeu("Artikelbezeichnung", "ASC");
                listeSort = true;
            }
                
        }

        private void SetDataGrid_Tabelle()
        {
            dataGridView_Einkauf.ColumnCount = 6;

            dataGridView_Einkauf.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView_Einkauf.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_Einkauf.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView_Einkauf.Font, FontStyle.Bold);

            dataGridView_Einkauf.Name = "dataGridView_Einkauf";
            dataGridView_Einkauf.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView_Einkauf.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_Einkauf.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Einkauf.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_Einkauf.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_Einkauf.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_Einkauf.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_Einkauf.Columns[3].DefaultCellStyle.Format = "N2";
            dataGridView_Einkauf.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView_Einkauf.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_Einkauf.Columns[2].Width = 200;
            dataGridView_Einkauf.Columns[5].Width = 120;


            dataGridView_Einkauf.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView_Einkauf.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView_Einkauf.GridColor = Color.Black;
            dataGridView_Einkauf.RowHeadersVisible = false;

            dataGridView_Einkauf.Columns[0].Name = "Position";
            dataGridView_Einkauf.Columns[0].ValueType = typeof(Int16);
            dataGridView_Einkauf.Columns[2].Name = "Artikel";
            dataGridView_Einkauf.Columns[2].ValueType = typeof(string);
            dataGridView_Einkauf.Columns[1].Name = "Menge";
            dataGridView_Einkauf.Columns[1].ValueType = typeof(Int16);
            dataGridView_Einkauf.Columns[3].Name = "Einzelpreis";
            dataGridView_Einkauf.Columns[3].ValueType = typeof(decimal);
            dataGridView_Einkauf.Columns[4].Name = "Gesamtpreis";
            dataGridView_Einkauf.Columns[4].ValueType = typeof(decimal);
            dataGridView_Einkauf.Columns[5].Name = "Projekt";
            dataGridView_Einkauf.Columns[5].ValueType = typeof(string);
        }

        private void BtnNeuerArtikel_Click(object sender, EventArgs e)
        {
            if (!DbConnect.EqualsArtikel(tb_NeuerArtikel.Text))
                DbConnect.AddNeuerArtikel(tb_NeuerArtikel.Text, conn);
            LadeArtikelNeu("Artikel_ID", "DESC");
            listeSort = false;
            tb_NeuerArtikel.Clear();
        }

        private void LadeEinzelpreis()
        {
            
            //var haendler = cb_Haendler.Text;
            char[] charToTrim = { ',', ' ' };
            var ort = cb_Haendler.Text.Substring(cb_Haendler.Text.IndexOf(',')).TrimStart(charToTrim);
            var haendler = cb_Haendler.Text;
 
            string[] subs = haendler.Split(',');
            haendler = subs[0];

            var artikel = cb_Artikel.Text;

            if (cb_Haendler.Text != "" && cb_Artikel.Text != "")
            {
                //string conn = Properties.Settings.Default.GartenProjekteConnectionString;

                using (SqlConnection sql_conn = new SqlConnection(conn))
                {
                    string artikelPreis = "SELECT Artikelpreis FROM Artikel_Preis " +
                            "JOIN Artikel_Haendler ON Artikel_Haendler.ArtikelHaendler_ID = Artikel_Preis.ArtikelHaendler_ID " +
                            "JOIN Artikel ON Artikel.Artikel_ID = Artikel_Haendler.Artikel_ID " +
                            "JOIN Haendler ON Haendler.Haendler_ID = Artikel_Haendler.Haendler_ID " +
                            "WHERE Haendler.Name = @Name AND Haendler.Ort = @Ort AND Artikel.Artikelbezeichnung = @Artikelbezeichnung " +
                            "ORDER BY Datum DESC";

                    SqlCommand sql_command = new SqlCommand(artikelPreis, sql_conn);
                    // Setzten der Paramter für WHERE
                    sql_command.Parameters.AddWithValue("@Name", haendler);
                    sql_command.Parameters.AddWithValue("@Ort", ort);
                    sql_command.Parameters.AddWithValue("@Artikelbezeichnung", artikel);

                    try
                    {
                        sql_conn.Open();
                        if (sql_command.ExecuteScalar() != null)
                            numericUpDown_Einzelpreis.Value = Convert.ToDecimal(sql_command.ExecuteScalar());
                        else
                            numericUpDown_Einzelpreis.Value = 0;
                        sql_conn.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Es ist ein Fehler, beim Eintragen in der Tabelle \"Einzelpreis\" aufgetreten", "Achtung", MessageBoxButtons.OK);
                    }
                }
            }
            if (numericUpDown_Menge.Value > 0)
            {
                CalculateAmount();
            }
        }

        private void Cb_Artikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LadeEinzelpreis();
        }

        private void Cb_Haendler_SelectedIndexChanged(object sender, EventArgs e)
        {
            LadeEinzelpreis();
        }

        private void NumericUpDown_Menge_ValueChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void NumericUpDown_Einzelpreis_ValueChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void BtnDgvDelete_Click(object sender, EventArgs e)
        {
            int count = dataGridView_Einkauf.Rows.Count;
            dataGridView_Einkauf.Rows.RemoveAt(count-1);
        }

        private void DataGridView_Einkauf_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_Einkauf.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DataGridView_Einkauf_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) { 
        
            var row = 0;
            var count = dataGridView_Einkauf.Rows.Count;
            for (int i = 1; i <= count; i++)
            {
                this.dataGridView_Einkauf.Rows[row].Cells["Position"].Value = i;
                row++;
                
            }
            positionDataGridView = count+1;
        }

        private void BtnNeuesProjekt_Click(object sender, EventArgs e)
        {
            AddNewProjectControl();
        }

        public Button okNewProject = new Button();
        public TextBox tbNewProject = new TextBox();

        public void AddNewProjectControl()
        {
            okNewProject = new Button();
            tbNewProject = new TextBox{
                Location = new Point(40, 470),
                Size = new Size(224, 24)
            };
            Controls.Add(tbNewProject);
            tbNewProject.Focus();

            Invoice.ActiveForm.Controls.Add(okNewProject);
            this.okNewProject.Click += new System.EventHandler(this.BtnOkNewProject_Click);
            okNewProject.Size = new Size(30, tbNewProject.ClientSize.Height + 2);
            okNewProject.Dock = DockStyle.Right;
            okNewProject.Cursor = Cursors.Default;
            okNewProject.FlatStyle = FlatStyle.Flat;
            okNewProject.ForeColor = Color.Black;
            okNewProject.FlatAppearance.BorderSize = 0;
            okNewProject.Font = new Font(okNewProject.Font.FontFamily, 6);
            okNewProject.Text = "OK";
            tbNewProject.Controls.Add(okNewProject);
            //this.AcceptButton = okNewProject;
        }

        private void RemoveNewProjectControl()
        {
            if (Invoice.ActiveForm.Controls.Contains(tbNewProject))
            {
                this.okNewProject.Click -= new System.EventHandler(this.BtnOkNewProject_Click);
                Invoice.ActiveForm.Controls.Remove(tbNewProject);
                Invoice.ActiveForm.Controls.Remove(okNewProject);
                tbNewProject.Dispose();
                okNewProject.Dispose();
            }
        }

        private void BtnOkNewProject_Click(object sender, EventArgs e)
        {
            if(tbNewProject.Text == "")
                RemoveNewProjectControl();

            if (!DbConnect.EqualsArtikel(tbNewProject.Text))
                DbConnect.AddNewProject(tbNewProject.Text, conn);
            else
                MessageBox.Show("Das Projekt existiert schon","Hinweis", MessageBoxButtons.OK);
            
            LadeProjektNeu("Projekt_ID", "DESC");
            //listeSort = false;

            RemoveNewProjectControl();
        }

        private void Tb_NeuerArtikel_TextChanged(object sender, EventArgs e)
        {
            var artikel = tb_NeuerArtikel.Text.Trim();

            if (artikel != "")
                BtnNewItem.Enabled = true;
            else
            {
                tb_NeuerArtikel.Text = artikel.Trim();
                BtnNewItem.Enabled = false;
            }
        }
    }
}
