﻿using System;
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
        static string conn = Properties.Settings.Default.GartenDB;
        //static string conn = Properties.Settings.Default.GartenProjekteConnectionString;

        internal static int Db_execute(string sql_Insert)
        {
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {
                if (sql_conn.State != ConnectionState.Open) sql_conn.Open();
                SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

                int nResult = sql_com.ExecuteNonQuery();
                sql_conn.Close();
                return nResult;
            }
            
        }
        internal static bool EqualsArtikel(string artikel)
        {
            //Abfrage-String für alle Namen aus der Händler Tabelle
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
                return false;
            }
        }
        internal static int AddNeuerArtikel(string newName, string connString)
        {
            int newArtikelID = 0;
            string sql =
                "INSERT INTO Artikel (Artikelbezeichnung) VALUES (@Artikelbezeichnung); "
                + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Artikelbezeichnung", SqlDbType.VarChar);
                cmd.Parameters["@Artikelbezeichnung"].Value = newName;
                try
                {
                    conn.Open();
                    newArtikelID = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newArtikelID;
        }
    }
}
