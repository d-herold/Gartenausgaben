using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class Evaluation : Form
    {
        readonly string conn = Properties.Settings.Default.GartenDB;
        int startEnd;
        ComboBox jahr;

        public Evaluation()
        {
            InitializeComponent();
            LadeProjekte();
            LblBetrag.Enabled = false;
            Cb_Jahr.SelectedIndex = 0;
            //CheckYear();
            Rb_GanzesJahr.Visible = false;
            Rb_Zeitraum.Visible = false;
            monthCalendar1.Visible = false;
            Tb_Auswertung_von.Visible = false;
            Tb_Auswertung_bis.Visible = false;
            Lbl_Datum_von.Visible = false;
            Lbl_Datum_bis.Visible = false;
            jahr = new ComboBox();
            jahr.Location = new Point(327, 134);
            jahr.Size = new Size(76, 20);
            jahr.Visible = false;
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Auswerten_Click(object sender, EventArgs e)
        {
            GetSumme();
        }

        private void GetSumme()
        {
            var projekt = Cb_Projekt.Text;
            LblBetrag.Enabled = true;

            int projektAuswahl;
            if (projekt == "Alle")
                projektAuswahl = 1;
            else
                projektAuswahl = 2;

            int auswahl = 0;
            if (projektAuswahl == 1 && Cb_Jahr.Text == "Alle Jahre")
            {
                auswahl = 1;
            }
            else if (projektAuswahl == 1 && Cb_Jahr.Text != "Alle Jahre")
            {
                if (Rb_GanzesJahr.Checked)
                {
                    auswahl = 2;
                }
                else
                {
                    auswahl = 3;
                }
            }
            else if (projektAuswahl == 2 && Cb_Jahr.Text == "Alle Jahre")
            {
                auswahl = 4;
            }
            else if (projektAuswahl == 2 && Cb_Jahr.Text != "Alle Jahre")
            {
                if (Rb_GanzesJahr.Checked)
                {
                    auswahl = 5;
                }
                else
                {
                    auswahl = 6;
                }
            }

            switch (auswahl)
            {
               case 1:
                    LblBetrag.Text = "";
                    break;
                case 2:
                    LblBetrag.Text = "";
                    break;
                case 3:
                    LblBetrag.Text = "";
                    break;
                case 4:
                    LblBetrag.Text = AddAusgabenProjket(Cb_Projekt.Text);
                    break;
                case 5:
                    LblBetrag.Text = "";
                    break;
                case 6:
                    LblBetrag.Text = "";
                    break;
                default:
                    LblBetrag.Text = "0.00 €";
                    break;
            }
        }
        private string AddAusgabenProjket(string name)
        {
            string sql_Select = "SELECT CAST (SUM (Menge * Artikelpreis) as DECIMAL (7,2)) AS Summe FROM Einkauf AS e " +
            "INNER JOIN Einkaufpositionen AS ep ON e.Einkauf_ID = ep.Einkauf_ID " +
            "INNER JOIN Artikel AS a ON a.Artikel_ID = ep.Artikel_ID " +
            "INNER JOIN Projekt AS p ON p.Projekt_ID = ep.Projekt_ID " +
            "INNER JOIN Artikel_Preis AS ap ON ap.Preis_ID = ep.Preis_ID " +
            "WHERE Projektname = @ProjektName";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select, sql_conn))
            {
                command.Parameters.AddWithValue("@ProjektName", name);
                
                try
                {
                    sql_conn.Open();
                    decimal summe = (decimal)command.ExecuteScalar();
                    sql_conn.Close();
                    return summe.ToString("0.00") + " €";
                }
                catch
                {
                    MessageBox.Show("Dieses Projekt hat noch keine Ausgaben!", "Hinweis", MessageBoxButtons.OK);
                    LblBetrag.Enabled = false;
                    return LblBetrag.Text = "";
                }
            }
        }
        private void LadeProjekte()
        {
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(conn))
            {

                //Abfrage-String für alle Namen aus der Händler Tabelle
                string querySql_Projekt = "SELECT Projektname FROM Projekt ORDER BY Projektname ASC";

                try
                {
                    sql_con.Open();
                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt_Projekt = new SqlDataAdapter(querySql_Projekt, sql_con);

                    //Projektdaten laden
                    DataTable tblData_Projekt = new DataTable();
                    sql_adapt_Projekt.Fill(tblData_Projekt);

                    Cb_Projekt.DisplayMember = "Projektname";
                    Cb_Projekt.ValueMember = "[Projekt_ID]";
                    Cb_Projekt.DataSource = tblData_Projekt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Achtung: " + ex.Message);
                }
                sql_con.Close();
            }
        }

        private void Cb_Projekt_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblBetrag.Text = "";
            LblBetrag.Enabled = false;
        }
        //private void CheckYear()
        //{
        //    var s = Convert.ToInt32(DateTime.Now.Year);
        //    var count = s - 2014;

        //    for (int i = 1; i <= count; i++)
        //    {
        //        Cb_Jahr.Items.Add(2014 + i);
        //    }

        //}

        private void Cb_Jahr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Jahr.SelectedItem.ToString() != "Alle Jahre")
            {
                Rb_Zeitraum.Visible = true;
                Rb_GanzesJahr.Visible = true;
            }
            if (Cb_Jahr.SelectedItem.ToString() == "Alle Jahre")
            {
                Rb_GanzesJahr.Checked = false;
                Rb_Zeitraum.Visible = false;
                Rb_GanzesJahr.Visible = false;
            }
        }

        private void Rb_Zeitraum_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Zeitraum.Checked)
            {
                Tb_Auswertung_von.Visible = true;
                Tb_Auswertung_bis.Visible = true;
                Lbl_Datum_von.Visible = true;
                Lbl_Datum_von.Text = "Auswertung von:";
                Lbl_Datum_bis.Visible = true;
                monthCalendar1.Visible = true;
                Tb_Auswertung_von.Focus();
                startEnd = 1;
                jahr.Visible = false;
            }
        }

        private void Tb_Auswertung_von_Click(object sender, EventArgs e)
        {
            startEnd = 1;
            monthCalendar1.Visible = true;
        }

        private void Tb_Auswertung_bis_Click(object sender, EventArgs e)
        {
            startEnd = 2;
            monthCalendar1.Visible = true;
        }

        private void Rb_GanzesJahr_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
            Tb_Auswertung_von.Visible = false;
            Tb_Auswertung_von.Clear();
            Tb_Auswertung_bis.Visible = false;
            Tb_Auswertung_bis.Clear();
            Lbl_Datum_von.Visible = false;
            Lbl_Datum_bis.Visible = false;
            Lbl_Datum_von.Text = "Jahr";

            jahr = new ComboBox();
            jahr.Visible = true;
            jahr.Enabled = true;
            var s = Convert.ToInt32(DateTime.Now.Year);
            var count = s - 2014;

            for (int i = 1; i <= count; i++)
            {
                jahr.Items.Add(2014 + i);
            }
            jahr.Location = new Point(327, 134);
            jahr.Size = new Size(76, 20);
            this.Controls.AddRange(new Control[] { this.jahr });
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            switch(startEnd)
            {
                case 1:
                    Tb_Auswertung_von.Focus();
                    Tb_Auswertung_von.Text = e.Start.ToShortDateString();
                    if(Tb_Auswertung_bis.Text != "")
                        if (Convert.ToDateTime(Tb_Auswertung_von.Text) > Convert.ToDateTime(Tb_Auswertung_bis.Text))
                            Tb_Auswertung_bis.Text = Tb_Auswertung_von.Text;
                    if (Tb_Auswertung_bis.Text == "")
                        Tb_Auswertung_bis.Focus();
                    else
                        monthCalendar1.Visible = false;
                    startEnd = 2;
                    break;
                case 2:
                    Tb_Auswertung_bis.Text = e.End.ToShortDateString();

                    if (Convert.ToDateTime(Tb_Auswertung_bis.Text) < Convert.ToDateTime(Tb_Auswertung_von.Text))
                        Tb_Auswertung_von.Text = Tb_Auswertung_bis.Text;
                    monthCalendar1.Visible = false;
                    break;
            }
        }
    }
}
