using System;
using System.Diagnostics;
using Network;
using NetworkApi.Communication.Scs.Communication.EndPoints.Tcp;
using NetworkApi.Communication.Scs.Communication.Messages;
using NetworkApi.Communication.Scs.Server;
using Utils;
using System.Threading;
using DevTera;
using Utils.Logger;
using Configuration;
using Database_Manager.Database;

namespace Network
{
    public class LoginServerConnection
    {
        public static bool ServerShutdown = false;
        public static bool ServerIsWork = true;

        public static void ServerStart()
        {
            Console.WriteLine("----------------------------------------------------------------------------\n"
            + "---===== Loading LoginServer Service.\n"
            + "----------------------------------------------------------------------------");

        }
        public static void MainLoop()
        {
            while (ServerIsWork)
            {
                try
                {
                    //Server Background Worker Do The Action Here!

                    //some loop for acc checks or else... but not needed..
                    //if (Functions.GetCurrentMilliseconds() - Data.DAO.DAOManager.LastSaveUts > 600000) // Backup Every 10 Min
                    //{
                    //    Data.DAO.DAOManager.LastSaveUts = Functions.GetCurrentMilliseconds();
                    //    Data.DAO.DAOManager.SaveData();
                    //}
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(LogState.Error, "MainLoop:\n" + ex);
                }

                Thread.Sleep(10);//Main Loop Check Timer
            }
        }
        public static void ShutdownServer()
        {
            if (ServerShutdown) 
            { 
                return; 
            }
            else 
            {
                ShowShutdownMessage();
                ServerShutdown = true;
                ServerIsWork = false;
            }
        }
        public static void ShowShutdownMessage()
        {
            Logger.WriteLine(LogState.Debug, "---------------------");
            Logger.WriteLine(LogState.Debug, "Starting shootdown...");
            Logger.WriteLine(LogState.Debug, "---------------------");
            Thread.Sleep(3000);
        }

    }
}
