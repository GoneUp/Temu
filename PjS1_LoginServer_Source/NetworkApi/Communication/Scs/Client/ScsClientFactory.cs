// Type: Hik.Communication.Scs.Client.ScsClientFactory
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication.EndPoints;

namespace NetworkApi.Communication.Scs.Client
{
    /// <summary>
    /// This class is used to create SCS Clients to connect to a SCS server.
    /// 
    /// </summary>
    public static class ScsClientFactory
    {
        /// <summary>
        /// Creates a new client to connect to a server using an end point.
        /// 
        /// </summary>
        /// <param name="endpoint">End point of the server to connect it</param>
        /// <returns>
        /// Created TCP client
        /// </returns>
        public static IScsClient CreateClient(ScsEndPoint endpoint)
        {
            return endpoint.CreateClient();
        }

        /// <summary>
        /// Creates a new client to connect to a server using an end point.
        /// 
        /// </summary>
        /// <param name="endpointAddress">End point address of the server to connect it</param>
        /// <returns>
        /// Created TCP client
        /// </returns>
        public static IScsClient CreateClient(string endpointAddress)
        {
            return ScsClientFactory.CreateClient(ScsEndPoint.CreateEndPoint(endpointAddress));
        }
    }
}
