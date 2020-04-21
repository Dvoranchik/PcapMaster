using System;
using PacketDotNet;
using System.Text;

namespace PcapParser
{
    public class HTTPParser : IProtocol
    {
        
        public
         virtual PPacket Parsing(Packet packet, TimeSpan time, int len)
        {
            var tcpPacket = packet.Extract<TcpPacket>();
            var words = Encoding.UTF8.GetString(tcpPacket.PayloadData).Split('\n');
            var ipPacket = (IPPacket)tcpPacket.ParentPacket;
            System.Net.IPAddress srcIp = ipPacket.SourceAddress;
            System.Net.IPAddress dstIp = ipPacket.DestinationAddress;
            int srcPort = tcpPacket.SourcePort;
            int dstPort = tcpPacket.DestinationPort;

            PPacket protocol = new PPacket("HTTP", Convert.ToString(time.Hours)+":"+Convert.ToString(time.Minutes)+":"+
                Convert.ToString(time.Seconds) + " " + Convert.ToString(time.Milliseconds), Convert.ToString(len),
                Convert.ToString(srcIp), Convert.ToString(dstIp), Convert.ToString(srcPort),
                Convert.ToString(dstPort), Convert.ToString(words[0]));
            
            return protocol;
        }
    }
}
