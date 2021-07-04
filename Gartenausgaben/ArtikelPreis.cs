using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    public class ArtikelPreis
    {
        //string conn = Properties.Settings.Default.GartenProjekteConnectionString; // Connection String aus der App.config
        readonly string conn = Properties.Settings.Default.GartenDB;
        public int PreisId { get; set; }
        public int ArtikelHaendlerId { get; set; }
        public int Preis { get; set; }
        public DateTime Datum { get; set; }

        public bool VergleichPreis()
        {
            using (SqlConnection sql_conn = new SqlConnection(conn))
            {

                string sql_Select = "SELECT * FROM ";
                string sql_Select_Haendler = "SELECT * FROM ";
                //string sql_Select_ArtikelHaendler = "SELECT * FROM " + list[0] + " WHERE Artikel_ID = @Artikel_ID AND Haendler_ID = @Haendler_ID";

                SqlDataAdapter adapterArtikel = new SqlDataAdapter(sql_Select, sql_conn);
                SqlDataAdapter adapterHaendler = new SqlDataAdapter(sql_Select_Haendler, sql_conn);
                //SqlDataAdapter adapterArtikelHaendler = new SqlDataAdapter(sql_Select_ArtikelHaendler, sql_conn);
                //adapterHaendler.SelectCommand.Parameters.AddWithValue("@Ort", SqlDbType.VarChar).Value = ort;
                //adapterArtikelHaendler.SelectCommand.Parameters.AddWithValue("@Artikel_ID", SqlDbType.Int).Value = artikelId;
                //adapterArtikelHaendler.SelectCommand.Parameters.AddWithValue("@Haendler_ID", SqlDbType.Int).Value = haendlerId;

                DataTable dt = new DataTable();
                sql_conn.Open();

                if (0==0)
                    return false;

            }
        }
    }
}
