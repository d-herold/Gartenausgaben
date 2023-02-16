using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    internal static class DbConnect
    {
        static readonly string conn = Properties.Settings.Default.GartenDB;

        public static string Conn
        {
            get { return conn; }
        }

        //obsolete?
        internal static int Db_execute(string sql_Insert)
        {
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                if (sql_conn.State != ConnectionState.Open)
                    sql_conn.Open();
                SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

                int nResult = sql_com.ExecuteNonQuery();
                sql_conn.Close();
                return nResult;
            }

        }
        internal static bool EqualsArtikel(string artikel)
        {
            string querySql = "SELECT Artikel_ID FROM Artikel " + "Where Artikelbezeichnung = @Artikelbezeichnung";

            // Connection String aus der App.config 
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(querySql, sql_conn);
                command.Parameters.AddWithValue("@Artikelbezeichnung", artikel);
                try
                {
                    if (sql_conn.State != ConnectionState.Open)
                        sql_conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                sql_conn.Close();
                return false;
            }
        }

        internal static int AddNeuerArtikel(string newName, string connString)
        {
            int newArtikelID = 0;
            string sql =
                "INSERT INTO Artikel (Artikelbezeichnung)" + "VALUES (@Artikelbezeichnung); "
                + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Artikelbezeichnung", newName);
                try
                {
                    conn.Open();
                    newArtikelID = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newArtikelID;
        }

        internal static int AddNewProject(string newName, string connString)
        {
            int newProjectID = 0;
            string sql =
                "INSERT INTO Projekt (Projektname)" + "VALUES (@NewProjectName); "
                + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NewProjectName", newName);
                try
                {
                    conn.Open();
                    newProjectID = (int)cmd.ExecuteScalar();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newProjectID;
        }

        internal static string Sum(int _where, string projectName, string year, string dateBegin, string dateEnd)
        {
            string where;
            switch(_where)
            {
                case 1:
                    where = "";
                    break;
                case 2:
                    where = "WHERE Year(e.Datum) = @Year";
                    break;
                case 3:
                    where = "WHERE Datum Between @DateBegin AND @DateEnd";
                    break;
                case 4:
                    where = "WHERE Projektname = @ProjectName";
                    break;
                case 5:
                    where = "WHERE Projektname = @ProjectName AND Year(e.Datum) = @Year";
                    break;
                case 6:
                    where = "WHERE Projektname = @ProjectName AND Datum Between @DateBegin AND @DateEnd";
                    break;
                default:
                    where = "";
                    break;
            }

            string sql_Select = "SELECT CAST (SUM (Menge * Artikelpreis) as DECIMAL (9,2)) AS Summe FROM Einkauf AS e " +
            "INNER JOIN Einkaufpositionen AS ep ON e.Einkauf_ID = ep.Einkauf_ID " +
            "INNER JOIN Artikel AS a ON a.Artikel_ID = ep.Artikel_ID " +
            "INNER JOIN Projekt AS p ON p.Projekt_ID = ep.Projekt_ID " +
            "INNER JOIN Artikel_Preis AS ap ON ap.Preis_ID = ep.Preis_ID " +
            where;

            using (SqlConnection sql_conn = new SqlConnection(Conn))
            using (SqlCommand command = new SqlCommand(sql_Select, sql_conn))
            {
                command.Parameters.AddWithValue("@ProjectName", projectName);
                command.Parameters.AddWithValue("@Year", year);
                command.Parameters.AddWithValue("@DateBegin", dateBegin);
                command.Parameters.AddWithValue("@DateEnd", dateEnd);

                try
                {
                    sql_conn.Open();
                    decimal summe = (decimal)command.ExecuteScalar();
                    sql_conn.Close();
                    return summe.ToString("0.00") + " €";
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
