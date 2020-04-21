using System.IO;
using NUnit.Framework;
using SharpPcap;
using PacketDotNet;
using System;
using PcapOpen.Test;

namespace PcapParser.Test
{
    [TestFixture]
    public class ICMPTest
    {
        private Packet icmpPacket;
        TimeSpan time;
        int len;

        [SetUp]
        public void Init()
        {
            ICaptureDevice device = Fixture.OpenFile("test.pcap");
            var rawCapture = Fixture.ReturnProtocol(device, 33);
            icmpPacket = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            time = Fixture.TimeToTimeSpan(rawCapture);
            len = rawCapture.Data.Length;
            var container = Registor.getInstance().Get();
        }

        [Test]
        public void ParsingTestTime()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(1), "20:18:32 660");
        }

        [Test]
        public void ParsingTestName()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(0), "ICMPv6");
        }

        [Test]
        public void ParsingTestInformation()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(7), "-");
        }

        [Test]
        public void ParsingTestTimeLen()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(2), "86");
        }

        [Test]
        public void ParsingTestSourceIp()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(3), "fe80::d1ec:8061:eeaf:3c78");
        }

        [Test]
        public void ParsingTestTimeDestinationIp()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(4), "fe80::76c1:4fff:fefe:1b89");
        }

        [Test]
        public void ParsingTestSource()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(5), "-");
        }

        [Test]
        public void ParsingTestTimeDestination()
        {
            var icmp = new ICMPParser();
            PPacket pack = icmp.Parsing(icmpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(6), "-");
        }
    }
}
