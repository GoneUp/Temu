// Type: Hik.Communication.Scs.Server.Tcp.ScsTcpServer
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication.Channels;
using Hik.Communication.Scs.Communication.Channels.Tcp;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;

namespace Hik.Communication.Scs.Server.Tcp
{
    /// <summary>
    /// This class is used to create a TCP server.
    /// 
    /// </summary>
    internal class ScsTcpServer : ScsServerBase
    {
        /// <summary>
        /// The endpoint address of the server to listen incoming connections.
        /// 
        /// </summary>
        private readonly ScsTcpEndPoint _endPoint;

        /// <summary>
        /// Creates a new ScsTcpServer object.
        /// 
        /// </summary>
        /// <param name="endPoint">The endpoint address of the server to listen incoming connections</param>
        public ScsTcpServer(ScsTcpEndPoint endPoint)
        {
            this._endPoint = endPoint;
        }

        /// <summary>
        /// Creates a TCP connection listener.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Created listener object
        /// </returns>
        protected override IConnectionListener CreateConnectionListener()
        {
            return (IConnectionListener)new TcpConnectionListener(this._endPoint);
        }
    }
}
