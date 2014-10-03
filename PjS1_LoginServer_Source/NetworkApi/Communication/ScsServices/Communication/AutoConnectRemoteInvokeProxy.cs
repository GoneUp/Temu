// Type: Hik.Communication.ScsServices.Communication.AutoConnectRemoteInvokeProxy`2
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Client;
using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.Messengers;
using System.Runtime.Remoting.Messaging;

namespace NetworkApi.Communication.ScsServices.Communication
{
    /// <summary>
    /// This class extends RemoteInvokeProxy to provide auto connect/disconnect mechanism
    ///             if client is not connected to the server when a service method is called.
    /// 
    /// </summary>
    /// <typeparam name="TProxy">Type of the proxy class/interface</typeparam><typeparam name="TMessenger">Type of the messenger object that is used to send/receive messages</typeparam>
    internal class AutoConnectRemoteInvokeProxy<TProxy, TMessenger> : RemoteInvokeProxy<TProxy, TMessenger> where TMessenger : IMessenger
    {
        /// <summary>
        /// Reference to the client object that is used to connect/disconnect.
        /// 
        /// </summary>
        private readonly IConnectableClient _client;

        /// <summary>
        /// Creates a new AutoConnectRemoteInvokeProxy object.
        /// 
        /// </summary>
        /// <param name="clientMessenger">Messenger object that is used to send/receive messages</param><param name="client">Reference to the client object that is used to connect/disconnect</param>
        public AutoConnectRemoteInvokeProxy(RequestReplyMessenger<TMessenger> clientMessenger, IConnectableClient client)
            : base(clientMessenger)
        {
            this._client = client;
        }

        /// <summary>
        /// Overrides message calls and translates them to messages to remote application.
        /// 
        /// </summary>
        /// <param name="msg">Method invoke message (from RealProxy base class)</param>
        /// <returns>
        /// Method invoke return message (to RealProxy base class)
        /// </returns>
        public override IMessage Invoke(IMessage msg)
        {
            if (this._client.CommunicationState == CommunicationStates.Connected)
                return base.Invoke(msg);
            this._client.Connect();
            try
            {
                return base.Invoke(msg);
            }
            finally
            {
                this._client.Disconnect();
            }
        }
    }
}
