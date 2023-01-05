using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    [Table(Name = "Einkaufspositionena")]
    public class Einkaufspositionen
    {
        public int ID { get; set; }
        public int ArtikelID { get; set; }
        public int ProjektID { get; set; }
        public int EinkaufID { get; set; }
        public int PreisID { get; set; }
        public int Menge { get; set; }


        int id; int artikelId; int projektId; int einkaufId; int preisId; int menge;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public int ArtikelId
        {
            get
            {
                return this.artikelId;
            }
            set
            {
                this.artikelId = value;
            }
        }
        public int ProjektId
        {
            get
            {
                return this.projektId;
            }
            set
            {
                this.projektId = value;
            }
        }
        public int EinkaufId
        {
            get
            {
                return this.einkaufId;
            }
            set
            {
                this.einkaufId = value;
            }
        }
        public int PreisId
        {
            get
            {
                return this.preisId;
            }
            set
            {
                this.preisId = value;
            }
        }
        public int Menge1 
        {
            get
            {
                return this.menge;
            }
            set
            {
                this.menge = value;
            }
        }
        private int SetEinkaufsposition(int artikel_id, int projekt_id, int einkauf_id, int artikelpreis_id, int menge)
        {
            string sql_Insert = "INSERT INTO Einkaufpositionen (Menge, Projekt_ID, Artikel_ID, Einkauf_ID, Preis_ID) " + "VALUES (@Menge,  @ProjektId, @ArtikelId, @EinkaufId, @PreisId); "
                + "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlCommand command = new SqlCommand(sql_Insert, sql_conn))
            {
                command.Parameters.AddWithValue("@ArtikelId", artikel_id);
                command.Parameters.AddWithValue("@ProjektId", projekt_id);
                command.Parameters.AddWithValue("@EinkaufId", einkauf_id);
                command.Parameters.AddWithValue("@PreisId", artikelpreis_id);
                command.Parameters.AddWithValue("@Menge", menge);
                try
                {
                    sql_conn.Open();
                    ID = (Int32)command.ExecuteScalar();
                    sql_conn.Close();
                    return ID;
                }
                catch
                {
                    MessageBox.Show("Es ist ein Fehler, beim Eintragen in der Tabelle \"Einkaufspositionen\" aufgetreten", "Achtung", MessageBoxButtons.OK);
                    return 0;
                }
            }

        }


    }
}
