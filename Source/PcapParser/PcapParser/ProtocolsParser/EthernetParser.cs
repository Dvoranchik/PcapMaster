using System;
using PacketDotNet;

namespace PcapParser
{
    public class EthernetParser : IProtocol
    {
        public
        virtual PPacket Parsing(Packet packet, TimeSpan time, int len)
        {
            var ethernetPacket = packet.Extract<PacketDotNet.EthernetPacket>();
            var arpPacket = packet.Extract<PacketDotNet.ArpPacket>();
            System.Net.IPAddress srcId = null, dstId = null;
            System.Net.NetworkInformation.PhysicalAddress srcPort = ethernetPacket.SourceHardwareAddress;
            System.Net.NetworkInformation.PhysicalAddress dstPort = ethernetPacket.DestinationHardwareAddress;
            if (arpPacket != null)
            {
                dstId = arpPacket.TargetProtocolAddress;
                srcId = arpPacket.SenderProtocolAddress;
            }
            PPacket protocol = new PPacket("Ethernet", Convert.ToString(time.Hours) + ":" + Convert.ToString(time.Minutes)+":"+
                Convert.ToString(time.Seconds) + " " + Convert.ToString(time.Milliseconds), Convert.ToString(len),
                Convert.ToString(srcId), Convert.ToString(dstId), Convert.ToString(srcPort),
                Convert.ToString(dstPort), "-");
            return protocol;
        }
    }
}
