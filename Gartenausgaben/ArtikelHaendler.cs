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
        //int haendlerId;
        int artikel_haendlerId;

        //public int ArtikelId { get; set; }
        public int Artikel_HaendlerID
        {
            get { return artikel_haendlerId; }
            private set { artikel_haendlerId = value; }
        }
        public int ArtikelId 
        {
            get { return artikelId; }
            set { artikelId = value; }
        }
        public int HaendlerId { get; set; }

        public ArtikelHaendler() { }

        public ArtikelHaendler(int artikel_id, int haendler_id)
        {
            ArtikelId = artikel_id;
            HaendlerId = haendler_id;
        }

        public int ID (int artikel_id, int haendler_id)
        {
            this.ArtikelId = artikel_id;
            this.HaendlerId = haendler_id;

            //int artikel_haendlerId = 0;

            string sql_Select_ArtikelHaendler = "SELECT * FROM Artikel_Haendler WHERE Artikel_ID = @ArtikelID AND Haendler_ID = @HaendlerID ";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Select_ArtikelHaendler, sql_conn))
            {
                command.Parameters.AddWithValue("@ArtikelID", ArtikelId);
                command.Parameters.AddWithValue("@HaendlerID", HaendlerId);
                try
                {
                    sql_conn.Open();
                    if (command.ExecuteScalar() == null)
                        artikel_haendlerId = InsertArtikelHaendler();
                    else
                        artikel_haendlerId = (Int32)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_conn.Close();
            }
            return artikel_haendlerId;
        }

        public int InsertArtikelHaendler()
        {
            string sql_Insert = "INSERT INTO Artikel_Haendler (Artikel_ID, Haendler_ID) " + "VALUES (@Artikel_ID, @Haendler_ID); " + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@Artikel_ID", ArtikelId);
                command.Parameters.AddWithValue("@Haendler_ID", HaendlerId);
                try
                {
                    sql_conn.Open();
                    Artikel_HaendlerID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen der Artikel_Händler_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
            return Artikel_HaendlerID;
        }

        public int Count_Ergebnis(int artikelhaendlerId)
        {
            int count = 0;
            string sql_Select_ArtikelPreis_Count = "SELECT COUNT (ap.Preis_ID) FROM Artikel_Preis AS ap " +
                "JOIN Einkaufpositionen ep ON ep.Preis_ID = ap.Preis_ID " +
                "WHERE ArtikelHaendler_ID = @ArtikelHaendler_ID";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
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
