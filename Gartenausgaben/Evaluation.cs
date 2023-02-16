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
        int startEnd;

        public Evaluation()
        {
            InitializeComponent();
            LoadAllProjects();
            LblBetrag.Enabled = false;
            monthCalendar1.Visible = false;
            Tb_Auswertung_von.Visible = false;
            Tb_Auswertung_bis.Visible = false;
            Lbl_Datum_von.Visible = false;
            Lbl_Datum_bis.Visible = false;
            ComboBox_EvaluationYear.Visible = false;
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Auswerten_Click(object sender, EventArgs e)
        {
            Lbl_Hinweis.Text = "";
            GetSumme();
        }

        private void GetSumme()
        {
            var projekt = Cb_Projekt.Text;
            LblBetrag.Enabled = true;

            int projektAuswahl;
            if (CheckBox_Evaluation_All.Checked)
                projektAuswahl = 1;
            else
                projektAuswahl = 2;

            int auswahl = 0;

            if (projektAuswahl == 1 && Rb_EvaluationToYear.Checked && ComboBox_EvaluationYear.SelectedItem.ToString() == "Alle Jahre") // && Cb_Jahr.Text == "Alle Jahre" 
                auswahl = 1;
            else if (projektAuswahl == 1 && Rb_EvaluationToYear.Checked && ComboBox_EvaluationYear.SelectedItem.ToString() != "Alle Jahre") // && Cb_Jahr.Text != "Alle Jahre" 
                auswahl = 2;
            else if (projektAuswahl == 1 && Rb_EvaluationByPeriod.Checked)
                auswahl = 3;
            else if (projektAuswahl == 2 && Rb_EvaluationToYear.Checked && ComboBox_EvaluationYear.SelectedItem.ToString() == "Alle Jahre")
                auswahl = 4;
            else if (projektAuswahl == 2 && Rb_EvaluationToYear.Checked && ComboBox_EvaluationYear.SelectedItem.ToString() != "Alle Jahre")
                auswahl = 5;
            else if (projektAuswahl == 2 && Rb_EvaluationByPeriod.Checked)
                auswahl = 6;

            switch (auswahl)
            {
               case 1:
                    LblBetrag.Text = GetSumAllYearAllProjects();
                    break;
                case 2:
                    LblBetrag.Text = GetSumOneYearAllProjects(ComboBox_EvaluationYear.SelectedItem.ToString());
                    break;
                case 3:
                    LblBetrag.Text = GetSumPeriodAllProjects(Tb_Auswertung_von.Text , Tb_Auswertung_bis.Text);
                    break;
                case 4:
                    LblBetrag.Text = GetSumAllYearOneProject(Cb_Projekt.Text);
                    break;
                case 5:
                    LblBetrag.Text = GetSumOneYearOneProject(ComboBox_EvaluationYear.SelectedItem.ToString(), Cb_Projekt.Text);
                    break;
                case 6:
                    LblBetrag.Text = GetSumPeriodOneProject(Cb_Projekt.Text, Tb_Auswertung_von.Text, Tb_Auswertung_bis.Text);
                    break;
                default:
                    LblBetrag.Text = "0.00 €";
                    break;
            }
        }

        /// <summary>
        /// Gibt die Gesamtsumme aller Ausgaben zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        //private string GetSumAllYearAllProjectsORIGINAL()
        //{
        //    string sql_Select = "SELECT CAST (SUM (Menge * Artikelpreis) as DECIMAL (9,2)) AS Summe FROM Einkauf AS e " +
        //    "INNER JOIN Einkaufpositionen AS ep ON e.Einkauf_ID = ep.Einkauf_ID " +
        //    "INNER JOIN Artikel AS a ON a.Artikel_ID = ep.Artikel_ID " +
        //    "INNER JOIN Projekt AS p ON p.Projekt_ID = ep.Projekt_ID " +
        //    "INNER JOIN Artikel_Preis AS ap ON ap.Preis_ID = ep.Preis_ID ";

        //    using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
        //    using (SqlCommand command = new SqlCommand(sql_Select, sql_conn))
        //    {
        //        try
        //        {
        //            sql_conn.Open();
        //            decimal summe = (decimal)command.ExecuteScalar();
        //            sql_conn.Close();
        //            return summe.ToString("0.00") + " €";
        //        }
        //        catch
        //        {
        //            Lbl_Hinweis.Visible = true;
        //            Lbl_Hinweis.ForeColor = Color.Red;
        //            Lbl_Hinweis.Text = "Für den ausgewählten Zeitraum sind keine  Ausgaben vorhanden!";
        //            LblBetrag.Enabled = false;
        //            return LblBetrag.Text = "0";
        //        }
        //    }
        //}
        private string GetSumAllYearAllProjects()
        {
            var x = DbConnect.Sum(1, "", "", "", "");
            if (x != null)
                return x;
            else
            {
                return NotFoundMessage();
            }
        }

        /// <summary>
        /// Gibt die Summe für alle Projekte während eines bestimmten Jahres zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        
        private string GetSumOneYearAllProjects(string year)
        {
            var x = DbConnect.Sum(2, "", year, "", "");
            if (x != null)
                return x;
            else
            {
                return NotFoundMessage();
            }
        }

        /// <summary>
        /// Gibt die Summe aller Projekte während einer bestimmten Zeitspanne zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetSumPeriodAllProjects(string dateBegin, string dateEnd)
        {
            var x = DbConnect.Sum(3, "", "", dateBegin, dateEnd);
            if (x != null)
                return x;
            else
            {
                return NotFoundMessage();
            }
        }

        /// <summary>
        /// Gibt die Summe eines Projekte über den gesamten Zeitraum zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetSumAllYearOneProject(string projectName)
        {
            var x = DbConnect.Sum(4, projectName, "", "", "");
            if (x != null)
                return x;
            else
            {
                return NotFoundMessage();
            }
        }

        /// <summary>
        /// Gibt die Summe eines Projekte während einer bestimmten Jahres zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetSumOneYearOneProject(string year, string projectName)
        {
            var x = DbConnect.Sum(5, projectName, year, "", "");
            if (x != null)
                return x;
            else
            {
                return NotFoundMessage();
            }
        }
        
        /// <summary>
        /// Gibt die Summe eines Projektes während einer bestimmten Zeitspanne zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetSumPeriodOneProject(string projectName, string dateBegin, string dateEnd)
        {
            var x = DbConnect.Sum(6, projectName, "", dateBegin, dateEnd);
            if (x != null)
                return x;
            else
            {
                return NotFoundMessage();
            }
        }

        /// <summary>
        /// Funktion zum Laden aller vorhandenen Projekte aus der Datenbank
        /// </summary>
        private void LoadAllProjects()
        {
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_con = new SqlConnection(DbConnect.Conn))
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
            Lbl_Hinweis.Text = "";
            LblBetrag.Text = "";
        }

        private void Tb_Auswertung_von_Click(object sender, EventArgs e)
        {
            Lbl_Hinweis.Text = "";
            LblBetrag.Text = "";
            startEnd = 1;
            monthCalendar1.Visible = true;
        }

        private void Tb_Auswertung_bis_Click(object sender, EventArgs e)
        {
            Lbl_Hinweis.Text = "";
            LblBetrag.Text = "";
            startEnd = 2;
            monthCalendar1.Visible = true;
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
                    if (Tb_Auswertung_von.Text != " ")
                        if (Convert.ToDateTime(Tb_Auswertung_bis.Text) < Convert.ToDateTime(Tb_Auswertung_von.Text))
                            Tb_Auswertung_von.Text = Tb_Auswertung_bis.Text;
                    if (Tb_Auswertung_von.Text == " " || Tb_Auswertung_von.Text == "")
                        Tb_Auswertung_von.Text =Tb_Auswertung_bis.Text;
                    monthCalendar1.Visible = false;
                    break;
            }
        }

        private void Rb_EvaluationByPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if(Rb_EvaluationByPeriod.Checked)
            {
                ComboBox_EvaluationYear.Visible = false;
                Lbl_Hinweis.Text = "";
                LblBetrag.Text = "";

                Tb_Auswertung_von.Visible = true;
                Tb_Auswertung_bis.Visible = true;
                Lbl_Datum_von.Visible = true;
                Lbl_Datum_bis.Visible = true;
                monthCalendar1.Visible = true;
                Tb_Auswertung_von.Focus();
                startEnd = 1;
            }
        }

        private void Rb_EvaluationToYear_CheckedChanged(object sender, EventArgs e)
        {
            Lbl_Hinweis.Text = "";
            LblBetrag.Text = "";
            monthCalendar1.Visible = false;
            Tb_Auswertung_von.Visible = false;
            Tb_Auswertung_von.Clear();
            Tb_Auswertung_bis.Visible = false;
            Tb_Auswertung_bis.Clear();
            Lbl_Datum_von.Visible = false;
            Lbl_Datum_bis.Visible = false;
            ComboBox_EvaluationYear.Items.Clear();
            ComboBox_EvaluationYear.Visible = true;

            var s = Convert.ToInt32(DateTime.Now.Year);
            var count = s - 2014;

            for (int i = 0; i <= count; i++)
            {
                if (i == 0)
                    ComboBox_EvaluationYear.Items.Add("Alle Jahre");
                ComboBox_EvaluationYear.Items.Add(2014 + i);
            }
            ComboBox_EvaluationYear.SelectedIndex = 0;
        }

        private void ComboBox_EvaluationYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lbl_Hinweis.Text = "";
            LblBetrag.Text = "";
        }

        private string NotFoundMessage()
        {
            Lbl_Hinweis.Visible = true;
            Lbl_Hinweis.ForeColor = Color.Red;
            Lbl_Hinweis.Text = "Keine Ausgaben vorhanden!";
            LblBetrag.Enabled = false;
            return LblBetrag.Text = "0";
        }

        private void CheckBox_Evaluation_All_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_Evaluation_All.Checked)
                Cb_Projekt.Visible = false;
            else
                Cb_Projekt.Visible = true;
        }
    }
}
