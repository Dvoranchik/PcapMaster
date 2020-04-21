using System;
using PacketDotNet;

namespace PcapParser
{
    public class UDPParser : IProtocol
    {
        public
         virtual PPacket Parsing(Packet packet, TimeSpan time, int len)
        {
            System.Net.IPAddress srcIp, dstIp = null;
            var adpPacket = packet.Extract<UdpPacket>();
            var ether = packet.Extract<EthernetPacket>();
            var icmpPacket = packet.Extract<IPv4Packet>();
            if (icmpPacket != null)
            {
                srcIp = icmpPacket.SourceAddress;
                dstIp = icmpPacket.DestinationAddress;
            }
            else
            {
                var icmpPacketv6 = packet.Extract<IPv6Packet>();
                srcIp = icmpPacketv6.SourceAddress;
                dstIp = icmpPacketv6.DestinationAddress;
            }
            int srcPort = adpPacket.SourcePort;
            int dstPort = adpPacket.DestinationPort;

            PPacket protocol = new PPacket ("UDP", Convert.ToString(time.Hours)+":"+Convert.ToString(time.Minutes)+":"+
                Convert.ToString(time.Seconds) + " " + Convert.ToString(time.Milliseconds), Convert.ToString(len),
                 Convert.ToString(srcIp), Convert.ToString(dstIp), Convert.ToString(srcPort),
                 Convert.ToString(dstPort), "-");
            return protocol;
        }
    }
}
