using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LMS
{
    public static class PatronManager
    {
        public static ObservableCollection<Patron> Patrons { get; set; }
        public static int Page { get; set; }

        static PatronManager()
        {
            // Initialize the collection
            Patrons = new ObservableCollection<Patron>();
            Page = 0;
        }

        //static string GenerateNextID(string currentID)
        //{
        //    // Extract the numeric part
        //    string numericPart = currentID.Substring(1);

        //    // Parse the numeric part to an integer and increment
        //    int number = int.Parse(numericPart) + 1;

        //    // Format the new number back to the desired length with leading zeros
        //    string newNumericPart = number.ToString(new string('0', numericPart.Length));

        //    // Combine the prefix and the incremented numeric part
        //    return currentID[0] + newNumericPart;
        //}

        public async static Task LoadFromDatabase(MySqlConnection connection)
        {
            //string query = "SELECT name, membership_id, email, phone, address, birthdate, membership_type, status FROM patron;"; 
            string query = "SELECT * FROM patron LIMIT 15 OFFSET @page_start;";
            connection.Open();
            try
            {
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("page_start", Page * 15);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            Patrons.Clear();
                            while (reader.Read())
                            {
                                Patron patron = new Patron
                                {
                                    FullName = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString(reader.GetOrdinal("name")),
                                    MembershipID = reader.IsDBNull(reader.GetOrdinal("membership_id")) ? string.Empty : reader.GetString(reader.GetOrdinal("membership_id")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString(reader.GetOrdinal("email")),
                                    PhoneNumber = reader.IsDBNull(reader.GetOrdinal("phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("phone")),
                                    Address = reader.IsDBNull(reader.GetOrdinal("address")) ? string.Empty : reader.GetString(reader.GetOrdinal("address")),
                                    DateOfBirth = (DateTime)(reader.IsDBNull(reader.GetOrdinal("birthdate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("birthdate"))),
                                    Status = reader.IsDBNull(reader.GetOrdinal("status")) ? string.Empty : reader.GetString(reader.GetOrdinal("status")),
                                    MembershipType = reader.IsDBNull(reader.GetOrdinal("membership_type")) ? string.Empty : reader.GetString(reader.GetOrdinal("membership_type")),
                                    ID = reader.IsDBNull(reader.GetOrdinal("id")) ? -1 : reader.GetInt32(reader.GetOrdinal("id"))
                                };
                                Patrons.Add(patron);
                            }
                        }
                        else
                        {
                            Page--;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            await connection.CloseAsync();
        }


        public static async Task AddPatron(Patron patron, MySqlConnection connection)
        {
            string query = "SELECT name FROM patron WHERE LOWER(email)=LOWER(@newPatronEmail);";
            bool isEmailExisted;
            uint numericPartID = 0;
            bool missingIDBetween = false;
            string memberShipID = null;

            await connection.OpenAsync(); 

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@newPatronEmail", patron.Email);
                var result = await cmd.ExecuteScalarAsync(); 
                isEmailExisted = result != null;

                if (isEmailExisted)
                {
                    string name = result.ToString();
                    MessageBox.Show($"Email is used by {name}.", "Email Exist!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Handle Missing ID
                    query = @"
                        WITH NumberSeries AS (
                            SELECT ROW_NUMBER() OVER (ORDER BY CAST(SUBSTRING(membership_id, 2) AS UNSIGNED)) AS ExpectedID,
                                   CAST(SUBSTRING(membership_id, 2) AS UNSIGNED) AS ActualID
                            FROM patron
                        ),
                        MissingIDs AS (
                            SELECT NS.ExpectedID
                            FROM NumberSeries NS
                            LEFT JOIN (
                                SELECT CAST(SUBSTRING(membership_id, 2) AS UNSIGNED) AS membership_number
                                FROM patron
                            ) P ON NS.ExpectedID = P.membership_number
                            WHERE P.membership_number IS NULL
                        )
                        SELECT ExpectedID AS missing_membership_id
                        FROM MissingIDs
                        LIMIT 1;
                    ";

                    using (var idCmd = new MySqlCommand(query, connection))
                    {
                        using (var reader = await idCmd.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                numericPartID = (uint)reader.GetInt32(reader.GetOrdinal("missing_membership_id")); 
                                missingIDBetween = true;
                            }
                        }
                    }

                    if (!missingIDBetween)
                    {
                        query = @"SELECT MAX(CAST(SUBSTRING(membership_id, 2) AS UNSIGNED)) AS max_memberhship_id FROM patron";
                        using (var getCmd = new MySqlCommand(query, connection))
                        {
                            using (var reader = await getCmd.ExecuteReaderAsync())
                            {
                                if (reader.Read())
                                {
                                    numericPartID = (uint)(reader.IsDBNull(0) ? 0 : reader.GetInt32(0));
                                    numericPartID++;
                                }
                            }
                        }
                    }

                    memberShipID = "M" + numericPartID.ToString("D5");

                    query = @"INSERT INTO patron (name, membership_id, email, phone, address, birthdate, membership_type, status)
                      VALUES (@name, @membership_id, @email, @phone, @address, @birthdate, @membership_type, @status)";

                    using (var addCmd = new MySqlCommand(query, connection))
                    {
                        addCmd.Parameters.AddWithValue("@name", patron.FullName);
                        addCmd.Parameters.AddWithValue("@membership_id", memberShipID);
                        addCmd.Parameters.AddWithValue("@email", patron.Email);
                        addCmd.Parameters.AddWithValue("@phone", patron.PhoneNumber);
                        addCmd.Parameters.AddWithValue("@address", patron.Address);
                        addCmd.Parameters.AddWithValue("@birthdate", patron.DateOfBirth);
                        addCmd.Parameters.AddWithValue("@membership_type", patron.MembershipType);
                        addCmd.Parameters.AddWithValue("@status", "Active");

                        await addCmd.ExecuteNonQueryAsync();
                    }
                }
            }

            await connection.CloseAsync();
            patron.MembershipID = memberShipID;
            Patrons.Add(patron);
        }





        public static async Task DeletePatron(Patron patron, MySqlConnection connection)
        {
            string query = "DELETE FROM patron WHERE membership_id = @membership_id;";
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@membership_id", patron.MembershipID);

                await cmd.ExecuteNonQueryAsync();
                Patrons.Remove(patron);
            }
            connection.Close();
        }

        public async static Task EditPatron(Patron patron, MySqlConnection connection)
        {
            string query = @"UPDATE patron 
                        SET name = @name, 
                            email = @email,
                            phone = @phone, 
                            address = @address,
                            membership_type = @membership_type,
                            birthdate = @birthdate, 
                            status = @status
                        WHERE membership_id = @membership_id;";

            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", patron.FullName);
                cmd.Parameters.AddWithValue("@email", patron.Email);
                cmd.Parameters.AddWithValue("@phone", patron.PhoneNumber);
                cmd.Parameters.AddWithValue("@address", patron.Address);
                cmd.Parameters.AddWithValue("@membership_type", patron.MembershipType);
                cmd.Parameters.AddWithValue("@birthdate", patron.DateOfBirth);
                cmd.Parameters.AddWithValue("@status", patron.Status);
                cmd.Parameters.AddWithValue("@membership_id", patron.MembershipID);

                await cmd.ExecuteNonQueryAsync();
            }
            Patron existingPatron = Patrons.FirstOrDefault(p => p.MembershipID == patron.MembershipID);
            if (existingPatron != null)
            {
                // Update the properties of the existing patron in the collection
                existingPatron.FullName = patron.FullName;
                existingPatron.Email = patron.Email;
                existingPatron.PhoneNumber = patron.PhoneNumber;
                existingPatron.Address = patron.Address;
                existingPatron.MembershipType = patron.MembershipType;
                existingPatron.DateOfBirth = patron.DateOfBirth;
                existingPatron.Status = patron.Status;
            }
            await connection.CloseAsync();
        }

        public async static Task<ObservableCollection<Patron>> FilterPatron(string searchKey, MySqlConnection connection)
        {
            string query = "SELECT * FROM patron " +
                   "WHERE name LIKE @searchKey OR " +
                   "email LIKE @searchKey OR " +
                   "phone LIKE @searchKey OR " +
                   "address LIKE @searchKey OR " +
                   "membership_type LIKE @searchKey OR " +
                   "birthdate LIKE @searchKey OR " +
                   "status LIKE @searchKeyForStatus OR " +
                   "membership_id LIKE @searchKey " +
                   "LIMIT 15 OFFSET @page_start;";
            Page = 0;
            ObservableCollection<Patron> filteredPatron = new ObservableCollection<Patron>();
            await connection.OpenAsync();
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@searchKey", "%" + searchKey + "%");
                cmd.Parameters.AddWithValue("@searchKeyForStatus", searchKey + "%");
                cmd.Parameters.AddWithValue("@page_start", Page * 15);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var book = new Patron
                            {
                                FullName = reader["name"].ToString(),
                                Email = reader["email"].ToString(),
                                PhoneNumber = reader["phone"].ToString(),
                                Address = reader["address"].ToString(),
                                MembershipType = reader["membership_type"].ToString(),
                                DateOfBirth = (DateTime)reader["birthdate"],
                                Status = reader["status"].ToString(),
                                MembershipID = reader["membership_id"].ToString()
                            };
                            filteredPatron.Add(book);
                        }
                    }
                    else
                    {
                        Page--;
                    }
                    
                }
            }
            await connection.CloseAsync();
            return filteredPatron;
        }

        // See if the patron exist
        public static bool PatronExist(string searchKey)
        {
            Patron patron = Patrons.FirstOrDefault(p => p.FullName.ToLower() == searchKey.ToLower());
            return patron != null;
        }

        public static Patron GetPatron(string searchKey)
        {
            return Patrons.FirstOrDefault(p => p.FullName.ToLower() == searchKey.ToLower());
        }

    }
}

