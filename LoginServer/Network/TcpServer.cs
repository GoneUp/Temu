using NetworkApi.Communication.Scs.Communication.EndPoints.Tcp;
using NetworkApi.Communication.Scs.Communication.Messages;
using NetworkApi.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tera.Data.Structures.Account;
using Tera.Network.Protocol;
using Utils.Logger;
using Database_Manager.Database;
using MySql.Data.MySqlClient;

namespace Network
{

    [Flags]
    public enum OpCode
    {
        LoginPacket = 0x1001,
        LoginPacket_Auth_OK = 0x1002,
        LoginPacket_Auth_INVALID_PASSWORD = 0x1003,
        LoginPacket_Auth_UNBANNED = 0x1004,
        LoginPacket_Auth_BANNED = 0x1005,
        LoginPacket_Auth_ONLINE = 0x1006,

        RegisterPacket = 0x2001,
        RegisterPacket_Register_Account_EXIST = 0x2002,
        RegisterPacket_Register_Account_CREATED = 0x2003,
    }

    public class TcpServer
    {
        protected string BindAddress;
        protected int BindPort;
        protected int MaxConnections;
        protected Dictionary<string, long> ConnectionsTime;

        public IScsServer Server;

        public static MySqlConnection connection;

        public TcpServer(string bindAddress, int bindPort, int maxConnections)
        {
            BindAddress = bindAddress;
            BindPort = bindPort;
            MaxConnections = maxConnections;
            ConnectionsTime = new Dictionary<string, long>();
        }

        public void BeginListening()
        {
            Server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(BindAddress, BindPort));
            
            Server.WireProtocolFactory = new KeyProtocolFactory();
            Server.ClientConnected += Server_ClientConnected;

            Server.Start();
            Logger.WriteLine(LogState.Info, "LoginServer Listening on: " + BindAddress + ":" + BindPort);
        }
        public void ShutdownServer()
        {
            Logger.WriteLine(LogState.Info, "Shutdown LoginServer...");
            Server.Stop();
        }
        static void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            e.Client.MessageReceived += Client_MessageReceived;
        }
        static void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;
            if (message == null)
            {
                return;
            }

            try
            {

                //trim the incoming message
                string[] trimMsg = message.ToString().Split(new Char[] { '&' });
                //foreach (string s in trimMsg) 
                //{
                //if (s.Trim() != "")
                //    Log.WriteLine(LogLevel.Info,"-->" + s);
                //}
                //Log.WriteLine(LogLevel.Info, "rcv. 0: " + trimMsg[0]);
                Logger.WriteLine(LogState.Info, "rcv. 1: " + trimMsg[1]);
                Logger.WriteLine(LogState.Info, "rcv. 2: " + trimMsg[2]);
                Logger.WriteLine(LogState.Info, "rcv. 3: " + trimMsg[3]);

                //setup client for response message
                var client = (IScsServerClient)sender;
                int data = Convert.ToInt32(trimMsg[1]);
               
                switch (data)
                {
                    //LoginPacket
                    case (int)OpCode.LoginPacket:
                        Logger.WriteLine(LogState.Info, "Login Try: " + trimMsg[2]);
                        client.SendMessage(new ScsTextMessage(isLoginValid(trimMsg[2], trimMsg[3])));
                        break;
                    //RegisterPacket
                    case (int)OpCode.RegisterPacket:
                        Logger.WriteLine(LogState.Info, "Register Try: " + trimMsg[2]);
                        client.SendMessage(new ScsTextMessage(isRegisterValid(trimMsg[2], trimMsg[3])));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {

                Logger.WriteLine(LogState.Exception, exception + "\n" + exception.StackTrace);
            }
        }

        public static string isLoginValid(string user, string pass)
        {
            LoginAccount loginAccount = new LoginAccount();
            loginAccount = DbQuerys.GetAccountData(user, pass);

            if (loginAccount == null)
            {
                return OpCode.LoginPacket_Auth_INVALID_PASSWORD.GetHashCode().ToString();
            }
            else
            {
                Logger.WriteLine(LogState.Debug, "Checking Account: " + loginAccount.Username);
                //BANN CHECK
                if (loginAccount.IsBanned)
                {
                    Logger.WriteLine(LogState.Debug, "Account.IsBanned: " + loginAccount.Username);
                    Logger.WriteLine(LogState.Debug, "Account.UnBanDate: " + loginAccount.UnBanDate);

                    Int64 timeStampNowValue = Convert.ToInt64(DateTime.Now.Ticks);
                    Int64 timeStampUnbannValue = loginAccount.UnBanDate;
                    Logger.WriteLine(LogState.Debug, "timeStampNowValue: '" + timeStampNowValue + "' !");
                    Logger.WriteLine(LogState.Debug, "timeStampUnbannValue: '" + timeStampUnbannValue + "' !");

                    //Unban Account if Time Ok!
                    if (timeStampUnbannValue <= timeStampNowValue)
                    {        
                        DbQuerys.SetBanStatus(user, false, 0);
                        return OpCode.LoginPacket_Auth_UNBANNED.GetHashCode().ToString();
                    }

                    //BANNED
                    loginAccount = null;
                    return OpCode.LoginPacket_Auth_BANNED.GetHashCode().ToString();
                }

                if (loginAccount.IsOnline)
                {
                    Logger.WriteLine(LogState.Info, "Account: " + loginAccount.Username + " already logged in!");
                    //DbQuerys.SetIsOnline(user, false); //this for later setting offline kicking other player maybe... for now no 2nd login possible!
                    loginAccount = null;
                    return OpCode.LoginPacket_Auth_ONLINE.GetHashCode().ToString();
                }

                //AuthorizationSuccessfull
                Logger.WriteLine(LogState.Info, "Account Verification for, " + loginAccount.Username + " Ok!");
                DbQuerys.SetLastOnlineUtc(loginAccount.Username);
                return OpCode.LoginPacket_Auth_OK.GetHashCode().ToString();
            }
        }
        public static string isRegisterValid(string user, string pass)
        {
            Logger.WriteLine(LogState.Info, "Checking Registration: " + user);

            if (DbQuerys.InsertAccountData(user, pass, "no-e@mail.de", 1, "20001", true, 0, 0, "127.0.0.1", "0", false, false, 0, Convert.ToInt64(DateTime.Now.Ticks)))
            {
                return OpCode.RegisterPacket_Register_Account_CREATED.GetHashCode().ToString();
            }
            return OpCode.RegisterPacket_Register_Account_EXIST.GetHashCode().ToString();

        }
    }
}

