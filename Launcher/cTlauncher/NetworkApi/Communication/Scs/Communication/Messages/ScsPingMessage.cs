// Type: Hik.Communication.Scs.Communication.Messages.ScsPingMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace Tera.NetworkApi.Communication.Scs.Communication.Messages
{
    /// <summary>
    /// This message is used to send/receive ping messages.
    ///             Ping messages is used to keep connection alive between server and client.
    /// 
    /// </summary>
    [Serializable]
    public sealed class ScsPingMessage : ScsMessage
    {
        /// <summary>
        /// Creates a new PingMessage object.
        /// 
        /// </summary>
        public ScsPingMessage()
        {
        }

        /// <summary>
        /// Creates a new reply PingMessage object.
        /// 
        /// </summary>
        /// <param name="repliedMessageId">Replied message id if this is a reply for
        ///             a message.
        ///             </param>
        public ScsPingMessage(string repliedMessageId)
            : this()
        {
            this.RepliedMessageId = repliedMessageId;
        }

        /// <summary>
        /// Creates a string to represents this object.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A string to represents this object
        /// </returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.RepliedMessageId))
                return string.Format("ScsPingMessage [{0}] Replied To [{1}]", (object)this.MessageId, (object)this.RepliedMessageId);
            else
                return string.Format("ScsPingMessage [{0}]", (object)this.MessageId);
        }
    }
}
