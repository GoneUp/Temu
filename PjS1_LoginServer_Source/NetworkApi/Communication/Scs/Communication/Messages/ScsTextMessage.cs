// Type: Hik.Communication.Scs.Communication.Messages.ScsTextMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Communication.Messages
{
    /// <summary>
    /// This message is used to send/receive a text as message data.
    /// 
    /// </summary>
    [Serializable]
    public class ScsTextMessage : ScsMessage
    {
        /// <summary>
        /// Message text that is being transmitted.
        /// 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Creates a new ScsTextMessage object.
        /// 
        /// </summary>
        public ScsTextMessage()
        {
        }

        /// <summary>
        /// Creates a new ScsTextMessage object with Text property.
        /// 
        /// </summary>
        /// <param name="text">Message text that is being transmitted</param>
        public ScsTextMessage(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Creates a new reply ScsTextMessage object with Text property.
        /// 
        /// </summary>
        /// <param name="text">Message text that is being transmitted</param><param name="repliedMessageId">Replied message id if this is a reply for
        ///             a message.
        ///             </param>
        public ScsTextMessage(string text, string repliedMessageId)
            : this(text)
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
                return string.Format("ScsTextMessage [{0}] Replied To [{1}]: {2}", (object)this.MessageId, (object)this.RepliedMessageId, (object)this.Text);
            else
                return string.Format("ScsTextMessage [{0}]: {1}", (object)this.MessageId, (object)this.Text);
        }
    }
}
