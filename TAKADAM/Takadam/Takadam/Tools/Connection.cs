using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Takadam.Tools
{
    public class Connection
    {
        private MySqlConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Connection()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "mysql5017.site4now.net";
            uid = "a149cb_ecom";
            password = "Ecom2017";
            database = "db_a149cb_ecom";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";port=3306";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //      MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        //       MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                //    MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public int Insert(string query, Dictionary<string, object> param = null)
        {
            int t = 0;
            //    try
            //     {
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                if (param != null)
                {
                    foreach (KeyValuePair<string, object> entry in param)
                    {
                        cmd.Parameters.Add(new MySqlParameter(entry.Key, entry.Value));
                    }
                }
                cmd.ExecuteNonQuery();
                //si insertion reussit
                t = (int)cmd.LastInsertedId;
            }
            //   }catch (MySqlException e) { }

            //si insertion echoue
            return t;
        }

        //Update statement
        public string Update(string query, Dictionary<string, object> param, string id = "")
        {
            string t = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                foreach (KeyValuePair<string, object> entry in param)
                {
                    cmd.Parameters.Add(new MySqlParameter(entry.Key, entry.Value));
                }
                cmd.ExecuteNonQuery();

                //si insertion reussit
                t = id;
            }
            catch (MySqlException e) { }

            //si insertion echoue
            return t;
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public MySqlDataReader Select(string query, Dictionary<string, object> param = null)
        {
            MySqlDataReader dataReader = null;
            //Open connection
            if (OpenConnection() == true)
            {

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //loop insert params
                if (param != null)
                    foreach (KeyValuePair<string, object> entry in param)
                    {
                        cmd.Parameters.Add(new MySqlParameter(entry.Key, entry.Value));
                    }
                dataReader = cmd.ExecuteReader();

            }
            //   CloseConnection();
            return dataReader;
        }


        //Count statement
        public int Count()
        {
            return 0;
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }

    }
}