// Type: Hik.Communication.ScsServices.Communication.Messages.ScsRemoteInvokeMessage
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication.Messages;
using System;

namespace Hik.Communication.ScsServices.Communication.Messages
{
    /// <summary>
    /// This message is sent to invoke a method of a remote application.
    /// 
    /// </summary>
    [Serializable]
    public class ScsRemoteInvokeMessage : ScsMessage
    {
        /// <summary>
        /// Name of the remove service class.
        /// 
        /// </summary>
        public string ServiceClassName { get; set; }

        /// <summary>
        /// Method of remote application to invoke.
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Parameters of method.
        /// 
        /// </summary>
        public object[] Parameters { get; set; }

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
            return string.Format("ScsRemoteInvokeMessage: {0}.{1}(...)", (object)this.ServiceClassName, (object)this.MethodName);
        }
    }
}
