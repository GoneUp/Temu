using System;
using Tera.NetworkApi.Communication.Scs.Client;
using Tera.NetworkApi.Communication.Scs.Communication.EndPoints.Tcp;
using Tera.NetworkApi.Communication.Scs.Communication.Messages;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;

namespace Tera.Network
{
    public class ClientConnection
    {
        public static void PrintMessage(string Message)
        {
            MainWindow.mainWindow.PrintMessage(Message);
        }
        public static bool CheckConnectionMessage(string connectString)
        {
            try
            {
                using (var client = ScsClientFactory.CreateClient(new ScsTcpEndPoint(MainWindow.launcherConfig.ServerIP, Convert.ToInt32(MainWindow.launcherConfig.ServerPort))))
                {
                    client.WireProtocol = new KeyProtocol();
                    
                    using (var synchronizedMessenger = new SynchronizedMessenger<IScsClient>(client))
                    {
                        //setup response manager
                        synchronizedMessenger.Start();

                        client.Connect();

                        synchronizedMessenger.SendMessage(new ScsTextMessage(connectString));
                        var replyPacket = synchronizedMessenger.ReceiveMessage<ScsTextMessage>();
                        if (replyPacket == null)
                        {
                            client.Disconnect();
                            return false;
                        }
                        //
                        string data = replyPacket.Text;
                        switch (data)
                        {
                            //LoginPacket Response
                            case "4099"://Auth FAILED PASS
                                PrintMessage("Sorry! Authorization for this Account failed, is your Password correct?");
                                client.Disconnect();
                                return false;
                           case "4098"://Auth OK
                                PrintMessage("Yes! Authorization for this Account was successfull!");
                                client.Disconnect();
                                return true;
                            case "4100"://Account Unbanned
                                PrintMessage("Yes! Your Account got Unbanned, have fun to play!");
                                client.Disconnect();
                                return true;
                            case "4101"://Account Banned
                                PrintMessage("Sorry! but your Account is Banned, please contact the support for more information!");
                                client.Disconnect();
                                return false;
                            case "4102"://Account Online
                                PrintMessage("Wtf! You are already Online, if its not you please contact the Support!");
                                client.Disconnect();
                                return false;

                            //Register Packet Response
                            case "8194"://Account already Exists
                                PrintMessage("Sorry! Account Registration failed, Account already exists!");
                                client.Disconnect();
                                return false;
                            case "8195"://Account creation successfull
                                PrintMessage("Yes! Registration for this Account was successful!");
                                client.Disconnect();
                                return true;

                            default:
                                PrintMessage("PacketFailure %$§&*$" + replyPacket.Text);
                                client.Disconnect();
                                return false;
                        }
                    }
                }
            }
            catch (Exception )//ex)
            {
                //MessageBox.Show("Connecting error!\n" + ex,"Error");
                return false;
            }
        }
    
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         