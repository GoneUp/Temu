// Type: Hik.Communication.ScsServices.Service.ServiceClientEventArgs
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace Hik.Communication.ScsServices.Service
{
    /// <summary>
    /// Stores service client informations to be used by an event.
    /// 
    /// </summary>
    public class ServiceClientEventArgs : EventArgs
    {
        /// <summary>
        /// Client that is associated with this event.
        /// 
        /// </summary>
        public IScsServiceClient Client { get; private set; }

        /// <summary>
        /// Creates a new ServiceClientEventArgs object.
        /// 
        /// </summary>
        /// <param name="client">Client that is associated with this event</param>
        public ServiceClientEventArgs(IScsServiceClient client)
        {
            this.Client = client;
        }
    }
}
