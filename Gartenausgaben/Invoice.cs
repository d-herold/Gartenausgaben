﻿using System;
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
using System.Text.RegularExpressions;
using Gartenausgaben.Datenbank;
using Gartenausgaben.Datenbank.GartenProjekteDataSetTableAdapters;

namespace Gartenausgaben
{
    public partial class Invoice : Form
    {
        decimal einzelPreis;
        decimal artikelMenge;
        decimal gesamtBetrag;
        string conn = Properties.Settings.Default.GartenProjekteConnectionString; // Connection String aus der App.config
        DataTable einkauf;
        DataSet datasetEinkauf = new DataSet();
        DataColumn column;
        //DataRow row;
        int positionDataGridView = 1;
        decimal x = 0.00m;
        string se;


        public Invoice()
        {
            InitializeComponent();
            SetMyCustomFormat();
            LadeStartDaten();
            ErstelleDataTable();
            SetDataGrid_Tabelle();
        }
        
        public void ErstelleDataTable()
        {
            einkauf = new DataTable();

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Haendler";
            einkauf.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "Datum";
            einkauf.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Artikel";
            einkauf.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Einzelpreis";
            einkauf.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Menge";
            einkauf.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Projekt";
            einkauf.Columns.Add(column);

            datasetEinkauf.Tables.Add(einkauf);
        }        

        public void SetMyCustomFormat()
        {
            // Set the Format type and the CustomFormat string.
            dateTimePickerDatum.Value = DateTime.Now;
            dateTimePickerDatum.Format = DateTimePickerFormat.Custom;
            dateTimePickerDatum.CustomFormat = "dd. MMMM yyyy"; //MMMM dd, yyyy";
        }

