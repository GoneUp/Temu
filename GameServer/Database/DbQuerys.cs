using System;
using System.Data;
using Database_Manager.Database.Session_Details.Interfaces;
using Tera.Data.Structures.Account;
using Utils.Logger;

namespace Tera.Database
{
    public class DbQuerys
    {

        //Set GM Status, Name,bool
        public static bool SetIsGM(string username, bool newIsGM)
        {
            if (!DbQuerys.UserExists(username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting IsGM! " + username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string SetIsGmQuery = "UPDATE accounts SET IsGM=" + newIsGM + " WHERE Username='" + username + "';";
                dbClient.setQuery(SetIsGmQuery);
                dbClient.addParameter("IsGM", newIsGM);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set IsGM: " + newIsGM + " for, " + username + ".)");
                return true;
            }
        }
        //Set Current Date(Int64)
        public static bool SetLastOnlineUtc(string username)
        {
            Int64 timeStampNowValue = Convert.ToInt64(DateTime.Now.Ticks);
            if (!DbQuerys.UserExists(username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting LastOnlineUtc! " + username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string setLastOnlineUtcQuery = "UPDATE accounts SET LastOnlineUtc='" + timeStampNowValue + "' WHERE Username='" + username + "';";
                dbClient.setQuery(setLastOnlineUtcQuery);
                dbClient.addParameter("LastOnlineUtc", timeStampNowValue);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set LastOnlineUtc: " + timeStampNowValue + " for, " + username + ".)");
                return true;
            }
        }
        //Set Coins
        public static bool SetCoins(string username, int newCoins)
        {
            if (!DbQuerys.UserExists(username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting Coins! " + username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string setCoinsQuery = "UPDATE accounts SET Coins='" + newCoins + "' WHERE Username='" + username + "';";
                dbClient.setQuery(setCoinsQuery);
                dbClient.addParameter("Coins", newCoins);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set Coins: " + newCoins + " for, " + username + ".)");
                return true;
            }
        }
        //Set Online Status, Name,bool
        public static bool SetIsOnline(string username, bool newIsOnline)
        {
            if (!DbQuerys.UserExists(username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting IsOnline! " + username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string setIsOnlineQuery = "UPDATE accounts SET IsOnline=" + newIsOnline + " WHERE Username='" + username + "';";
                dbClient.setQuery(setIsOnlineQuery);
                dbClient.addParameter("IsOnline", newIsOnline);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set IsOnline: " + newIsOnline + " for, " + username + ".)");
                return true;
            }
        }
        //Set Ban Status, Name,bool,Date
        public static bool SetBanStatus(string username, bool newIsBanned, Int64 newUnBanDate)
        {
            Int64 timeStampNowValue = Convert.ToInt64(DateTime.Now.Ticks);

            if (!DbQuerys.UserExists(username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting SetBanStatus! " + username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string setBanStatusQuery = "UPDATE accounts SET IsBanned=" + newIsBanned + ", UnBanDate='" + newUnBanDate + "' WHERE Username='" + username + "';";
                dbClient.setQuery(setBanStatusQuery);
                dbClient.addParameter("IsBanned", newIsBanned);
                dbClient.addParameter("UnBanDate", newUnBanDate);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set isBanned: " + newIsBanned + " till: " + newUnBanDate + " for, " + username + ".)");
                return true;
            }
        }

