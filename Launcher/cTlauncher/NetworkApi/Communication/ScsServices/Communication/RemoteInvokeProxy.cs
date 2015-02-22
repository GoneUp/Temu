// Type: Hik.Communication.ScsServices.Communication.RemoteInvokeProxy`2
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Tera.NetworkApi.Communication.Scs.Communication.Messages;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;
using Tera.NetworkApi.Communication.ScsServices.Communication.Messages;

namespace Tera.NetworkApi.Communication.ScsServices.Communication
{
    /// <summary>
    /// This class is used to generate a dynamic proxy to invoke remote methods.
    ///             It translates method invocations to messaging.
    /// 
    /// </summary>
    /// <typeparam name="TProxy">Type of the proxy class/interface</typeparam><typeparam name="TMessenger">Type of the messenger object that is used to send/receive messages</typeparam>
    internal class RemoteInvokeProxy<TProxy, TMessenger> : RealProxy where TMessenger : IMessenger
    {
        /// <summary>
        /// Messenger object that is used to send/receive messages.
        /// 
        /// </summary>
        private readonly RequestReplyMessenger<TMessenger> _clientMessenger;

        /// <summary>
        /// Creates a new RemoteInvokeProxy object.
        /// 
        /// </summary>
        /// <param name="clientMessenger">Messenger object that is used to send/receive messages</param>
        public RemoteInvokeProxy(RequestReplyMessenger<TMessenger> clientMessenger)
            : base(typeof(TProxy))
        {
            this._clientMessenger = clientMessenger;
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
            IMethodCallMessage mcm = msg as IMethodCallMessage;
            if (mcm == null)
                return (IMessage)null;
            ScsRemoteInvokeReturnMessage invokeReturnMessage = this._clientMessenger.SendMessageAndWaitForResponse((IScsMessage)new ScsRemoteInvokeMessage()
            {
                ServiceClassName = typeof(TProxy).Name,
                MethodName = mcm.MethodName,
                Parameters = mcm.InArgs
            }) as ScsRemoteInvokeReturnMessage;
            if (invokeReturnMessage == null)
                return (IMessage)null;
            if (invokeReturnMessage.RemoteException == null)
                return (IMessage)new ReturnMessage(invokeReturnMessage.ReturnValue, (object[])null, 0, mcm.LogicalCallContext, mcm);
            else
                return (IMessage)new ReturnMessage((Exception)invokeReturnMessage.RemoteException, mcm);
        }
    }
}
