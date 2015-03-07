// Type: Hik.Communication.Scs.Communication.Messages.ScsMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace Tera.NetworkApi.Communication.Scs.Communication.Messages
{
    /// <summary>
    /// Represents a message that is sent and received by server and client.
    ///             This is the base class for all messages.
    /// 
    /// </summary>
    [Serializable]
    public class ScsMessage : IScsMessage
    {
        /// <summary>
        /// Unique identified for this message.
        ///             Default value: New GUID.
        ///             Do not change if you do not want to do low level changes
        ///             such as custom wire protocols.
        /// 
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// This property is used to indicate that this is
        ///             a Reply message to a message.
        ///             It may be null if this is not a reply message.
        /// 
        /// </summary>
        public string RepliedMessageId { get; set; }

        /// <summary>
        /// Creates a new ScsMessage.
        /// 
        /// </summary>
        public ScsMessage()
        {
            this.MessageId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Creates a new reply ScsMessage.
        /// 
        /// </summary>
        /// <param name="repliedMessageId">Replied message id if this is a reply for
        ///             a message.
        ///             </param>
        public ScsMessage(string repliedMessageId)
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
                return string.Format("ScsMessage [{0}] Replied To [{1}]", (object)this.MessageId, (object)this.RepliedMessageId);
            else
                return string.Format("ScsMessage [{0}]", (object)this.MessageId);
        }
    }
}
