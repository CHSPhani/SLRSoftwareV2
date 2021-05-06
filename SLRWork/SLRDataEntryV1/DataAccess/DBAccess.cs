using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using MongoDB.Driver;

namespace UoB.SLR.SLRDataEntryV1.DataAccess
{
    public class MongoDBConnection
    {
        IMongoDatabase db;

        public MongoDBConnection(string dbName)
        {
            var client = new MongoClient();
            db = client.GetDatabase(dbName);
        }

    }

    public sealed class ConnectToDb
    {
        private static ConnectToDb instance = null;
        private static readonly object padlock = new object();
        public MySqlConnection conn = null;
        ConnectToDb()
        {
            string connStr = "server=localhost;user id=root;password=Welcome123#;persistsecurityinfo=True;database=slrv1";
            conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                Console.WriteLine("Opened DB");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static ConnectToDb Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConnectToDb();
                    }
                    return instance;
                }
            }
        }
    }
}
