// Type: Hik.Communication.Scs.Communication.Protocols.BinarySerialization.BinarySerializationProtocolFactory
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Communication.Protocols;

namespace Hik.Communication.Scs.Communication.Protocols.BinarySerialization
{
    /// <summary>
    /// This class is used to create Binary Serialization Protocol objects.
    /// 
    /// </summary>
    public class BinarySerializationProtocolFactory : IScsWireProtocolFactory
    {
        /// <summary>
        /// Creates a new Wire Protocol object.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Newly created wire protocol object
        /// </returns>
        public IScsWireProtocol CreateWireProtocol()
        {
            return (IScsWireProtocol)new BinarySerializationProtocol();
        }
    }
}
