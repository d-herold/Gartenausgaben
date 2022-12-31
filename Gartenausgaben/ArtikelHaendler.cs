using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public class ArtikelHaendler
    {
        int artikelId;
        int haendlerId;

        //public int ArtikelId { get; set; }

        public int ArtikelId 
        {
            get { return artikelId; }
            set { artikelId = value; }
        }
        public int HaendlerId { get; set; }

        string conn = Properties.Settings.Default.GartenDB; // Connection String aus der App.config

        public ArtikelHaendler() { }

        public ArtikelHaendler(int id)
        {
            artikelId = id;
        }

        public int ID (int haendler_id , int artikel_id)
        {
            this.artikelId = artikel_id;
            this.haendlerId = haendler_id;

            //int id = 0;
            //char[] charToTrim = { ',', ' ' };
            //string ort = lbl_Haendler.Text.Substring(lbl_Haendler.Text.IndexOf(',')).TrimStart(charToTrim);

            int id = 0;

            string sql_Select_ArtikelHaendler = "SELECT * FROM Artikel_Haendlers AS ah " +
                    "JOIN Artikel AS a ON ah.Artikel_ID = a.Artikel_ID " +
                    "WHERE a.Artikel_ID = @Artikel_ID";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select_ArtikelHaendler, sql_conn))
            {
                command.Parameters.AddWithValue("@Artikel_ID", artikelId);
                try
                {
                    sql_conn.Open();
                    id = (Int32)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_conn.Close();
            }
            return id;
        }

        public int Count_Ergebnis(int artikelhaendlerId)
        {
            int count = 0;
            string sql_Select_ArtikelPreis_Count = "SELECT COUNT (ap.Preis_ID) FROM Artikel_Preis AS ap " +
                "JOIN Einkaufpositionen ep ON ep.Preis_ID = ap.Preis_ID " +
                "WHERE ArtikelHaendler_ID = @ArtikelHaendler_ID";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select_ArtikelPreis_Count, sql_conn))
            {
                try
                {
                    sql_conn.Open();
                    count = (Int32)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
            }
            return count;
        }
    
    }
}
