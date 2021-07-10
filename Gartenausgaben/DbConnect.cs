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
        //static string conn = Properties.Settings.Default.GartenProjekteConnectionString;

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

        internal static bool EqualsProject(string project)
        {
            string querySql = "SELECT Projekt_ID FROM Projekt " + "Where Projektname = @NewProjcetName";

            // Connection String aus der App.config 
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(querySql, sql_conn);
                command.Parameters.AddWithValue("@NewProjcetName", project);
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

        /* Überarbeiten !!!!!!! */
        internal static bool EqualsDBEinträge(string select, string from, string where)
        {
            string querySql = "SELECT " + select + " FROM Projekt " + "Where Projektname = @NewProjcetName";

            // Connection String aus der App.config 
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand(querySql, sql_conn);
                command.Parameters.AddWithValue("@NewProjcetName", from);
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
        internal static int InsertArtikelHaendler()
        {
            var a = new Invoice();
            string sql_Insert = "INSERT INTO Artikel_Haendler (Artikel_ID, Haendler_ID) " + "VALUES (@Artikel_ID, @Haendler_ID); " + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@Artikel_ID", a.ArtikelID);
                command.Parameters.AddWithValue("@Haendler_ID", a.HaendlerID);
                try
                {
                    sql_conn.Open();
                    a.ArtikelHaendlerID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //MessageBox.Show("Es ist ein Fehler, beim Eintragen der Artikel_Händler_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
            return a.ArtikelHaendlerID;
        }
    }
}
