// Type: Hik.Communication.Scs.Server.ServerClientEventArgs
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Server
{
    /// <summary>
    /// Stores client information to be used by an event.
    /// 
    /// </summary>
    public class ServerClientEventArgs : EventArgs
    {
        /// <summary>
        /// Client that is associated with this event.
        /// 
        /// </summary>
        public IScsServerClient Client { get; private set; }

        /// <summary>
        /// Creates a new ServerClientEventArgs object.
        /// 
        /// </summary>
        /// <param name="client">Client that is associated with this event</param>
        public ServerClientEventArgs(IScsServerClient client)
        {
            this.Client = client;
        }
    }
}
