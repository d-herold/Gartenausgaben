using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gartenausgaben
{
    public partial class NeuerHaendler : Form
    {
        public NeuerHaendler()
        {
            InitializeComponent();
        }

        private void btnNeuerHaendlerSpeichern_Click(object sender, EventArgs e)
        {
            SetNeuerHaendler();
            this.Close();
        }
        private int Db_execute(string sql_Insert)
        {
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            SqlConnection sql_conn = new SqlConnection(conn);
            if (sql_conn.State != ConnectionState.Open) sql_conn.Open();
            SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

            int nResult = sql_com.ExecuteNonQuery();
            sql_conn.Close();
            return nResult;
        }
        private void SetNeuerHaendler()
        {
            string name = txbNeuerHaendlerName.Text;
            string strasse = txbNeuerHaendlerStrasse.Text;
            string plz = txbNeuerHaendlerPlz.Text;
            string ort = txbNeuerHaendlerOrt.Text;
            string telefon = txbNeuerHaendlerTelefon.Text;

            //Abfrage-String um einen neuen Händler aus der TextBox zur Händler Tabelle hinzuzufügen
            string sql_Insert = "INSERT INTO Haendler (Name , Anschrift , PLZ , Ort , Telefon ) " +
                "VALUES ('" + name + "' , '" + strasse + "' , '" + plz + "' , '" + ort + "' , '" + telefon + "') ";

            DbConnect.Db_execute(sql_Insert);
        }

        private void btnNeuerHaendlerAbbrechen_Click(object sender, EventArgs e)
        {
            if (txbNeuerHaendlerTelefon.Text == "" && txbNeuerHaendlerName.Text == "" && 
                txbNeuerHaendlerStrasse.Text == "" && txbNeuerHaendlerPlz.Text == "" && txbNeuerHaendlerOrt.Text == "")
                Close();
            else
                MessageBox.Show("Die Daten werden nicht gespeichert", "Abbrechen", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}
