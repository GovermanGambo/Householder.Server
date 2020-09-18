using System;
using MySqlConnector;

namespace Householder.Server.DataAccess
{
    public class MySqlDatabase : IMySqlDatabase
    {
        public MySqlConnection Connection { get; }

        public MySqlDatabase(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection.Dispose();
    }
}