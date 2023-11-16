using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace Room_System
{
    class Reservation
    {
        DBConnection conn = new DBConnection();
        //get all reservations
        public DataTable GetAllReservations()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM reservations", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //get all reservations  By MultipleFields
        public DataTable GetReservationsByMultipleFields(String searchTerm, String dateSearchTerm)
        {
            using (MySqlConnection connection = conn.GetConnection())
            {
                DataTable table = new DataTable();

                try
                {
                    connection.Open();

                    // Update the command to handle numeric and date fields appropriately
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM reservations WHERE (id LIKE @searchTerm OR room_number LIKE @searchTerm OR client_id LIKE @searchTerm) AND (@dateSearchTerm BETWEEN date_in AND date_out)", connection))

                    {
                        // Add the parameter for text search
                        string likeSearchTerm = "%" + searchTerm + "%";
                        command.Parameters.Add("@searchTerm", MySqlDbType.VarChar).Value = likeSearchTerm;

                        // Convert the date from dd/MM/yyyy format to DateTime
                        if (DateTime.TryParseExact(dateSearchTerm, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                        {
                            // If the date is valid, add it as a parameter
                            command.Parameters.Add("@dateSearchTerm", MySqlDbType.Date).Value = parsedDate;
                        }
                        else
                        {
                            // If the date is invalid, throw an exception or handle it as required
                            throw new ArgumentException("Invalid date format. Please use dd/MM/yyyy format.");
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return table;
            }
        }


        //get all reservations  By Fields

        public DataTable GetReservationsByFields(String searchTerm)
        {
            using (MySqlConnection connection = conn.GetConnection())
            {
                DataTable table = new DataTable();

                try
                {
                    connection.Open();

                    // Update the command to handle numeric and date fields appropriately
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM reservations WHERE (id LIKE @searchTerm OR room_number LIKE @searchTerm OR client_id LIKE @searchTerm) ", connection))

                    {
                        // Add the parameter for text search
                        string likeSearchTerm = "%" + searchTerm + "%";
                        command.Parameters.Add("@searchTerm", MySqlDbType.VarChar).Value = likeSearchTerm;


                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("MySQL Error: " + ex.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return table;
            }
        }




        //make new reservation
        public bool MakeReservation(int number, int client, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String queryInsert = "INSERT INTO `reservations`(`room_number`, `client_id`, `date_in`, `date_out`) VALUES (@number, @client, @dateIn, @dateOut)";
            command.CommandText = queryInsert;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@client", MySqlDbType.Int32).Value = client;
            command.Parameters.Add("@dateIn", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dateOut", MySqlDbType.Date).Value = dateOut;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

        //edit reservation
        public bool EditReservation(int id, int number, int client, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String queryUpdate = "UPDATE `reservations` SET `room_number`=@number,`client_id`=@client,`date_in`=@dateIn,`date_out`=@dateOut WHERE id=@id";
            command.CommandText = queryUpdate;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@number", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@client", MySqlDbType.Int32).Value = client;
            command.Parameters.Add("@dateIn", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dateOut", MySqlDbType.Date).Value = dateOut;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

        //remove room
        public bool RemoveReservation(int id)
        {
            MySqlCommand command = new MySqlCommand();
            String queryDelete = "DELETE FROM reservations WHERE id=@id";
            command.CommandText = queryDelete;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            conn.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                conn.CloseConnection();
                return true;
            }
            else
            {
                conn.CloseConnection();
                return false;
            }
        }

    }
}
