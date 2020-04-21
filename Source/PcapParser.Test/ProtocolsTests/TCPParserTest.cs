using NUnit.Framework;
using SharpPcap;
using PacketDotNet;
using System;
using PcapOpen.Test;

namespace PcapParser.Test
{
    [TestFixture]
    public class TCPTest
    {
        private TcpPacket tcpPacket;
        TimeSpan time;
        int len;

        [SetUp]
        public void Init()
        {
            ICaptureDevice device = Fixture.OpenFile("test.pcap");
            var rawCapture = Fixture.ReturnProtocol(device, 1);
            Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            time = Fixture.TimeToTimeSpan(rawCapture);
            len = rawCapture.Data.Length;
            var container = Registor.getInstance().Get();
            tcpPacket = packet.Extract<TcpPacket>();
            Controller controller = Controller.getInstance();
        }

        [Test]
        public void ParsingTestTime()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(1), "20:18:32 170");
        }

        [Test]
        public void ParsingTestName()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(0), "TCP");
        }

        [Test]
        public void ParsingTestInformation()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(7), "-");
        }

        [Test]
        public void ParsingTestTimeLen()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(2), "62");
        }

        [Test]
        public void ParsingTestSourceIp()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(3), "192.168.8.100");
        }

        [Test]
        public void ParsingTestTimeDestinationIp()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(4), "173.194.222.100");
        }

        [Test]
        public void ParsingTestSource()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(5), "49842");
        }

        [Test]
        public void ParsingTestTimeDestination()
        {
            var tcp = new TCPParser();
            PPacket pack = tcp.Parsing(tcpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(6), "443");
        }
    }
}
