using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using Utils;
using Utils.Logger;

namespace Network
{
    public class TcpServer
    {
        protected string BindAddress;
        protected int BindPort;
        protected int MaxConnections;
        protected Dictionary<string, long> ConnectionsTime;

        public IScsServer Server;

        public TcpServer(string bindAddress, int bindPort, int maxConnections)
        {
            BindAddress = bindAddress;
            BindPort = bindPort;
            MaxConnections = maxConnections;
            ConnectionsTime = new Dictionary<string, long>();
        }

        public void BeginListening()
        {
            Logger.WriteLine(LogState.Info,"GameServer Listening on: {0}:{1}...", BindAddress, BindPort);
            Server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(BindAddress, BindPort));
            Server.Start();

            Server.ClientConnected += OnConnected;
            Server.ClientDisconnected += OnDisconnected;
        }

        public void ShutdownServer()
        {
            Logger.WriteLine(LogState.Info,"Shutdown GameServer...");
            Server.Stop();
        }

        protected void OnConnected(object sender, ServerClientEventArgs e)
        {
            string ip = Regex.Match(e.Client.RemoteEndPoint.ToString(), "([0-9]+).([0-9]+).([0-9]+).([0-9]+)").Value;

            Logger.WriteLine(LogState.Info,"Client connected!");

            if (ConnectionsTime.ContainsKey(ip))
            {
                if (RandomUtilities.GetCurrentMilliseconds() - ConnectionsTime[ip] < 2000)
                {
                    Process.Start("cmd",
                                  "/c netsh advfirewall firewall add rule name=\"AutoBAN (" + ip +
                                  ")\" protocol=TCP dir=in remoteip=" + ip + " action=block");

                    ConnectionsTime.Remove(ip);

                    Logger.WriteLine(LogState.Info,"TcpServer: FloodAttack prevent! Ip " + ip + " added to firewall");
                    return;
                }
                ConnectionsTime[ip] = RandomUtilities.GetCurrentMilliseconds();
            }
            else
                ConnectionsTime.Add(ip, RandomUtilities.GetCurrentMilliseconds());

            new Connection(e.Client);
        }

        protected void OnDisconnected(object sender, ServerClientEventArgs e)
        {
            Logger.WriteLine(LogState.Info,"Client disconnected!");
        }
    }
}