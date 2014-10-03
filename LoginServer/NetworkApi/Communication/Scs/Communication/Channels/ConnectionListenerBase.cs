// Type: Hik.Communication.Scs.Communication.Channels.ConnectionListenerBase
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Communication.Channels
{
    /// <summary>
    /// This class provides base functionality for communication listener classes.
    /// 
    /// </summary>
    internal abstract class ConnectionListenerBase : IConnectionListener
    {
        /// <summary>
        /// This event is raised when a new communication channel is connected.
        /// 
        /// </summary>
        public event EventHandler<CommunicationChannelEventArgs> CommunicationChannelConnected;

        /// <summary>
        /// Starts listening incoming connections.
        /// 
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stops listening incoming connections.
        /// 
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// Raises CommunicationChannelConnected event.
        /// 
        /// </summary>
        /// <param name="client"/>
        protected virtual void OnCommunicationChannelConnected(ICommunicationChannel client)
        {
            EventHandler<CommunicationChannelEventArgs> eventHandler = this.CommunicationChannelConnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new CommunicationChannelEventArgs(client));
        }
    }
}
