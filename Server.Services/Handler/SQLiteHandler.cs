using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SQLite;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Server.Handler
{
    public class SQLiteHandler
    {
        public static string ConnectionString = string.Empty;

        public static bool CreateDataBase(string path, string query)
        {
            SQLiteConnection.CreateFile(path);
            return Exec(query);
        }

        public static string GetJson(string request)
        {
            string response = string.Empty;
            DataTable dt = new DataTable();
            SQLiteConnection cnn = new SQLiteConnection(ConnectionString);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = request;
            SQLiteDataReader reader = mycommand.ExecuteReader();
            dt.Load(reader);
            response = JsonConvert.SerializeObject(dt, Formatting.Indented);
            reader.Close();
            cnn.Close();
            return response;
        }

        public static DataTable GetDt(string request)
        {
            string response = string.Empty;
            DataTable dt = new DataTable();
            SQLiteConnection cnn = new SQLiteConnection(ConnectionString);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = request;
            SQLiteDataReader reader = mycommand.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            cnn.Close();
            return dt;
        }

        public static string GetScalar(string request)
        {
            string scalarResult = string.Empty;
            SQLiteConnection cnn = new SQLiteConnection(ConnectionString);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = request;
            object result = mycommand.ExecuteScalar();
            if (result != null)
            {
                scalarResult = result.ToString();
            }
            cnn.Close();
            return scalarResult;
        }

        public static bool Exec(string query)
        {
            bool response = false;
            SQLiteConnection Conn = new SQLiteConnection(ConnectionString);
            SQLiteCommand Command = new SQLiteCommand(query, Conn);
            Conn.Open();

            try
            {
                Command.ExecuteNonQuery();
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }

            Conn.Close();

            return response;
        }

    }

}

