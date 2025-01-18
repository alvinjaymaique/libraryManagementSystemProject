using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;

namespace LMS
{
    public static class AccountManager
    {
        //static List<Account> accounts = new List<Account>() { new Account("admin", "admin", "admin@gmail.com", "admin") };
        private static List<Account> accounts = new List<Account>()
        {
            new Account("admin", "admin", "admin@gmail.com", "admin")
        };

        public static List<Account> Accounts
        {
            get { return accounts; }
        }

        public static bool AddAccount(string user, string pass, string email, string role, MySqlConnection connection)
        {
            string hash_password = BCrypt.Net.BCrypt.HashPassword(pass);
            string checkQuery = "SELECT COUNT(*) FROM account WHERE username = @Username OR email = @Email";
            string insertQuery = "INSERT INTO account(username, hash_password, password, email, role) " +
                                 "VALUES (@Username, @HashPassword, @Password, @Email, @Role)";

            try
            {
                connection.Open();

                // Check for duplicates
                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@Username", user);
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    var result = checkCmd.ExecuteScalar();
                    int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;

                    if (count > 0)
                    {
                        MessageBox.Show("Username or email already exists. Please choose a different one.",
                                        "Duplicate Entry", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }

                // Proceed with registration
                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                {
                    insertCmd.Parameters.AddWithValue("@Username", user);
                    insertCmd.Parameters.AddWithValue("@HashPassword", hash_password); // Use the hashed password
                    insertCmd.Parameters.AddWithValue("@Password", pass); // Plain-text password (use only for testing)
                    insertCmd.Parameters.AddWithValue("@Email", email);
                    insertCmd.Parameters.AddWithValue("@Role", role);

                    insertCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Registration Successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }


        public async static Task<(bool, string, string)> IsValidAccount(string username, string password, MySqlConnection connection)
        {
            string query = "SELECT hash_password, role, username FROM account WHERE username = @Username"; // AND hash_password = @Password
            connection.Open();
            try
            {
                using (var command = new MySqlCommand(query, connection))
                {   
                    // Use parameterized queries to prevent SQL injection
                    command.Parameters.AddWithValue("@Username", username);
                    //command.Parameters.AddWithValue("@Password", hash_password);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string storedHash = Helper.GetSqlCellString(reader, "hash_password");
                                string role = Helper.GetSqlCellString(reader, "role").ToLower();
                                string name = Helper.GetSqlCellString(reader, "username").ToLower();
                                bool isValid = BCrypt.Net.BCrypt.Verify(password, storedHash);
                                return (isValid, role, name);
                            }
                        }
                    }
   
                }
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return (false, string.Empty, string.Empty);
            }
            finally
            {
                connection.Close();
            }
            return (false, string.Empty, string.Empty);
        }

    }
}
