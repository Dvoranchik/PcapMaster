using NUnit.Framework;
using SharpPcap;
using PacketDotNet;
using System;
using PcapOpen.Test;

namespace PcapParser.Test
{
    [TestFixture]
    public class UDPTest
    {
        private Packet udpPacket;
        TimeSpan time;
        int len;
        [SetUp]
        public void Init()
        {
            ICaptureDevice device = Fixture.OpenFile("test.pcap");
            var rawCapture = Fixture.ReturnProtocol(device, 14);
            udpPacket = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            time = Fixture.TimeToTimeSpan(rawCapture);
            len = rawCapture.Data.Length;
            var container = Registor.getInstance().Get();
        }

        [Test]
        public void ParsingTestTime()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(1), "20:18:32 473");
        }

        [Test]
        public void ParsingTestName()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(0), "UDP");
        }

        [Test]
        public void ParsingTestInformation()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(7), "-");
        }

        [Test]
        public void ParsingTestTimeLen()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(2), "62");
        }

        [Test]
        public void ParsingTestSourceIp()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(3), "173.194.73.113");
        }

        [Test]
        public void ParsingTestTimeDestinationIp()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(4), "192.168.8.100");
        }

        [Test]
        public void ParsingTestSource()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(5), "443");
        }

        [Test]
        public void ParsingTestTimeDestination()
        {
            var udp = new UDPParser();
            PPacket pack = udp.Parsing(udpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(6), "59149");
        }
    }
}
