using Npgsql;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Backend
{
    public class Connection
    {
        private readonly string connectionString;

        public Connection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<T> loadDB<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                return conn.Query<T>(sqlStatement, parameters).ToList();
            }
        }

        public void sqlCommand<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                conn.Execute(sqlStatement, parameters);
            }
        }
    }
}
