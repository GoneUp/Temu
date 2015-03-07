// Type: Hik.Communication.ScsServices.Communication.Messages.ScsRemoteInvokeReturnMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Communication.Messages;

namespace Tera.NetworkApi.Communication.ScsServices.Communication.Messages
{
    /// <summary>
    /// This message is sent as response message to a ScsRemoteInvokeMessage.
    ///             It is used to send return value of method invocation.
    /// 
    /// </summary>
    [Serializable]
    public class ScsRemoteInvokeReturnMessage : ScsMessage
    {
        /// <summary>
        /// Return value of remote method invocation.
        /// 
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// If any exception occured during method invocation, this field contains Exception object.
        ///             If no exception occured, this field is null.
        /// 
        /// </summary>
        public ScsRemoteException RemoteException { get; set; }

        /// <summary>
        /// Represents this object as string.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// String representation of this object
        /// </returns>
        public override string ToString()
        {
            return string.Format("ScsRemoteInvokeReturnMessage: Returns {0}, Exception = {1}", this.ReturnValue, (object)this.RemoteException);
        }
    }
}
