// Type: Hik.Communication.ScsServices.Service.IScsServiceClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.EndPoints;
using System;

namespace NetworkApi.Communication.ScsServices.Service
{
    /// <summary>
    /// Represents a client that uses a SDS service.
    /// 
    /// </summary>
    public interface IScsServiceClient
    {
        /// <summary>
        /// Unique identifier for this client.
        /// 
        /// </summary>
        long ClientId { get; }

        /// <summary>
        /// Gets endpoint of remote application.
        /// 
        /// </summary>
        ScsEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Gets the communication state of the Client.
        /// 
        /// </summary>
        CommunicationStates CommunicationState { get; }

        /// <summary>
        /// This event is raised when client is disconnected from service.
        /// 
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Closes client connection.
        /// 
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Gets the client proxy interface that provides calling client methods remotely.
        /// 
        /// </summary>
        /// <typeparam name="T">Type of client interface</typeparam>
        /// <returns>
        /// Client interface
        /// </returns>
        T GetClientProxy<T>() where T : class;
    }
}
