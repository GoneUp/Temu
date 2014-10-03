// Type: Hik.Communication.Scs.Communication.Channels.Tcp.TcpCommunicationChannel
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Channels;
using Hik.Communication.Scs.Communication.EndPoints;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using System;
using System.Net;
using System.Net.Sockets;

namespace Hik.Communication.Scs.Communication.Channels.Tcp
{
    /// <summary>
    /// This class is used to communicate with a remote application over TCP/IP protocol.
    /// 
    /// </summary>
    internal class TcpCommunicationChannel : CommunicationChannelBase
    {
        /// <summary>
        /// Size of the buffer that is used to receive bytes from TCP socket.
        /// 
        /// </summary>
        private const int ReceiveBufferSize = 4096;
        private readonly ScsTcpEndPoint _remoteEndPoint;
        /// <summary>
        /// This buffer is used to receive bytes
        /// 
        /// </summary>
        private readonly byte[] _buffer;
        /// <summary>
        /// Socket object to send/reveice messages.
        /// 
        /// </summary>
        private readonly Socket _clientSocket;
        /// <summary>
        /// A flag to control thread's running
        /// 
        /// </summary>
        private volatile bool _running;
        /// <summary>
        /// This object is just used for thread synchronizing (locking).
        /// 
        /// </summary>
        private readonly object _syncLock;

        /// <summary>
        /// Gets the endpoint of remote application.
        /// 
        /// </summary>
        public override ScsEndPoint RemoteEndPoint
        {
            get
            {
                return (ScsEndPoint)this._remoteEndPoint;
            }
        }

        /// <summary>
        /// Creates a new TcpCommunicationChannel object.
        /// 
        /// </summary>
        /// <param name="clientSocket">A connected Socket object that is
        ///             used to communicate over network</param>
        public TcpCommunicationChannel(Socket clientSocket)
        {
            this._clientSocket = clientSocket;
            this._clientSocket.NoDelay = true;
            IPEndPoint ipEndPoint = (IPEndPoint)this._clientSocket.RemoteEndPoint;
            this._remoteEndPoint = new ScsTcpEndPoint(ipEndPoint.Address.ToString(), ipEndPoint.Port);
            this._buffer = new byte[4096];
            this._syncLock = new object();
        }

        /// <summary>
        /// Disconnects from remote application and closes channel.
        /// 
        /// </summary>
        public override void Disconnect()
        {
            if (this.CommunicationState != CommunicationStates.Connected)
                return;
            this._running = false;
            try
            {
                if (this._clientSocket.Connected)
                    this._clientSocket.Close();
                this._clientSocket.Dispose();
            }
            catch
            {
            }
            this.CommunicationState = CommunicationStates.Disconnected;
            this.OnDisconnected();
        }

        /// <summary>
        /// Starts the thread to receive messages from socket.
        /// 
        /// </summary>
        protected override void StartInternal()
        {
            this._running = true;
            this._clientSocket.BeginReceive(this._buffer, 0, this._buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), (object)null);
        }

        /// <summary>
        /// Sends a message to the remote application.
        /// 
        /// </summary>
        /// <param name="message">Message to be sent</param>
        protected override void SendMessageInternal(IScsMessage message)
        {
            int offset = 0;
            lock (this._syncLock)
            {
                byte[] local_1 = this.WireProtocol.GetBytes(message);
                while (offset < local_1.Length)
                {
                    int local_2 = this._clientSocket.Send(local_1, offset, local_1.Length - offset, SocketFlags.None);
                    if (local_2 <= 0)
                        throw new CommunicationException("Message could not be sent via TCP socket. Only " + (object)offset + " bytes of " + (string)(object)local_1.Length + " bytes are sent.");
                    else
                        offset += local_2;
                }
                this.LastSentMessageTime = DateTime.Now;
                this.OnMessageSent(message);
            }
        }

        /// <summary>
        /// This method is used as callback method in _clientSocket's BeginReceive method.
        ///             It reveives bytes from socker.
        /// 
        /// </summary>
        /// <param name="ar">Asyncronous call result</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            if (!this._running)
                return;
            try
            {
                int length = this._clientSocket.EndReceive(ar);
                if (length <= 0)
                    throw new CommunicationException("Tcp socket is closed");
                this.LastReceivedMessageTime = DateTime.Now;
                byte[] receivedBytes = new byte[length];
                Array.Copy((Array)this._buffer, 0, (Array)receivedBytes, 0, length);
                foreach (IScsMessage message in this.WireProtocol.CreateMessages(receivedBytes))
                    this.OnMessageReceived(message);
                if (!this._running)
                    return;
                this._clientSocket.BeginReceive(this._buffer, 0, this._buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceiveCallback), (object)null);
            }
            catch
            {
                this.Disconnect();
            }
        }
    }
}
