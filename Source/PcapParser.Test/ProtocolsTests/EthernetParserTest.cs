using System.IO;
using NUnit.Framework;
using SharpPcap;
using PacketDotNet;
using System;
using PcapOpen.Test;

namespace PcapParser.Test
{
    [TestFixture]
    public class EthernetTest
    {
        private EthernetPacket ethernetPacket;
        TimeSpan time;
        int len;

        [SetUp]
        public void Init()
        {
            ICaptureDevice device = Fixture.OpenFile("test.pcap");
            var rawCapture = Fixture.ReturnProtocol(device, 0);
            Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            time = Fixture.TimeToTimeSpan(rawCapture);
            len = rawCapture.Data.Length;
            var container = Registor.getInstance().Get();
            ethernetPacket = packet.Extract<EthernetPacket>();
            Controller controller = Controller.getInstance();
            
        }

        [Test]
        public void ParsingTestTime()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(1), "20:18:32 160");
        }

        [Test]
        public void ParsingTestName()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(0), "Ethernet");
        }

        [Test]
        public void ParsingTestTimeLen()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(2), "42");
        }

        [Test]
        public void ParsingTestSourceIp()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(3), "192.168.8.100");
        }

        [Test]
        public void ParsingTestTimeDestinationIp()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(4), "169.254.169.254");
        }

        [Test]
        public void ParsingTestSource()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(5), "0C5B8F279A64");
        }

        [Test]
        public void ParsingTestTimeDestination()
        {
            var ethernet = new EthernetParser();
            PPacket pack = ethernet.Parsing(ethernetPacket, time, len);
            Assert.AreEqual(pack.GetPacket(6), "FFFFFFFFFFFF");
        }
    }
}
