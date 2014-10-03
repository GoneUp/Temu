// Type: Hik.Communication.ScsServices.Communication.Messages.ScsRemoteException
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Runtime.Serialization;

namespace NetworkApi.Communication.ScsServices.Communication.Messages
{
    /// <summary>
    /// Represents a SCS Remote Exception.
    ///             This exception is used to send an exception from an application to another application.
    /// 
    /// </summary>
    [Serializable]
    public class ScsRemoteException : Exception
    {
        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        public ScsRemoteException()
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        public ScsRemoteException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        /// <param name="message">Exception message</param>
        public ScsRemoteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Contstructor.
        /// 
        /// </summary>
        /// <param name="message">Exception message</param><param name="innerException">Inner exception</param>
        public ScsRemoteException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
