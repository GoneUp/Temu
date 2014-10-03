// Type: Hik.Communication.Scs.Communication.CommunicationStateException
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Runtime.Serialization;

namespace NetworkApi.Communication.Scs.Communication
{
    /// <summary>
    /// This application is thrown if communication is not expected state.
    /// 
    /// </summary>
    [Serializable]
    public class CommunicationStateException : CommunicationException
    {
        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        public CommunicationStateException()
        {
        }

        /// <summary>
        /// Contstructor for serializing.
        /// 
        /// </summary>
        public CommunicationStateException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        /// <param name="message">Exception message</param>
        public CommunicationStateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        /// <param name="message">Exception message</param><param name="innerException">Inner exception</param>
        public CommunicationStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
