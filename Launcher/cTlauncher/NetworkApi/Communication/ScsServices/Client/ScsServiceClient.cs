// Type: Hik.Communication.ScsServices.Client.ScsServiceClient`1
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Reflection;
using Tera.NetworkApi.Communication.Scs.Client;
using Tera.NetworkApi.Communication.Scs.Communication;
using Tera.NetworkApi.Communication.Scs.Communication.Messages;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;
using Tera.NetworkApi.Communication.ScsServices.Communication;
using Tera.NetworkApi.Communication.ScsServices.Communication.Messages;

namespace Tera.NetworkApi.Communication.ScsServices.Client
{
    /// <summary>
    /// Represents a service client that consumes a SCS service.
    /// 
    /// </summary>
    /// <typeparam name="T">Type of service interface</typeparam>
    internal class ScsServiceClient<T> : IScsServiceClient<T>, IConnectableClient, IDisposable where T : class
    {
        /// <summary>
        /// Underlying IScsClient object to communicate with server.
        /// 
        /// </summary>
        private readonly IScsClient _client;
        /// <summary>
        /// Messenger object to send/receive messages over _client.
        /// 
        /// </summary>
        private readonly RequestReplyMessenger<IScsClient> _requestReplyMessenger;
        /// <summary>
        /// This object is used to create a transparent proxy to invoke remote methods on server.
        /// 
        /// </summary>
        private readonly AutoConnectRemoteInvokeProxy<T, IScsClient> _realServiceProxy;
        /// <summary>
        /// The client object that is used to call method invokes in client side.
        ///             May be null if client has no methods to be invoked by server.
        /// 
        /// </summary>
        private readonly object _clientObject;

        /// <summary>
        /// Timeout for connecting to a server (as milliseconds).
        ///             Default value: 15 seconds (15000 ms).
        /// 
        /// </summary>
        public int ConnectTimeout
        {
            get
            {
                return this._client.ConnectTimeout;
            }
            set
            {
                this._client.ConnectTimeout = value;
            }
        }

        /// <summary>
        /// Gets the current communication state.
        /// 
        /// </summary>
        public CommunicationStates CommunicationState
        {
            get
            {
                return this._client.CommunicationState;
            }
        }

        /// <summary>
        /// Reference to the service proxy to invoke remote service methods.
        /// 
        /// </summary>
        public T ServiceProxy { get; private set; }

        /// <summary>
        /// Timeout value when invoking a service method.
        ///             If timeout occurs before end of remote method call, an exception is thrown.
        ///             Use -1 for no timeout (wait indefinite).
        ///             Default value: 60000 (1 minute).
        /// 
        /// </summary>
        public int Timeout
        {
            get
            {
                return this._requestReplyMessenger.Timeout;
            }
            set
            {
                this._requestReplyMessenger.Timeout = value;
            }
        }

        /// <summary>
        /// This event is raised when client connected to server.
        /// 
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// This event is raised when client disconnected from server.
        /// 
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Creates a new ScsServiceClient object.
        /// 
        /// </summary>
        /// <param name="client">Underlying IScsClient object to communicate with server</param><param name="clientObject">The client object that is used to call method invokes in client side.
        ///             May be null if client has no methods to be invoked by server.</param>
        public ScsServiceClient(IScsClient client, object clientObject)
        {
            this._client = client;
            this._clientObject = clientObject;
            this._client.Connected += new EventHandler(this.Client_Connected);
            this._client.Disconnected += new EventHandler(this.Client_Disconnected);
            this._requestReplyMessenger = new RequestReplyMessenger<IScsClient>(client);
            this._requestReplyMessenger.MessageReceived += new EventHandler<MessageEventArgs>(this.RequestReplyMessenger_MessageReceived);
            this._realServiceProxy = new AutoConnectRemoteInvokeProxy<T, IScsClient>(this._requestReplyMessenger, (IConnectableClient)this);
            this.ServiceProxy = (T)this._realServiceProxy.GetTransparentProxy();
        }

        /// <summary>
        /// Connects to server.
        /// 
        /// </summary>
        public void Connect()
        {
            this._client.Connect();
        }

        /// <summary>
        /// Disconnects from server.
        ///             Does nothing if already disconnected.
        /// 
        /// </summary>
        public void Disconnect()
        {
            this._client.Disconnect();
        }

        /// <summary>
        /// Calls Disconnect method.
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Disconnect();
        }

        /// <summary>
        /// Handles MessageReceived event of messenger.
        ///             It gets messages from server and invokes appropriate method.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void RequestReplyMessenger_MessageReceived(object sender, MessageEventArgs e)
        {
            ScsRemoteInvokeMessage remoteInvokeMessage = e.Message as ScsRemoteInvokeMessage;
            if (remoteInvokeMessage == null)
                return;
            if (this._clientObject == null)
            {
                this.SendInvokeResponse((IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException("Client does not wait for method invocations by server."));
            }
            else
            {
                object returnValue;
                try
                {
                    returnValue = this._clientObject.GetType().GetMethod(remoteInvokeMessage.MethodName).Invoke(this._clientObject, remoteInvokeMessage.Parameters);
                }
                catch (TargetInvocationException ex)
                {
                    Exception innerException = ex.InnerException;
                    this.SendInvokeResponse((IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException(innerException.Message, innerException));
                    return;
                }
                catch (Exception ex)
                {
                    this.SendInvokeResponse((IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException(ex.Message, ex));
                    return;
                }
                this.SendInvokeResponse((IScsMessage)remoteInvokeMessage, returnValue, (ScsRemoteException)null);
            }
        }

        /// <summary>
        /// Sends response to the remote application that invoked a service method.
        /// 
        /// </summary>
        /// <param name="requestMessage">Request message</param><param name="returnValue">Return value to send</param><param name="exception">Exception to send</param>
        private void SendInvokeResponse(IScsMessage requestMessage, object returnValue, ScsRemoteException exception)
        {
            try
            {
                RequestReplyMessenger<IScsClient> requestReplyMessenger = this._requestReplyMessenger;
                ScsRemoteInvokeReturnMessage invokeReturnMessage1 = new ScsRemoteInvokeReturnMessage();
                invokeReturnMessage1.RepliedMessageId = requestMessage.MessageId;
                invokeReturnMessage1.ReturnValue = returnValue;
                invokeReturnMessage1.RemoteException = exception;
                ScsRemoteInvokeReturnMessage invokeReturnMessage2 = invokeReturnMessage1;
                requestReplyMessenger.SendMessage((IScsMessage)invokeReturnMessage2);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Handles Connected event of _client object.
        /// 
        /// </summary>
        /// <param name="sender">Source of object</param><param name="e">Event arguments</param>
        private void Client_Connected(object sender, EventArgs e)
        {
            this._requestReplyMessenger.Start();
            this.OnConnected();
        }

        /// <summary>
        /// Handles Disconnected event of _client object.
        /// 
        /// </summary>
        /// <param name="sender">Source of object</param><param name="e">Event arguments</param>
        private void Client_Disconnected(object sender, EventArgs e)
        {
            this._requestReplyMessenger.Stop();
            this.OnDisconnected();
        }

        /// <summary>
        /// Raises Connected event.
        /// 
        /// </summary>
        private void OnConnected()
        {
            EventHandler eventHandler = this.Connected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, EventArgs.Empty);
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
