// Type: Hik.Communication.Scs.Communication.Messengers.RequestReplyMessenger`1
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.Messages;
using NetworkApi.Communication.Scs.Communication.Protocols;
using NetworkApi.Threading;
using System;
using System.Collections.Generic;
using System.Threading;

namespace NetworkApi.Communication.Scs.Communication.Messengers
{
    /// <summary>
    /// This class adds SendMessageAndWaitForResponse(...) and SendAndReceiveMessage methods
    ///             to a IMessenger for synchronous request/response style messaging.
    ///             It also adds queued processing of incoming messages.
    /// 
    /// </summary>
    /// <typeparam name="T">Type of IMessenger object to use as underlying communication</typeparam>
    public class RequestReplyMessenger<T> : IMessenger, IDisposable where T : IMessenger
    {
        /// <summary>
        /// This object is used for thread synchronization.
        /// 
        /// </summary>
        private readonly object _syncObj = new object();
        /// <summary>
        /// Default Timeout value.
        /// 
        /// </summary>
        private const int DefaultTimeout = 60000;
        /// <summary>
        /// This messages are waiting for a response those are used when
        ///             SendMessageAndWaitForResponse is called.
        ///             Key: MessageID of waiting request message.
        ///             Value: A WaitingMessage instance.
        /// 
        /// </summary>
        private readonly SortedList<string, RequestReplyMessenger<T>.WaitingMessage> _waitingMessages;
        /// <summary>
        /// This object is used to process incoming messages sequentially.
        /// 
        /// </summary>
        private readonly SequentialItemProcessor<IScsMessage> _incomingMessageProcessor;

        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// 
        /// </summary>
        public IScsWireProtocol WireProtocol
        {
            get
            {
                return this.Messenger.WireProtocol;
            }
            set
            {
                this.Messenger.WireProtocol = value;
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
                return this.Messenger.LastReceivedMessageTime;
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
                return this.Messenger.LastSentMessageTime;
            }
        }

        /// <summary>
        /// Gets the underlying IMessenger object.
        /// 
        /// </summary>
        public T Messenger { get; private set; }

        /// <summary>
        /// Timeout value as milliseconds to wait for a receiving message on
        ///             SendMessageAndWaitForResponse and SendAndReceiveMessage methods.
        ///             Default value: 60000 (1 minute).
        /// 
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// This event is raised when a new message is received from underlying messenger.
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
        /// Creates a new RequestReplyMessenger.
        /// 
        /// </summary>
        /// <param name="messenger">IMessenger object to use as underlying communication</param>
        public RequestReplyMessenger(T messenger)
        {
            this.Messenger = messenger;
            messenger.MessageReceived += new EventHandler<MessageEventArgs>(this.Messenger_MessageReceived);
            messenger.MessageSent += new EventHandler<MessageEventArgs>(this.Messenger_MessageSent);
            this._incomingMessageProcessor = new SequentialItemProcessor<IScsMessage>(new Action<IScsMessage>(this.OnMessageReceived));
            this._waitingMessages = new SortedList<string, RequestReplyMessenger<T>.WaitingMessage>();
            this.Timeout = 60000;
        }

        /// <summary>
        /// Starts the messenger.
        /// 
        /// </summary>
        public virtual void Start()
        {
            this._incomingMessageProcessor.Start();
        }

        /// <summary>
        /// Stops the messenger.
        ///             Cancels all waiting threads in SendMessageAndWaitForResponse method and stops message queue.
        ///             SendMessageAndWaitForResponse method throws exception if there is a thread that is waiting for response message.
        ///             Also stops incoming message processing and deletes all messages in incoming message queue.
        /// 
        /// </summary>
        public virtual void Stop()
        {
            this._incomingMessageProcessor.Stop();
            lock (this._syncObj)
            {
                foreach (RequestReplyMessenger<T>.WaitingMessage item_0 in (IEnumerable<RequestReplyMessenger<T>.WaitingMessage>)this._waitingMessages.Values)
                {
                    item_0.State = (RequestReplyMessenger<T>.WaitingMessageStates)1;
                    item_0.WaitEvent.Set();
                }
                this._waitingMessages.Clear();
            }
        }

        /// <summary>
        /// Calls Stop method of this object.
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Stop();
        }

        /// <summary>
        /// Sends a message.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param>
        public void SendMessage(IScsMessage message)
        {
            this.Messenger.SendMessage(message);
        }

