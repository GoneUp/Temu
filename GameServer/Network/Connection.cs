using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using Network;
using Tera.Communication.Logic;
using Tera.Crypt;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Account;
using Tera.Data.Structures.Player;
using Tera.Network.Messages;
using Tera.Network.Protocol;
using Utils;
using Utils.Logger;

namespace Tera.Network
{
    public class Connection : IConnection
    {
        public static List<Connection> Connections = new List<Connection>();
        public static Thread SendAllThread = new Thread(SendAll);

        protected static void SendAll()
        {
            while (true)
            {
                for (int i = 0; i < Connections.Count; i++)
                {
                    try
                    {
                        if (!Connections[i].Send())
                        {
                            Connections.RemoveAt(i--);
                        }
                    }
                    catch (Exception ex)
                    {
                       Logger.WriteLine(LogState.Exception,"Connection: SendAll: " + ex.Message);
                    }
                }

                Thread.Sleep(10);
            }
        }

        private GameAccount m_gameAccount;

        public GameAccount GameAccount
        {
            get { return m_gameAccount; }
            set
            {
                if (value.Connection != null)
                    value.Connection.Close();

                m_gameAccount = value;
                m_gameAccount.Connection = this;
            }
        }

        public Player Player { get; set; }

        public byte[] Buffer;

        protected int State;

        protected Session Session = new Session();

        protected IScsServerClient Client;

        protected List<byte[]> SendData = new List<byte[]>();

        protected int SendDataSize;

        protected object SendLock = new object();

        public Connection(IScsServerClient client)
        {
            Client = client;
            Client.WireProtocol = new KeyProtocol();

            Client.Disconnected += OnDisconnected;
            Client.MessageReceived += OnMessageReceived;

            Client.SendMessage(new KeyMessage { Key = new byte[] { 1, 0, 0, 0 } });

            Connections.Add(this);
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            AccountLogic.ClientDisconnected(this);

            if (m_gameAccount != null)
                m_gameAccount.Connection = null;
            m_gameAccount = null;

            if (Player != null)
                Player.Connection = null;
            Player = null;

            Buffer = null;
            Session = null;
            Client = null;
            SendData = null;
            SendLock = null;
        }

        private bool Send()
        {
            GameMessage message;

            if (SendLock == null)
                return false;

            lock (SendLock)
            {
                if (SendData.Count == 0)
                    return Client.CommunicationState == CommunicationStates.Connected;

                message = new GameMessage {Data = new byte[SendDataSize]};

                int pointer = 0;
                for (int i = 0; i < SendData.Count; i++)
                {
                    Array.Copy(SendData[i], 0, message.Data, pointer, SendData[i].Length);
                    pointer += SendData[i].Length;
                }

                SendData.Clear();
                SendDataSize = 0;
            }

            try
            {
                Client.SendMessage(message);
            }
            catch
            {
                //Already closed
                return false;
            }

            return true;
        }

        private void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if (State == 0)
            {
                KeyMessage keyMessage = (KeyMessage)e.Message;
                System.Buffer.BlockCopy(keyMessage.Key, 0, Session.ClientKey1, 0, 128);
                Client.SendMessage(new KeyMessage { Key = Session.ServerKey1 });
                State++;
                return;
            }

            if (State == 1)
            {
                KeyMessage keyMessage = (KeyMessage)e.Message;
                System.Buffer.BlockCopy(keyMessage.Key, 0, Session.ClientKey2, 0, 128);
                Client.SendMessage(new KeyMessage { Key = Session.ServerKey2 });
                Session.Init();
                Client.WireProtocol = new GameProtocol(Session);
                State++;
                return;
            }

            GameMessage message = (GameMessage)e.Message;
            Buffer = message.Data;

            if (OpCodes.Recv.ContainsKey(message.OpCode))
            {
               GlobalLogic.PacketReceived(GameAccount, OpCodes.Recv[message.OpCode], Buffer);

                ((ARecvPacket)Activator.CreateInstance(OpCodes.Recv[message.OpCode])).Process(this);

                string opCodeLittleEndianHex = BitConverter.GetBytes(message.OpCode).ToHex();
                Logger.WriteLine(LogState.Debug, "GsPacket opCode: 0x{0}{1} [{2}]",
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Buffer.Length);

                Logger.WriteLine(LogState.Debug, "Data:\n{0}", Buffer.FormatHex());

            }
            else
            {
                GlobalLogic.PacketReceived(GameAccount, null, Buffer);

                string opCodeLittleEndianHex = BitConverter.GetBytes(message.OpCode).ToHex();
                Logger.WriteLine(LogState.Debug, "Unknown GsPacket opCode: 0x{0}{1} [{2}]",
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Buffer.Length);

                Logger.WriteLine(LogState.Debug, "Data:\n{0}", Buffer.FormatHex());
            }
        }

        public bool IsValid
        {
            get { return true; }
        }

        public void Close()
        {
            if (m_gameAccount != null)
                m_gameAccount.LastOnlineUtc = RandomUtilities.GetCurrentMilliseconds();

            Client.Disconnect();
        }

        public void PushPacket(byte[] data)
        {
            //Already closed
            if (SendLock == null)
                return;

            lock (SendLock)
            {
                short opCode = BitConverter.ToInt16(data, 2);
                GlobalLogic.PacketSent(GameAccount,
                                       OpCodes.SendNames.ContainsKey(opCode)
                                           ? OpCodes.SendNames[opCode]
                                           : "unk",
                                       data);

                SendData.Add(data);
                SendDataSize += data.Length;
            }
        }

        public long Ping()
        {
            Ping ping = new Ping();

            //"tcp://127.0.0.1:27230"
            string ipAddress = Client.RemoteEndPoint.ToString().Substring(6);
            ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(':'));

            PingReply pingReply = ping.Send(ipAddress);

            return (pingReply != null) ? pingReply.RoundtripTime : 0;
        }
    }
}