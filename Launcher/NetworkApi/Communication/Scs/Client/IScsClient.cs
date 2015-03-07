// Type: Hik.Communication.Scs.Client.IScsClient
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;
using Tera.NetworkApi.Communication.Scs.Communication.Messengers;

namespace Tera.NetworkApi.Communication.Scs.Client
{
    /// <summary>
    /// Represents a client to connect to server.
    /// 
    /// </summary>
    public interface IScsClient : IMessenger, IConnectableClient, IDisposable
    {
    }
}
