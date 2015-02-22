using Database_Manager.Database;
using DevTera;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utils.Logger;

namespace Configuration
{
    public class DatabaseSystem
    {
        #region properties
        public DatabaseSystem()
        {
        }
        #endregion properties

        #region Init
        public void Init()
        {
            Logger.WriteLine(LogState.Info, "DatabaseSystem connected!");
            //init database system
            DatabaseType DataType = (DatabaseType)Enum.Parse(typeof(DatabaseType), LoginServer.loginserverConfig.DbType);
            LoginServer.dbManager = new DatabaseManager(LoginServer.loginserverConfig.DbMaxPoolSize, LoginServer.loginserverConfig.DbMinPoolSize, DataType);
            LoginServer.dbManager.setServerDetails(LoginServer.loginserverConfig.DbHost, LoginServer.loginserverConfig.DbPort, LoginServer.loginserverConfig.DbUser, LoginServer.loginserverConfig.DbPassword, LoginServer.loginserverConfig.DbName);
            LoginServer.dbManager.Init();
        }
        #endregion Init
    
        #region Functions
        #endregion Functions

    }
}
