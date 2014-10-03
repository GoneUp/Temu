// Type: Hik.Communication.Scs.Communication.Messages.IScsMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

namespace NetworkApi.Communication.Scs.Communication.Messages
{
    /// <summary>
    /// Represents a message that is sent and received by server and client.
    /// 
    /// </summary>
    public interface IScsMessage
    {
        /// <summary>
        /// Unique identified for this message.
        /// 
        /// </summary>
        string MessageId { get; }

        /// <summary>
        /// Unique identified for this message.
        /// 
        /// </summary>
        string RepliedMessageId { get; set; }
    }
}
