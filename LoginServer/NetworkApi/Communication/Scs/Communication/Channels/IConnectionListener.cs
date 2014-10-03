// Type: Hik.Communication.Scs.Communication.Channels.IConnectionListener
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Communication.Channels
{
    /// <summary>
    /// Represents a communication listener.
    ///             A connection listener is used to accept incoming client connection requests.
    /// 
    /// </summary>
    internal interface IConnectionListener
    {
        /// <summary>
        /// This event is raised when a new communication channel connected.
        /// 
        /// </summary>
        event EventHandler<CommunicationChannelEventArgs> CommunicationChannelConnected;

        /// <summary>
        /// Starts listening incoming connections.
        /// 
        /// </summary>
        void Start();

        /// <summary>
        /// Stops listening incoming connections.
        /// 
        /// </summary>
        void Stop();
    }
}
