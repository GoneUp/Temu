using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_Manager.Database.Database_Exceptions;
using Database_Manager.Database.Session_Details.Interfaces;
using Database_Manager.Managers.Database;
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Collections;
using System.Data.SqlClient;
using Utils.Logger;
using DevTera;

namespace Database_Manager.Database
{
    public class DatabaseManager
    {
        private int beginClientAmount;
        private string connectionString;
        private List<MySqlClient> databaseClients;
        private bool isConnected = false;
        private uint maxPoolSize;
        private DatabaseServer server;
        private Queue connections;
        internal DatabaseType type { get; set; }

        public static bool dbEnabled = true;

        public DatabaseManager(uint maxPoolSize, int clientAmount, DatabaseType dbType)
        {
            if (maxPoolSize < clientAmount)
                throw new DatabaseException("The poolsize can not be larger than the client amount!");

            this.type = dbType;
            this.beginClientAmount = clientAmount;
            this.maxPoolSize = maxPoolSize;
            this.connections = new Queue();
        }

        private void addConnection(int id)
        {
            MySqlClient item = new MySqlClient(this, id);
            item.connect();
            this.databaseClients.Add(item);
        }

        private void createNewConnectionString()
        {
            if (this.type == DatabaseType.MySQL)
            {
                MySqlConnectionStringBuilder connectionString = new MySqlConnectionStringBuilder
                {
                    Server = this.server.getHost(),
                    Port = this.server.getPort(),
                    UserID = this.server.getUsername(),
                    Password = this.server.getPassword(),
                    Database = this.server.getDatabaseName(),
                    MinimumPoolSize = this.maxPoolSize / 2,
                    MaximumPoolSize = this.maxPoolSize,
                    AllowZeroDateTime = true,
                    ConvertZeroDateTime = true,
                    DefaultCommandTimeout = 300,
                    ConnectionTimeout = 10
                };

                this.setConnectionString(connectionString.ToString());
            }
            else
            {
                SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder
                {
                    DataSource = this.server.getHost(),
                    //Port = this.server.getPort(),
                    UserID = this.server.getUsername(),
                    Password = this.server.getPassword(),
                    InitialCatalog = this.server.getDatabaseName(),
                    MinPoolSize = (int)this.maxPoolSize / 2,
                    MaxPoolSize = (int)this.maxPoolSize,
                    //AllowZeroDateTime = true,
                    //ConvertZeroDateTime = true,
                    //DefaultCommandTimeout = 300,
                    ConnectTimeout = 10,
                    Pooling = true
                };

                this.setConnectionString(connectionString.ToString());
            }
        }

        public void destroy()
        {
            lock (this)
            {
                this.isConnected = false;
                if (this.databaseClients != null)
                {
                    foreach (MySqlClient client in this.databaseClients)
                    {
                        if (!client.isAvailable())
                        {
                            client.Dispose();
                        }
                        client.disconnect();
                    }
                    this.databaseClients.Clear();
                }
            }
        }

        private void disconnectUnusedClients()
        {
            lock (this)
            {
                foreach (MySqlClient client in this.databaseClients)
                {
                    if (client.isAvailable())
                    {
                        client.disconnect();
                    }
                }
            }
        }

        internal string getConnectionString()
        {
            return this.connectionString;
        }

        public IQueryAdapter getQueryreactor()
        {
            IDatabaseClient dbClient = null;
            lock (connections.SyncRoot)
            {
                if (connections.Count > 0)
                {
                    dbClient = (IDatabaseClient)connections.Dequeue();
                }
            }

            if (dbClient != null)
            {
                dbClient.connect();
                dbClient.prepare();
                return dbClient.getQueryReactor();
            }
            else
            {
                if (type == DatabaseType.MySQL)
                {
                    IDatabaseClient connection = new MySqlClient(this, 0);
                    connection.connect();
                    connection.prepare();
                    return connection.getQueryReactor();
                }
                else
                {
                    IDatabaseClient connection = new MsSQLClient(this, 0);
                    connection.connect();
                    connection.prepare();
                    return connection.getQueryReactor();
                }
            }
        }

        internal void FreeConnection(IDatabaseClient dbClient)
        {
            lock (connections.SyncRoot)
            {
                connections.Enqueue(dbClient);
            }
        }

        public void Setup()
        {
            Logger.WriteLine(LogState.Info, "DatabaseSystem connected!");
            //init database system
            DatabaseType DataType = (DatabaseType)Enum.Parse(typeof(DatabaseType), LoginServer.loginserverConfig.DbType);
            LoginServer.dbManager = new DatabaseManager(LoginServer.loginserverConfig.DbMaxPoolSize, LoginServer.loginserverConfig.DbMinPoolSize, DataType);
            LoginServer.dbManager.setServerDetails(LoginServer.loginserverConfig.DbHost, LoginServer.loginserverConfig.DbPort, LoginServer.loginserverConfig.DbUser, LoginServer.loginserverConfig.DbPassword, LoginServer.loginserverConfig.DbName);
        }

        public void Init()
        {
            try
            {
                this.createNewConnectionString();
                this.databaseClients = new List<MySqlClient>((int)this.maxPoolSize);
            }
            catch (MySqlException exception)
            {
                this.isConnected = false;
                throw new Exception("Could not connect the clients to the database: " + exception.Message);
            }
            this.isConnected = true;
        }

        public bool isConnectedToDatabase()
        {
            return this.isConnected;
        }

        private void setConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool setServerDetails(string host, uint port, string username, string password, string databaseName)
        {
            try
            {
                this.server = new DatabaseServer(host, port, username, password, databaseName);
                return true;
            }
            catch (DatabaseException)
            {
                this.isConnected = false;
                return false;
            }
        }
    }
}