// Type: Hik.Communication.Scs.Server.ScsServerBase
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Collections;
using Tera.NetworkApi.Communication.Scs.Communication.Channels;
using Tera.NetworkApi.Communication.Scs.Communication.Protocols;

namespace Tera.NetworkApi.Communication.Scs.Server
{
    /// <summary>
    /// This class provides base functionality for server classes.
    /// 
    /// </summary>
    internal abstract class ScsServerBase : IScsServer
    {
        /// <summary>
        /// This object is used to listen incoming connections.
        /// 
        /// </summary>
        private IConnectionListener _connectionListener;

        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// 
        /// </summary>
        public IScsWireProtocolFactory WireProtocolFactory { get; set; }

        /// <summary>
        /// A collection of clients that are connected to the server.
        /// 
        /// </summary>
        public ThreadSafeSortedList<long, IScsServerClient> Clients { get; private set; }

        /// <summary>
        /// This event is raised when a new client is connected.
        /// 
        /// </summary>
        public event EventHandler<ServerClientEventArgs> ClientConnected;

        /// <summary>
        /// This event is raised when a client disconnected from the server.
        /// 
        /// </summary>
        public event EventHandler<ServerClientEventArgs> ClientDisconnected;

        /// <summary>
        /// Constructor.
        /// 
        /// </summary>
        protected ScsServerBase()
        {
            this.Clients = new ThreadSafeSortedList<long, IScsServerClient>();
            this.WireProtocolFactory = WireProtocolManager.GetDefaultWireProtocolFactory();
        }

        /// <summary>
        /// Starts the server.
        /// 
        /// </summary>
        public virtual void Start()
        {
            this._connectionListener = this.CreateConnectionListener();
            this._connectionListener.CommunicationChannelConnected += new EventHandler<CommunicationChannelEventArgs>(this.ConnectionListener_CommunicationChannelConnected);
            this._connectionListener.Start();
        }

        /// <summary>
        /// Stops the server.
        /// 
        /// </summary>
        public virtual void Stop()
        {
            if (this._connectionListener != null)
                this._connectionListener.Stop();
            foreach (IScsServerClient scsServerClient in this.Clients.GetAllItems())
                scsServerClient.Disconnect();
        }

        /// <summary>
        /// This method is implemented by derived classes to create appropriate connection listener to listen incoming connection requets.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        protected abstract IConnectionListener CreateConnectionListener();

        /// <summary>
        /// Handles CommunicationChannelConnected event of _connectionListener object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void ConnectionListener_CommunicationChannelConnected(object sender, CommunicationChannelEventArgs e)
        {
            ScsServerClient scsServerClient = new ScsServerClient(e.Channel)
            {
                ClientId = ScsServerManager.GetClientId(),
                WireProtocol = this.WireProtocolFactory.CreateWireProtocol()
            };
            scsServerClient.Disconnected += new EventHandler(this.Client_Disconnected);
            this.Clients[scsServerClient.ClientId] = (IScsServerClient)scsServerClient;
            this.OnClientConnected((IScsServerClient)scsServerClient);
            e.Channel.Start();
        }

        /// <summary>
        /// Handles Disconnected events of all connected clients.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void Client_Disconnected(object sender, EventArgs e)
        {
            IScsServerClient client = (IScsServerClient)sender;
            this.Clients.Remove(client.ClientId);
            this.OnClientDisconnected(client);
        }

        /// <summary>
        /// Raises ClientConnected event.
        /// 
        /// </summary>
        /// <param name="client">Connected client</param>
        protected virtual void OnClientConnected(IScsServerClient client)
        {
            EventHandler<ServerClientEventArgs> eventHandler = this.ClientConnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new ServerClientEventArgs(client));
        }

        /// <summary>
        /// Raises ClientDisconnected event.
        /// 
        /// </summary>
        /// <param name="client">Disconnected client</param>
        protected virtual void OnClientDisconnected(IScsServerClient client)
        {
            EventHandler<ServerClientEventArgs> eventHandler = this.ClientDisconnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new ServerClientEventArgs(client));
        }
    }
}
