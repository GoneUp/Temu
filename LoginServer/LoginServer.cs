//@P5yl0
using Configuration;
using Database_Manager.Database;
using Network;
using System;
using System.Diagnostics;
using System.Threading;
using Utils.Logger;


namespace DevTera
{
    internal class LoginServer
    {
        public static TcpServer TcpLoginServer;
        public static LoginServerConfig loginserverConfig = new LoginServerConfig();
        public static Logger logger = new Logger();
        public static DatabaseSystem dbs = new DatabaseSystem();
        public static DatabaseManager dbManager;

        #region Main
        public static void Main()
        {
            //Set Title
            Console.Title = "[Tera-Online] {ProjectS1.LoginServer}";
            //Cancel KeyEvent
            Console.CancelKeyPress += CancelEventHandler;

            try
            {
                RunServer();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Error, "Can't start server!\n " + ex);
                return;
            }

            //Main Loop
            LoginServerConnection.MainLoop();
            //Stop Server on Errors
            StopServer();
            //Kill Server Process
            Process.GetCurrentProcess().Kill();
        }
        #endregion Main
        public static void RunServer()
        {
            Stopwatch serverStartStopwatch = Stopwatch.StartNew();//Server Start Timer
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;//ExceptionHandler

            PrintServerLicence();//Server Copyright,Infos
            
            //init configs
            Config.Init_LS_Config();
            Config.Init_LOG_Config();
            Config.Init_DB_Config();

            //Initialize TcpServer
            TcpLoginServer = new TcpServer(loginserverConfig.ServerListenIP, Convert.ToInt32(loginserverConfig.ServerPort), Convert.ToInt32(loginserverConfig.ServerMaxConnections));
            //Start Tcp-Server Listening
            LoginServerConnection.ServerStart(); //Server Service
            TcpLoginServer.BeginListening();  //Server Connection Listener

            //Stop ServerStartTime
            serverStartStopwatch.Stop();
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("---===== LoginServer OnLine in {0}", (serverStartStopwatch.ElapsedMilliseconds / 1000.0).ToString("0.00s"));
            Console.WriteLine("----------------------------------------------------------------------------");

            //Logger.WriteLine(LogState.Error, "TEST: " + idfactorySystemConfig.currentId);
            //DbQuerys.SetIsGM("test", true);
            //DbQuerys.SetIsOnline("test", true);
            //DbQuerys.SetEmail("test", "hahaha@hahaha.de");
            //DbQuerys.SetCoins("test", 9999);
            //DbQuerys.SetBanStatus("test", true, 99999999);
            //DbQuerys.SetMembership("test", 20001);
            //DbQuerys.SetPassword("test", "test");
            //DbQuerys.SetAccessLevel("test", 1);
            //DbQuerys.SetLastOnlineUtc("test");
            //DbQuerys.SetUsername("prototype", "prototype");
            //DbQuerys.InsertAccountData("test23", "test", "t@t.de", 0, "1", true, 0, 0, "1", "000010000", true, true, 1, 0);
            //DbQuerys.UpdateAccountData_byID(5, "test8987", "PASSWORD", "t@t.de", 0, "1", true, 0, 0, "1", "000010000", true, true, 1);



            try
            {
                throw new ObjectDisposedException("");
            }
            catch (Exception ex)
            {
                
               Console.WriteLine (ex);
               Console.WriteLine(ex.ToString());
               Console.WriteLine(ex.Message);
 
            }
        }

        #region ConsoleOutput-Infos
        //License Text
        public static void PrintServerLicence()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            //Licence Info
            Console.WriteLine("----------------------------------------------------------------------------\n"
            + "ProjectS1.LoginServer\n\n"
            + "This program is free software;\nyou can redistribute it and/or modify it under the terms of the \nGNU General Public License as published by the Free Software Foundation;\neither version 2 of the License, or (at your option) any later version.\n\n"
            + "This program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.\n\nSee the GNU General Public License for more details.\n\n"
            + "Copyright (C) 2013 (ProjectS1.LoginServer)\n"
            + "Author: P5yl0\n "
            + "----------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        
        
        #endregion ConsoleOutput-Infos

        #region ServerEvents
        //Unhandled Server Exception Event
        public static void UnhandledException(Object sender, UnhandledExceptionEventArgs args)
        {
            Logger.WriteLine(LogState.Error, "UnhandledException:\n" + (Exception)args.ExceptionObject);
            LoginServerConnection.ShutdownServer();

            while (true)
            {
                Thread.Sleep(1);
            }
        }
        //Server Cancel Event
        public static void CancelEventHandler(object sender, ConsoleCancelEventArgs args)
        {
            LoginServerConnection.ShutdownServer();
            while (true)
            {
                Thread.Sleep(1);
            }
        }
        //Server Stop
        public static void StopServer()
        {
            TcpLoginServer.ShutdownServer();
            LoginServerConnection.ShutdownServer();
        }
        
        #endregion ServerEvents

    }
}
