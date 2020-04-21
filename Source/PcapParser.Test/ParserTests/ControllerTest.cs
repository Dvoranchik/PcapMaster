using System;
using NUnit.Framework;
using SharpPcap;
using System.IO;

namespace PcapParser.Test
{
    [TestFixture]
    public class ControllerTest
    {
        private static string directory;
        [SetUp]
        public void Init()
        {
            directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Libs\\TestPcapFiles\\test.pcap");
        }

        [Test]
        public void GetInstance()
        {
            Controller controller = Controller.getInstance();
            Controller controllerSec = Controller.getInstance();
            Assert.AreSame(controller, controllerSec);
        }

        [Test]
        public void Constuctor()
        {
            Controller controllerSec = new Controller();
            Controller controller = Controller.getInstance();
            Assert.AreNotSame(controller, controllerSec);
        }

        [Test]
        public void AddandGet()
        {
            Controller controller = Controller.getInstance();
            PPacket arr = new PPacket ("TCP", "00000.1", "73", "169.54.20.232", "192.168.43.240", "443", "50831", "Get error 404");
            controller.Add(arr);
            Assert.AreEqual(arr, controller.GetList()[0]);
        }

        [Test]
        public void Clear()
        {
            Controller controller = Controller.getInstance();
            PPacket arr = new PPacket("Http", "00000.1", "73", "169.54.20.232", "192.168.43.240", "443", "50831", "Get error 404");
            PPacket arr1 = new PPacket("Ethernet", "00000.1", "73", "169.54.20.232", "192.168.43.240", "443", "50831", "Get error 404");
            PPacket arr2 = new PPacket("Http", "00000.1", "73", "169.54.20.232", "192.168.43.240", "443", "50831", "Get error 404");
            controller.Add(arr);
            controller.Add(arr1);
            controller.Add(arr2);
            controller.Clear();
            Assert.AreEqual(0, controller.GetList().Count);
        }

        [Test]
        public void Start()
        {
            int number = 100;
            Controller controller = Controller.getInstance();
            Assert.AreSame(controller.Start(directory, number), controller.GetDevice());
        }

        [Test]
        public void Load()
        {
            int number = 2;
            Controller controller = Controller.getInstance();
            
            controller.Start(directory, number);
            Assert.IsInstanceOf(typeof(ICaptureDevice), controller.Load());
            controller.Clear();
            controller.Close();
        }

    }
}
