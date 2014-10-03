// Type: Hik.Communication.Scs.Communication.Messages.ScsRawDataMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Communication.Messages
{
    /// <summary>
    /// This message is used to send/receive a raw byte array as message data.
    /// 
    /// </summary>
    [Serializable]
    public class ScsRawDataMessage : ScsMessage
    {
        /// <summary>
        /// Message data that is being transmitted.
        /// 
        /// </summary>
        public byte[] MessageData { get; set; }

        /// <summary>
        /// Default empty constructor.
        /// 
        /// </summary>
        public ScsRawDataMessage()
        {
        }

        /// <summary>
        /// Creates a new ScsRawDataMessage object with MessageData property.
        /// 
        /// </summary>
        /// <param name="messageData">Message data that is being transmitted</param>
        public ScsRawDataMessage(byte[] messageData)
        {
            this.MessageData = messageData;
        }

        /// <summary>
        /// Creates a new reply ScsRawDataMessage object with MessageData property.
        /// 
        /// </summary>
        /// <param name="messageData">Message data that is being transmitted</param><param name="repliedMessageId">Replied message id if this is a reply for
        ///             a message.
        ///             </param>
        public ScsRawDataMessage(byte[] messageData, string repliedMessageId)
            : this(messageData)
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
            int num = this.MessageData == null ? 0 : this.MessageData.Length;
            if (!string.IsNullOrEmpty(this.RepliedMessageId))
                return string.Format("ScsRawDataMessage [{0}] Replied To [{1}]: {2} bytes", (object)this.MessageId, (object)this.RepliedMessageId, (object)num);
            else
                return string.Format("ScsRawDataMessage [{0}]: {1} bytes", (object)this.MessageId, (object)num);
        }
    }
}
