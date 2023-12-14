using BravoHub.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using BCrypt.Net;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Org.BouncyCastle.Crypto.Generators;

namespace BravoHub.DatabaseModule {
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
        {
            string server = "bravohub.mysql.database.azure.com"; // Hostname
            string uid = "bravo"; // Username (usually in the format 'username@hostname')
            string password = "Conestoga1"; 
            string database = "bravoazure"; // Database name



            //string server = Environment.GetEnvironmentVariable("DB_SERVER");
            //string uid = Environment.GetEnvironmentVariable("DB_USER");
            //string password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            //string database = "bravohub"; 
            _connectionString = $"server={server};port=3306;database={database};uid={uid};password={password};SslMode=required";
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

        public UserModel GetUserByUsername(string username)
        {
            try
            {
                MySqlConnection conn = GetConnection();
                conn.Open();
                string query = "SELECT * FROM users WHERE username = @username;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                DataTable dataTable = new DataTable();

                // Use a DataAdapter to fill the DataTable
                UserModel user = null;
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                {
                    dataAdapter.Fill(dataTable);
                    if(dataTable.Rows.Count > 0) { 
                        user = new UserModel();
                        // Now, iterate through the DataTable rows
                        foreach (DataRow row in dataTable.Rows) {
                            // Access each field by column name or index
                            user.Username = row["username"].ToString();
                            user.Password = row["hashedpassword"].ToString();     // Alan added to get password in Admin page
                            user.Email = row["email"].ToString();
                            user.Role = row["role"].ToString();
                        }
                    }
                }

                dataTable.Dispose();
                cmd.Dispose();
                conn.Dispose();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool InsertNewUser(string username, string password)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                    string query = "INSERT INTO users (username, hashedpassword, email, role) VALUES (@username, @hashedpassword, @email, @role)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@hashedpassword", hashedPassword);
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
                    string query = "SELECT hashedpassword FROM users WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@password", user.Password); // Password should be hashed and compared

                        int result = Convert.ToInt32(cmd.ExecuteScalar());
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
            }
            return false;
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