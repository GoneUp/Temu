using System;

namespace Tera
{
    [Flags]
    public enum OpCode
    {
        LoginPacket = 0x1001,
        RegisterPacket = 0x2001,
    }

}
