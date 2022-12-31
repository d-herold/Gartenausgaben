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
            Invoice test = new Invoice();
            var x = test.ArtikelPreisID;



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

                if (x != 0)
                {

                }



                if (0==0)
                    return false;

            }
        }

        /// <summary>
        /// Gibt die Artikelpreis_ID zurück
        /// </summary>
        /// <param name="artikelHaendler_ID"></param>
        /// <param name="preis"></param>
        /// <param name="datum"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ID(int artikelHaendler_ID, decimal preis, string datum, params object[] list)
        {
            var _preis = preis;
            var _datum = datum;
            var ahID = artikelHaendler_ID;
            int id = 0;

            string sql_Select_ArtikelPreis = "SELECT * FROM Artikel_Preis AS ap " +
                    "JOIN Einkaufpositionen ep ON ep.Preis_ID = ap.Preis_ID " +
                    "WHERE ArtikelHaendler_ID = @ArtikelHaendler_ID";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select_ArtikelPreis, sql_conn))
            {

                SqlDataAdapter adapterArtikelPreis = new SqlDataAdapter(sql_Select_ArtikelPreis, sql_conn);

                adapterArtikelPreis.SelectCommand.Parameters.AddWithValue("@ArtikelHaendler_ID", ahID);
                adapterArtikelPreis.SelectCommand.Parameters.AddWithValue("@Artikelpreis", _preis);

                DataTable dt = new DataTable();

                try
                {
                    sql_conn.Open();

                    /// <value>Gibt die ArtikelPreisID zuück</value>
                    if (list[0].ToString() == "Artikel_Preis")
                    {
                        adapterArtikelPreis.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row.ItemArray[1].ToString() == list[1].ToString() && row.ItemArray[2].ToString() == list[2].ToString())
                            {
                                id = (int)row.ItemArray[0];
                                return id;
                            }
                            else
                                id = 0;
                        }
                    }
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
