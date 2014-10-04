using Database_Manager.Database;
using Tera;
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
            DatabaseType DataType = (DatabaseType)Enum.Parse(typeof(DatabaseType), GameServer.gameserverConfig.DbType);
            GameServer.dbManager = new DatabaseManager(GameServer.gameserverConfig.DbMaxPoolSize, GameServer.gameserverConfig.DbMinPoolSize, DataType);
            GameServer.dbManager.setServerDetails(GameServer.gameserverConfig.DbHost, GameServer.gameserverConfig.DbPort, GameServer.gameserverConfig.DbUser, GameServer.gameserverConfig.DbPassword, GameServer.gameserverConfig.DbName);
            GameServer.dbManager.Init();
        }
        #endregion Init
    
        #region Functions
        #endregion Functions

    }
}
