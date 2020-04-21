using System;
using PacketDotNet;

namespace PcapParser
{
    public interface IProtocol
    {
        PPacket Parsing(Packet packet, TimeSpan time, int len);
    }
}