        /// <summary>
        /// Sends a message and waits a response for that message.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Response message is matched with RepliedMessageId property, so if
        ///             any other message (that is not reply for sent message) is received
        ///             from remote application, it is not considered as a reply and is not
        ///             returned as return value of this method.
        /// 
        ///             MessageReceived event is not raised for response messages.
        /// 
        /// </remarks>
        /// <param name="message">message to send</param>
        /// <returns>
        /// Response message
        /// </returns>
        public IScsMessage SendMessageAndWaitForResponse(IScsMessage message)
        {
            return this.SendMessageAndWaitForResponse(message, this.Timeout);
        }

        /// <summary>
        /// Sends a message and waits a response for that message.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Response message is matched with RepliedMessageId property, so if
        ///             any other message (that is not reply for sent message) is received
        ///             from remote application, it is not considered as a reply and is not
        ///             returned as return value of this method.
        /// 
        ///             MessageReceived event is not raised for response messages.
        /// 
        /// </remarks>
        /// <param name="message">message to send</param><param name="timeoutMilliseconds">Timeout duration as milliseconds.</param>
        /// <returns>
        /// Response message
        /// </returns>
        /// <exception cref="T:System.TimeoutException">Throws TimeoutException if can not receive reply message in timeout value</exception><exception cref="T:Hik.Communication.Scs.Communication.CommunicationException">Throws CommunicationException if communication fails before reply message.</exception>
        public IScsMessage SendMessageAndWaitForResponse(IScsMessage message, int timeoutMilliseconds)
        {
            RequestReplyMessenger<T>.WaitingMessage waitingMessage = new RequestReplyMessenger<T>.WaitingMessage();
            lock (this._syncObj)
                this._waitingMessages[message.MessageId] = waitingMessage;
            try
            {
                this.Messenger.SendMessage(message);
                waitingMessage.WaitEvent.Wait(timeoutMilliseconds);
                switch ((int)waitingMessage.State)
                {
                    case 0:
                        throw new TimeoutException("Timeout occured. Can not received response.");
                    case 1:
                        throw new CommunicationException("Disconnected before response received.");
                    default:
                        return waitingMessage.ResponseMessage;
                }
            }
            finally
            {
                lock (this._syncObj)
                {
                    if (this._waitingMessages.ContainsKey(message.MessageId))
                        this._waitingMessages.Remove(message.MessageId);
                }
            }
        }

        /// <summary>
        /// Handles MessageReceived event of Messenger object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void Messenger_MessageReceived(object sender, MessageEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Message.RepliedMessageId))
            {
                RequestReplyMessenger<T>.WaitingMessage waitingMessage = (RequestReplyMessenger<T>.WaitingMessage)null;
                lock (this._syncObj)
                {
                    if (this._waitingMessages.ContainsKey(e.Message.RepliedMessageId))
                        waitingMessage = this._waitingMessages[e.Message.RepliedMessageId];
                }
                if (waitingMessage != null)
                {
                    waitingMessage.ResponseMessage = e.Message;
                    waitingMessage.State = (RequestReplyMessenger<T>.WaitingMessageStates)2;
                    waitingMessage.WaitEvent.Set();
                    return;
                }
            }
            this._incomingMessageProcessor.EnqueueMessage(e.Message);
        }

        /// <summary>
        /// Handles MessageSent event of Messenger object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void Messenger_MessageSent(object sender, MessageEventArgs e)
        {
            this.OnMessageSent(e.Message);
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

        /// <summary>
        /// This class is used to store messaging context for a request message
        ///             until response is received.
        /// 
        /// </summary>
        private sealed class WaitingMessage
        {
            /// <summary>
            /// Response message for request message
            ///             (null if response is not received yet).
            /// 
            /// </summary>
            public IScsMessage ResponseMessage { get; set; }

            /// <summary>
            /// ManualResetEvent to block thread until response is received.
            /// 
            /// </summary>
            public ManualResetEventSlim WaitEvent { get; private set; }

            /// <summary>
            /// State of the request message.
            /// 
            /// </summary>
            public RequestReplyMessenger<T>.WaitingMessageStates State { get; set; }

            /// <summary>
            /// Creates a new WaitingMessage object.
            /// 
            /// </summary>
            public WaitingMessage()
            {
                this.WaitEvent = new ManualResetEventSlim(false);
                this.State = (RequestReplyMessenger<T>.WaitingMessageStates)0;
            }
        }

        /// <summary>
        /// This enum is used to store the state of a waiting message.
        /// 
        /// </summary>
        private enum WaitingMessageStates
        {
            WaitingForResponse,
            Cancelled,
            ResponseReceived,
        }
    }
}
