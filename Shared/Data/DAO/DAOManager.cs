using Data.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Logger;

namespace Data.DAO
{
    public abstract class DAOManager
    {
        private string ConnectionString = string.Empty;
        public static MySqlConnection Connection;

        public static AccountDAO accountDAO;
        public static GuildDAO guildDAO;
        public static InventoryDAO inventoryDAO;
        public static PlayerDAO playerDAO;
        public static QuestDAO questDAO;
        public static SkillsDAO skillDAO;
        
        public DAOManager(string con)
        {
            this.ConnectionString = con;
            Connection = new MySqlConnection(this.ConnectionString);

            try
            {
                Connection.Open();
            }
            catch (Exception ex)
           {
                Logger.WriteLine(LogState.Exception, "Cannot connect to MySQL" + ex);
            }
            finally
            {
                Connection.Close();
            }
        }


        public static void Initialize(string constr)
        {
            accountDAO = new AccountDAO(constr);
            guildDAO = new GuildDAO(constr);
            inventoryDAO = new InventoryDAO(constr);
            playerDAO = new PlayerDAO(constr);
            questDAO = new QuestDAO(constr);
            skillDAO = new SkillsDAO(constr);
        }

        public byte[] HexToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public string BytesToHex(byte[] bytes)
        {
            return (bytes != null) ? BitConverter.ToString(bytes).Replace("-", "") : "";
        }
    }
}
