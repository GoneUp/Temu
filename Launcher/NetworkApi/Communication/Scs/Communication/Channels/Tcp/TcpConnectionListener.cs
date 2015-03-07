// Type: Hik.Communication.Scs.Communication.Channels.Tcp.TcpConnectionListener
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System.Net;
using System.Net.Sockets;
using System.Threading;
using Tera.NetworkApi.Communication.Scs.Communication.EndPoints.Tcp;

namespace Tera.NetworkApi.Communication.Scs.Communication.Channels.Tcp
{
    /// <summary>
    /// This class is used to listen and accept incoming TCP
    ///             connection requests on a TCP port.
    /// 
    /// </summary>
    internal class TcpConnectionListener : ConnectionListenerBase
    {
        /// <summary>
        /// The endpoint address of the server to listen incoming connections.
        /// 
        /// </summary>
        private readonly ScsTcpEndPoint _endPoint;
        /// <summary>
        /// Server socket to listen incoming connection requests.
        /// 
        /// </summary>
        private TcpListener _listenerSocket;
        /// <summary>
        /// The thread to listen socket
        /// 
        /// </summary>
        private Thread _thread;
        /// <summary>
        /// A flag to control thread's running
        /// 
        /// </summary>
        private volatile bool _running;

        /// <summary>
        /// Creates a new TcpConnectionListener for given endpoint.
        /// 
        /// </summary>
        /// <param name="endPoint">The endpoint address of the server to listen incoming connections</param>
        public TcpConnectionListener(ScsTcpEndPoint endPoint)
        {
            this._endPoint = endPoint;
        }

        /// <summary>
        /// Starts listening incoming connections.
        /// 
        /// </summary>
        public override void Start()
        {
            this.StartSocket();
            this._running = true;
            this._thread = new Thread(new ThreadStart(this.DoListenAsThread));
            this._thread.Start();
        }

        /// <summary>
        /// Stops listening incoming connections.
        /// 
        /// </summary>
        public override void Stop()
        {
            this._running = false;
            this.StopSocket();
        }

        /// <summary>
        /// Starts listening socket.
        /// 
        /// </summary>
        private void StartSocket()
        {
            this._listenerSocket = new TcpListener(IPAddress.Any, this._endPoint.TcpPort);
            this._listenerSocket.Start();
        }

        /// <summary>
        /// Stops listening socket.
        /// 
        /// </summary>
        private void StopSocket()
        {
            try
            {
                this._listenerSocket.Stop();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Entrance point of the thread.
        ///             This method is used by the thread to listen incoming requests.
        /// 
        /// </summary>
        private void DoListenAsThread()
        {
            while (this._running)
            {
                try
                {
                    Socket clientSocket = this._listenerSocket.AcceptSocket();
                    if (clientSocket.Connected)
                        this.OnCommunicationChannelConnected((ICommunicationChannel)new TcpCommunicationChannel(clientSocket));
                }
                catch
                {
                    this.StopSocket();
                    Thread.Sleep(1000);
                    if (!this._running)
                        break;
                    try
                    {
                        this.StartSocket();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
