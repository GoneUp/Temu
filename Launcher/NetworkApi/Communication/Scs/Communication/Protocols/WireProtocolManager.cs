// Type: Hik.Communication.Scs.Communication.Protocols.WireProtocolManager
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Tera.NetworkApi.Communication.Scs.Communication.Protocols.BinarySerialization;

namespace Tera.NetworkApi.Communication.Scs.Communication.Protocols
{
    /// <summary>
    /// This class is used to get default protocols.
    /// 
    /// </summary>
    internal static class WireProtocolManager
    {
        /// <summary>
        /// Creates a default wire protocol factory object to be used on communicating of applications.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A new instance of default wire protocol
        /// </returns>
        public static IScsWireProtocolFactory GetDefaultWireProtocolFactory()
        {
            return (IScsWireProtocolFactory)new BinarySerializationProtocolFactory();
        }

        /// <summary>
        /// Creates a default wire protocol object to be used on communicating of applications.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// A new instance of default wire protocol
        /// </returns>
        public static IScsWireProtocol GetDefaultWireProtocol()
        {
            return (IScsWireProtocol)new BinarySerializationProtocol();
        }
    }
}
