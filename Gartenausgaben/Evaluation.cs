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

        public Evaluation()
        {
            InitializeComponent();
            LadeProjekte();
            LblBetrag.Enabled = false;
            Cb_Jahr.SelectedIndex = 0;
            CheckYear();
            Rb_GanzesJahr.Visible = false;
            Rb_Zeitraum.Visible = false;
            monthCalendar_StartDate.Visible = false;
            monthCalendar_EndDate.Visible = false;
            Tb_Auswertung_von.Visible = false;
            Tb_Auswertung_bis.Visible = false;
            Lbl_Datum_von.Visible = false;
            Lbl_Datum_bis.Visible = false;
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
            LblBetrag.Enabled = true;
            var summe = AddAusgabenProjket(Cb_Projekt.Text);
            LblBetrag.Text = summe;
        }
        private string AddAusgabenProjket(string name)
        {
            decimal summe = 0;
            string sql_Select = "SELECT SUM(ap.Artikelpreis*ep.Menge) AS Summe FROM Artikel_Preis AS ap " +
            "INNER JOIN Artikel_Haendler AS ah ON ah.ArtikelHaendler_ID = ap.ArtikelHaendler_ID " +
            "INNER JOIN Artikel AS a ON a.Artikel_ID = ah.Artikel_ID " +
            "INNER JOIN Einkaufpositionen AS ep ON ep.Artikel_ID = a.Artikel_ID " +
            "INNER JOIN Projekt AS p ON p.Projekt_ID = ep.Projekt_ID " +
            "INNER JOIN Einkauf AS e ON e.Einkauf_ID = ep.Einkauf_ID " +
            "WHERE Projektname = @ProjektName";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select, sql_conn))
            {
                command.Parameters.AddWithValue("@ProjektName", name);
                
                try
                {
                    sql_conn.Open();
                    summe = (decimal)command.ExecuteScalar();
                    sql_conn.Close();
                    return summe.ToString("0.00") + " €";
                }
                catch
                {
                    MessageBox.Show("Dieses Projekt hat noch keine Ausgaben!", "Hinweis", MessageBoxButtons.OK);
                    LblBetrag.Enabled = false;
                    ;
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
        private void CheckYear()
        {
            var s = Convert.ToInt32(DateTime.Now.Year);
            var count = s - 2014;

            for (int i = 1; i <= count; i++)
            {
                Cb_Jahr.Items.Add(2014 + i);
            }
                
        }

        private void Cb_Jahr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Jahr.SelectedItem.ToString() != "Alle")
            {
                Rb_Zeitraum.Visible = true;
                Rb_GanzesJahr.Visible = true;
            }
        }

        private void Rb_Zeitraum_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Zeitraum.Checked)
            {
                
                Tb_Auswertung_von.Visible = true;
                Tb_Auswertung_bis.Visible = true;
                Lbl_Datum_von.Visible = true;
                Lbl_Datum_bis.Visible = true;
                monthCalendar_StartDate.Visible = true;
                Tb_Auswertung_von.Focus();
            }
        }

        private void monthCalendar_StartDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            Tb_Auswertung_von.Text = e.Start.ToShortDateString();
            monthCalendar_StartDate.Visible = false;

            if(Tb_Auswertung_bis.Text != "")
                if (Convert.ToDateTime(Tb_Auswertung_von.Text) >= Convert.ToDateTime(e.End.ToShortDateString()))
                    Tb_Auswertung_bis.Text = e.Start.ToShortDateString();

            Tb_Auswertung_bis.Focus();
            monthCalendar_EndDate.Visible = true;
        }

        private void monthCalendar_EndDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (Convert.ToDateTime(Tb_Auswertung_von.Text) <= Convert.ToDateTime(e.End.ToShortDateString()))
                Tb_Auswertung_bis.Text = e.End.ToShortDateString();
            else
            {
                Tb_Auswertung_bis.Text = e.End.ToShortDateString();
                Tb_Auswertung_von.Text = e.End.ToShortDateString();
            }
                


        }

        private void Tb_Auswertung_von_Click(object sender, EventArgs e)
        {
             monthCalendar_EndDate.Visible = false;
             monthCalendar_StartDate.Visible = true;
        }

        private void Tb_Auswertung_bis_Click(object sender, EventArgs e)
        {
            monthCalendar_StartDate.Visible = false;
            monthCalendar_EndDate.Visible = true;
        }

        private void Rb_GanzesJahr_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendar_StartDate.Visible = false;
            monthCalendar_EndDate.Visible = false;
            Tb_Auswertung_von.Visible = false;
            Tb_Auswertung_von.Clear();
            Tb_Auswertung_bis.Visible = false;
            Tb_Auswertung_bis.Clear();
            Lbl_Datum_von.Visible = false;
            Lbl_Datum_bis.Visible = false;
        }
    }
}
