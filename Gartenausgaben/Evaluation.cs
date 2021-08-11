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
            string sql_Select = "SELECT SUM(AP.Artikelpreis*EP.Menge) AS Summe FROM Artikel_Preis AS AP " +
            "INNER JOIN Artikel_Haendler AS AH ON AH.ArtikelHaendler_ID = AP.ArtikelHaendler_ID " +
            "INNER JOIN Artikel AS A ON A.Artikel_ID = AH.Artikel_ID " +
            "INNER JOIN Einkaufpositionen AS EP ON EP.Artikel_ID = A.Artikel_ID " +
            "INNER JOIN Projekt AS P ON P.Projekt_ID = EP.Projekt_ID " +
            "INNER JOIN Einkauf AS E ON E.Einkauf_ID = EP.Einkauf_ID " +
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
        public void LadeProjekte()
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
    }
}
