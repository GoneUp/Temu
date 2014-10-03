// Type: Hik.Communication.Scs.Server.IScsServerClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.EndPoints;
using NetworkApi.Communication.Scs.Communication.Messengers;
using System;

namespace NetworkApi.Communication.Scs.Server
{
    /// <summary>
    /// Represents a client from a perspective of a server.
    /// 
    /// </summary>
    public interface IScsServerClient : IMessenger
    {
        /// <summary>
        /// Unique identifier for this client in server.
        /// 
        /// </summary>
        long ClientId { get; }

        /// <summary>
        /// Gets endpoint of remote application.
        /// 
        /// </summary>
        ScsEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Gets the current communication state.
        /// 
        /// </summary>
        CommunicationStates CommunicationState { get; }

        /// <summary>
        /// This event is raised when client disconnected from server.
        /// 
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Disconnects from server.
        /// 
        /// </summary>
        void Disconnect();
    }
}
