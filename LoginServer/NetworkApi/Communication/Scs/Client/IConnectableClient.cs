// Type: Hik.Communication.Scs.Client.IConnectableClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using System;

namespace NetworkApi.Communication.Scs.Client
{
    /// <summary>
    /// Represents a client for SCS servers.
    /// 
    /// </summary>
    public interface IConnectableClient : IDisposable
    {
        /// <summary>
        /// Timeout for connecting to a server (as milliseconds).
        ///             Default value: 15 seconds (15000 ms).
        /// 
        /// </summary>
        int ConnectTimeout { get; set; }

        /// <summary>
        /// Gets the current communication state.
        /// 
        /// </summary>
        CommunicationStates CommunicationState { get; }

        /// <summary>
        /// This event is raised when client connected to server.
        /// 
        /// </summary>
        event EventHandler Connected;

        /// <summary>
        /// This event is raised when client disconnected from server.
        /// 
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Connects to server.
        /// 
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects from server.
        ///             Does nothing if already disconnected.
        /// 
        /// </summary>
        void Disconnect();
    }
}
