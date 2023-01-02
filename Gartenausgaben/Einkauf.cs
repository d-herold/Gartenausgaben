using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public class Einkauf
    {
        private decimal summe;
        public int Id { get; set; }
        public int HaendlerId { get; set; }
        public DateTime Datum { get; set; }
        public decimal Summe
        { 
            get
            {
                return summe;
            } 
            set
            {
                summe = Decimal.Round(value, 2);
            }
        }
        public int SetNewEinkaufId(int haendler_id, DateTime dateTime)
        {
            string sql_Insert = "INSERT INTO Einkauf (Datum, Haendler_ID) " + "VALUES (@Datum, @HaendlerID); " + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@Datum", dateTime);
                //command.Parameters.AddWithValue("@Datum", dateTimePickerDatum.Value.Date);
                command.Parameters.AddWithValue("@HaendlerID", haendler_id);
                try
                {
                    sql_conn.Open();
                    Id = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen einer neuen Einkauf_ID aufgetreten", "Achtung", MessageBoxButtons.OK);
                }
            }
            return Id;
        }

    }
}
