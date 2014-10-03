// Type: Hik.Communication.Scs.Client.Tcp.TcpHelper
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkApi.Communication.Scs.Client.Tcp
{
    /// <summary>
    /// This class is used to simplify TCP socket operations.
    /// 
    /// </summary>
    internal static class TcpHelper
    {
        /// <summary>
        /// This code is used to connect to a TCP socket with timeout option.
        /// 
        /// </summary>
        /// <param name="endPoint">IP endpoint of remote server</param><param name="timeoutMs">Timeout to wait until connect</param>
        /// <returns>
        /// Socket object connected to server
        /// </returns>
        /// <exception cref="T:System.Net.Sockets.SocketException">Throws SocketException if can not connect.</exception><exception cref="T:System.TimeoutException">Throws TimeoutException if can not connect within specified timeoutMs</exception>
        public static Socket ConnectToServer(EndPoint endPoint, int timeoutMs)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Blocking = false;
                socket.Connect(endPoint);
                socket.Blocking = true;
                return socket;
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode != 10035)
                {
                    socket.Close();
                    throw;
                }
                else if (!socket.Poll(timeoutMs * 1000, SelectMode.SelectWrite))
                {
                    socket.Close();
                    throw new TimeoutException("The host failed to connect. Timeout occured.");
                }
                else
                {
                    socket.Blocking = true;
                    return socket;
                }
            }
        }
    }
}
