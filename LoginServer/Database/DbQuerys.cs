using Database_Manager.Database.Session_Details.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Data;
using AccountService;
using Utils.Logger;
using DevTera;
using Utils.Crypt;


namespace Database_Manager.Database
{
    public class DbQuerys
    {
        //Set New Username, Username,NewUsername
        public static bool SetUsername(string oldUsername, string newUserName)
        {
            if (DbQuerys.UserExists(newUserName))
            {
                Logger.WriteLine(LogState.Exception, "Error setting Username! " + newUserName + ", User already Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetUsernameQuery = "UPDATE accounts SET Username='" + newUserName + "' WHERE Username='" + oldUsername + "';";
                dbClient.setQuery(SetUsernameQuery);
                dbClient.addParameter("Username", newUserName);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set Username: " + newUserName + " for, " + oldUsername + ".)");
                return true;
            }
        }
        //Set New Password, Username,NewPassword
        public static bool SetPassword(string Username, string newPassword)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting Password! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                newPassword = Crypt.StringToMD5(newPassword);
                string SetPasswordQuery = "UPDATE accounts SET Password='" + newPassword + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetPasswordQuery);
                dbClient.addParameter("Password", newPassword);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set Password: " + newPassword + " for, " + Username + ".)");
                return true;
            }
        }
        //Set New Email, Username,NewEmail
        public static bool SetEmail(string Username, string newEmail)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting Email! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetEmailQuery = "UPDATE accounts SET Email='" + newEmail + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetEmailQuery);
                dbClient.addParameter("Email", newEmail);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set Email: " + newEmail + " for, " + Username + ".)");
                return true;
            }
        }
        //Set Access Level 0-....
        public static bool SetAccessLevel(string Username, int newAccessLevel)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting AccessLevel! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetAccessLevelQuery = "UPDATE accounts SET AccessLevel='" + newAccessLevel + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetAccessLevelQuery);
                dbClient.addParameter("AccessLevel", newAccessLevel);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set AccessLevel: " + newAccessLevel + " for, " + Username + ".)");
                return true;
            }
        }
        //Set Membership, 20000,20001,.....
        public static bool SetMembership(string Username, int newMembership)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting Membership! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetMembershipQuery = "UPDATE accounts SET Membership='" + newMembership + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetMembershipQuery);
                dbClient.addParameter("Membership", newMembership);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set Membership: " + newMembership + " for, " + Username + ".)");
                return true;
            }
        }
        //Set GM Status, Name,bool
        public static bool SetIsGM(string Username, bool newIsGM)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting IsGM! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetIsGmQuery = "UPDATE accounts SET IsGM=" + newIsGM + " WHERE Username='" + Username + "';";
                dbClient.setQuery(SetIsGmQuery);
                dbClient.addParameter("IsGM", newIsGM);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set IsGM: " + newIsGM + " for, " + Username + ".)");
                return true;
            }
        }
        //Set Current Date(Int64)
        public static bool SetLastOnlineUtc(string Username)
        {
            Int64 timeStampNowValue = Convert.ToInt64(DateTime.Now.Ticks);
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting LastOnlineUtc! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetLastOnlineUtcQuery = "UPDATE accounts SET LastOnlineUtc='" + timeStampNowValue + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetLastOnlineUtcQuery);
                dbClient.addParameter("LastOnlineUtc", timeStampNowValue);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set LastOnlineUtc: " + timeStampNowValue + " for, " + Username + ".)");
                return true;
            }
        }
        //Set Coins
        public static bool SetCoins(string Username, int newCoins)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting Coins! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetCoinsQuery = "UPDATE accounts SET Coins='" + newCoins + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetCoinsQuery);
                dbClient.addParameter("Coins", newCoins);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set Coins: " + newCoins + " for, " + Username + ".)");
                return true;
            }
        }
        //Set Online Status, Name,bool
        public static bool SetIsOnline(string Username, bool newIsOnline)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting IsOnline! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetIsOnlineQuery = "UPDATE accounts SET IsOnline=" + newIsOnline + " WHERE Username='" + Username + "';";
                dbClient.setQuery(SetIsOnlineQuery);
                dbClient.addParameter("IsOnline", newIsOnline);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set IsOnline: " + newIsOnline + " for, " + Username + ".)");
                return true;
            }
        }
        //Set Ban Status, Name,bool,Date
        public static bool SetBanStatus(string Username, bool newIsBanned, Int64 newUnBanDate)
        {
            Int64 timeStampNowValue = Convert.ToInt64(DateTime.Now.Ticks);

            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "Error setting SetBanStatus! " + Username + ", User does not  Exists!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string SetBanStatusQuery = "UPDATE accounts SET IsBanned=" + newIsBanned + ", UnBanDate='" + newUnBanDate + "' WHERE Username='" + Username + "';";
                dbClient.setQuery(SetBanStatusQuery);
                dbClient.addParameter("IsBanned", newIsBanned);
                dbClient.addParameter("UnBanDate", newUnBanDate);
                dbClient.runQuery();
                Logger.WriteLine(LogState.Info, "Set isBanned: " + newIsBanned + " till: " + newUnBanDate + " for, " + Username + ".)");
                return true;
            }
        }

        //Check if Username Exists
        public static bool UserExists(string Username)
        {
            DataTable Data = new DataTable();
            Account Account = new Account();

            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts WHERE Username='" + Username + "' LIMIT 1;";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new Account()
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
                            IsOnline = Convert.ToBoolean(int.Parse(Row["isonline"].ToString())),
                            IsBanned = Convert.ToBoolean(int.Parse(Row["isbanned"].ToString())),
                            UnBanDate = Convert.ToInt64((long.Parse(Row["unbandate"].ToString()))),
                            RegisterDate = Convert.ToInt64((long.Parse(Row["registerdate"].ToString()))),
                        };


                        if (Account.Username != null || Account.Username == Username)
                        {
                            Logger.WriteLine(LogState.Debug, "User {0} found!!", Username);
                            return true;
                        }
                    }
                }
            }
            Logger.WriteLine(LogState.Debug, "User {0} not found!", Username);
            return false;
        }
        //Verify User&Pass
        public static bool VerifyUser(string Username, string Password)
        {
            DataTable Data = new DataTable();
            Account Account = new Account();

            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts WHERE Username='" + Username + "' && Password='" + Password + "';";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new Account()
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
                            IsOnline = Convert.ToBoolean(int.Parse(Row["isonline"].ToString())),
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
        //Insert New Account Data
        public static bool InsertAccountData(string Username, string Password, string Email, int AccessLevel, string Membership, bool isGM, long LastOnlineUtc, int Coins, string Ip, string Settings, bool IsOnline, bool IsBanned, long UnBanDate, long RegisterDate)
        {
            if (DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "InsertAccount Failure: " + Username + ", already Exists, please choose another!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string AddAccount = "INSERT INTO accounts VALUES(NULL, @Username,@Password,@Email,@AccessLevel,@Membership,@isGM,@LastOnlineUtc,@Coins,@Ip,@Settings,@IsOnline,@IsBanned,@UnBanDate,@RegisterDate)";
                dbClient.setQuery(AddAccount);
                dbClient.addParameter("@Username", Username);
                dbClient.addParameter("@Password", Crypt.StringToMD5(Password));
                dbClient.addParameter("@Email", Email);
                dbClient.addParameter("@AccessLevel", AccessLevel);
                dbClient.addParameter("@Membership", Membership);
                dbClient.addParameter("@isGM", isGM);
                dbClient.addParameter("@LastOnlineUtc", LastOnlineUtc);
                dbClient.addParameter("@Coins", Coins);
                dbClient.addParameter("@Ip", Ip);
                dbClient.addParameter("@Settings", Settings);
                dbClient.addParameter("@IsOnline", IsOnline);
                dbClient.addParameter("@IsBanned", IsBanned);
                dbClient.addParameter("@UnBanDate", UnBanDate);
                dbClient.addParameter("@RegisterDate", RegisterDate);
                dbClient.runQuery();

                Logger.WriteLine(LogState.Info, "Inserted Account: {0} successfully.)", Username);
                return true;
            }
        }
        //Update by Account Name
        public static bool UpdateAccountData_byUsername(string Username, string Password, string Email, int AccessLevel, string Membership, bool isGM, long LastOnlineUtc, int Coins, string Ip, string Settings, bool IsOnline, bool IsBanned, long UnBanDate)
        {
            if (!DbQuerys.UserExists(Username))
            {
                Logger.WriteLine(LogState.Exception, "UpdateAccountData Failure: " + Username + ",not Exists, please choose another!");
                return false;
            }
            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
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

            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
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
        public static Account GetAccountData(string Username, string Password)
        {
            DataTable Data = new DataTable();
            Account Account = new Account();

            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts WHERE Username='" + Username + "' AND Password='" + Password + "';";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new Account()
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
                            IsOnline = Convert.ToBoolean(int.Parse(Row["isonline"].ToString())),
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
            Account Account = new Account();

            using (IQueryAdapter dbClient = LoginServer.dbManager.getQueryreactor())
            {
                string GetAccountQuery = "SELECT * FROM accounts";
                dbClient.setQuery(GetAccountQuery);
                Data = dbClient.getTable();

                if (Data != null)
                {
                    foreach (DataRow Row in Data.Rows)
                    {
                        Account = new Account()
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
                            IsOnline = bool.Parse(Row["IsOnline"].ToString()),
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