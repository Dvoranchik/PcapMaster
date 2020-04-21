using System;
using SharpPcap;
using LightInject;
using System.Text;
using PacketDotNet;

namespace PcapParser
{
     public class Parser
    {
        static int numberOfProtocols;
        static DateTime startTime;
        bool isParse = false;

        internal Parser(int num)
        {
            numberOfProtocols = num;
        }

        internal ICaptureDevice LoadProtocols(ICaptureDevice device)
        {
            HandlPacket(device);
            return device;
        }

        internal ICaptureDevice NewFile(string filename)
        {
            ICaptureDevice device = new Open().open(filename);
            return device;
        }

        private ICaptureDevice HandlPacket(ICaptureDevice device)
        {
            RawCapture rawCapture = null;
            Controller control = Controller.getInstance();
            var number = 0;
            while ((number != numberOfProtocols))
            {
                rawCapture = device.GetNextPacket();

                //Count the time
                if (rawCapture == null)
                    break;
                ++number;
                var time = rawCapture.Timeval.Date;
                startTime = (isParse ? startTime : time);
                isParse = true;
                TimeSpan istime = time - startTime;
                var len = rawCapture.Data.Length;

                //Open containter LightInject
                var container = Registor.getInstance().Get();
                Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
                var udpPacket = packet.Extract<UdpPacket>();
                if (udpPacket != null)
                {
                    var udp = container.GetInstance<IProtocol>("UDP");
                    control.Add(udp.Parsing(packet, istime, len));
                    continue;
                }

                var tcpPacket = packet.Extract<TcpPacket>();
                if (tcpPacket != null)
                {
                    if (Encoding.UTF8.GetString(tcpPacket.PayloadData).IndexOf("HTTP/1.1") >= 0 )
                    {
                        var http = container.GetInstance<IProtocol>("HTTP");
                        control.Add(http.Parsing(tcpPacket, istime, len));
                        continue;
                    }
                    var tcp = container.GetInstance<IProtocol>("TCP");
                    control.Add(tcp.Parsing(tcpPacket, istime, len));
                    continue;
                }
                var icmpPacketv6 = packet.Extract<PacketDotNet.IcmpV6Packet>();
                var icmpPacket = packet.Extract<IcmpV4Packet>();
                if (icmpPacket != null || icmpPacketv6 != null)
                {
                    var icmp = container.GetInstance<IProtocol>("ICMP");
                    control.Add(icmp.Parsing(packet, istime, len));
                    continue;
                }

                var ethernetPacket = packet.Extract<EthernetPacket>();
                if (ethernetPacket != null)
                {
                    var ethernet = container.GetInstance<IProtocol>("Ethernet");
                    control.Add(ethernet.Parsing(ethernetPacket, istime, len));
                    continue;
                }
            }
            return device;
        }
    }
}