        public decimal GetEinzelPreis
        {
            get 
            { 
                return einzelPreis; 
            }
            set 
            {
                this.einzelPreis = numericUpDown_Einzelpreis.Value;
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
                artikelMenge = numericUpDown_Menge.Value;

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

        private void CalculateAmount()
        {
            gesamtBetrag = numericUpDown_Einzelpreis.Value * numericUpDown_Menge.Value;

            tb_GesamtBetrag.Text = gesamtBetrag.ToString("0.00");

        }

        /// <summary>
        /// Lädt beim Start alle verfügbaren Händler und Artikel. Sortierung ASC
        /// </summary>
        public void LadeStartDaten()
        {
            // Connection String aus der App.config 
            //string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql_Haendler = "SELECT Name, Ort FROM Haendler ORDER BY Name ASC";
            string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY Artikelbezeichnung ASC";
            string querySql_Projekt = "SELECT Projektname FROM Projekt ORDER BY Projektname ASC";

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


            //Projektdaten laden
            DataTable tblData_Projekt = new DataTable();
            sql_adapt_Projekt.Fill(tblData_Projekt);

            cb_Projekt.DisplayMember = "Projektname";
            cb_Projekt.ValueMember = "[Projekt_ID]";
            cb_Projekt.DataSource = tblData_Projekt;

            sql_con.Close();
            lbl_Haendler.Text = cb_Haendler.Text;
            lbl_Datum.Text = dateTimePickerDatum.Value.ToString("dd. MMMM yyyy");
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
            cb_Haendler.ValueMember = "[Haendler_ID]";

            //Zuweisen der Datentabelle zur Datenquelle
            cb_Haendler.DataSource = tblData;
        }

        /// <summary>
        /// Sortierung nach ID Absteigend
        /// </summary>
        /// <param name="sort"></param>
        public void LadeArtikelNeu(string sort)
        {
            // Connection String aus der App.config 
            //string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY " + sort + " DESC";

            //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
            SqlDataAdapter sql_adapt_Artikel = new SqlDataAdapter(querySql_Artikel, sql_con);

            DataTable tblData_Artikel = new DataTable();
            sql_adapt_Artikel.Fill(tblData_Artikel);

            cb_Artikel.DisplayMember = "Artikelbezeichnung";
            cb_Artikel.ValueMember = "[Artikel_ID]";
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

            //Löschen der Summenzeile
            var s = dataGridView_Einkauf.Rows.Count;
            if (s > 1)
            {
                dataGridView_Einkauf.Rows.Remove(dataGridView_Einkauf.Rows[s - 1]);
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


            using (SqlConnection sql_conn = new SqlConnection(this.conn))
            {
                string querySql1 = "SELECT * FROM Einkaufpositionen";
                string querySql2 = "SELECT * FROM Artikel";
                string querySql3 = "SELECT * FROM Haendler";
                string querySql4 = "SELECT * FROM Artikel_Haendler";
                string querySql5 = "SELECT * FROM Artikel_Preis";
                string querySql6 = "SELECT * FROM Projekt";
                string querySql7 = "SELECT * FROM Einkauf";

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

                sql_conn.Close();
            }

            /* 1. Hole Einkauf_ID und erhöhe um eins
             * 2. Hole Händler ID
             * 3. Nimm erste Reihe DGV
             * 4. Hole die ID Artikel und Projekt ID
             * 5. Hole PreisID 
             *      5.1 Hat Haendler diesen Artikel schon? Stimmt Preis noch?
             *      5.2 über Abfrage mit Datum Preis == oder != Ist Preis ungleich neue Artikel_HändlerID ---> Preis eintragen mit Datum und ID holen
             * 6. Hole Menge
             * 7. Trage in Tabelle Einkaufposition erste Reihe DGV ein (ID)
             * 8. Nimm nächste Reihe DGV
             * */



            /* Es muss ein Objekt Einkauf gebildet werden, welches aus verschiedenen Unterobjekten besteht,
           für die Unterobjekte werden Werte gebraucht, die zuerst aus der DataGridView geholt werden müssen. 
            Frage, Muss es gemacht werden?????*/


            // Holen der jeweilgen Id aus DB
            var artikelId = GetId("Artikelbezeichnung","Artikel","Artikel", s /* Hier muss der index der DgV Reihe rein*/);
            var projektId = GetId("Projektname","Projekt","Projekt", s);
            var haendlerId = GetId("Name", "Haendler", lbl_Haendler.Text.Remove(lbl_Haendler.Text.IndexOf(",")), s);
            //var artPreisId = GetId("", "", "");



            /*
            // nicht mehr notwendig----------------------------
            int idArtikel;
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                //int preisID; int artikelHaendlerID; int artikelID; int projektID; int haendlerID;int einkaufID;
                
                string sql_Select = "SELECT * FROM Artikel";
                //string sql_Select = "SELECT * FROM Artikel WHERE Artikelbezeichnung = @Artikelbezeichnung";
                //string sql_Insert = "INSERT INTO Artikel (Artikelbezeichnung) VALUES (@Artikelbezeichnung)";

                SqlCommand sql_command = new SqlCommand(sql_Select, sql_conn);

                //sql_command.Parameters.Add("@Artikelbezeichnung", SqlDbType.VarChar).Value = Artikelbezeichnung;
                
                SqlDataAdapter adapter = new SqlDataAdapter(sql_Select, sql_conn);
                DataTable artikel = new DataTable();
                sql_conn.Open();

                adapter.Fill(artikel);

                // --------------------Nicht mehr notwendig da IDs über GetId geholtb werden----------------------

                for (int i = 0; i < dataGridView_Einkauf.RowCount; i++)
                {
                    foreach (DataColumn column in artikel.Columns)
                    {
                        if (column.ColumnName == "Artikelbezeichnung")
                        {
                            foreach (DataRow row in artikel.Rows)
                            {
                                for (int j = 0; j < row.ItemArray.Length; j++)
                                {
                                    if (row.ItemArray[j].ToString() == dataGridView_Einkauf.Rows[i].Cells["Artikel"].Value.ToString())
                                    {
                                        idArtikel = (int)row.ItemArray[0];
                                    }
                                }
                            }
                        }
                    }
                }

                //foreach (DataTable thisTable in datasetEinkauf.Tables)
                //{
                //    foreach (DataRow row in thisTable.Rows)
                //    {
                //        foreach (DataColumn column in thisTable.Columns)
                //        {
                //            if(row[column].ToString() == "Artikel")
                //            se = row[column].ToString();
                //        }
                //    }
                //}



                //if (sql_command.ExecuteScalar() == null)
                //{
                //    SqlCommand sql_command2 = new SqlCommand(sql_Insert, sql_conn);

                //    sql_command2.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;


                //    sql_command2.ExecuteNonQuery();
                //}
                //else
                //{
                //    MessageBox.Show("Der Händler ist schon vorhanden", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            

                sql_conn.Close();
            }
            //new Invoice();
            //LadeStartDaten();
            */
        }
        private int GetId(string DbColumnName, string DbTable, string DgvColumnName, int index)
        {
            int id = 0;
            string ort = lbl_Haendler.Text.Substring(lbl_Haendler.Text.IndexOf(' ')).Trim();

            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                string sql_Select = "SELECT * FROM " + DbTable;
                string sql_Select_Haendler = "SELECT * FROM " + DbTable + " WHERE Ort = @Ort";

                SqlDataAdapter adapter = new SqlDataAdapter(sql_Select, sql_conn);
                SqlDataAdapter adapterhaendler = new SqlDataAdapter(sql_Select_Haendler, sql_conn);
                adapterhaendler.SelectCommand.Parameters.AddWithValue("@Ort", SqlDbType.VarChar).Value = ort;

                DataTable dt = new DataTable();
                sql_conn.Open();

                if (DbTable != "Haendler")
                {
                    adapter.Fill(dt);

                    for (int i = 0; i < dataGridView_Einkauf.RowCount; i++)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            if (column.ColumnName == DbColumnName)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    for (int j = 0; j < row.ItemArray.Length; j++)
                                    {
                                        if (row.ItemArray[j].ToString() == dataGridView_Einkauf.Rows[i].Cells[DgvColumnName].Value.ToString())
                                        {
                                            //Wenn die richtige Reihe (index) gewählt wurde, dann nimm die ID
                                            if(index == j)
                                                id = (int)row.ItemArray[0];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // Hole Haendler ID
                else if (DbTable == "Haendler")
                {
                    adapterhaendler.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        for (int j = 0; j < row.ItemArray.Length; j++)
                        {
                            if (row.ItemArray[j].ToString() == DgvColumnName)
                                id = (int)row.ItemArray[0];
                        }
                    }
                }
                sql_conn.Close();
            }
                return id;
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

        private void btnEintragen_Click(object sender, EventArgs e)
        {
            if(numericUpDown_Einzelpreis.Value != 0 && numericUpDown_Menge.Value != 0 && cb_Artikel.Text != "")
            {
                var row = einkauf.NewRow();
                row["Haendler"] = cb_Haendler.Text;
                row["Datum"] = dateTimePickerDatum.Value;
                row["Artikel"] = cb_Artikel.Text;
                row["Menge"] = numericUpDown_Menge.Value;
                row["Einzelpreis"] = numericUpDown_Einzelpreis.Value;
                row["Projekt"] = cb_Projekt.Text;

                lbl_Datum.Text = dateTimePickerDatum.Value.ToString("dd. MMMM yyyy");
                lbl_Haendler.Text = cb_Haendler.Text;

                var s = dataGridView_Einkauf.Rows.Count;
                if (s > 1)
                {
                    dataGridView_Einkauf.Rows.Remove(dataGridView_Einkauf.Rows[s - 1]);
                }

                dataGridView_Einkauf.Rows.Add(positionDataGridView, numericUpDown_Menge.Value, cb_Artikel.Text, numericUpDown_Einzelpreis.Value, tb_GesamtBetrag.Text, cb_Projekt.Text);

                //Summe aller Gesamtpreise - Zur Kontrolle des Kassenbons
                if (s > 1)
                {
                    x = 0;

                    for (int i = 0; i < s; i++)
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

                dataGridView_Einkauf.Rows.Add("Summe", "", "", "", x + " €");

                //letzte Zeile Fett darstellen
                s = dataGridView_Einkauf.Rows.Count;
                if (s < 2)
                    dataGridView_Einkauf.Rows[s - 1].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                else
                    dataGridView_Einkauf.Rows[s - 1].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                //Hochzählen der Positionen
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
            dataGridView_Einkauf.Columns[0].ValueType = typeof(int);
            dataGridView_Einkauf.Columns[2].Name = "Artikel";
            dataGridView_Einkauf.Columns[2].ValueType = typeof(string);
            dataGridView_Einkauf.Columns[1].Name = "Menge";
            dataGridView_Einkauf.Columns[1].ValueType = typeof(Int32);
            dataGridView_Einkauf.Columns[3].Name = "Einzelpreis";
            dataGridView_Einkauf.Columns[3].ValueType = typeof(decimal);
            dataGridView_Einkauf.Columns[4].Name = "Gesamtpreis";
            dataGridView_Einkauf.Columns[4].ValueType = typeof(decimal);
            dataGridView_Einkauf.Columns[5].Name = "Projekt";
            dataGridView_Einkauf.Columns[5].ValueType = typeof(string);
        }

        private void btnNeuerArtikel_Click(object sender, EventArgs e)
        {
            if (!DbConnect.EqualsArtikel(tb_NeuerArtikel.Text))
                DbConnect.AddNeuerArtikel(tb_NeuerArtikel.Text, conn);
            LadeArtikelNeu("Artikel_ID");
            tb_NeuerArtikel.Clear();
        }
        private void LadeEinzelpreis()
        {
            var haendler = cb_Haendler.Text;
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
                            "WHERE Haendler.Name = @Name AND Artikel.Artikelbezeichnung = @Artikelbezeichnung";


                    SqlCommand sql_command = new SqlCommand(artikelPreis, sql_conn);
                    // Setzten der Paramter für WHERE
                    sql_command.Parameters.Add("@Name", SqlDbType.VarChar).Value = haendler;
                    sql_command.Parameters.Add("@Artikelbezeichnung", SqlDbType.VarChar).Value = artikel;

                    sql_conn.Open();
                    
                    if(sql_command.ExecuteScalar() != null)
                        numericUpDown_Einzelpreis.Value = Convert.ToDecimal(sql_command.ExecuteScalar());
                    else
                        numericUpDown_Einzelpreis.Value = 0;
                    
                    sql_conn.Close();
                }
            }
            if (numericUpDown_Menge.Value > 0)
            {
                CalculateAmount();
            }
        }

        private void cb_Artikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LadeEinzelpreis();
        }

        private void cb_Haendler_SelectedIndexChanged(object sender, EventArgs e)
        {
            LadeEinzelpreis();
        }

        private void numericUpDown_Menge_ValueChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void numericUpDown_Einzelpreis_ValueChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private int PrüfeArtikel()
        {
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                int id;
                string artikel = cb_Artikel.Text;
                string sql_SelectArtikel = "SELECT Artikelbezeichnung FROM Artikel WHERE Artikelbezeichnung = @Artikelbezeichnung";
                string sql_SelectId = "SELECT ID FROM Artikel WHERE Artikelbezeichnung = @Artikelbezeichnung";
                string sql_InsertArtikel = "INSERT INTO Artikel (Artikelbezeichnung) VALUES (@Artikelbezeichnung)";

                SqlCommand sql_command = new SqlCommand(sql_SelectArtikel, sql_conn);
                
                sql_command.Parameters.Add("@Artikelbezeichnung", SqlDbType.VarChar).Value = artikel;

                sql_conn.Open();

                if (sql_command.ExecuteScalar() == null)
                {
                    SqlCommand sql_command2 = new SqlCommand(sql_InsertArtikel, sql_conn);

                    sql_command2.Parameters.Add("@Artikelbezeichnung", SqlDbType.VarChar).Value = artikel;

                    sql_command2.ExecuteNonQuery();

                    SqlCommand sql_comnand_ID = new SqlCommand(sql_SelectId, sql_conn);

                    id = Convert.ToInt32(sql_comnand_ID.ExecuteScalar().ToString());

                    sql_conn.Close();
                    return id;
                }
                else
                {
                    SqlCommand sql_comnand_ID = new SqlCommand(sql_SelectId, sql_conn);

                    id = Convert.ToInt32(sql_comnand_ID.ExecuteScalar().ToString());

                    sql_conn.Close();

                    return id;
                }

                
            }

        }
    }
}
