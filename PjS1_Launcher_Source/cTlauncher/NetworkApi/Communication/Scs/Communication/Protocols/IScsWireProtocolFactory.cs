// Type: Hik.Communication.Scs.Communication.Protocols.IScsWireProtocolFactory
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

namespace Hik.Communication.Scs.Communication.Protocols
{
    /// <summary>
    /// Defines a Wire Protocol Factory class that is used to create Wire Protocol objects.
    /// 
    /// </summary>
    public interface IScsWireProtocolFactory
    {
        /// <summary>
        /// Creates a new Wire Protocol object.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Newly created wire protocol object
        /// </returns>
        IScsWireProtocol CreateWireProtocol();
    }
}
