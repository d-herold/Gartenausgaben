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
    public class Artikel
    {
        string artikelbezeichnung;
        int artikelId;
        
        public List<string> Artikelliste{ get; set ; }
        
        internal int ArtikelId
        { 
            get
            {
                return artikelId;
            }
            set
            {
                artikelId = value;
            }
                 
        }
        internal string Artikelbezeichnung
        { 
            get
            {
                return artikelbezeichnung;
            }
            
            set 
            {
                artikelbezeichnung = value;
            } 
        }

        public Artikel() 
        { }
        public Artikel (string artikelbezeichnung)
        {
            this.artikelbezeichnung = artikelbezeichnung;
        }

        public int ID(string artikelname)
        {
            int id = 0;
            Artikelbezeichnung = artikelname;

            string sql_Select_Artikel = "SELECT * FROM Artikel" +
                    " WHERE Artikelbezeichnung = @Artikelbezeichnung";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn)) // Connection String aus der App.config
            using (SqlCommand command = new SqlCommand(sql_Select_Artikel, sql_conn))
            {
                command.Parameters.AddWithValue("@Artikelbezeichnung", Artikelbezeichnung);
                try
                {
                    sql_conn.Open();
                    id = (Int32)command.ExecuteScalar();
                    ArtikelId = id;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_conn.Close();
            }
            return id;
        }

        public void GetArtikelliste()
        {
            List<string> artikelliste = new List<string>();
            DataTable dt = new DataTable();
            string sql_Select_Artikel = "SELECT Artikelbezeichnung FROM Artikel ORDER BY Artikelbezeichnung ASC";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlDataAdapter adapterArtikel = new SqlDataAdapter(sql_Select_Artikel, sql_conn))
            {
                try
                {
                    sql_conn.Open();

                    //Artikelliste laden
                    adapterArtikel.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        artikelliste.Add(row.ItemArray[0].ToString().Trim());
                    }
                    Artikelliste = artikelliste;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Achtung: " + ex.Message);
                }
                sql_conn.Close();
            }
        }
    }
}
