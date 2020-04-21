using System;
using PacketDotNet;
namespace PcapParser
{
    public class TCPParser : IProtocol
    {
        public
         virtual PPacket Parsing(Packet packet, TimeSpan time, int len)
        {
            var tcpPacket = packet.Extract<TcpPacket>();
            var ipPacket = (IPPacket)tcpPacket.ParentPacket;
            System.Net.IPAddress srcIp = ipPacket.SourceAddress;
            System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
            int srcPort = tcpPacket.SourcePort;
            int dstPort = tcpPacket.DestinationPort;

            PPacket protocol = new PPacket( "TCP", Convert.ToString(time.Hours)+":"+Convert.ToString(time.Minutes)+":"+
                Convert.ToString(time.Seconds) + " " + Convert.ToString(time.Milliseconds), Convert.ToString(len),
                Convert.ToString(srcIp), Convert.ToString(dstIp), Convert.ToString(srcPort),
                Convert.ToString(dstPort), "-");
            return protocol;
        }
    }
}
