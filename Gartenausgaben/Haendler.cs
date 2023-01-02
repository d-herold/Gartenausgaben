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
        public int HaendlerId { get; set; }
        public string Name { get; set; }
        public string Anschrift { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }

        

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
    }
}