        //Check if Username Exists
        public static bool UserExists(string username)
        {
            DataTable Data = new DataTable();
            GameAccount Account = new GameAccount();

            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts WHERE Username='" + username + "' LIMIT 1;";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new GameAccount()
                        {
                            AccountId = uint.Parse(Row["id"].ToString()),
                            Username = Row["username"].ToString(),
                            Password = Row["password"].ToString(),
                            Email = Row["email"].ToString(),
                            AccessLevel = uint.Parse(Row["accesslevel"].ToString()),
                            Membership = Row["membership"].ToString(),
                            IsGM = Convert.ToBoolean(int.Parse(Row["isgm"].ToString())),
                            LastOnlineUtc = Convert.ToInt64((long.Parse(Row["lastonlineutc"].ToString()))),
                            Coins = Convert.ToUInt32((Row["coins"].ToString())),
                            Ip = Row["ip"].ToString(),
                            UiSettings = Row["settings"].ToString(),
                            IsBanned = Convert.ToBoolean(int.Parse(Row["isbanned"].ToString())),
                            UnBanDate = Convert.ToInt64((long.Parse(Row["unbandate"].ToString()))),
                            RegisterDate = Convert.ToInt64((long.Parse(Row["registerdate"].ToString()))),
                        };


                        if (Account.Username != null || Account.Username == username)
                        {
                            Logger.WriteLine(LogState.Debug, "User {0} found!!", username);
                            return true;
                        }
                    }
                }
            }
            Logger.WriteLine(LogState.Debug, "User {0} not found!", username);
            return false;
        }
        //Verify User&Pass
        public static bool VerifyUser(string Username, string Password)
        {
            DataTable Data = new DataTable();
            GameAccount Account = new GameAccount();

            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts WHERE Username='" + Username + "' && Password='" + Password + "';";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new GameAccount()
                        {
                            AccountId = uint.Parse(Row["id"].ToString()),
                            Username = Row["username"].ToString(),
                            Password = Row["password"].ToString(),
                            Email = Row["email"].ToString(),
                            AccessLevel = uint.Parse(Row["accesslevel"].ToString()),
                            Membership = Row["membership"].ToString(),
                            IsGM = Convert.ToBoolean(int.Parse(Row["isgm"].ToString())),
                            LastOnlineUtc = Convert.ToInt64((long.Parse(Row["lastonlineutc"].ToString()))),
                            Coins = Convert.ToUInt32((Row["coins"].ToString())),
                            Ip = Row["ip"].ToString(),
                            Settings = Row["settings"].ToString(),
                            //IsOnline = Convert.ToBoolean(int.Parse(Row["isonline"].ToString())),
                            IsBanned = Convert.ToBoolean(int.Parse(Row["isbanned"].ToString())),
                            UnBanDate = Convert.ToInt64((long.Parse(Row["unbandate"].ToString()))),
                            RegisterDate = Convert.ToInt64((long.Parse(Row["registerdate"].ToString()))),
                        };

                    }

                    if (Account.Username != null)
                    {
                        if (Account.Username != Username)
                        {
                            Logger.WriteLine(LogState.Error, "Account.Username missmatch!");
                            return false;
                        }
                        if (Account.Username == Username && Account.Password == Password)
                        {
                            Logger.WriteLine(LogState.Info, "Verify ok!");
                            return true;
                        }
                    }
                }
            }
            Logger.WriteLine(LogState.Error, "Verify failed!");
            return false;
        }
        //Update by Account Name
        public static bool UpdateAccountData_byUsername(string Username, string Password, string Email, int AccessLevel, string Membership, bool isGM, long LastOnlineUtc, int Coins, string Ip, string Settings, bool IsOnline, bool IsBanned, long UnBanDate)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "UpdateAccountData Failure: " + Username + ",not Exists, please choose another!");
                return false;
            }
            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string UpdateAccountQuery = "UPDATE accounts SET Username='" + Username + "',Password='" + Password + "',Email='" + Email + "',AccessLevel='" + AccessLevel + "',Membership='" + Membership + "',isGM=" + isGM + ",LastOnlineUtc='" + LastOnlineUtc + "',Coins='" + Coins + "',Ip='" + Ip + "',Settings='" + Settings + "',IsOnline=" + IsOnline + ",IsBanned=" + IsBanned + ",UnBanDate='" + UnBanDate + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(UpdateAccountQuery);
                dbClient.addParameter("Username", Username);
                dbClient.addParameter("Password", Password);
                dbClient.addParameter("Email", Email);
                dbClient.addParameter("AccessLevel", AccessLevel);
                dbClient.addParameter("Membership", Membership);
                dbClient.addParameter("isGM", isGM);
                dbClient.addParameter("LastOnlineUtc", LastOnlineUtc);
                dbClient.addParameter("Coins", Coins);
                dbClient.addParameter("Ip", Ip.ToString());
                dbClient.addParameter("Settings", Settings);
                dbClient.addParameter("IsOnline", IsOnline);
                dbClient.addParameter("IsBanned", IsBanned);
                dbClient.addParameter("UnBanDate", UnBanDate);
                dbClient.runQuery();

                Logger.WriteLine(LogState.Info, "Updated Account: {0}.)", Username);
                return true;
            }
        }
        //Update by Account Id
        public static bool UpdateAccountData_byID(int AccountId, string Username, string Password, string Email, int AccessLevel, string Membership, bool isGM, long LastOnlineUtc, int Coins, string Ip, string Settings, bool IsOnline, bool IsBanned, long UnBanDate)
        {
            if (DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "UpdateAccountData Failure: " + Username + ", already Exists, please choose another!");
                return false;
            }

            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string UpdateAccountQuery = "UPDATE accounts SET Username='" + Username + "',Password='" + Password + "',Email='" + Email + "',AccessLevel='" + AccessLevel + "',Membership='" + Membership + "',isGM=" + isGM + ",LastOnlineUtc='" + LastOnlineUtc + "',Coins='" + Coins + "',Ip='" + Ip + "',Settings='" + Settings + "',IsOnline=" + IsOnline + ",IsBanned=" + IsBanned + ",UnBanDate='" + UnBanDate + "' WHERE Id=" + AccountId + ";";
                dbClient.setQuery(UpdateAccountQuery);
                dbClient.addParameter("Username", Username);
                dbClient.addParameter("Password", Password);
                dbClient.addParameter("Email", Email);
                dbClient.addParameter("AccessLevel", AccessLevel);
                dbClient.addParameter("Membership", Membership);
                dbClient.addParameter("isGM", isGM);
                dbClient.addParameter("LastOnlineUtc", LastOnlineUtc);
                dbClient.addParameter("Coins", Coins);
                dbClient.addParameter("Ip", Ip.ToString());
                dbClient.addParameter("Settings", Settings);
                dbClient.addParameter("IsOnline", IsOnline);
                dbClient.addParameter("IsBanned", IsBanned);
                dbClient.addParameter("UnBanDate", UnBanDate);

                dbClient.runQuery();

                Logger.WriteLine(LogState.Info, "Updated AccountId: {0} as {1}.)", AccountId, Username);
                return true;
            }
        }

        //Read AccountData for Username&Pass, return as Account()
        public static GameAccount GetAccountData(string Username, string Password)
        {
            DataTable Data = new DataTable();
            GameAccount Account = new GameAccount();

            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts WHERE Username='" + Username + "' AND Password='" + Password + "';";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new GameAccount()
                        {
                            AccountId = uint.Parse(Row["id"].ToString()),
                            Username = Row["username"].ToString(),
                            Password = Row["password"].ToString(),
                            Email = Row["email"].ToString(),
                            AccessLevel = uint.Parse(Row["accesslevel"].ToString()),
                            Membership = Row["membership"].ToString(),
                            IsGM = Convert.ToBoolean(int.Parse(Row["isgm"].ToString())),
                            LastOnlineUtc = Convert.ToInt64((long.Parse(Row["lastonlineutc"].ToString()))),
                            Coins = Convert.ToUInt32((Row["coins"].ToString())),
                            Ip = Row["ip"].ToString(),
                            Settings = Row["settings"].ToString(),
                            //IsOnline = Convert.ToBoolean(int.Parse(Row["isonline"].ToString())),
                            IsBanned = Convert.ToBoolean(int.Parse(Row["isbanned"].ToString())),
                            UnBanDate = Convert.ToInt64((long.Parse(Row["unbandate"].ToString()))),
                            RegisterDate = Convert.ToInt64((long.Parse(Row["registerdate"].ToString()))),
                        };

                        return Account;
                    }
                }
            }
            Logger.WriteLine(LogState.Error, "Get Account Data failed, wrong pass?");
            Account = null;
            return Account;
        }
        public static int GetCurrentAccountCount()
        {
            DataTable Data = new DataTable();
            GameAccount Account = new GameAccount();

            using (IQueryAdapter dbClient = GameServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new GameAccount()
                        {
                            AccountId = uint.Parse(Row["Id"].ToString()),
                            Username = Row["Username"].ToString(),
                            Password = Row["Password"].ToString(),
                            Email = Row["Email"].ToString(),
                            AccessLevel = uint.Parse(Row["AccessLevel"].ToString()),
                            Membership = Row["Membership"].ToString(),
                            IsGM = bool.Parse(Row["IsGM"].ToString()),
                            LastOnlineUtc = Convert.ToInt64((long.Parse(Row["LastOnlineUtc"].ToString()))),
                            Coins = Convert.ToUInt32((Row["Coins"].ToString())),
                            Ip = Row["Ip"].ToString(),
                            Settings = Row["Settings"].ToString(),
                            //IsOnline = bool.Parse(Row["IsOnline"].ToString()),
                            IsBanned = bool.Parse(Row["IsBanned"].ToString()),
                            UnBanDate = Convert.ToInt64((long.Parse(Row["UnBanDate"].ToString()))),
                        };
                    }

                    return (int)Data.Rows.Count;
                }
            }
            Logger.WriteLine(LogState.Info, "No Users found, All Accounts free?");
            return 0;
        }

    }

}