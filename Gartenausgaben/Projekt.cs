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
    public class Projekt
    {
        readonly string conn = Properties.Settings.Default.GartenDB; // Connection String aus der App.config
        public int Id { get; set; }
        public string Projektname { get; set; }
        public List<string> ProjektListe { get; set; }

        public Projekt() { }

        public int ID()
        {
            int Id = 0;

            string sql_Select_Projekt = "SELECT * FROM Projekt" +
                    " WHERE Projektname = @Projektname";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_Select_Projekt, sql_conn))
            {
                command.Parameters.AddWithValue("@Projektname", Projektname);
                try
                {
                    sql_conn.Open();
                    if (command.ExecuteScalar() != null)
                        Id = (Int32)command.ExecuteScalar();
                    else
                        Id = AddNewProject(Projektname);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception Message: " + ex.Message);
                }
                sql_conn.Close();
            }
            return Id;
        }

        public int AddNewProject(string newName)
        {
            int newProjectID = 0;
            string sql_newProjekt = "INSERT INTO Projekt (Projektname)" + "VALUES (@NewProjectName);" +
                "SELECT CAST(scope_identity() AS int)";

            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(sql_newProjekt, sql_conn))
            {
                command.Parameters.AddWithValue("@NewProjectName", newName);
                try
                {
                    sql_conn.Open();
                    newProjectID = (int)command.ExecuteScalar();
                    sql_conn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return newProjectID;
        }

        public bool EqualsProject(string project)
        {
            string querySql = "SELECT Projekt_ID FROM Projekt " + "Where Projektname = @NewProjcetName";

            // Connection String aus der App.config 
            //Erstellt eine neue Verbindund zur übergebenen Datenbank
            using (SqlConnection sql_conn = new SqlConnection(conn))
            using (SqlCommand command = new SqlCommand(querySql, sql_conn))
            {
                command.Parameters.AddWithValue("@NewProjcetName", project);
                try
                {
                    if (sql_conn.State != ConnectionState.Open)
                        sql_conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                sql_conn.Close();
                return false;
            }
        }

        public void GetProjektListe()
        {
            List<string> projekt = new List<string>();
            DataTable dt = new DataTable();
            string sql_Select_Projekt = "SELECT Projektname FROM Projekt ORDER BY Projektname ASC";

            using (SqlConnection sql_conn = new SqlConnection(DbConnect.Conn))
            using (SqlDataAdapter adapterHaendler = new SqlDataAdapter(sql_Select_Projekt, sql_conn))
            {
                try
                {
                    sql_conn.Open();

                    //Artikelliste laden
                    adapterHaendler.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        projekt.Add(row.ItemArray[0].ToString());
                    }
                    ProjektListe = projekt;
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
