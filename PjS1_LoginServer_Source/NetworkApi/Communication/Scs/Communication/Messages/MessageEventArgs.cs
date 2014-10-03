// Type: Hik.Communication.Scs.Communication.Messages.MessageEventArgs
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Communication.Messages
{
    /// <summary>
    /// Stores message to be used by an event.
    /// 
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Message object that is associated with this event.
        /// 
        /// </summary>
        public IScsMessage Message { get; private set; }

        /// <summary>
        /// Creates a new MessageEventArgs object.
        /// 
        /// </summary>
        /// <param name="message">Message object that is associated with this event</param>
        public MessageEventArgs(IScsMessage message)
        {
            this.Message = message;
        }
    }
}
