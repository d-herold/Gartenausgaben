using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public class Haendler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Anschrift { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }

        string conn = Properties.Settings.Default.GartenDB; // Connection String aus der App.config

        public Haendler () { }
        public Haendler (string name )
        {
            Name = name;
        }

        public int ID(string name)
        {
            int id = 0;

            string sql_Select_Haendler = "SELECT * FROM Haendlers WHERE a.Artikel_ID = @Artikel_ID";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select_Haendler, sql_conn))
            {
                command.Parameters.AddWithValue("@Artikel_ID", id);
                try
                {
                    sql_conn.Open();
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
    }
}
