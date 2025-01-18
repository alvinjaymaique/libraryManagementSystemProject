using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace LMS
{
    public class Database
    {
        //private readonly MySqlConnection _mySqlConnection;
        private string user {  get; set; }
        private string password { get; set; }
        private string databaseName { get; set; }
        private string _connectionString { get; set; }
        public Database(string username, string password, string databaseName, string host= "localhost") 
        {
            user = username;
            this.password = password;
            this.databaseName = databaseName;
            _connectionString = $"server={host}; database={databaseName}; user={username}; password={password};";
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
