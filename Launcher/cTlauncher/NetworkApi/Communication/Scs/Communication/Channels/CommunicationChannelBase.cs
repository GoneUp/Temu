// Type: Hik.Communication.Scs.Communication.Channels.CommunicationChannelBase
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Communication.EndPoints;
using Tera.NetworkApi.Communication.Scs.Communication.Messages;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;
using Tera.NetworkApi.Communication.Scs.Communication.Protocols;

namespace Tera.NetworkApi.Communication.Scs.Communication.Channels
{
    /// <summary>
    /// This class provides base functionality for all communication channel classes.
    /// 
    /// </summary>
    internal abstract class CommunicationChannelBase : ICommunicationChannel, IMessenger
    {
        /// <summary>
        /// Gets endpoint of remote application.
        /// 
        /// </summary>
        public abstract ScsEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Gets the current communication state.
        /// 
        /// </summary>
        public CommunicationStates CommunicationState { get; protected set; }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// 
        /// </summary>
        public DateTime LastReceivedMessageTime { get; protected set; }

        /// <summary>
        /// Gets the time of the last succesfully sent message.
        /// 
        /// </summary>
        public DateTime LastSentMessageTime { get; protected set; }

        /// <summary>
        /// Gets/sets wire protocol that the channel uses.
        ///             This property must set before first communication.
        /// 
        /// </summary>
        public IScsWireProtocol WireProtocol { get; set; }

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
        public event EventHandler Disconnected;

        /// <summary>
        /// Constructor.
        /// 
        /// </summary>
        protected CommunicationChannelBase()
        {
            this.CommunicationState = CommunicationStates.Disconnected;
            this.LastReceivedMessageTime = DateTime.MinValue;
            this.LastSentMessageTime = DateTime.MinValue;
        }

        /// <summary>
        /// Disconnects from remote application and closes this channel.
        /// 
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Starts the communication with remote application.
        /// 
        /// </summary>
        public void Start()
        {
            this.StartInternal();
            this.CommunicationState = CommunicationStates.Connected;
        }

        /// <summary>
        /// Sends a message to the remote application.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param><exception cref="T:System.ArgumentNullException">Throws ArgumentNullException if message is null</exception>
        public void SendMessage(IScsMessage message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
            this.SendMessageInternal(message);
        }

        /// <summary>
        /// Starts the communication with remote application really.
        /// 
        /// </summary>
        protected abstract void StartInternal();

        /// <summary>
        /// Sends a message to the remote application.
        ///             This method is overrided by derived classes to really send to message.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param>
        protected abstract void SendMessageInternal(IScsMessage message);

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
