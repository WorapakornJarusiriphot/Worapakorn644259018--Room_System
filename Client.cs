using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace Room_System
{

    /*
     * class for insert/update/delete/get all client
     * 
     * */
    class Client
    {
        DBConnection conn = new DBConnection();

        //insert new client
        public bool InsertClient(String fname, String lname, String phone, String type, String address)
        {
            MySqlCommand command = new MySqlCommand();
            String queryInsert = "INSERT INTO clients(first_name,last_name,phone,type,address) VALUES (@fname, @lname, @phone,@type, @address)";
            command.CommandText = queryInsert;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = type;
            command.Parameters.Add("@address", MySqlDbType.VarChar).Value = address;

            conn.OpenConnection();
            if(command.ExecuteNonQuery() == 1)
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

        //get all clients
        public DataTable GetAllClients()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM clients", conn.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //get all clients by First Name
        public DataTable GetClientsByfName(String fname)
        {
            // Assuming 'conn' is an instance of a class that handles the connection, 
            // and GetConnection returns a new MySqlConnection object.
            using (MySqlConnection connection = conn.GetConnection())
            {
                // Create a new DataTable.
                DataTable table = new DataTable();

                try
                {
                    // Open the connection
                    connection.Open();

                    // Create the command with a parameterized query
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM clients WHERE first_name=@fname", connection))
                    {
                        // Add the parameter to the command
                        command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;

                        // Create a new MySqlDataAdapter using the command
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            // Fill the DataTable with the result of the command
                            adapter.Fill(table);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Handle any MySQL errors here
                    Console.WriteLine("MySQL Error: " + ex.Message);
                    // Depending on your application's needs, you might want to throw the exception, return an empty table, or log the error.
                }
                finally
                {
                    // Ensure the connection is closed when we're done with it
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                // Return the filled DataTable
                return table;
            }
        }

        //get Clients By Multiple Fields
        public DataTable GetClientsByMultipleFields(String searchTerm)
        {
            using (MySqlConnection connection = conn.GetConnection())
            {
                DataTable table = new DataTable();

                try
                {
                    connection.Open();

                    // Update the command to search across multiple fields
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM clients WHERE first_name LIKE @searchTerm OR last_name LIKE @searchTerm OR phone LIKE @searchTerm OR type LIKE @searchTerm OR address LIKE @searchTerm", connection))
                    {
                        // Add the parameter to the command
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


        //edit client data
        public bool EditClient(int id, String fname, String lname, String phone,String type, String address)
        {
            MySqlCommand command = new MySqlCommand();
            String queryUpdate = "UPDATE clients SET first_name=@fname, last_name=@lname, phone=@phone,type=@type, address=@address WHERE id=@cid";
            command.CommandText = queryUpdate;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id; 
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = type;
            command.Parameters.Add("@address", MySqlDbType.VarChar).Value = address;

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

        //remove client
        public bool RemoveClient(int id)
        {
            MySqlCommand command = new MySqlCommand();
            String queryDelete = "DELETE FROM clients WHERE id=@cid";
            command.CommandText = queryDelete;
            command.Connection = conn.GetConnection();

            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = id; 

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
