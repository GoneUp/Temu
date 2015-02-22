// Type: Hik.Communication.Scs.Client.ClientReConnecter
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Communication;
using Tera.NetworkApi.Threading;

namespace Tera.NetworkApi.Communication.Scs.Client
{
    /// <summary>
    /// This class is used to automatically re-connect to server if disconnected.
    ///             It attempts to reconnect to server periodically until connection established.
    /// 
    /// </summary>
    public class ClientReConnecter : IDisposable
    {
        /// <summary>
        /// Reference to client object.
        /// 
        /// </summary>
        private readonly IConnectableClient _client;
        /// <summary>
        /// Timer to  attempt ro reconnect periodically.
        /// 
        /// </summary>
        private readonly Timer _reconnectTimer;
        /// <summary>
        /// Indicates the dispose state of this object.
        /// 
        /// </summary>
        private volatile bool _disposed;

        /// <summary>
        /// Reconnect check period.
        ///             Default: 20 seconds.
        /// 
        /// </summary>
        public int ReConnectCheckPeriod
        {
            get
            {
                return this._reconnectTimer.Period;
            }
            set
            {
                this._reconnectTimer.Period = value;
            }
        }

        /// <summary>
        /// Creates a new ClientReConnecter object.
        ///             It is not needed to start ClientReConnecter since it automatically
        ///             starts when the client disconnected.
        /// 
        /// </summary>
        /// <param name="client">Reference to client object</param><exception cref="T:System.ArgumentNullException">Throws ArgumentNullException if client is null.</exception>
        public ClientReConnecter(IConnectableClient client)
        {
            if (client == null)
                throw new ArgumentNullException("client");
            this._client = client;
            this._client.Disconnected += new EventHandler(this.Client_Disconnected);
            this._reconnectTimer = new Timer(20000);
            this._reconnectTimer.Elapsed += new EventHandler(this.ReconnectTimer_Elapsed);
            this._reconnectTimer.Start();
        }

        /// <summary>
        /// Disposes this object.
        ///             Does nothing if already disposed.
        /// 
        /// </summary>
        public void Dispose()
        {
            if (this._disposed)
                return;
            this._disposed = true;
            this._client.Disconnected -= new EventHandler(this.Client_Disconnected);
            this._reconnectTimer.Stop();
        }

        /// <summary>
        /// Handles Disconnected event of _client object.
        /// 
        /// </summary>
        /// <param name="sender">Source of the event</param><param name="e">Event arguments</param>
        private void Client_Disconnected(object sender, EventArgs e)
        {
            this._reconnectTimer.Start();
        }

        /// <summary>
        /// Hadles Elapsed event of _reconnectTimer.
        /// 
        /// </summary>
        /// <param name="sender">Source of the event</param><param name="e">Event arguments</param>
        private void ReconnectTimer_Elapsed(object sender, EventArgs e)
        {
            if (!this._disposed)
            {
                if (this._client.CommunicationState != CommunicationStates.Connected)
                {
                    try
                    {
                        this._client.Connect();
                        this._reconnectTimer.Stop();
                        return;
                    }
                    catch
                    {
                        return;
                    }
                }
            }
            this._reconnectTimer.Stop();
        }
    }
}
