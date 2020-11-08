using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gartenausgaben
{
    internal static class DbConnect
    {
        internal static int Db_execute(string sql_Insert)
        {
            string conn = Properties.Settings.Default.GartenProjekteConnectionString;

            SqlConnection sql_conn = new SqlConnection(conn);
            if (sql_conn.State != ConnectionState.Open) sql_conn.Open();
            SqlCommand sql_com = new SqlCommand(sql_Insert, sql_conn);

            int nResult = sql_com.ExecuteNonQuery();
            sql_conn.Close();
            return nResult;
        }

    }
}
