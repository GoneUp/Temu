// Type: Hik.Communication.ScsServices.Client.IScsServiceClient`1
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Client;

namespace Tera.NetworkApi.Communication.ScsServices.Client
{
    /// <summary>
    /// Represents a service client that consumes a SCS service.
    /// 
    /// </summary>
    /// <typeparam name="T">Type of service interface</typeparam>
    public interface IScsServiceClient<out T> : IConnectableClient, IDisposable where T : class
    {
        /// <summary>
        /// Reference to the service proxy to invoke remote service methods.
        /// 
        /// </summary>
        T ServiceProxy { get; }

        /// <summary>
        /// Timeout value when invoking a service method.
        ///             If timeout occurs before end of remote method call, an exception is thrown.
        ///             Use -1 for no timeout (wait indefinite).
        ///             Default value: 60000 (1 minute).
        /// 
        /// </summary>
        int Timeout { get; set; }
    }
}
