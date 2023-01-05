using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class Invoice : Form
    {
        DataTable einkauf;
        bool listeSort = true;
        int positionDataGridView = 1;
        public decimal GesamtBetrag { get; set; }
        public int EinkaufPositionID { get; set; }

        public Invoice()
        {
            InitializeComponent();
            // ComboBox-Inhalte Rechtsbündig darstellen
            this.cb_Artikel.DropDownStyle = ComboBoxStyle.DropDownList;
            SetMyCustomFormat();
            LadeStartDaten();
            ErstelleDataTable();
            SetDataGrid_Tabelle();
            BtnNewItem.Enabled = false;
            
        }

        void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            string txt = "";
            if (e.Index >= 0)
                txt = cb_Artikel.Items[e.Index].ToString();

            TextRenderer.DrawText(e.Graphics, txt, e.Font, e.Bounds, e.ForeColor, TextFormatFlags.Right);
            e.DrawFocusRectangle();
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

        /// <summary>
        /// Lädt beim Start alle verfügbaren Händler und Artikel. Sortierung ASC
        /// </summary>
        public void LadeStartDaten()
        {
            Artikel artikel = new Artikel();
            Haendler haendler = new Haendler();
            Projekt projekt = new Projekt();
            artikel.GetArtikelliste();
            haendler.GetHaendlerListe();
            projekt.GetProjektListe();

            foreach (var item in artikel.Artikelliste)
            {
                cb_Artikel.Items.Add(item);
            }
            foreach (var item in haendler.HaendlerListe)
            {
                cb_Haendler.Items.Add(item.Item1.TrimStart('(')+ ", " + item.Item2.TrimEnd(')'));
            }
            foreach (var item in projekt.ProjektListe)
            {
                cb_Projekt.Items.Add(item);
            }

            cb_Artikel.SelectedIndex = 0;
            cb_Haendler.SelectedIndex = 0;

            LadeEinzelpreis(artikel.Artikelliste[0], cb_Haendler.Items[0].ToString());

            cb_Projekt.SelectedIndex = 0;

            lbl_Haendler.Text = cb_Haendler.Text;
            lbl_Datum.Text = dateTimePickerDatum.Value.ToString("dd. MMMM yyyy");
        }

        /// <summary>
        /// Lädt nach einem Update die neue Händlerliste. Hinzugefügter Händler steht oben
        /// </summary>
        /// <param name="sort"></param>
        public void LadeHaendlerNeu(string sortierung)
        {
            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql = "SELECT Name, Ort FROM Haendler ORDER BY " + sortierung + " DESC";

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(DbConnect.Conn))
            {
                try
                {
                    sql_con.Open();
                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt = new SqlDataAdapter(querySql, sql_con);

                    //Erstellt eine neue Tabelle im Arbeitsspeicher
                    DataTable tblData = new DataTable();
                    //Befüllt die DataTable
                    sql_adapt.Fill(tblData);
                    tblData.Columns.Add("NameOrt", typeof(string), "Name + ', ' + Ort");

                    //Anzeige in der ComboBox alle Namen der vorhanden Ids
                    cb_Haendler.DisplayMember = "NameOrt";
                    cb_Haendler.ValueMember = "[Haendler_ID]";

                    //Zuweisen der Datentabelle zur Datenquelle
                    cb_Haendler.DataSource = tblData;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_con.Close();
            }
        }

        /// <summary>
        /// Sortierung nach ID Absteigend
        /// </summary>
        /// <param name="sort"></param>
        public void LadeArtikelNeu(string sort, string orderBy)
        {
            string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY " + sort + " " + orderBy;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(DbConnect.Conn))
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
                catch (Exception ex)
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
            using (SqlConnection sql_con = new SqlConnection(DbConnect.Conn))
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveInDB();
        }

        /// <summary>
        /// Funktion zum Speichern des Inhalts der DataGridView in der DB
        /// </summary>
        private void SaveInDB()
        {
            char[] charToTrim = { ',', ' ' };
            var h_ort = lbl_Haendler.Text.Substring(lbl_Haendler.Text.IndexOf(',')).TrimStart(charToTrim);
            var h_name = lbl_Haendler.Text.Remove(lbl_Haendler.Text.IndexOf(","));
            
            Haendler haendler = new Haendler(h_name, h_ort);

            //Erstelle DataTables
            var dgvEinkauf = new DataTable();
            
            //Hinzufügen der Spalten
            foreach (DataGridViewColumn column in dataGridView_Einkauf.Columns)
            {
                dgvEinkauf.Columns.Add(column.HeaderText, column.ValueType);
            }

            int dgvEinkaufCountRow = 0;
            //Hinzufügen der Zeilen
            foreach (DataGridViewRow row in dataGridView_Einkauf.Rows)
            {
                dgvEinkauf.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgvEinkauf.Rows[dgvEinkauf.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                }
                dgvEinkaufCountRow++;
            }

            var dataset = new DataSet();
            dataset.Tables.Add(dgvEinkauf);

            Einkauf einkauf = new Einkauf();
            var summe = Convert.ToDecimal(lbl_SummeBetrag.Text.Remove(lbl_SummeBetrag.Text.IndexOf(" ")));
            var datum = dateTimePickerDatum.Value.Date.ToString("dd-MM-yyyy");
            if (!einkauf.EinkaufIstVorhanden(haendler.HaendlerId, summe, datum, dgvEinkaufCountRow ))
            {
                /* *************** Neuen Einkauf anlegen *************** */
                einkauf.SetNewEinkaufId(haendler.HaendlerId, dateTimePickerDatum.Value.Date);

                /* *************** Übertragen der einzelnen Positionen in die Datenbank *************** */
                foreach (DataGridViewRow row in dataGridView_Einkauf.Rows)
                {
                    //SetEinzelpreisUndMenge(row.Index);

                    Artikel art = new Artikel();
                    ArtikelHaendler art_haend = new ArtikelHaendler();
                    Projekt proj = new Projekt();
                    ArtikelPreis art_preis = new ArtikelPreis();
                    int Menge = 0;
                    
                    //
                    for (int i = 0; i < dataGridView_Einkauf.Columns.Count; i++)
                    {
                        if (dataGridView_Einkauf.Columns[i].Name == "Einzelpreis" || dataGridView_Einkauf.Columns[i].Name == "Menge")
                        {
                            for (int j = 0; j < dataGridView_Einkauf.RowCount; j++)
                            {
                                if (j == row.Index && dataGridView_Einkauf.Columns[i].Name == "Einzelpreis")
                                {
                                    art_preis.Preis = Convert.ToDecimal(dataGridView_Einkauf.Rows[j].Cells[i].Value);
                                }
                                else if (j == row.Index && dataGridView_Einkauf.Columns[i].Name == "Menge")
                                {
                                    Menge = Convert.ToInt32(dataGridView_Einkauf.Rows[j].Cells[i].Value);
                                }
                            }
                        }
                    }

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if(cell.ColumnIndex == 2)
                        {
                            art.Artikelbezeichnung = cell.Value.ToString();
                        }
                        if(cell.ColumnIndex == 5)
                        {
                            proj.Projektname = cell.Value.ToString();
                        }

                    }
                    art.ID(art.Artikelbezeichnung);

                    art_haend.ID(art.ArtikelId,haendler.HaendlerId);

                    var projekt_id = proj.ID();

                    art_preis.ID(art_haend.Artikel_HaendlerID, art_preis.Preis);

                    var x = 0; // Breakpoint vor speichern von Einkaufpositionen

                    SetEinkaufsposition(art.ArtikelId, projekt_id, einkauf.Id, art_preis.PreisId, Menge);
                }
                dataGridView_Einkauf.Rows.Clear();
                lbl_SummeBetrag.Text = "0,00 €";
            }
            else
            {
                var result = MessageBox.Show("Ihre Daten wurden nicht gespeichert. Möchten Sie einen neuen Bon anlegen?", "Achtung", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dataGridView_Einkauf.Rows.Clear();
                    lbl_SummeBetrag.Text = "0,00 €";
                }
            }
        }

        private int SetEinkaufsposition(int artikel_id, int projekt_id, int einkauf_id, int artikelpreis_id, int menge)
        {
            string sql_Insert = "INSERT INTO Einkaufpositionen (Menge, Projekt_ID, Artikel_ID, Einkauf_ID, Preis_ID) " + "VALUES (@Menge,  @ProjektId, @ArtikelId, @EinkaufId, @PreisId); "
                + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@ArtikelId", artikel_id);
                command.Parameters.AddWithValue("@ProjektId", projekt_id);
                command.Parameters.AddWithValue("@EinkaufId", einkauf_id);
                command.Parameters.AddWithValue("@PreisId", artikelpreis_id);
                command.Parameters.AddWithValue("@Menge", menge);
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
            this.LadeHaendlerNeu("Haendler_ID");
        }

        private void BtnCopyToList_Click(object sender, EventArgs e)
        {
            if (numericUpDown_Einzelpreis.Value != 0 && numericUpDown_Menge.Value != 0 && cb_Artikel.Text != "")
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

                Gesamtsumme();

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

        private decimal Gesamtsumme()
        {
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
            return x;
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
                DbConnect.AddNeuerArtikel(tb_NeuerArtikel.Text, DbConnect.Conn);
            LadeArtikelNeu("Artikel_ID", "DESC");
            listeSort = false;
            tb_NeuerArtikel.Clear();
        }

        private void LadeEinzelpreis(string _artikel, string _haendler)
        {
            var haendler = _haendler;
            var artikel = _artikel;
            char[] charToTrim = { ',', ' ' };
            var ort = haendler.Substring(cb_Haendler.Text.IndexOf(',')).TrimStart(charToTrim);

            string[] subs = haendler.Split(',');
            haendler = subs[0];

            if (cb_Haendler.Text != "" && cb_Artikel.Text != "" && ort !="")
            {
                using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
                {
                    string artikelPreis = "SELECT ap.Artikelpreis FROM Artikel_Preis AS ap " +
                            "JOIN Artikel_Haendler AS ah ON ah.ArtikelHaendler_ID = ap.ArtikelHaendler_ID " +
                            "JOIN Artikel AS a ON a.Artikel_ID = ah.Artikel_ID " +
                            "JOIN Haendler AS h ON h.Haendler_ID = ah.Haendler_ID " +
                            "WHERE h.Name = @Name AND h.Ort = @Ort AND a.Artikelbezeichnung = @Artikelbezeichnung " +
                            "ORDER BY ap.Artikelpreis DESC";
                    
                    SqlCommand sql_command = new SqlCommand(artikelPreis, sql_conn);
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
            if (cb_Haendler.Text != "" && cb_Artikel.Text != "" && ort == "")
            {
                using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
                {
                    string artikelPreis = "SELECT ap.Artikelpreis FROM Artikel_Preis AS ap " +
                            "JOIN Artikel_Haendler AS ah ON ah.ArtikelHaendler_ID = ap.ArtikelHaendler_ID " +
                            "JOIN Artikel AS a ON a.Artikel_ID = ah.Artikel_ID " +
                            "JOIN Haendler AS h ON h.Haendler_ID = ah.Haendler_ID " +
                            "WHERE h.Name = @Name AND a.Artikelbezeichnung = @Artikelbezeichnung " +
                            "ORDER BY ap.Artikelpreis DESC";

                    SqlCommand sql_command = new SqlCommand(artikelPreis, sql_conn);
                    sql_command.Parameters.AddWithValue("@Name", haendler);
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
                tb_GesamtBetrag.Text = Calculate.CalculateAmount(numericUpDown_Einzelpreis.Value, numericUpDown_Menge.Value);
            }
        }

        private void Cb_Artikel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Haendler.Text != "")
                LadeEinzelpreis(cb_Artikel.Text, cb_Haendler.Text);
        }

        private void Cb_Haendler_SelectedIndexChanged(object sender, EventArgs e)
        {
                LadeEinzelpreis(cb_Artikel.Text, cb_Haendler.Text);
        }

        private void NumericUpDown_Menge_ValueChanged(object sender, EventArgs e)
        {
            tb_GesamtBetrag.Text = Calculate.CalculateAmount(numericUpDown_Einzelpreis.Value, numericUpDown_Menge.Value);
        }

        private void NumericUpDown_Einzelpreis_ValueChanged(object sender, EventArgs e)
        {
            tb_GesamtBetrag.Text = Calculate.CalculateAmount(numericUpDown_Einzelpreis.Value, numericUpDown_Menge.Value);
        }

        private void BtnDgvDeleteLastRow_Click(object sender, EventArgs e)
        {
            int count = dataGridView_Einkauf.Rows.Count;
            dataGridView_Einkauf.Rows.RemoveAt(count - 1);
            Gesamtsumme();
        }

        private void DataGridView_Einkauf_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_Einkauf.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void DataGridView_Einkauf_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            var row = 0;
            var count = dataGridView_Einkauf.Rows.Count;
            for (int i = 1; i <= count; i++)
            {
                this.dataGridView_Einkauf.Rows[row].Cells["Position"].Value = i;
                row++;

            }
            positionDataGridView = count + 1;
            Gesamtsumme();
        }

        private void BtnNeuesProjekt_Click(object sender, EventArgs e)
        {
            AddNewProjectControl();
        }

        private Button okNewProject = new Button();
        private TextBox tbNewProject = new TextBox();

        public void AddNewProjectControl()
        {
            okNewProject = new Button();
            tbNewProject = new TextBox
            {
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
            Projekt projekt = new Projekt();
            var projektname = tbNewProject.Text.Trim();

            if (projektname == "")
                RemoveNewProjectControl();
            else
            {
                if (!projekt.EqualsProject(projektname))
                {
                    projekt.AddNewProject(projektname);
                    LadeProjektNeu("Projekt_ID", "DESC");
                }
                else
                    MessageBox.Show("Das Projekt existiert schon", "Hinweis", MessageBoxButtons.OK);
            }
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

        private void BtnNewShopping_Click(object sender, EventArgs e)
        {
            if (dataGridView_Einkauf.Rows.Count < 1)
            {
                MessageBox.Show("Es sind keine Artikel zum löschen vorhanden!", "Hinweis", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result =
                    MessageBox.Show("Wollen Sie wirklich alle Artikel aus der Liste löschen? Die vorhandenen Daten werden nicht gespeichert!", "ACHTUNG", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    dataGridView_Einkauf.Rows.Clear();
            }
        }

        /// <summary>
        /// Funktion für die Autosize Größe der Artikel-ComboBox
        /// </summary>
        private int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(myCombo.GetItemText(obj), myCombo.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            return maxWidth + SystemInformation.VerticalScrollBarWidth;
        }

        /// <summary>
        /// Funktion für die Autosize Größe der Artikel-ComboBox
        /// </summary>
        private void Invoice_Load(object sender, EventArgs e)
        {
            //LadeStartDaten();
            cb_Artikel.DropDownWidth = DropDownWidth(cb_Artikel);
        }

        /// <summary>
        /// Zentrierte Ausrichtung der Combo Box zulassen
        /// </summary>
        private void cbxDesign_DrawItem(object sender, DrawItemEventArgs e)
        {
            // By using Sender, one method could handle multiple ComboBoxes
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                // Always draw the background
                e.DrawBackground();

                // Drawing one of the items?
                if (e.Index >= 0)
                {
                    // Set the string alignment.  Choices are Center, Near and Far
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;

                    // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                    // Assumes Brush is solid
                    Brush brush = new SolidBrush(cbx.ForeColor);

                    // If drawing highlighted selection, change brush
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        brush = SystemBrushes.HighlightText;

                    // Draw the string
                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        /// <summary>
        /// Funktion zum Prüfen, ob der aktuelle Bon/Kassenzettel schon einmal eingegeben wurde
        /// </summary>
        /// <returns>true -> Bon existiert</returns>
        //private bool EinkaufIstVorhanden(int haendler_id)
        //{
        //    Einkauf[] einkauf = null;
        //    var summe = Convert.ToDecimal(lbl_SummeBetrag.Text.Remove(lbl_SummeBetrag.Text.IndexOf(" ")));
        //    var datum = dateTimePickerDatum.Value.Date.ToString("dd-MM-yyyy");


        //    string querySql = "SELECT e.Einkauf_ID, e.Haendler_ID, SUM(AP.Artikelpreis * EP.Menge) AS Summe FROM Artikel_Preis AS ap " +
        //    "INNER JOIN Artikel_Haendler AS ah ON ah.ArtikelHaendler_ID = ap.ArtikelHaendler_ID " +
        //    "INNER JOIN Artikel AS a ON a.Artikel_ID = ah.Artikel_ID " +
        //    "INNER JOIN Einkaufpositionen AS ep ON ep.Artikel_ID = a.Artikel_ID " +
        //    "INNER JOIN Einkauf AS e ON e.Einkauf_ID = ep.Einkauf_ID " +
        //    "WHERE e.Datum = @Datum " +
        //    "GROUP BY e.Einkauf_ID, e.Haendler_ID ";

        //    using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
        //    {
        //        SqlCommand command = new SqlCommand(querySql, sql_conn);
        //        command.Parameters.AddWithValue("@Haendler", haendler_id);
        //        command.Parameters.AddWithValue("@Datum", datum);

        //        try
        //        {
        //            if (sql_conn.State != ConnectionState.Open)
        //                sql_conn.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                var list = new List<Einkauf>();
        //                while (reader.Read())
        //                {
        //                    list.Add(new Einkauf { Id = reader.GetInt32(0), HaendlerId = reader.GetInt32(1), Summe = reader.GetDecimal(2) });
        //                    einkauf = list.ToArray();
        //                }
        //                reader.Close();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        sql_conn.Close();
        //    }
        //    if (einkauf != null)
        //    //if (einkauf.Length > 0)
        //    {
        //        var count = 0;

        //        foreach (var item in einkauf)
        //        {
        //            if (haendler_id == item.HaendlerId && item.Summe == summe)
        //                count++;
        //        }
        //        if (count > 0)
        //        {
        //            var result = MessageBox.Show("Ein Kassenbon von diesem Händler und dem selben Tag existiert schon " + count + "x." + "\n" + "Möchten Sie diese Buchung erneut speichern?", "Achtung", MessageBoxButtons.YesNo);
        //            if (result == DialogResult.Yes)
        //            {
        //                return false;
        //            }
        //            else
        //                return true;
        //        }
        //    }
        //    return false;
        //}
        /// <summary>
        /// Obsolete Funktion zum Laden der Startdaten
        /// </summary>
        //public void LadeStartDaten()
        //{
        //    //Erstellt eine neue Verbindund zur übergebenen Datenbank
        //    using (SqlConnection sql_con = new SqlConnection(DbConnect.Conn))
        //    {
        //        //Abfrage-String für alle Namen aus der Händler Tabelle
        //        string querySql_Haendler = "SELECT Name, Ort FROM Haendler ORDER BY Name ASC";
        //        string querySql_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY Artikelbezeichnung ASC";
        //        string querySql_Projekt = "SELECT Projektname FROM Projekt ORDER BY Projektname ASC";

        //        try
        //        {
        //            sql_con.Open();
        //            //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
        //            SqlDataAdapter sql_adapt_Haendler = new SqlDataAdapter(querySql_Haendler, sql_con);
        //            SqlDataAdapter sql_adapt_Artikel = new SqlDataAdapter(querySql_Artikel, sql_con);
        //            SqlDataAdapter sql_adapt_Projekt = new SqlDataAdapter(querySql_Projekt, sql_con);

        //            //Händlerliste laden
        //            //Erstellt eine neue Tabelle im Arbeitsspeicher
        //            DataTable tblData_Haendler = new DataTable();
        //            //Befüllt die DataTable
        //            sql_adapt_Haendler.Fill(tblData_Haendler);
        //            //Anzeige in der ComboBox alle Namen der vorhanden Ids
        //            tblData_Haendler.Columns.Add("NameOrt", typeof(string), "Name + ', ' + Ort");
        //            cb_Haendler.DisplayMember = "NameOrt";
        //            cb_Haendler.ValueMember = "[Haendler_ID]";

        //            //Zuweisen der Datentabelle zur Datenquelle
        //            cb_Haendler.DataSource = tblData_Haendler;

        //            //Artikelliste laden
        //            DataTable tblData_Artikel = new DataTable();
        //            sql_adapt_Artikel.Fill(tblData_Artikel);
        //            cb_Artikel.DisplayMember = "Artikelbezeichnung";
        //            cb_Artikel.ValueMember = "[Artikel_ID]";
        //            cb_Artikel.DataSource = tblData_Artikel;

        //            //nochmaliges zuweisen, da Artikel am Anfang noch nicht geladen wurden und der Einzelpreis sonst leer bleibt
        //            cb_Haendler.DataSource = tblData_Haendler;

        //            //Projektdaten laden
        //            DataTable tblData_Projekt = new DataTable();
        //            sql_adapt_Projekt.Fill(tblData_Projekt);
        //            cb_Projekt.DisplayMember = "Projektname";
        //            cb_Projekt.ValueMember = "[Projekt_ID]";
        //            cb_Projekt.DataSource = tblData_Projekt;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Achtung: " + ex.Message);
        //        }
        //        sql_con.Close();
        //    }
        //    lbl_Haendler.Text = cb_Haendler.Text;
        //    lbl_Datum.Text = dateTimePickerDatum.Value.ToString("dd. MMMM yyyy");
        //    countSelectedIndexChanged = 1;
        //}
    }
}
