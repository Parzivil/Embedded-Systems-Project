using MySql.Data.MySqlClient;

namespace Embedded_Systems_Project
{

    internal class Database
    {
        public MySqlConnection mySqlConnection;
        public MySqlDataReader mySqlDataReader;

        //Database Credentials
        public readonly string SERVER_NAME = "127.0.0.1";
        public readonly string USER_NAME = "ST123456";
        public readonly string DATABASE_NAME = "temperature_record";
        public readonly string PASSWORD_NAME = "ZJx(]8djn-3@.Q/u";
        public readonly string TABLE_NAME = "temperature";
        public string connectionString;



        public void connect()
        {
            connectionString = $"server={SERVER_NAME};user={USER_NAME};database={DATABASE_NAME};password={PASSWORD_NAME};";
            

            mySqlConnection = new MySqlConnection(connectionString);

            mySqlConnection.Open();
            mySqlConnection.Close();

        }


        /// <summary>
        /// Sends data to the set database
        /// </summary>
        /// <param name="data"></param>
        public void SendToDatabase(float data)
        {
            mySqlConnection.Open();
            string Query = "insert into " + DATABASE_NAME + "." + TABLE_NAME + "(timeStamp,temperature,remark) values('"
                + DateTime.Now + "','" + data + "','" + USER_NAME + "');";
            MySqlCommand Command = new(Query, mySqlConnection);

            mySqlDataReader = Command.ExecuteReader(); //Exicute the command
            mySqlConnection.Close();

        }



    }

}
