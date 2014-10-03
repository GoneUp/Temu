// Type: Hik.Communication.Scs.Communication.Messengers.IMessenger
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Protocols;
using System;

namespace Hik.Communication.Scs.Communication.Messengers
{
    /// <summary>
    /// Represents an object that can send and receive messages.
    /// 
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Gets/sets wire protocol that is used while reading and writing messages.
        /// 
        /// </summary>
        IScsWireProtocol WireProtocol { get; set; }

        /// <summary>
        /// Gets the time of the last succesfully received message.
        /// 
        /// </summary>
        DateTime LastReceivedMessageTime { get; }

        /// <summary>
        /// Gets the time of the last succesfully sent message.
        /// 
        /// </summary>
        DateTime LastSentMessageTime { get; }

        /// <summary>
        /// This event is raised when a new message is received.
        /// 
        /// </summary>
        event EventHandler<MessageEventArgs> MessageReceived;

        /// <summary>
        /// This event is raised when a new message is sent without any error.
        ///             It does not guaranties that message is properly handled and processed by remote application.
        /// 
        /// </summary>
        event EventHandler<MessageEventArgs> MessageSent;

        /// <summary>
        /// Sends a message to the remote application.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param>
        void SendMessage(IScsMessage message);
    }
}
