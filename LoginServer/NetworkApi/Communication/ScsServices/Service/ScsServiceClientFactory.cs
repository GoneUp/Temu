// Type: Hik.Communication.ScsServices.Service.ScsServiceClientFactory
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication.Messengers;
using NetworkApi.Communication.Scs.Server;

namespace NetworkApi.Communication.ScsServices.Service
{
    /// <summary>
    /// This class is used to create service client objects that is used in server-side.
    /// 
    /// </summary>
    internal static class ScsServiceClientFactory
    {
        /// <summary>
        /// Creates a new service client object that is used in server-side.
        /// 
        /// </summary>
        /// <param name="serverClient">Underlying server client object</param><param name="requestReplyMessenger">RequestReplyMessenger object to send/receive messages over serverClient</param>
        /// <returns/>
        public static IScsServiceClient CreateServiceClient(IScsServerClient serverClient, RequestReplyMessenger<IScsServerClient> requestReplyMessenger)
        {
            return (IScsServiceClient)new ScsServiceClient(serverClient, requestReplyMessenger);
        }
    }
}
