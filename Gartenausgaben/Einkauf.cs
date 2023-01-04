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
    public class Einkauf
    {
        public int AnzahlEinkaufpositionen { get; set; }
        public int Db_AnzahlEinkaufpositionen { get; private set; }
        public int Id { get; set; }
        public int HaendlerId { get; set; }
        public DateTime Datum { get; set; }
        public decimal Summe { get; set; }

        /// <summary>
        /// Funktion zum Setzen einer neuen Einkauf_ID in der Datenbank
        /// </summary>
        /// <returns>Einkauf_ID</returns>
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
        /// <summary>
        /// Funktion zum Prüfen, ob der aktuelle Bon/Kassenzettel schon existiert. <para/>
        /// Prüfung erfolgt auf Datum, Händler und Gesamtsumme des Beleges
        /// </summary>
        /// <returns>true -> Bon existiert</returns>
        public bool EinkaufIstVorhanden(int haendler_id, decimal summe, string datum, int anzahlEinkaufpositionen)
        {
            this.Summe = summe;
            this.AnzahlEinkaufpositionen = anzahlEinkaufpositionen;
            Einkauf[] einkauf = null;

            string querySql = "SELECT e.Einkauf_ID, e.Haendler_ID, SUM(AP.Artikelpreis * EP.Menge) AS Summe, COUNT (ep.Einkauf_ID) AS Anzahl " +
                "FROM Artikel_Preis AS ap " +
                "INNER JOIN Einkaufpositionen AS ep ON ep.Preis_ID = ap.Preis_ID " +
                "INNER JOIN Einkauf AS e ON e.Einkauf_ID = ep.Einkauf_ID " +
                "WHERE e.Datum = @Datum " +
                "GROUP BY e.Einkauf_ID, e.Haendler_ID";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(querySql, sql_conn))
            {

                //command.Parameters.AddWithValue("@Haendler", haendler_id);
                command.Parameters.AddWithValue("@Datum", datum);

                try
                {
                    if (sql_conn.State != ConnectionState.Open)
                        sql_conn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var list = new List<Einkauf>();

                        while (reader.Read())
                        {
                            list.Add(new Einkauf { Id = reader.GetInt32(0), HaendlerId = reader.GetInt32(1), Summe = reader.GetDecimal(2), Db_AnzahlEinkaufpositionen = reader.GetInt32(3) });
                            einkauf = list.ToArray();
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                sql_conn.Close();
            }
            if (einkauf != null)
            {
                var count = 0;
                foreach (var item in einkauf)
                {
                    if (haendler_id == item.HaendlerId && Summe == item.Summe && AnzahlEinkaufpositionen == item.Db_AnzahlEinkaufpositionen)
                        count++;
                }
                if (count > 0)
                {
                    var result = MessageBox.Show("Ein oder mehrere Kassenbeleg(e) von diesem Händler und dem selben Tag existiert schon (Anzahl = "
                        + count + "x ). " + "\n" + "Möchten Sie diesen Beleg erneut speichern?", "Achtung", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        return false;
                    }
                    else
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Funktion zum Abrufen der Gesamtanzahl von Einkaufpositionen, eines Einkaufs an einem bestimmten Tag
        /// </summary>
        /// <returns>true -> Bon existiert</returns>
        private int CountEinkaufpostionen(string datum)
        {
            var count = 0;
            string sql_Anzahl = "SELECT COUNT (ep.Einkauf_ID) AS Anzahl FROM Einkaufpositionen AS ep " +
                "JOIN Einkauf e ON e.Einkauf_ID = ep.Einkauf_ID " +
                "WHERE e.Datum = @Datum " +
                "GROUP BY e.Einkauf_ID";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Anzahl, sql_conn))
            {
                command.Parameters.AddWithValue("@Datum", datum);

                try
                {
                    sql_conn.Open();
                    count = (Int32)command.ExecuteScalar();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                sql_conn.Close();
            }
            return count;
        }
    }
}
