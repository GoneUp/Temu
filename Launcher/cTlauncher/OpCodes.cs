using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    [Flags]
    public enum OpCode
    {
        LoginPacket = 0x1001,
        RegisterPacket = 0x2001,
    }

}
