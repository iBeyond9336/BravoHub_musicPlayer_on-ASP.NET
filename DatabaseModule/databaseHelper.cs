using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using MySql.Data.MySqlClient;

namespace BravoHub.DatabaseModule
{
    public class databaseHelper
    {
        private readonly string _connectionString;

        public databaseHelper()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["BravoHubConnectionString"].ConnectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public bool TestConnection()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                return false;
            }
        }

        public bool InsertNewUser(string username, string password)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO users (username, password, email) VALUES (@username, @password, @email)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Password should be hashed for security
                        cmd.Parameters.AddWithValue("@email", "example@com");
                        cmd.Parameters.AddWithValue("@date_joined", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return false;
            }
        }
    }
}