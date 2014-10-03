// Type: Hik.Communication.Scs.Communication.Messengers.SynchronizedMessenger`1
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication.Messages;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Hik.Communication.Scs.Communication.Messengers
{
    /// <summary>
    /// This class is a wrapper for IMessenger and is used
    ///             to synchronize message receiving operation.
    ///             It extends RequestReplyMessenger.
    ///             It is suitable to use in applications those want to receive
    ///             messages by synchronized method calls instead of asynchronous
    ///             MessageReceived event.
    /// 
    /// </summary>
    public class SynchronizedMessenger<T> : RequestReplyMessenger<T> where T : IMessenger
    {
        /// <summary>
        /// A queue that is used to store receiving messages until Receive(...)
        ///             method is called to get them.
        /// 
        /// </summary>
        private readonly Queue<IScsMessage> _receivingMessageQueue;
        /// <summary>
        /// This object is used to synchronize/wait threads.
        /// 
        /// </summary>
        private readonly ManualResetEventSlim _receiveWaiter;
        /// <summary>
        /// This boolean value indicates the running state of this class.
        /// 
        /// </summary>
        private volatile bool _running;

        /// <summary>
        /// Gets/sets capacity of the incoming message queue.
        ///              No message is received from remote application if
        ///              number of messages in internal queue exceeds this value.
        ///              Default value: int.MaxValue (2147483647).
        /// 
        /// </summary>
        public int IncomingMessageQueueCapacity { get; set; }

        /// <summary>
        /// Creates a new SynchronizedMessenger object.
        /// 
        /// </summary>
        /// <param name="messenger">A IMessenger object to be used to send/receive messages</param>
        public SynchronizedMessenger(T messenger)
            : this(messenger, int.MaxValue)
        {
        }

        /// <summary>
        /// Creates a new SynchronizedMessenger object.
        /// 
        /// </summary>
        /// <param name="messenger">A IMessenger object to be used to send/receive messages</param><param name="incomingMessageQueueCapacity">capacity of the incoming message queue</param>
        public SynchronizedMessenger(T messenger, int incomingMessageQueueCapacity)
            : base(messenger)
        {
            this._receiveWaiter = new ManualResetEventSlim();
            this._receivingMessageQueue = new Queue<IScsMessage>();
            this.IncomingMessageQueueCapacity = incomingMessageQueueCapacity;
        }

        /// <summary>
        /// Starts the messenger.
        /// 
        /// </summary>
        public override void Start()
        {
            lock (this._receivingMessageQueue)
                this._running = true;
            base.Start();
        }

        /// <summary>
        /// Stops the messenger.
        /// 
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            lock (this._receivingMessageQueue)
            {
                this._running = false;
                this._receiveWaiter.Set();
            }
        }

        /// <summary>
        /// This method is used to receive a message from remote application.
        ///             It waits until a message is received.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Received message
        /// </returns>
        public IScsMessage ReceiveMessage()
        {
            return this.ReceiveMessage(-1);
        }

        /// <summary>
        /// This method is used to receive a message from remote application.
        ///             It waits until a message is received or timeout occurs.
        /// 
        /// </summary>
        /// <param name="timeout">Timeout value to wait if no message is received.
        ///             Use -1 to wait indefinitely.
        ///             </param>
        /// <returns>
        /// Received message
        /// </returns>
        /// <exception cref="T:System.TimeoutException">Throws TimeoutException if timeout occurs</exception><exception cref="T:System.Exception">Throws Exception if SynchronizedMessenger stops before a message is received</exception>
        public IScsMessage ReceiveMessage(int timeout)
        {
            while (this._running)
            {
                lock (this._receivingMessageQueue)
                {
                    if (!this._running)
                        throw new Exception("SynchronizedMessenger is stopped. Can not receive message.");
                    if (this._receivingMessageQueue.Count > 0)
                        return this._receivingMessageQueue.Dequeue();
                    this._receiveWaiter.Reset();
                }
                if (!this._receiveWaiter.Wait(timeout))
                    throw new TimeoutException("Timeout occured. Can not received any message");
            }
            throw new Exception("SynchronizedMessenger is stopped. Can not receive message.");
        }

        /// <summary>
        /// This method is used to receive a specific type of message from remote application.
        ///             It waits until a message is received.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Received message
        /// </returns>
        public TMessage ReceiveMessage<TMessage>() where TMessage : IScsMessage
        {
            return this.ReceiveMessage<TMessage>(-1);
        }

        /// <summary>
        /// This method is used to receive a specific type of message from remote application.
        ///             It waits until a message is received or timeout occurs.
        /// 
        /// </summary>
        /// <param name="timeout">Timeout value to wait if no message is received.
        ///             Use -1 to wait indefinitely.
        ///             </param>
        /// <returns>
        /// Received message
        /// </returns>
        public TMessage ReceiveMessage<TMessage>(int timeout) where TMessage : IScsMessage
        {
            IScsMessage scsMessage = this.ReceiveMessage(timeout);
            if (!(scsMessage is TMessage))
                throw new Exception("Unexpected message received. Expected type: " + typeof(TMessage).Name + ". Received message type: " + scsMessage.GetType().Name);
            else
                return (TMessage)scsMessage;
        }

        /// <summary>
        /// Overrides
        /// 
        /// </summary>
        /// <param name="message"/>
        protected override void OnMessageReceived(IScsMessage message)
        {
            lock (this._receivingMessageQueue)
            {
                if (this._receivingMessageQueue.Count < this.IncomingMessageQueueCapacity)
                    this._receivingMessageQueue.Enqueue(message);
                this._receiveWaiter.Set();
            }
        }
    }
}
