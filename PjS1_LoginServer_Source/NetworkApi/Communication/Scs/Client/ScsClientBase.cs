// Type: Hik.Communication.Scs.Client.ScsClientBase
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.Channels;
using NetworkApi.Communication.Scs.Communication.Messages;
using NetworkApi.Communication.Scs.Communication.Messengers;
using NetworkApi.Communication.Scs.Communication.Protocols;
using NetworkApi.Threading;
using System;

namespace NetworkApi.Communication.Scs.Client
{
    /// <summary>
    /// This class provides base functionality for client classes.
    /// 
    /// </summary>
    internal abstract class ScsClientBase : IScsClient, IMessenger, IConnectableClient, IDisposable
    {
        /// <summary>
        /// Default timeout value for connecting a server.
        /// 
        /// </summary>
        private const int DefaultConnectionAttemptTimeout = 15000;
        private IScsWireProtocol _wireProtocol;
        /// <summary>
        /// The communication channel that is used by client to send and receive messages.
        /// 
        /// </summary>
        private ICommunicationChannel _communicationChannel;
        /// <summary>
        /// This timer is used to send PingMessage messages to server periodically.
        /// 
        /// </summary>
        private readonly Timer _pingTimer;

        /// <summary>
        /// Timeout for connecting to a server (as milliseconds).
        ///             Default value: 15 seconds (15000 ms).
        /// 
        /// </summary>
        public int ConnectTimeout { get; set; }

        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// 
        /// </summary>
        public IScsWireProtocol WireProtocol
        {
            get
            {
                return this._wireProtocol;
            }
            set
            {
                if (this.CommunicationState == CommunicationStates.Connected)
                    throw new ApplicationException("Wire protocol can not be changed while connected to server.");
                this._wireProtocol = value;
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
                if (this._communicationChannel == null)
                    return CommunicationStates.Disconnected;
                else
                    return this._communicationChannel.CommunicationState;
            }
        }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// 
        /// </summary>
        public DateTime LastReceivedMessageTime
        {
            get
            {
                if (this._communicationChannel == null)
                    return DateTime.MinValue;
                else
                    return this._communicationChannel.LastReceivedMessageTime;
            }
        }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// 
        /// </summary>
        public DateTime LastSentMessageTime
        {
            get
            {
                if (this._communicationChannel == null)
                    return DateTime.MinValue;
                else
                    return this._communicationChannel.LastSentMessageTime;
            }
        }

        /// <summary>
        /// This event is raised when a new message is received.
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;

        /// <summary>
        /// This event is raised when a new message is sent without any error.
        ///             It does not guaranties that message is properly handled and processed by remote application.
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageSent;

        /// <summary>
        /// This event is raised when communication channel closed.
        /// 
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// This event is raised when client disconnected from server.
        /// 
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Constructor.
        /// 
        /// </summary>
        protected ScsClientBase()
        {
            this._pingTimer = new Timer(30000);
            this._pingTimer.Elapsed += new EventHandler(this.PingTimer_Elapsed);
            this.ConnectTimeout = 15000;
            this.WireProtocol = WireProtocolManager.GetDefaultWireProtocol();
        }

        /// <summary>
        /// Connects to server.
        /// 
        /// </summary>
        public void Connect()
        {
            this.WireProtocol.Reset();
            this._communicationChannel = this.CreateCommunicationChannel();
            this._communicationChannel.WireProtocol = this.WireProtocol;
            this._communicationChannel.Disconnected += new EventHandler(this.CommunicationChannel_Disconnected);
            this._communicationChannel.MessageReceived += new EventHandler<MessageEventArgs>(this.CommunicationChannel_MessageReceived);
            this._communicationChannel.MessageSent += new EventHandler<MessageEventArgs>(this.CommunicationChannel_MessageSent);
            this._communicationChannel.Start();
            this._pingTimer.Start();
            this.OnConnected();
        }

        /// <summary>
        /// Disconnects from server.
        ///             Does nothing if already disconnected.
        /// 
        /// </summary>
        public void Disconnect()
        {
            if (this.CommunicationState != CommunicationStates.Connected)
                return;
            this._communicationChannel.Disconnect();
        }

        /// <summary>
        /// Disposes this object and closes underlying connection.
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Disconnect();
        }

        /// <summary>
        /// Sends a message to the server.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param><exception cref="T:Hik.Communication.Scs.Communication.CommunicationStateException">Throws a CommunicationStateException if client is not connected to the server.</exception>
        public void SendMessage(IScsMessage message)
        {
            if (this.CommunicationState != CommunicationStates.Connected)
                throw new CommunicationStateException("Client is not connected to the server.");
            this._communicationChannel.SendMessage(message);
        }

        /// <summary>
        /// This method is implemented by derived classes to create appropriate communication channel.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Ready communication channel to communicate
        /// </returns>
        protected abstract ICommunicationChannel CreateCommunicationChannel();

        /// <summary>
        /// Handles MessageReceived event of _communicationChannel object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void CommunicationChannel_MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message is ScsPingMessage)
                return;
            this.OnMessageReceived(e.Message);
        }

        /// <summary>
        /// Handles MessageSent event of _communicationChannel object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void CommunicationChannel_MessageSent(object sender, MessageEventArgs e)
        {
            this.OnMessageSent(e.Message);
        }

        /// <summary>
        /// Handles Disconnected event of _communicationChannel object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void CommunicationChannel_Disconnected(object sender, EventArgs e)
        {
            this._pingTimer.Stop();
            this.OnDisconnected();
        }

        /// <summary>
        /// Handles Elapsed event of _pingTimer to send PingMessage messages to server.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void PingTimer_Elapsed(object sender, EventArgs e)
        {
            if (this.CommunicationState != CommunicationStates.Connected)
                return;
            try
            {
                DateTime dateTime = DateTime.Now.AddMinutes(-1.0);
                if (this._communicationChannel.LastReceivedMessageTime > dateTime || this._communicationChannel.LastSentMessageTime > dateTime)
                    return;
                this._communicationChannel.SendMessage((IScsMessage)new ScsPingMessage());
            }
            catch
            {
            }
        }

        /// <summary>
        /// Raises Connected event.
        /// 
        /// </summary>
        protected virtual void OnConnected()
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
        protected virtual void OnDisconnected()
        {
            EventHandler eventHandler = this.Disconnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, EventArgs.Empty);
        }

        /// <summary>
        /// Raises MessageReceived event.
        /// 
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnMessageReceived(IScsMessage message)
        {
            EventHandler<MessageEventArgs> eventHandler = this.MessageReceived;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new MessageEventArgs(message));
        }

        /// <summary>
        /// Raises MessageSent event.
        /// 
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnMessageSent(IScsMessage message)
        {
            EventHandler<MessageEventArgs> eventHandler = this.MessageSent;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new MessageEventArgs(message));
        }
    }
}
