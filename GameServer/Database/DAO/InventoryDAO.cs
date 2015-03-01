using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Tera.Data.Enums.Item;
using Tera.Data.Structures.Account;
using Tera.Data.Structures.Player;
using Utils.Logger;

namespace Tera.Database.DAO
{
    public class InventoryDAO : DAOManager
    {
        private MySqlConnection InventoryDAOConnection;

        public InventoryDAO(string conStr) : base(conStr)
        {
            InventoryDAOConnection = new MySqlConnection(conStr);
            InventoryDAOConnection.Open();
            Logger.WriteLine(LogState.Info,"DAO: InventoryDAO Initialized!");
        }

        public bool AddItem(Player player, StorageType type, KeyValuePair<int, StorageItem> KeyVP)
        {
            string SQL = "INSERT INTO `inventory` "
                    + "(`accountname`, `playerid`, `itemid`, `amount`, `color`, `slot`, `storagetype`) "
                    + "VALUES(?accountname, ?pid, ?itemid, ?amount, ?color, ?slot, ?type);";
            MySqlCommand cmd = new MySqlCommand(SQL, InventoryDAOConnection);
            cmd.Parameters.AddWithValue("?accountname", player.AccountName);
            cmd.Parameters.AddWithValue("?pid", player.Id);
            cmd.Parameters.AddWithValue("?itemid", KeyVP.Value.ItemId);
            cmd.Parameters.AddWithValue("?amount", KeyVP.Value.Amount);
            cmd.Parameters.AddWithValue("?color", KeyVP.Value.Color);
            cmd.Parameters.AddWithValue("?slot", KeyVP.Key);
            cmd.Parameters.AddWithValue("?type", type.ToString());

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "DAO: ADD ITEM ERROR!"+  ex.Message);
            }

            return false;
        }

        public void SaveStorage(Player player, Storage storage)
        {
            string cmdString = "DELETE FROM inventory WHERE PlayerId=?pid";
            MySqlCommand command = new MySqlCommand(cmdString, InventoryDAOConnection);
            command.Parameters.AddWithValue("?pid", player.Id);
            command.ExecuteNonQuery();

            storage.Items.Add(999, storage.MoneyToItem); // add temporary save money item

            if (storage.Items.Count > 0)
            {
                foreach (var item in storage.Items)
                    AddItem(player, storage.StorageType, item);
            }
            storage.Items.Remove(999); // remove temporary save money item
        }

        public Storage LoadStorage(Player player, StorageType type)
        {
            string SQL = "SELECT * FROM `inventory` WHERE "
                + "`playerid` = ?pid AND `storagetype` = ?type";
            MySqlCommand cmd = new MySqlCommand(SQL, InventoryDAOConnection);
            cmd.Parameters.AddWithValue("?pid", player.Id);
            cmd.Parameters.AddWithValue("?type", type.ToString());
            MySqlDataReader LoadStorageReader = cmd.ExecuteReader();

            var storage = new Storage { StorageType = StorageType.Inventory };
            if (LoadStorageReader.HasRows)
            {
                while (LoadStorageReader.Read())
                {
                    if (LoadStorageReader.GetInt32(2) != 20000000)
                    {
                        StorageItem item = new StorageItem()
                        {
                            ItemId = LoadStorageReader.GetInt32(2),
                            Amount = LoadStorageReader.GetInt32(3),
                            Color = LoadStorageReader.GetInt32(4),
                        };

                        storage.Items.Add(LoadStorageReader.GetInt32(5), item);
                    }
                    else
                    {
                        storage.Money = LoadStorageReader.GetInt32(3);
                    }
                }
            }
            LoadStorageReader.Close();

            return storage;
        }

        public Storage LoadAccountStorage(GameAccount gameAccount)
        {
            string SQL = "SELECT * FROM `inventory` WHERE "
                + "`accountname` = ?accountname AND `storagetype` = ?type";
            MySqlCommand cmd = new MySqlCommand(SQL, InventoryDAOConnection);
            cmd.Parameters.AddWithValue("?accountname", gameAccount.Username);
            cmd.Parameters.AddWithValue("?type", StorageType.AccountWarehouse.ToString());
            MySqlDataReader LoadAccountStorageReader = cmd.ExecuteReader();

            var storage = new Storage { StorageType = StorageType.AccountWarehouse };
            if (LoadAccountStorageReader.HasRows)
            {
                while (LoadAccountStorageReader.Read())
                {
                    StorageItem item = new StorageItem()
                    {
                        ItemId = LoadAccountStorageReader.GetInt32(2),
                        Amount = LoadAccountStorageReader.GetInt32(3),
                        Color = LoadAccountStorageReader.GetInt32(4),
                    };
                    storage.Items.Add(LoadAccountStorageReader.GetInt32(5), item);
                }
            }
            LoadAccountStorageReader.Close();

            return storage;
        }
    }
}
