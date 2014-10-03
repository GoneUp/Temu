using Database_Manager.Database;
using DevTera;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration
{
    /// <summary>
    /// This Configuration class is basically just a set of 
    /// properties with a couple of static methods to manage
    /// the serialization to and deserialization from a
    /// simple XML file.
    /// </summary>
    [Serializable]
    public class LoginServerConfig
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

        //Public 
        public LoginServerConfig()
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
                LoginServer.loginserverConfig.ServerListenIP = _ServerListenIP;
                LoginServer.loginserverConfig.ServerPort = _ServerPort;
                LoginServer.loginserverConfig.ServerExternalIP = _ServerExternalIP;
                LoginServer.loginserverConfig.ServerMaxConnections = _ServerMaxConnections;
                LoginServer.loginserverConfig.InterServerPort = _InterServerPort;
                LoginServer.loginserverConfig.InterServerPassword = _InterServerPassword;
                //Database Config
                LoginServer.loginserverConfig.DbHost = _DbHost;
                LoginServer.loginserverConfig.DbPort = _DbPort;
                LoginServer.loginserverConfig.DbName = _DbName;
                LoginServer.loginserverConfig.DbUser = _DbUser;
                LoginServer.loginserverConfig.DbPassword = _DbPassword;
                LoginServer.loginserverConfig.DbConnectionString = _DbConnectionString;
                LoginServer.loginserverConfig.DbMinPoolSize = _DbMinPoolSize;
                LoginServer.loginserverConfig.DbMaxPoolSize = _DbMaxPoolSize;
                LoginServer.loginserverConfig.DbType = _DbType;
                //Logging Config
                LoginServer.loginserverConfig.LogLevel = _LogLevel;
                LoginServer.loginserverConfig.LogFile = _LogFile;
                // Serialize the configuration object to a file
                Config.WriteToXmlFile<LoginServerConfig>(Config.lsPath, LoginServer.loginserverConfig);
            }
            else
            {
                //Read the configuration object from a file
                LoginServer.loginserverConfig = Config.ReadFromXmlFile<LoginServerConfig>(Config.lsPath);
                //read out the variables from file
                ServerListenIP = LoginServer.loginserverConfig.ServerListenIP;
                ServerPort = LoginServer.loginserverConfig.ServerPort;
                ServerExternalIP = LoginServer.loginserverConfig.ServerExternalIP;
                ServerMaxConnections = LoginServer.loginserverConfig.ServerMaxConnections;
                InterServerPort = LoginServer.loginserverConfig.InterServerPort;
                InterServerPassword = LoginServer.loginserverConfig.InterServerPassword;
                //Database Config
                DbHost = LoginServer.loginserverConfig.DbHost;
                DbPort = LoginServer.loginserverConfig.DbPort;
                DbName = LoginServer.loginserverConfig.DbName;
                DbUser = LoginServer.loginserverConfig.DbUser;
                DbPassword = LoginServer.loginserverConfig.DbPassword;
                DbConnectionString = LoginServer.loginserverConfig.DbConnectionString;
                DbMinPoolSize = LoginServer.loginserverConfig.DbMinPoolSize;
                DbMaxPoolSize = LoginServer.loginserverConfig.DbMaxPoolSize;
                DbType = LoginServer.loginserverConfig.DbType;
                //Logging Config
                LogLevel = LoginServer.loginserverConfig.LogLevel;
                LogFile = LoginServer.loginserverConfig.LogFile;
            }
        }
        #endregion Init
    }
}
