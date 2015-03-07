using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Tera.Data.Structures.Account;
using Tera.Data.Structures.Player;
using Utils.Logger;

namespace Tera.Database.DAO
{
    public class AccountDAO : DAOManager
    {
        private MySqlConnection AccountDAOConnection;

        public AccountDAO(string conStr) : base(conStr)
        {            
            Stopwatch stopwatch = Stopwatch.StartNew();
            AccountDAOConnection = new MySqlConnection(conStr);
            AccountDAOConnection.Open();

            stopwatch.Stop();
            Logger.WriteLine(LogState.Info,"DAO: AccountDAO Initialized with {0} Accounts in {1}s"
            , LoadTotalAccounts()
            , (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00"));
        }

        public GameAccount LoadAccount(string username)
        {
                string SqlQuery = "SELECT * FROM `accounts` WHERE `username` = @username";
                MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
                SqlCommand.Parameters.AddWithValue("@username", username);
                MySqlDataReader AccountReader = SqlCommand.ExecuteReader();

                GameAccount acc = new GameAccount();
                if (AccountReader.HasRows)
                {
                    while (AccountReader.Read())
                    {
                        acc.AccountId = AccountReader.GetUInt32(0);
                        acc.Username = AccountReader.GetString(1);
                        acc.Password = AccountReader.GetString(2);
                        acc.Email = AccountReader.GetString(3);
                        acc.AccessLevel = (byte)AccountReader.GetInt32(4);
                        acc.Membership = AccountReader.GetString(5);
                        acc.IsGM = AccountReader.GetBoolean(6);
                        acc.LastOnlineUtc = AccountReader.GetInt64(7);
                        acc.Coins = AccountReader.GetUInt32(8);
                        acc.Ip = AccountReader.GetString(9);
                        acc.UiSettings = AccountReader.GetString(10);

                    }
                }
                AccountReader.Close();


                SqlQuery = "SELECT * FROM `accout_items` WHERE `Id` = @ID";
                SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
                SqlCommand.Parameters.AddWithValue("@Id", acc.AccountId);
                AccountReader = SqlCommand.ExecuteReader();

                if (AccountReader.HasRows)
                {
                    while (AccountReader.Read())
                    {
                        AccountItem tmpItem = new AccountItem();
                        tmpItem.ItemId = AccountReader.GetInt32(1);
                        tmpItem.Options = AccountReader.GetInt32(2);

                        acc.AccountItems.Add(tmpItem);
                    }
                }
                AccountReader.Close();


                return (acc.Username == "") ? null : acc;
        }

        public int LoadTotalAccounts()
        {
            string SqlQuery = "SELECT COUNT(*) FROM `accounts`";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
            MySqlDataReader AccountReader = SqlCommand.ExecuteReader();

            int count = 0;
            try
            {
                while (AccountReader.Read())
                {
                    count = AccountReader.GetInt32(0);
                }
                AccountReader.Close();
                return count;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "DAO: LOAD TOTAL ACCOUNTS ERROR! " + ex.Message);
            }
            return count;
        }

        public bool SaveAccount(GameAccount gameAccount)
        {
            string SqlQuery = "INSERT INTO `accounts` (`username`,`password`) VALUES(?username, ?password);";
            MySqlCommand SqlCommand = new MySqlCommand(SqlQuery, AccountDAOConnection);
            SqlCommand.Parameters.AddWithValue("?username", gameAccount.Username);
            SqlCommand.Parameters.AddWithValue("?password", gameAccount.Password);
            try
            {
                SqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Exception, "DAO: SAVE ACCOUNT ERROR! " + ex.Message);
            }
            return false;
        }

    }
}
