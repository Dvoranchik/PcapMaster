using System.IO;
using PcapOpen.Test;
using NUnit.Framework;
using SharpPcap;
using PacketDotNet;
using System;

namespace PcapParser.Test
{
    [TestFixture]
    public class HTTPTest
    {
        private TcpPacket httpPacket;
        TimeSpan time;
        int len;

        [SetUp]
        public void Init()
        {
            ICaptureDevice device = Fixture.OpenFile("test.pcap");
            var rawCapture = Fixture.ReturnProtocol(device, 53);
            Packet packet = Packet.ParsePacket(rawCapture.LinkLayerType, rawCapture.Data);
            time = Fixture.TimeToTimeSpan(rawCapture);
            len = rawCapture.Data.Length;
            var container = Registor.getInstance().Get();
            httpPacket = packet.Extract<TcpPacket>();
            Controller controller = Controller.getInstance();
        }

        [Test]
        public void ParsingTestTime()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(1), "20:18:33 22");
        }

        [Test]
        public void ParsingTestName()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(0), "HTTP");
        }

        [Test]
        public void ParsingTestInformation()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(7), "GET /lib/jquery.qtip.css HTTP/1.1\r");
        }

        [Test]
        public void ParsingTestTimeLen()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(2), "602");
        }

        [Test]
        public void ParsingTestSourceIp()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(3), "192.168.8.100");
        }

        [Test]
        public void ParsingTestTimeDestinationIp()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(4), "192.168.8.1");
        }

        [Test]
        public void ParsingTestSource()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(5), "49826");
        }

        [Test]
        public void ParsingTestTimeDestination()
        {
            var http = new HTTPParser();
            PPacket pack = http.Parsing(httpPacket, time, len);
            Assert.AreEqual(pack.GetPacket(6), "80");
        }
    }
}
