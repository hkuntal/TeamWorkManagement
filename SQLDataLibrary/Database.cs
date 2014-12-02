using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Sql;

namespace SQLDataLibrary
{
    public class Database
    {
        public SqlConnection DbConnection { get; set; }

        public SqlCommand DbCommand { get; set; }

        public Database(string connectionString)
        {
            try
            {
                // create the sql connection object
                DbConnection = new SqlConnection(connectionString);
                DbCommand = new SqlCommand();
                DbCommand.Connection = DbConnection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ExecuteCommand(string command)
        {
            try
            {
                Thread.Sleep(0);
                // Execute the command
                DbCommand.CommandType = CommandType.Text;
                DbCommand.CommandText = command;
                DbCommand.CommandTimeout = 1;
                var dt = new DataTable {Locale = CultureInfo.CurrentCulture};
                using (var sqlDa = new SqlDataAdapter())
                {
                    sqlDa.SelectCommand = DbCommand;
                    sqlDa.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw;
                
            }
        }

        public DataTable ExecuteStoreProc(string sp)
        {
            try
            {
                // Execute the command
                DbCommand.CommandType = CommandType.StoredProcedure;
                DbCommand.CommandText = sp;
                DbCommand.CommandTimeout = 1;
                var dt = new DataTable { Locale = CultureInfo.CurrentCulture };
                using (var sqlDa = new SqlDataAdapter())
                {
                    sqlDa.SelectCommand = DbCommand;
                    sqlDa.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
