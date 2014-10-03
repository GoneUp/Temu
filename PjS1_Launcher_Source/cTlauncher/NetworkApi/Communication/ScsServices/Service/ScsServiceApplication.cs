// Type: Hik.Communication.ScsServices.Service.ScsServiceApplication
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Collections;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Communication.Messengers;
using Hik.Communication.Scs.Server;
using Hik.Communication.ScsServices.Communication.Messages;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hik.Communication.ScsServices.Service
{
    /// <summary>
    /// Implements IScsServiceApplication and provides all functionallity.
    /// 
    /// </summary>
    internal class ScsServiceApplication : IScsServiceApplication
    {
        /// <summary>
        /// Underlying IScsServer object to accept and manage client connections.
        /// 
        /// </summary>
        private readonly IScsServer _scsServer;
        /// <summary>
        /// User service objects that is used to invoke incoming method invocation requests.
        ///             Key: Service interface type's name.
        ///             Value: Service object.
        /// 
        /// </summary>
        private readonly ThreadSafeSortedList<string, ScsServiceApplication.ServiceObject> _serviceObjects;
        /// <summary>
        /// All connected clients to service.
        ///             Key: Client's unique Id.
        ///             Value: Reference to the client.
        /// 
        /// </summary>
        private readonly ThreadSafeSortedList<long, IScsServiceClient> _serviceClients;

        /// <summary>
        /// This event is raised when a new client connected to the service.
        /// 
        /// </summary>
        public event EventHandler<ServiceClientEventArgs> ClientConnected;

        /// <summary>
        /// This event is raised when a client disconnected from the service.
        /// 
        /// </summary>
        public event EventHandler<ServiceClientEventArgs> ClientDisconnected;

        /// <summary>
        /// Creates a new ScsServiceApplication object.
        /// 
        /// </summary>
        /// <param name="scsServer">Underlying IScsServer object to accept and manage client connections</param><exception cref="T:System.ArgumentNullException">Throws ArgumentNullException if scsServer argument is null</exception>
        public ScsServiceApplication(IScsServer scsServer)
        {
            if (scsServer == null)
                throw new ArgumentNullException("scsServer");
            this._scsServer = scsServer;
            this._scsServer.ClientConnected += new EventHandler<ServerClientEventArgs>(this.ScsServer_ClientConnected);
            this._scsServer.ClientDisconnected += new EventHandler<ServerClientEventArgs>(this.ScsServer_ClientDisconnected);
            this._serviceObjects = new ThreadSafeSortedList<string, ScsServiceApplication.ServiceObject>();
            this._serviceClients = new ThreadSafeSortedList<long, IScsServiceClient>();
        }

        /// <summary>
        /// Starts service application.
        /// 
        /// </summary>
        public void Start()
        {
            this._scsServer.Start();
        }

        /// <summary>
        /// Stops service application.
        /// 
        /// </summary>
        public void Stop()
        {
            this._scsServer.Stop();
        }

        /// <summary>
        /// Adds a service object to this service application.
        ///             Only single service object can be added for a service interface type.
        /// 
        /// </summary>
        /// <typeparam name="TServiceInterface">Service interface type</typeparam><typeparam name="TServiceClass">Service class type. Must be delivered from ScsService and must implement TServiceInterface.</typeparam><param name="service">An instance of TServiceClass.</param><exception cref="T:System.ArgumentNullException">Throws ArgumentNullException if service argument is null</exception><exception cref="T:System.Exception">Throws Exception if service is already added before</exception>
        public void AddService<TServiceInterface, TServiceClass>(TServiceClass service)
            where TServiceInterface : class
            where TServiceClass : ScsService, TServiceInterface
        {
            if ((object)service == null)
                throw new ArgumentNullException("service");
            Type serviceInterfaceType = typeof(TServiceInterface);
            if (this._serviceObjects[serviceInterfaceType.Name] != null)
                throw new Exception("Service '" + serviceInterfaceType.Name + "' is already added before.");
            this._serviceObjects[serviceInterfaceType.Name] = new ScsServiceApplication.ServiceObject(serviceInterfaceType, (ScsService)service);
        }

        /// <summary>
        /// Removes a previously added service object from this service application.
        ///             It removes object according to interface type.
        /// 
        /// </summary>
        /// <typeparam name="TServiceInterface">Service interface type</typeparam>
        /// <returns>
        /// True: removed. False: no service object with this interface
        /// </returns>
        public bool RemoveService<TServiceInterface>() where TServiceInterface : class
        {
            return this._serviceObjects.Remove(typeof(TServiceInterface).Name);
        }

        /// <summary>
        /// Handles ClientConnected event of _scsServer object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void ScsServer_ClientConnected(object sender, ServerClientEventArgs e)
        {
            RequestReplyMessenger<IScsServerClient> requestReplyMessenger = new RequestReplyMessenger<IScsServerClient>(e.Client);
            requestReplyMessenger.MessageReceived += new EventHandler<MessageEventArgs>(this.Client_MessageReceived);
            requestReplyMessenger.Start();
            IScsServiceClient serviceClient = ScsServiceClientFactory.CreateServiceClient(e.Client, requestReplyMessenger);
            this._serviceClients[serviceClient.ClientId] = serviceClient;
            this.OnClientConnected(serviceClient);
        }

        /// <summary>
        /// Handles ClientDisconnected event of _scsServer object.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void ScsServer_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            IScsServiceClient client = this._serviceClients[e.Client.ClientId];
            if (client == null)
                return;
            this._serviceClients.Remove(e.Client.ClientId);
            this.OnClientDisconnected(client);
        }

        /// <summary>
        /// Handles MessageReceived events of all clients, evaluates each message,
        ///             finds appropriate service object and invokes appropriate method.
        /// 
        /// </summary>
        /// <param name="sender">Source of event</param><param name="e">Event arguments</param>
        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            RequestReplyMessenger<IScsServerClient> requestReplyMessenger = (RequestReplyMessenger<IScsServerClient>)sender;
            ScsRemoteInvokeMessage remoteInvokeMessage = e.Message as ScsRemoteInvokeMessage;
            if (remoteInvokeMessage == null)
                return;
            try
            {
                IScsServiceClient scsServiceClient = this._serviceClients[requestReplyMessenger.Messenger.ClientId];
                if (scsServiceClient == null)
                {
                    requestReplyMessenger.Messenger.Disconnect();
                }
                else
                {
                    ScsServiceApplication.ServiceObject serviceObject = this._serviceObjects[remoteInvokeMessage.ServiceClassName];
                    if (serviceObject == null)
                    {
                        ScsServiceApplication.SendInvokeResponse((IMessenger)requestReplyMessenger, (IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException("There is no service with name '" + remoteInvokeMessage.ServiceClassName + "'"));
                    }
                    else
                    {
                        try
                        {
                            serviceObject.Service.CurrentClient = scsServiceClient;
                            object returnValue;
                            try
                            {
                                returnValue = serviceObject.InvokeMethod(remoteInvokeMessage.MethodName, remoteInvokeMessage.Parameters);
                            }
                            finally
                            {
                                serviceObject.Service.CurrentClient = (IScsServiceClient)null;
                            }
                            ScsServiceApplication.SendInvokeResponse((IMessenger)requestReplyMessenger, (IScsMessage)remoteInvokeMessage, returnValue, (ScsRemoteException)null);
                        }
                        catch (TargetInvocationException ex)
                        {
                            Exception innerException = ex.InnerException;
                            ScsServiceApplication.SendInvokeResponse((IMessenger)requestReplyMessenger, (IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException(innerException.Message + Environment.NewLine + "Service Version: " + serviceObject.ServiceAttribute.Version, innerException));
                        }
                        catch (Exception ex)
                        {
                            ScsServiceApplication.SendInvokeResponse((IMessenger)requestReplyMessenger, (IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException(ex.Message + Environment.NewLine + "Service Version: " + serviceObject.ServiceAttribute.Version, ex));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScsServiceApplication.SendInvokeResponse((IMessenger)requestReplyMessenger, (IScsMessage)remoteInvokeMessage, (object)null, new ScsRemoteException("An error occured during remote service method call.", ex));
            }
        }

        /// <summary>
        /// Sends response to the remote application that invoked a service method.
        /// 
        /// </summary>
        /// <param name="client">Client that sent invoke message</param><param name="requestMessage">Request message</param><param name="returnValue">Return value to send</param><param name="exception">Exception to send</param>
        private static void SendInvokeResponse(IMessenger client, IScsMessage requestMessage, object returnValue, ScsRemoteException exception)
        {
            try
            {
                IMessenger messenger = client;
                ScsRemoteInvokeReturnMessage invokeReturnMessage1 = new ScsRemoteInvokeReturnMessage();
                invokeReturnMessage1.RepliedMessageId = requestMessage.MessageId;
                invokeReturnMessage1.ReturnValue = returnValue;
                invokeReturnMessage1.RemoteException = exception;
                ScsRemoteInvokeReturnMessage invokeReturnMessage2 = invokeReturnMessage1;
                messenger.SendMessage((IScsMessage)invokeReturnMessage2);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Raises ClientConnected event.
        /// 
        /// </summary>
        /// <param name="client"/>
        private void OnClientConnected(IScsServiceClient client)
        {
            EventHandler<ServiceClientEventArgs> eventHandler = this.ClientConnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new ServiceClientEventArgs(client));
        }

        /// <summary>
        /// Raises ClientDisconnected event.
        /// 
        /// </summary>
        /// <param name="client"/>
        private void OnClientDisconnected(IScsServiceClient client)
        {
            EventHandler<ServiceClientEventArgs> eventHandler = this.ClientDisconnected;
            if (eventHandler == null)
                return;
            eventHandler((object)this, new ServiceClientEventArgs(client));
        }

        /// <summary>
        /// Represents a user service object.
        ///             It is used to invoke methods on a ScsService object.
        /// 
        /// </summary>
        private sealed class ServiceObject
        {
            /// <summary>
            /// This collection stores a list of all methods of service object.
            ///             Key: Method name
            ///             Value: Informations about method.
            /// 
            /// </summary>
            private readonly SortedList<string, MethodInfo> _methods;

            /// <summary>
            /// The service object that is used to invoke methods on.
            /// 
            /// </summary>
            public ScsService Service { get; private set; }

            /// <summary>
            /// ScsService attribute of Service object's class.
            /// 
            /// </summary>
            public ScsServiceAttribute ServiceAttribute { get; private set; }

            /// <summary>
            /// Creates a new ServiceObject.
            /// 
            /// </summary>
            /// <param name="serviceInterfaceType">Type of service interface</param><param name="service">The service object that is used to invoke methods on</param>
            public ServiceObject(Type serviceInterfaceType, ScsService service)
            {
                this.Service = service;
                object[] customAttributes = serviceInterfaceType.GetCustomAttributes(typeof(ScsServiceAttribute), true);
                if (customAttributes.Length <= 0)
                    throw new Exception("Service interface (" + serviceInterfaceType.Name + ") must has ScsService attribute.");
                this.ServiceAttribute = customAttributes[0] as ScsServiceAttribute;
                this._methods = new SortedList<string, MethodInfo>();
                foreach (MethodInfo methodInfo in serviceInterfaceType.GetMethods())
                    this._methods.Add(methodInfo.Name, methodInfo);
            }

            /// <summary>
            /// Invokes a method of Service object.
            /// 
            /// </summary>
            /// <param name="methodName">Name of the method to invoke</param><param name="parameters">Parameters of method</param>
            /// <returns>
            /// Return value of method
            /// </returns>
            public object InvokeMethod(string methodName, params object[] parameters)
            {
                if (!this._methods.ContainsKey(methodName))
                    throw new Exception("There is not a method with name '" + methodName + "' in service class.");
                else
                    return this._methods[methodName].Invoke((object)this.Service, parameters);
            }
        }
    }
}
