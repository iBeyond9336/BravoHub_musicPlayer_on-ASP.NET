using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;

namespace BravoHub.DatabaseModule {
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
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

        public bool CheckUserCredentials(string username, string password)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM users WHERE username = @username AND password = @password"; // Password should ideally be hashed

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password); // Password should be hashed and compared

                        int result = Convert.ToInt32(cmd.ExecuteScalar());
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


        // use to delete user
        public bool DeleteUser(string username)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM users WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

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

        public bool InsertRecord(string tableName, Dictionary<string, object> columnData)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string columns = string.Join(", ", columnData.Keys);
                    string values = string.Join(", ", columnData.Keys.Select(k => "@" + k));

                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        foreach (var pair in columnData)
                        {
                            cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                        }

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

        /*
         * usage of InsertRecord, other methods similar
         Dictionary<string, object> albumData = new Dictionary<string, object>
            {
                { "album_name", "New Album" },
                { "release_date", DateTime.Now }
            };

            bool isInserted = db.InsertRecord("albums", albumData);

         
         */


        public bool DeleteRecord(string tableName, string condition)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = $"DELETE FROM {tableName} WHERE {condition}";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
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


        public bool UpdateRecord(string tableName, Dictionary<string, object> columnData, string condition)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string setClause = string.Join(", ", columnData.Keys.Select(key => $"{key} = @{key}"));

                    string query = $"UPDATE {tableName} SET {setClause} WHERE {condition}";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        foreach (var pair in columnData)
                        {
                            cmd.Parameters.AddWithValue("@" + pair.Key, pair.Value);
                        }

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