// Type: Hik.Communication.Scs.Communication.CommunicationException
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Runtime.Serialization;

namespace NetworkApi.Communication.Scs.Communication
{
    /// <summary>
    /// This application is thrown in a communication error.
    /// 
    /// </summary>
    [Serializable]
    public class CommunicationException : Exception
    {
        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        public CommunicationException()
        {
        }

        /// <summary>
        /// Contstructor for serializing.
        /// 
        /// </summary>
        public CommunicationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        /// <param name="message">Exception message</param>
        public CommunicationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        /// <param name="message">Exception message</param><param name="innerException">Inner exception</param>
        public CommunicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
