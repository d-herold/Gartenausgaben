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
    public class Haendler
    {
        public int HaendlerId { get; set; }
        public string Name { get; set; }
        public string Anschrift { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }

        public List<(string, string)> HaendlerListe { get; set; }
        

        public Haendler () { }
        public Haendler (string name, string ort )
        {
            Name = name;
            Ort = ort;
            ID();
        }

        private int ID()
        {
            int id = 0;

            string sql_Select_Haendler = "SELECT * FROM Haendler WHERE Name = @Haendlername AND Ort = @Ort";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Select_Haendler, sql_conn))
            {
                command.Parameters.AddWithValue("@Haendlername", Name);
                command.Parameters.AddWithValue("@Ort", Ort);
                try
                {
                    sql_conn.Open();
                    this.HaendlerId = (Int32)command.ExecuteScalar();
                    id = (Int32)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_conn.Close();
            }
            return id;
        }
        public void GetHaendlerListe()
        {
            List<(string, string)> haendler = new List<(string, string)>() ;
            DataTable dt = new DataTable();
            string sql_Select_Haendler = "SELECT Name, Ort FROM Haendler ORDER BY Name ASC";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlDataAdapter adapterHaendler = new SqlDataAdapter(sql_Select_Haendler, sql_conn))
            {
                try
                {
                    sql_conn.Open();

                    //Artikelliste laden
                    adapterHaendler.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        haendler.Add((row.ItemArray[0].ToString().Trim(), row.ItemArray[1].ToString()));
                    }
                    HaendlerListe = haendler;
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
