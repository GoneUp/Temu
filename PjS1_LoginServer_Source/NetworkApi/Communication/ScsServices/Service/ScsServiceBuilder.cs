// Type: Hik.Communication.ScsServices.Service.ScsServiceBuilder
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication.EndPoints;
using NetworkApi.Communication.Scs.Server;

namespace NetworkApi.Communication.ScsServices.Service
{
    /// <summary>
    /// This class is used to build ScsService applications.
    /// 
    /// </summary>
    public static class ScsServiceBuilder
    {
        /// <summary>
        /// Creates a new SCS Service application using an EndPoint.
        /// 
        /// </summary>
        /// <param name="endPoint">EndPoint that represents address of the service</param>
        /// <returns>
        /// Created SCS service application
        /// </returns>
        public static IScsServiceApplication CreateService(ScsEndPoint endPoint)
        {
            return (IScsServiceApplication)new ScsServiceApplication(ScsServerFactory.CreateServer(endPoint));
        }
    }
}
