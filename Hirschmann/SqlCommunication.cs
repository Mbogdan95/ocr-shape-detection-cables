using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Hirschmann
{
    class SqlCommunication
    {
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();

            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = "SELECT * FROM users";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);
                // Create a data reader and execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    User user = new User
                    {
                        IdBadge = dataReader["idbadge"].ToString(),
                        Rank = (Rank)Enum.Parse(typeof(Rank), dataReader["rank"].ToString())
                    };

                    users.Add(user);
                }

                // Close data reader
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();

            // Return list of users
            return users;
        }

        public static User CheckUser(string idBadge, string password)
        {
            User user = new User();

            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"SELECT * FROM users WHERE idbadge = '{idBadge}' AND password = '{password}'";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);
                // Create a data reader and execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    user.IdBadge = dataReader["idbadge"].ToString();
                    user.Rank = (Rank)Enum.Parse(typeof(Rank), dataReader["rank"].ToString());
                }

                // Close data reader
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();

            return user;
        }

        public static User CheckUserProgram(string idBadge)
        {
            User user = new User();

            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"SELECT * FROM users WHERE idbadge = '{idBadge}'";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);
                // Create a data reader and execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    user.IdBadge = dataReader["idbadge"].ToString();
                    user.Rank = (Rank)Enum.Parse(typeof(Rank), dataReader["rank"].ToString());
                }

                // Close data reader
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();

            return user;
        }

        public static void DeleteUser(string idBadge)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"DELETE FROM users WHERE idbadge = '{idBadge}'";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }

        public static void InsertUser(string idBadge, string password, string rank)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"INSERT INTO users (`idbadge`, `rank`, `password`) VALUES ('{idBadge}', '{rank}', '{password}')";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }

        public static List<ProgramEntry> GetPrograms()
        {
            List<ProgramEntry> programs = new List<ProgramEntry>();

            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = "SELECT * FROM programs";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);
                // Create a data reader and execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    ProgramEntry program = new ProgramEntry
                    {
                        Name = dataReader["name"].ToString(),
                        TriggerOffset = Convert.ToInt32(dataReader["triggerOffset"]),
                        StartWastingOffset = Convert.ToInt32(dataReader["startWastingOffset"]),
                        WasteOffset = Convert.ToInt32(dataReader["wasteOffset"]),
                        ColorCamera1 = dataReader["colorCamera1"].ToString(),
                        ColorCamera2 = dataReader["colorCamera2"].ToString(),
                        Camera1 = Convert.ToBoolean(dataReader["camera1"]),
                        Camera2 = Convert.ToBoolean(dataReader["camera2"]),
                        LogosCamera1 = Convert.ToBoolean(dataReader["logosCamera1"]),
                        LogosCamera2 = Convert.ToBoolean(dataReader["logosCamera2"]),
                        NumberOfLogosCamera1 = Convert.ToInt32(dataReader["numberOfLogosCamera1"]),
                        NumberOfLogosCamera2 = Convert.ToInt32(dataReader["numberOfLogosCamera2"]),
                        Logo1Camera1SaveLocation = dataReader["logo1Camera1SaveLocation"].ToString(),
                        Logo2Camera1SaveLocation = dataReader["logo2Camera1SaveLocation"].ToString(),
                        Logo1Camera2SaveLocation = dataReader["logo1Camera2SaveLocation"].ToString(),
                        Logo2Camera2SaveLocation = dataReader["logo2Camera2SaveLocation"].ToString(),
                        ShapesCamera1 = Convert.ToBoolean(dataReader["shapesCamera1"]),
                        ShapesCamera2 = Convert.ToBoolean(dataReader["shapesCamera2"]),
                        ShapeTypeCamera1 = dataReader["shapeTypeCamera1"].ToString(),
                        ShapeTypeCamera2 = dataReader["shapeTypeCamera2"].ToString(),
                        TextToDetectCamera1 = dataReader["textToDetectCamera1"].ToString(),
                        TextToDetectCamera2 = dataReader["textToDetectCamera2"].ToString()
                    };

                    programs.Add(program);
                }

                // Close data reader
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();

            // Return list of users
            return programs;
        }

        public static void InsertProgram(string name,
            int triggerOffset,
            int startWastingOffset,
            int wasteOffset,
            string colorCamera1,
            string colorCamera2,
            bool camera1,
            bool camera2,
            bool logosCamera1,
            bool logosCamera2,
            int numberOfLogosCamera1,
            int numberOfLogosCamera2,
            string logo1Camera1SaveLocation,
            string logo2Camera1SaveLocation,
            string logo1Camera2SaveLocation,
            string logo2Camera2SaveLocation,
            bool shapesCamera1,
            bool shapesCamera2,
            string shapeTypeCamera1,
            string shapeTypeCamera2,
            string textToDetectCamera1,
            string textToDetectCamera2)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"INSERT INTO programs (name, " +
                $"triggerOffset, " +
                $"startWastingOffset, " +
                $"wasteOffset, " +
                $"colorCamera1, " +
                $"colorCamera2, " +
                $"camera1, " +
                $"camera2, " +
                $"logosCamera1, " +
                $"logosCamera2, " +
                $"numberOfLogosCamera1, " +
                $"numberOfLogosCamera2, " +
                $"logo1Camera1SaveLocation, " +
                $"logo2Camera1SaveLocation, " +
                $"logo1Camera2SaveLocation, " +
                $"logo2Camera2SaveLocation, " +
                $"shapesCamera1, " +
                $"shapesCamera2, " +
                $"shapeTypeCamera1, " +
                $"shapeTypeCamera2, " +
                $"textToDetectCamera1, " +
                $"textToDetectCamera2) VALUES" +
                $"('{name}', " +
                $"{triggerOffset}, " +
                $"{startWastingOffset}, " +
                $"{wasteOffset}, " +
                $"'{colorCamera1}', " +
                $"'{colorCamera2}', " +
                $"'{Convert.ToInt32(camera1)}', " +
                $"'{Convert.ToInt32(camera2)}', " +
                $"'{Convert.ToInt32(logosCamera1)}', " +
                $"'{Convert.ToInt32(logosCamera2)}', " +
                $"'{numberOfLogosCamera1}', " +
                $"'{numberOfLogosCamera2}', " +
                $"'{logo1Camera1SaveLocation}', " +
                $"'{logo2Camera1SaveLocation}', " +
                $"'{logo1Camera2SaveLocation}', " +
                $"'{logo2Camera2SaveLocation}', " +
                $"'{Convert.ToInt32(shapesCamera1)}', " +
                $"'{Convert.ToInt32(shapesCamera2)}', " +
                $"'{shapeTypeCamera1}', " +
                $"'{shapeTypeCamera2}', " +
                $"'{textToDetectCamera1}', " +
                $"'{textToDetectCamera2}');";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }

        public static void UpdateProgramManageProgramsForm(string name,
            int triggerOffset,
            int startWastingOffset,
            int wasteOffset,
            string colorCamera1,
            string colorCamera2,
            bool camera1,
            bool camera2,
            bool logosCamera1,
            bool logosCamera2,
            int numberOfLogosCamera1,
            int numberOfLogosCamera2,
            bool shapesCamera1,
            bool shapesCamera2,
            string shapeTypeCamera1,
            string shapeTypeCamera2,
            string textToDetectCamera1,
            string textToDetectCamera2)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"UPDATE programs SET `triggerOffset` = '{triggerOffset}', " +
                $"`startWastingOffset` = '{startWastingOffset}', " +
                $"`wasteOffset` = '{wasteOffset}', " +
                $"`colorCamera1` = '{colorCamera1}'," +
                $"`colorCamera2` = '{colorCamera2}'," +
                $"`camera1` = '{Convert.ToInt32(camera1)}'," +
                $"`camera2` = '{Convert.ToInt32(camera2)}'," +
                $"`logosCamera1` = '{Convert.ToInt32(logosCamera1)}'," +
                $"`logosCamera2` = '{Convert.ToInt32(logosCamera2)}'," +
                $"`numberOfLogosCamera1` = '{numberOfLogosCamera1}'," +
                $"`numberOfLogosCamera2` = '{numberOfLogosCamera2}'," +
                $"`shapesCamera1` = '{Convert.ToInt32(shapesCamera1)}'," +
                $"`shapesCamera2` = '{Convert.ToInt32(shapesCamera2)}'," +
                $"`shapeTypeCamera1` = '{shapeTypeCamera1}'," +
                $"`shapeTypeCamera1` = '{shapeTypeCamera2}'," +
                $"`textToDetectCamera1` = '{textToDetectCamera1}'," +
                $"`textToDetectCamera2` = '{textToDetectCamera2}'" +
                $"WHERE (`name` = '{name}');";
            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }

        public static void UpdateProgramProductConfigurationForm(string name, int triggerOffset, int startWastingOffset, int wasteOffset)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"UPDATE programs SET `triggerOffset` = '{triggerOffset}', `startWastingOffset` = '{startWastingOffset}', `wasteOffset` = '{wasteOffset}' WHERE (`name` = '{name}');";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }


        public static ProgramEntry CheckProgram(string programName)
        {
            ProgramEntry programEntry = new ProgramEntry();

            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"SELECT * FROM programs WHERE name = '{programName}'";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);
                // Create a data reader and execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    programEntry.Name = dataReader["name"].ToString();
                    programEntry.TriggerOffset = Convert.ToInt32(dataReader["triggerOffset"]);
                    programEntry.StartWastingOffset = Convert.ToInt32(dataReader["startWastingOffset"]);
                    programEntry.WasteOffset = Convert.ToInt32(dataReader["wasteOffset"]);
                    programEntry.ColorCamera1 = dataReader["colorCamera1"].ToString();
                    programEntry.ColorCamera2 = dataReader["colorCamera2"].ToString();
                    programEntry.Camera1 = Convert.ToBoolean(dataReader["camera1"]);
                    programEntry.Camera2 = Convert.ToBoolean(dataReader["camera2"]);
                    programEntry.LogosCamera1 = Convert.ToBoolean(dataReader["logosCamera1"]);
                    programEntry.LogosCamera2 = Convert.ToBoolean(dataReader["logosCamera2"]);
                    programEntry.NumberOfLogosCamera1 = Convert.ToInt32(dataReader["numberOfLogosCamera1"]);
                    programEntry.NumberOfLogosCamera2 = Convert.ToInt32(dataReader["numberOfLogosCamera2"]);
                    programEntry.Logo1Camera1SaveLocation = dataReader["logo1Camera1SaveLocation"].ToString();
                    programEntry.Logo2Camera1SaveLocation = dataReader["logo2Camera1SaveLocation"].ToString();
                    programEntry.Logo1Camera2SaveLocation = dataReader["logo1Camera2SaveLocation"].ToString();
                    programEntry.Logo2Camera2SaveLocation = dataReader["logo2Camera2SaveLocation"].ToString();
                    programEntry.ShapesCamera1 = Convert.ToBoolean(dataReader["shapesCamera1"]);
                    programEntry.ShapesCamera2 = Convert.ToBoolean(dataReader["shapesCamera2"]);
                    programEntry.ShapeTypeCamera1 = dataReader["shapeTypeCamera1"].ToString();
                    programEntry.ShapeTypeCamera2 = dataReader["shapeTypeCamera2"].ToString();
                    programEntry.TextToDetectCamera1 = dataReader["textToDetectCamera1"].ToString();
                    programEntry.TextToDetectCamera2 = dataReader["textToDetectCamera2"].ToString();
                }

                // Close data reader
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();

            return programEntry;
        }

        public static void DeleteProgram(string programName)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"DELETE FROM programs WHERE name = '{programName}'";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }


        public static List<LogEntry> GetLogs()
        {
            List<LogEntry> logEntries = new List<LogEntry>();

            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = "SELECT * FROM logs";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);
                // Create a data reader and execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    LogEntry logEntry = new LogEntry
                    {
                        IdBadge = dataReader["idbadge"].ToString(),
                        Action = dataReader["action"].ToString(),
                        Date = Convert.ToDateTime(dataReader["date"].ToString()),
                        PhotoLocationCamera1 = dataReader["photolocationcamera1"].ToString(),
                        PhotoLocationCamera2 = dataReader["photolocationcamera2"].ToString()
                    };

                    logEntries.Add(logEntry);
                }

                // Close data reader
                dataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();

            // Return list of users
            return logEntries;
        }

        public static void InsertLog(string idBadge, string action, string photolocationCamera1, string photolocationCamera2)
        {
            string server = "localhost";
            string dbName = "hirschmann";
            string dbUser = "root";
            string dbPassword = "admin";
            string connectionString = $"datasource = {server}; database = {dbName}; username = {dbUser}; password = {dbPassword}";
            string dbQuery = $"INSERT INTO logs (`idbadge`, `action`, `date`, `photolocationcamera1`, `photolocationcamera2`) VALUES ('{idBadge}', '{action}', '{DateTime.Now}', '{photolocationCamera1}', '{photolocationCamera2}')";

            // Create connection
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                // Open connection
                connection.Open();

                // Create command
                MySqlCommand cmd = new MySqlCommand(dbQuery, connection);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "SQL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Close connection
            connection.Close();
        }
    }
}
