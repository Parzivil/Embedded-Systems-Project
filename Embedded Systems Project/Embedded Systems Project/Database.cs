namespace Embedded_Systems_Project
{

    internal class Database
    {
        public MySql.Data.MySqlClient.MySqlConnection mySqlConnection;
        public MySql.Data.MySqlClient.MySqlDataReader mySqlDataReader;

        //Database Credentials
        public readonly string SERVER_NAME = "127.0.0.1";
        public readonly string USER_NAME = "ST123456";
        public readonly string DATABASE_NAME = "temperature_record";
        public readonly string PASSWORD_NAME = "yMHW][Y33X6PLe!6";
        public readonly string TABLE_NAME = "temperature";
        public string connectionString;
        public bool connected = false;



        public void connect()
        {
            connectionString = $"server={SERVER_NAME};user={USER_NAME};database={DATABASE_NAME};password={PASSWORD_NAME};";


            mySqlConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

            connected = true;
        }


        /// <summary>
        /// Sends data to the set database
        /// </summary>
        /// <param name="data"></param>
        public void SendToDatabase(float data)
        {
            if (connected)
            {
                mySqlConnection.Open();
                string Query = "insert into " + DATABASE_NAME + "." + TABLE_NAME + "(timeStamp,temperature,remark) values('"
                    + DateTime.Now + "','" + data + "','" + USER_NAME + "');";
                MySql.Data.MySqlClient.MySqlCommand Command = new(Query, mySqlConnection);

                mySqlDataReader = Command.ExecuteReader(); //Exicute the command
                mySqlConnection.Close();
            }

            else MessageBox.Show("Database is not connected");
        }
    }
}
