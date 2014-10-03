// Type: Hik.Communication.Scs.Server.ScsServerManager
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System.Threading;

namespace NetworkApi.Communication.Scs.Server
{
    /// <summary>
    /// Provides some functionality that are used by servers.
    /// 
    /// </summary>
    internal static class ScsServerManager
    {
        /// <summary>
        /// Used to set an auto incremential unique identifier to clients.
        /// 
        /// </summary>
        private static long _lastClientId;

        /// <summary>
        /// Gets an unique number to be used as idenfitier of a client.
        /// 
        /// </summary>
        /// 
        /// <returns/>
        public static long GetClientId()
        {
            return Interlocked.Increment(ref ScsServerManager._lastClientId);
        }
    }
}
