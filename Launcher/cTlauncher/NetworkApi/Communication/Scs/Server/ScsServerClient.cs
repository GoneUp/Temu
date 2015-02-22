// Type: Hik.Communication.Scs.Server.ScsServerClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Communication;
using Tera.NetworkApi.Communication.Scs.Communication.Channels;
using Tera.NetworkApi.Communication.Scs.Communication.EndPoints;
using Tera.NetworkApi.Communication.Scs.Communication.Messages;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;
using Tera.NetworkApi.Communication.Scs.Communication.Protocols;

namespace Tera.NetworkApi.Communication.Scs.Server
{
    /// <summary>
    /// This class represents a client in server side.
    /// 
    /// </summary>
    internal class ScsServerClient : IScsServerClient, IMessenger
    {
        /// <summary>
        /// The communication channel that is used by client to send and receive messages.
        /// 
        /// </summary>
        private readonly ICommunicationChannel _communicationChannel;

        /// <summary>
        /// Unique identifier for this client in server.
        /// 
        /// </summary>
        public long ClientId { get; set; }

        /// <summary>
        /// Gets the communication state of the Client.
        /// 
        /// </summary>
        public CommunicationStates CommunicationState
        {
            get
            {
                return this._communicationChannel.CommunicationState;
            }
        }

        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// 
        /// </summary>
        public IScsWireProtocol WireProtocol
        {
            get
            {
                return this._communicationChannel.WireProtocol;
            }
            set
            {
                this._communicationChannel.WireProtocol = value;
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
                return this._communicationChannel.RemoteEndPoint;
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
        /// This event is raised when client is disconnected from server.
        /// 
        /// </summary>
        public event EventHandler Disconnected;

        /// <summary>
        /// Creates a new ScsClient object.
        /// 
        /// </summary>
        /// <param name="communicationChannel">The communication channel that is used by client to send and receive messages</param>
        public ScsServerClient(ICommunicationChannel communicationChannel)
        {
            this._communicationChannel = communicationChannel;
            this._communicationChannel.MessageReceived += new EventHandler<MessageEventArgs>(this.CommunicationChannel_MessageReceived);
            this._communicationChannel.MessageSent += new EventHandler<MessageEventArgs>(this.CommunicationChannel_MessageSent);
            this._communicationChannel.Disconnected += new EventHandler(this.CommunicationChannel_Disconnected);
        }

        /// <summary>
        /// Disconnects from client and closes underlying communication channel.
        /// 
        /// </summary>
        public void Disconnect()
        {
            this._communicationChannel.Disconnect();
        }

        /// <summary>
        /// Sends a message to the client.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void SendMessage(IScsMessage message)
        {
            this._communicationChannel.SendMessage(message);
        }

        /// <summary>
        /// Handles Disconnected event of _communicationChannel object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void CommunicationChannel_Disconnected(object sender, EventArgs e)
        {
            this.OnDisconnected();
        }

        /// <summary>
        /// Handles MessageReceived event of _communicationChannel object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void CommunicationChannel_MessageReceived(object sender, MessageEventArgs e)
        {
            IScsMessage message = e.Message;
            if (message is ScsPingMessage)
            {
                ICommunicationChannel communicationChannel = this._communicationChannel;
                ScsPingMessage scsPingMessage1 = new ScsPingMessage();
                scsPingMessage1.RepliedMessageId = message.MessageId;
                ScsPingMessage scsPingMessage2 = scsPingMessage1;
                communicationChannel.SendMessage((IScsMessage)scsPingMessage2);
            }
            else
                this.OnMessageReceived(message);
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

        /// <summary>
        /// Raises MessageReceived event.
        /// 
        /// </summary>
        /// <param name="message">Received message</param>
        private void OnMessageReceived(IScsMessage message)
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
