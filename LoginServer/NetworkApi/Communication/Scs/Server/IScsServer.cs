// Type: Hik.Communication.Scs.Server.IScsServer
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Collections;
using NetworkApi.Communication.Scs.Communication.Protocols;
using System;

namespace NetworkApi.Communication.Scs.Server
{
    /// <summary>
    /// Represents a SCS server that is used to accept and manage client connections.
    /// 
    /// </summary>
    public interface IScsServer
    {
        /// <summary>
        /// Gets/sets wire protocol factory to create IWireProtocol objects.
        /// 
        /// </summary>
        IScsWireProtocolFactory WireProtocolFactory { get; set; }

        /// <summary>
        /// A collection of clients that are connected to the server.
        /// 
        /// </summary>
        ThreadSafeSortedList<long, IScsServerClient> Clients { get; }

        /// <summary>
        /// This event is raised when a new client connected to the server.
        /// 
        /// </summary>
        event EventHandler<ServerClientEventArgs> ClientConnected;

        /// <summary>
        /// This event is raised when a client disconnected from the server.
        /// 
        /// </summary>
        event EventHandler<ServerClientEventArgs> ClientDisconnected;

        /// <summary>
        /// Starts the server.
        /// 
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the server.
        /// 
        /// </summary>
        void Stop();
    }
}
