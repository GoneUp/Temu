// Type: Hik.Communication.Scs.Client.Tcp.ScsTcpClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System.Net;
using Tera.NetworkApi.Communication.Scs.Communication.Channels;
using Tera.NetworkApi.Communication.Scs.Communication.Channels.Tcp;
using Tera.NetworkApi.Communication.Scs.Communication.EndPoints.Tcp;

namespace Tera.NetworkApi.Communication.Scs.Client.Tcp
{
    /// <summary>
    /// This class is used to communicate with server over TCP/IP protocol.
    /// 
    /// </summary>
    internal class ScsTcpClient : ScsClientBase
    {
        /// <summary>
        /// The endpoint address of the server.
        /// 
        /// </summary>
        private readonly ScsTcpEndPoint _serverEndPoint;

        /// <summary>
        /// Creates a new ScsTcpClient object.
        /// 
        /// </summary>
        /// <param name="serverEndPoint">The endpoint address to connect to the server</param>
        public ScsTcpClient(ScsTcpEndPoint serverEndPoint)
        {
            this._serverEndPoint = serverEndPoint;
        }

        /// <summary>
        /// Creates a communication channel using ServerIpAddress and ServerPort.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Ready communication channel to communicate
        /// </returns>
        protected override ICommunicationChannel CreateCommunicationChannel()
        {
            return (ICommunicationChannel)new TcpCommunicationChannel(TcpHelper.ConnectToServer((EndPoint)new IPEndPoint(IPAddress.Parse(this._serverEndPoint.IpAddress), this._serverEndPoint.TcpPort), this.ConnectTimeout));
        }
    }
}
