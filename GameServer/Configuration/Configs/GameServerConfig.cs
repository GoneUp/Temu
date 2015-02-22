using System;
using System.IO;
using Tera.Data.Enums;

namespace Tera.Configuration.Configs
{
    /// <summary>
    /// This Configuration class is basically just a set of 
    /// properties with a couple of static methods to manage
    /// the serialization to and deserialization from a
    /// simple XML file.
    /// </summary> 

    
  
    [Serializable]
    public class GameServerConfig
    {
         

        #region properties
        //Server
        string _ServerListenIP;
        int _ServerPort;
        string _ServerExternalIP;
        int _ServerMaxConnections;
        int _InterServerPort;
        string _InterServerPassword;
        //Database
        string _DbHost;
        uint _DbPort;
        string _DbName;
        string _DbUser;
        string _DbPassword;
        string _DbConnectionString;
        int _DbMinPoolSize;
        uint _DbMaxPoolSize;
        string _DbType;
        //Logging
        uint _LogLevel;
        string _LogFile;

        //GameSpecific Things
        eServerMode _ServerMode;
        string _WelcomeMessage;
        int _LevelCap;
        int _ServerRates;

        //Public 
        public GameServerConfig()
        {
            //Server
            _ServerListenIP = "0.0.0.0";
            _ServerPort = 2101;
            _ServerExternalIP = "127.0.0.1";
            _ServerMaxConnections = 100;
            _InterServerPort = 1000;
            _InterServerPassword = "PASSWORD";
            //Database
            _DbHost = "127.0.0.1";
            _DbPort = 3306;
            _DbName = "tera";
            _DbUser = "root";
            _DbPassword = "PASSWORD";
            _DbConnectionString = "server=" + DbHost + ";database=" + DbName + ";user id=" + DbUser + ";password=" + DbPassword + ";";
            _DbMinPoolSize = 5;
            _DbMaxPoolSize = 100;
            _DbType = "MySQL";
            //Logging
            _LogLevel = 1;
            _LogFile = string.Format(@"logs\{0}.log", DateTime.Now.ToString("d_M_yyyy HH_mm_ss"));
            //GameSpecific Things
            _ServerMode = eServerMode.Release;
            _WelcomeMessage = "Welcome to teh World of Temu..";
            _LevelCap = 60;
            _ServerRates = 1;
        }
        //Server
        public string ServerListenIP
        {
            get { return _ServerListenIP; }
            set { _ServerListenIP = value; }
        }
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }
        public string ServerExternalIP
        {
            get { return _ServerExternalIP; }
            set { _ServerExternalIP = value; }
        }
        public int ServerMaxConnections
        {
            get { return _ServerMaxConnections; }
            set { _ServerMaxConnections = value; }
        }
        public int InterServerPort
        {
            get { return _InterServerPort; }
            set { _InterServerPort = value; }
        }
        public string InterServerPassword
        {
            get { return _InterServerPassword; }
            set { _InterServerPassword = value; }
        }
        //Database
        public string DbHost
        {
            get { return _DbHost; }
            set { _DbHost = value; }
        }
        public uint DbPort
        {
            get { return _DbPort; }
            set { _DbPort = value; }
        }
        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }
        public string DbUser
        {
            get { return _DbUser; }
            set { _DbUser = value; }
        }
        public string DbPassword
        {
            get { return _DbPassword; }
            set { _DbPassword = value; }
        }
        public string DbConnectionString
        {
            get { return _DbConnectionString; }
            set { _DbConnectionString = value; }
        }
        public int DbMinPoolSize
        {
            get { return _DbMinPoolSize; }
            set { _DbMinPoolSize = value; }
        }
        public uint DbMaxPoolSize
        {
            get { return _DbMaxPoolSize; }
            set { _DbMaxPoolSize = value; }
        }
        public string DbType
        {
            get { return _DbType; }
            set { _DbType = value; }
        }
        //Logging
        public uint LogLevel
        {
            get { return _LogLevel; }
            set { _LogLevel = value; }
        }
        public string LogFile
        {
            get { return _LogFile; }
            set { _LogFile = value; }
        }

        //Game Things
        public eServerMode ServerMode
        {
            get { return _ServerMode; }
            set { _ServerMode = value; }
        }
        public string WelcomeMessage
        {
            get { return _WelcomeMessage; }
            set { _WelcomeMessage = value; }
        }
        public int LevelCap
        {
            get { return _LevelCap; }
            set { _LevelCap = value; }
        }
        public int ServerRates
        {
            get { return _LevelCap; }
            set { _LevelCap = value; }
        }


        #endregion properties

        #region Init
        public void Init()
        {
            if (!File.Exists(Config.lsPath))
            {
                //Create a new Configuration Folder
                Config.CreateDefaultFolder();
                // Create a new configuration object and initialize some variables
                //Server Config
                Tera.GameServer.gameserverConfig.ServerListenIP = _ServerListenIP;
                Tera.GameServer.gameserverConfig.ServerPort = _ServerPort;
                Tera.GameServer.gameserverConfig.ServerExternalIP = _ServerExternalIP;
                Tera.GameServer.gameserverConfig.ServerMaxConnections = _ServerMaxConnections;
                Tera.GameServer.gameserverConfig.InterServerPort = _InterServerPort;
                Tera.GameServer.gameserverConfig.InterServerPassword = _InterServerPassword;
                //Database Config
                Tera.GameServer.gameserverConfig.DbHost = _DbHost;
                Tera.GameServer.gameserverConfig.DbPort = _DbPort;
                Tera.GameServer.gameserverConfig.DbName = _DbName;
                Tera.GameServer.gameserverConfig.DbUser = _DbUser;
                Tera.GameServer.gameserverConfig.DbPassword = _DbPassword;
                Tera.GameServer.gameserverConfig.DbConnectionString = _DbConnectionString;
                Tera.GameServer.gameserverConfig.DbMinPoolSize = _DbMinPoolSize;
                Tera.GameServer.gameserverConfig.DbMaxPoolSize = _DbMaxPoolSize;
                Tera.GameServer.gameserverConfig.DbType = _DbType;
                //Logging Config
                Tera.GameServer.gameserverConfig.LogLevel = _LogLevel;
                Tera.GameServer.gameserverConfig.LogFile = _LogFile;
                // Serialize the configuration object to a file
            Config.WriteToXmlFile<GameServerConfig>(Config.lsPath,  Tera.GameServer.gameserverConfig);
            }
            else
            {
                //Read the configuration object from a file
                 Tera.GameServer.gameserverConfig = Config.ReadFromXmlFile<GameServerConfig>(Config.lsPath);
                //read out the variables from file
                ServerListenIP =  Tera.GameServer.gameserverConfig.ServerListenIP;
                ServerPort =  Tera.GameServer.gameserverConfig.ServerPort;
                ServerExternalIP =  Tera.GameServer.gameserverConfig.ServerExternalIP;
                ServerMaxConnections =  Tera.GameServer.gameserverConfig.ServerMaxConnections;
                InterServerPort =  Tera.GameServer.gameserverConfig.InterServerPort;
                InterServerPassword =  Tera.GameServer.gameserverConfig.InterServerPassword;
                //Database Config
                DbHost =  Tera.GameServer.gameserverConfig.DbHost;
                DbPort =  Tera.GameServer.gameserverConfig.DbPort;
                DbName =  Tera.GameServer.gameserverConfig.DbName;
                DbUser =  Tera.GameServer.gameserverConfig.DbUser;
                DbPassword =  Tera.GameServer.gameserverConfig.DbPassword;
                DbConnectionString =  Tera.GameServer.gameserverConfig.DbConnectionString;
                DbMinPoolSize =  Tera.GameServer.gameserverConfig.DbMinPoolSize;
                DbMaxPoolSize =  Tera.GameServer.gameserverConfig.DbMaxPoolSize;
                DbType =  Tera.GameServer.gameserverConfig.DbType;
                //Logging Config
                LogLevel =  Tera.GameServer.gameserverConfig.LogLevel;
                LogFile =  Tera.GameServer.gameserverConfig.LogFile;
            }
        }
        #endregion Init
    }
}
