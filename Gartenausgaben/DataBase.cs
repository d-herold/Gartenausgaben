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
    class DataBase
    {
        
        public List<string> ArtikelPreis { get; private set; }

        public void LoadTableArtikelPreis()
        {
            var ArtikelPreis = new List<string>();

            var dbArtikelPreis = new DataTable();
            var dataset = new DataSet();

            //Holen der Daten aus der Datenbank
            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            {
                string querySql = "SELECT * FROM Artikel_Preis";

                try
                {
                    sql_conn.Open();

                    //Erstellt einen Adapter um die Daten aus der DB-Tabelle in eine Tabelle zu laden
                    SqlDataAdapter sql_adapt = new SqlDataAdapter(querySql, sql_conn);

                    sql_adapt.Fill(dbArtikelPreis);

                    dataset.Tables.Add(dbArtikelPreis);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Achtung: " + ex.Message);
                }
                sql_conn.Close();
            }

            foreach (DataColumn colum in dbArtikelPreis.Columns)
            {
                ArtikelPreis.Add("");
            }
        }












    }
}
