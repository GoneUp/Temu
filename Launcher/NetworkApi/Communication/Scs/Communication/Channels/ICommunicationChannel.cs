// Type: Hik.Communication.Scs.Communication.Channels.ICommunicationChannel
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Communication.EndPoints;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;

namespace Tera.NetworkApi.Communication.Scs.Communication.Channels
{
    /// <summary>
    /// Represents a communication channel.
    ///             A communication channel is used to communicate (send/receive messages) with a remote application.
    /// 
    /// </summary>
    internal interface ICommunicationChannel : IMessenger
    {
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
        /// Starts the communication with remote application.
        /// 
        /// </summary>
        void Start();

        /// <summary>
        /// Closes messenger.
        /// 
        /// </summary>
        void Disconnect();
    }
}
