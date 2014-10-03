// Type: Hik.Communication.ScsServices.Service.ScsServiceClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.EndPoints;
using NetworkApi.Communication.Scs.Communication.Messengers;
using NetworkApi.Communication.Scs.Server;
using NetworkApi.Communication.ScsServices.Communication;
using System;
using System.Runtime.Remoting.Proxies;

namespace NetworkApi.Communication.ScsServices.Service
{
    /// <summary>
    /// Implements IScsServiceClient.
    ///             It is used to manage and monitor a service client.
    /// 
    /// </summary>
    internal class ScsServiceClient : IScsServiceClient
    {
        /// <summary>
        /// Reference to underlying IScsServerClient object.
        /// 
        /// </summary>
        private readonly IScsServerClient _serverClient;
        /// <summary>
        /// This object is used to send messages to client.
        /// 
        /// </summary>
        private readonly RequestReplyMessenger<IScsServerClient> _requestReplyMessenger;
        /// <summary>
        /// Last created proxy object to invoke remote medhods.
        /// 
        /// </summary>
        private RealProxy _realProxy;

        /// <summary>
        /// Unique identifier for this client.
        /// 
        /// </summary>
        public long ClientId
        {
            get
            {
                return this._serverClient.ClientId;
            }
        }

        /// <summary>
        /// Gets endpoint of remote application.
        /// 
        /// </summary>
        public ScsEndPoint RemoteEndPoint
        {
            get
            {
                return this._serverClient.RemoteEndPoint;
            }
        }

        /// <summary>
        /// Gets the communication state of the Client.
        /// 
        /// </summary>
        public CommunicationStates CommunicationState
        {
            get
            {
                return this._serverClient.CommunicationState;
            }
        }

        /// <summary>
        /// This event is raised when this client is disconnected from server.
        /// 
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Creates a new ScsServiceClient object.
        /// 
        /// </summary>
        /// <param name="serverClient">Reference to underlying IScsServerClient object</param><param name="requestReplyMessenger">RequestReplyMessenger to send messages</param>
        public ScsServiceClient(IScsServerClient serverClient, RequestReplyMessenger<IScsServerClient> requestReplyMessenger)
        {
            this._serverClient = serverClient;
            this._serverClient.Disconnected += new EventHandler(this.Client_Disconnected);
            this._requestReplyMessenger = requestReplyMessenger;
        }

        /// <summary>
        /// Closes client connection.
        /// 
        /// </summary>
        public void Disconnect()
        {
            this._serverClient.Disconnect();
        }

        /// <summary>
        /// Gets the client proxy interface that provides calling client methods remotely.
        /// 
        /// </summary>
        /// <typeparam name="T">Type of client interface</typeparam>
        /// <returns>
        /// Client interface
        /// </returns>
        public T GetClientProxy<T>() where T : class
        {
            this._realProxy = (RealProxy)new RemoteInvokeProxy<T, IScsServerClient>(this._requestReplyMessenger);
            return (T)this._realProxy.GetTransparentProxy();
        }

        /// <summary>
        /// Handles disconnect event of _serverClient object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void Client_Disconnected(object sender, EventArgs e)
        {
            this._requestReplyMessenger.Stop();
            this.OnDisconnected();
        }

        /// <summary>
        /// Raises Disconnected event.
        /// 
        /// </summary>
        private void OnDisconnected()
        {
            EventHandler eventHandler = this.Disconnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, EventArgs.Empty);
        }
    }
}
