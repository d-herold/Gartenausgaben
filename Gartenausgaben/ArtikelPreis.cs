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
        int preisId;
        int artikelHaendlerId;
        public int PreisId
        {
            get { return preisId; }
            private set { preisId = value; }
        }
        public int ArtikelHaendlerId
        {
            get { return artikelHaendlerId; }
            private set { artikelHaendlerId = value; }
        }
        public decimal Preis { get; set; }

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <returns></returns>
        //public bool VergleichPreis()
        //{
        //    Invoice test = new Invoice();
        //    var x = test.ArtikelPreisID;

        //    using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
        //    {
        //        string sql_Select = "SELECT * FROM ";
        //        string sql_Select_Haendler = "SELECT * FROM ";
        //        //string sql_Select_ArtikelHaendler = "SELECT * FROM " + list[0] + " WHERE Artikel_ID = @Artikel_ID AND Haendler_ID = @Haendler_ID";

        //        SqlDataAdapter adapterArtikel = new SqlDataAdapter(sql_Select, sql_conn);
        //        SqlDataAdapter adapterHaendler = new SqlDataAdapter(sql_Select_Haendler, sql_conn);
        //        //SqlDataAdapter adapterArtikelHaendler = new SqlDataAdapter(sql_Select_ArtikelHaendler, sql_conn);
        //        //adapterHaendler.SelectCommand.Parameters.AddWithValue("@Ort", SqlDbType.VarChar).Value = ort;
        //        //adapterArtikelHaendler.SelectCommand.Parameters.AddWithValue("@Artikel_ID", SqlDbType.Int).Value = artikelId;
        //        //adapterArtikelHaendler.SelectCommand.Parameters.AddWithValue("@Haendler_ID", SqlDbType.Int).Value = haendlerId;

        //        DataTable dt = new DataTable();
        //        sql_conn.Open();

        //        if (x != 0)
        //        { }
        //        if (0 == 0)
        //            return false;
        //    }
        //}

        /// <summary>
        /// Gibt die Artikelpreis_ID zurück
        /// </summary>
        /// <param name="artikelHaendler_ID"></param>
        /// <param name="preis"></param>
        /// <param name="datum"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int ID(int artikelhaendler_id, decimal preis)
        {
            this.Preis = preis;
            ArtikelHaendlerId = artikelhaendler_id;
            this.preisId = 0;
            var count = Count_Ergebnis();

            string sql_Select_ArtikelPreis = "SELECT Preis_ID, Artikelpreis FROM Artikel_Preis " +
                "WHERE ArtikelHaendler_ID = @ArtikelHaendler_ID";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Select_ArtikelPreis, sql_conn))
            using (SqlDataAdapter adapterArtikelPreis = new SqlDataAdapter(sql_Select_ArtikelPreis, sql_conn))
            {
                adapterArtikelPreis.SelectCommand.Parameters.AddWithValue("@ArtikelHaendler_ID", ArtikelHaendlerId);
                adapterArtikelPreis.SelectCommand.Parameters.AddWithValue("@Artikelpreis", Preis);
                command.Parameters.AddWithValue("@ArtikelHaendler_ID", ArtikelHaendlerId);
                command.Parameters.AddWithValue("@Artikelpreis", Preis);

                DataTable dt = new DataTable();

                try
                {
                    sql_conn.Open();
                    if (count == 0)
                        preisId = InsertArtikelPreis();
                    else
                    {
                        adapterArtikelPreis.Fill(dt);

                        foreach (DataRow row in dt.Rows)
                        {
                            if ((decimal)row.ItemArray[1] == Preis)
                            {
                                preisId = (int)row.ItemArray[0];
                                return preisId;
                            }
                        }
                        if (preisId == 0)
                            preisId = InsertArtikelPreis();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_conn.Close();
            }
            return preisId;
        }

        private int Count_Ergebnis()
        {
            int count = 0;
            string sql_Select_ArtikelPreis_Count = "SELECT COUNT (Preis_ID) FROM Artikel_Preis " +
                "WHERE ArtikelHaendler_ID = @ArtikelHaendler_ID";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Select_ArtikelPreis_Count, sql_conn))
            {
                
                command.Parameters.AddWithValue("@ArtikelHaendler_ID", artikelHaendlerId);
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
        private int InsertArtikelPreis()
        {
            string sql_Insert = "INSERT INTO Artikel_Preis (ArtikelHaendler_ID, Artikelpreis) " +
                "VALUES (@ArtikelHaendler_ID, @Artikelpreis); " +
                "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@ArtikelHaendler_ID", artikelHaendlerId);
                command.Parameters.AddWithValue("@Artikelpreis", Preis);
                try
                {
                    sql_conn.Open();
                    preisId = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen der Artikel_Preis_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
            return preisId;
        }
    }
}
