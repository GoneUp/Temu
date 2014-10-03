// Type: Hik.Communication.Scs.Server.ScsServerFactory
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication.EndPoints;

namespace Hik.Communication.Scs.Server
{
    /// <summary>
    /// This class is used to create SCS servers.
    /// 
    /// </summary>
    public static class ScsServerFactory
    {
        /// <summary>
        /// Creates a new SCS Server using an EndPoint.
        /// 
        /// </summary>
        /// <param name="endPoint">Endpoint that represents address of the server</param>
        /// <returns>
        /// Created TCP server
        /// </returns>
        public static IScsServer CreateServer(ScsEndPoint endPoint)
        {
            return endPoint.CreateServer();
        }
    }
}
