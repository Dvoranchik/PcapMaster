using NUnit.Framework;
using SharpPcap;
using System;
using System.IO;

namespace PcapParser.Test
{
    [TestFixture]
    public class ParserTest
    {
        private static string directory;
        [SetUp]
        public void Init()
        {
            directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "..\\..\\..\\Libs\\TestPcapFiles\\test.pcap");
        }

        [Test]
        public void LoadandHandlPacket()
        {
            int number = 200;
            Controller controller = Controller.getInstance();
            controller.Start(directory, number);
            controller.Load();
            Assert.AreEqual(controller.GetList().Count, 200);
        }

        [Test]
        public void NewFile()
        {
            int number = 3;
            ICaptureDevice parser = new Parser(number).NewFile(Path.Combine(AppDomain
                .CurrentDomain.BaseDirectory, "..\\..\\..\\Libs\\TestPcapFiles\\test.pcap"));
            Assert.AreEqual(parser.Description, "Capture file reader device");
            
        }
    }
}
