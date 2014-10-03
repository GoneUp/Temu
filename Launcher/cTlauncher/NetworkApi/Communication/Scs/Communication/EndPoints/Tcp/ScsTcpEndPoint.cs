// Type: Hik.Communication.Scs.Communication.EndPoints.Tcp.ScsTcpEndPoint
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Client.Tcp;
using Hik.Communication.Scs.Communication.EndPoints;
using Hik.Communication.Scs.Server;
using Hik.Communication.Scs.Server.Tcp;
using System;

namespace Hik.Communication.Scs.Communication.EndPoints.Tcp
{
    /// <summary>
    /// Represens a TCP end point in SCS.
    /// 
    /// </summary>
    public sealed class ScsTcpEndPoint : ScsEndPoint
    {
        /// <summary>
        /// IP address of the server.
        /// 
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Listening TCP Port for incoming connection requests on server.
        /// 
        /// </summary>
        public int TcpPort { get; private set; }

        /// <summary>
        /// Creates a new ScsTcpEndPoint object with specified port number.
        /// 
        /// </summary>
        /// <param name="tcpPort">Listening TCP Port for incoming connection requests on server</param>
        public ScsTcpEndPoint(int tcpPort)
        {
            this.TcpPort = tcpPort;
        }

        /// <summary>
        /// Creates a new ScsTcpEndPoint object with specified IP address and port number.
        /// 
        /// </summary>
        /// <param name="ipAddress">IP address of the server</param><param name="port">Listening TCP Port for incoming connection requests on server</param>
        public ScsTcpEndPoint(string ipAddress, int port)
        {
            this.IpAddress = ipAddress;
            this.TcpPort = port;
        }

        /// <summary>
        /// Creates a new ScsTcpEndPoint from a string address.
        ///             Address format must be like IPAddress:Port (For example: 127.0.0.1:10085).
        /// 
        /// </summary>
        /// <param name="address">TCP end point Address</param>
        /// <returns>
        /// Created ScsTcpEndpoint object
        /// </returns>
        public ScsTcpEndPoint(string address)
        {
            string[] strArray = address.Trim().Split(new char[1]
      {
        ':'
      });
            this.IpAddress = strArray[0].Trim();
            this.TcpPort = Convert.ToInt32(strArray[1].Trim());
        }

        /// <summary>
        /// Creates a Scs Server that uses this end point to listen incoming connections.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Scs Server
        /// </returns>
        internal override IScsServer CreateServer()
        {
            return (IScsServer)new ScsTcpServer(this);
        }

        /// <summary>
        /// Creates a Scs Client that uses this end point to connect to server.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Scs Client
        /// </returns>
        internal override IScsClient CreateClient()
        {
            return (IScsClient)new ScsTcpClient(this);
        }

        /// <summary>
        /// Generates a string representation of this end point object.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// String representation of this end point object
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.IpAddress))
                return "tcp://" + (object)this.TcpPort;
            return string.Concat(new object[4]
      {
        (object) "tcp://",
        (object) this.IpAddress,
        (object) ":",
        (object) this.TcpPort
      });
        }
    }
}
