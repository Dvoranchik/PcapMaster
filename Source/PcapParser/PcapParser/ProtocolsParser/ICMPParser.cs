using System;
using PacketDotNet;

namespace PcapParser
{
     public class ICMPParser : IProtocol
    {
        public
         virtual PPacket Parsing(Packet packet, TimeSpan time, int len)
        {
            var icmpPacket = packet.Extract<IPv4Packet>();
            if (icmpPacket == null)
            {
                var icmpPacketv6 = packet.Extract<IPv6Packet>();
                System.Net.IPAddress srcIp = icmpPacketv6.SourceAddress;
                System.Net.IPAddress dstIp = icmpPacketv6.DestinationAddress;
                PPacket protocol = new PPacket("ICMPv6", Convert.ToString(time.Hours) + ":" + Convert.ToString(time.Minutes) + ":" +
                Convert.ToString(time.Seconds) + " " + Convert.ToString(time.Milliseconds), Convert.ToString(len),
                Convert.ToString(srcIp), Convert.ToString(dstIp), "-", "-", "-");
                return protocol;
            }
            else
            {
                System.Net.IPAddress srcIp = icmpPacket.SourceAddress;
                System.Net.IPAddress dstIp = icmpPacket.DestinationAddress;
                PPacket protocol = new PPacket("ICMPv4", Convert.ToString(time.Hours) + ":" + Convert.ToString(time.Minutes) + ":" +
                Convert.ToString(time.Seconds) + " " + Convert.ToString(time.Milliseconds), Convert.ToString(len),
                Convert.ToString(srcIp), Convert.ToString(dstIp), "-", "-", "-");
                return protocol;
            }
            
        }
    }
}
