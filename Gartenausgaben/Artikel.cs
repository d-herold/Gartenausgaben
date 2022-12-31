using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public class Artikel : Interface1
    {
        string artikelbezeichnung;
        int artikelId;

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

        string conn = Properties.Settings.Default.GartenDB; // Connection String aus der App.config

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

            using (SqlConnection sql_conn = new SqlConnection(conn))
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

        public int IGetId(params object[] list)
        {
            throw new NotImplementedException();
        }
    }
}
