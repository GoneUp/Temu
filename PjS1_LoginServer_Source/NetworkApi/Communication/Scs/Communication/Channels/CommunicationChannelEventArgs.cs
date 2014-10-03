// Type: Hik.Communication.Scs.Communication.Channels.CommunicationChannelEventArgs
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace NetworkApi.Communication.Scs.Communication.Channels
{
    /// <summary>
    /// Stores communication channel information to be used by an event.
    /// 
    /// </summary>
    internal class CommunicationChannelEventArgs : EventArgs
    {
        /// <summary>
        /// Communication channel that is associated with this event.
        /// 
        /// </summary>
        public ICommunicationChannel Channel { get; private set; }

        /// <summary>
        /// Creates a new CommunicationChannelEventArgs object.
        /// 
        /// </summary>
        /// <param name="channel">Communication channel that is associated with this event</param>
        public CommunicationChannelEventArgs(ICommunicationChannel channel)
        {
            this.Channel = channel;
        }
    }
}
