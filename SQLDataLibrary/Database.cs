using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
            // create the sql connection object
            DbConnection = new SqlConnection(connectionString);
            DbCommand = new SqlCommand();
            DbCommand.Connection = DbConnection;
        }

        public bool ExecuteCommand(string command)
        {
            // Execute the command
            DbCommand.
        }
    }
}
