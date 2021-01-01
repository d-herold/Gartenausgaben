using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class NeuerHaendler : Form
    {
        public NeuerHaendler()
        {
            InitializeComponent();
        }

        private void btnNeuerHaendlerSpeichern_Click(object sender, EventArgs e)
        {
            SetNeuerHaendler();
            this.Close();
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
        private void SetNeuerHaendler()
        {
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            string name = txbNeuerHaendlerName.Text;
            string strasse = txbNeuerHaendlerStrasse.Text;
            string plz = txbNeuerHaendlerPlz.Text;
            string ort = txbNeuerHaendlerOrt.Text;
            string telefon = txbNeuerHaendlerTelefon.Text;

            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                string sql_Select = "SELECT Name, Ort FROM Haendler WHERE Name = @Name AND Ort = @Ort ";
                string sql_Insert = "INSERT INTO Haendler (Name , Anschrift , PLZ , Ort , Telefon ) " +
                    "VALUES (@Name, @Anschrift, @PLZ, @Ort, @Telefon) ";

                SqlCommand sql_command = new SqlCommand(sql_Select, sql_conn);
                // Setzten der Paramter für WHERE
                sql_command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                sql_command.Parameters.Add("@Ort", SqlDbType.VarChar).Value = ort;

                sql_conn.Open();

                if (sql_command.ExecuteScalar() == null)
                {
                    SqlCommand sql_command2 = new SqlCommand(sql_Insert, sql_conn);

                    sql_command2.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                    sql_command2.Parameters.Add("@Anschrift", SqlDbType.VarChar).Value = strasse;
                    sql_command2.Parameters.Add("@PLZ", SqlDbType.VarChar).Value = plz;
                    sql_command2.Parameters.Add("@Ort", SqlDbType.VarChar).Value = ort;
                    sql_command2.Parameters.Add("@Telefon", SqlDbType.VarChar).Value = telefon;

                    sql_command2.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Der Händler ist schon vorhanden", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                sql_conn.Close();
            }

            //DbConnect.Db_execute(sql_Insert);
        }

        private void btnNeuerHaendlerAbbrechen_Click(object sender, EventArgs e)
        {
            if (txbNeuerHaendlerTelefon.Text == "" && txbNeuerHaendlerName.Text == "" && 
                txbNeuerHaendlerStrasse.Text == "" && txbNeuerHaendlerPlz.Text == "" && txbNeuerHaendlerOrt.Text == "")
                Close();
            else
                MessageBox.Show("Die Daten werden nicht gespeichert", "Abbrechen", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}
