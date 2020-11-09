using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    internal static class DbConnect
    {
        internal static int Db_execute(string sql_Insert)
        {
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            SqlConnection sql_conn = new SqlConnection(conn);
            if (sql_conn.State != ConnectionState.Open) sql_conn.Open();
            SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

            int nResult = sql_com.ExecuteNonQuery();
            sql_conn.Close();
            return nResult;
        }
        internal static bool EqualsArtikel(string artikel)
        {
            // Connection String aus der App.config 
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            SqlConnection sql_con = new SqlConnection(conn);

            //Abfrage-String für alle Namen aus der Händler Tabelle
            string querySql = "SELECT Artikel_Id FROM Artikel Where ([Artikelbezeichnung] = @Artikelbezeichnung)";                 // '" + artikel + "'";

            SqlCommand command = new SqlCommand(querySql, sql_con);

            command.Parameters.AddWithValue("@Artikelbezeichnung", artikel);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
                
            else
            {
                reader.Close();
                return false;
            }
        }
        internal static void SetNeuerArtikel(string artikel)
        {
            //Abfrage-String um einen neuen Händler aus der TextBox zur Händler Tabelle hinzuzufügen
            string sql_Insert = "INSERT INTO Artikel (Artikelbezeichnung) VALUES ('" + artikel + "') ";

            Db_execute(sql_Insert);
        }
    }
}
