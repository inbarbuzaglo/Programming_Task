using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Programming_Task
{
    //Both WriteCsvToSql and ReadFromSql are inherited the db server info from SqlHandlerBase.
    class SqlHandlerBase
    {
        protected string SQLserver;
        protected string db;
        protected string tableName;

        public SqlHandlerBase()
        {
            SQLserver = "DESKTOP-56HATAL";
            db = "Covid";
            tableName = "CovidData";
        }

        //Creates the connection to the db.
        protected SqlConnection CreateConnection()
        {
            string connString = "Data Source=" + SQLserver + ";Initial Catalog=" + db + ";Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
    }
}
