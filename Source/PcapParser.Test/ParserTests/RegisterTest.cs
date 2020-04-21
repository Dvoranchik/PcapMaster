using System;
using NUnit.Framework;
using PcapParser;
using LightInject;

namespace PcapParser.Test
{
    [TestFixture]
    public class RegisterTest
    {
        [Test]
        public void Constructor()
        {
            Registor regist = Registor.getInstance();
            Assert.IsInstanceOf(typeof(UDPParser), regist.Get().GetInstance<IProtocol>("UDP"));
        }

        [Test]
        public void RegistrationICMP()
        {
            var container = Registor.getInstance().Get();
            var instance = container.GetInstance<IProtocol>("ICMP");
            Assert.IsInstanceOf(typeof(ICMPParser), instance);
            
        }

        [Test]
        public void RegistrationTCP()
        {
            Registor regist = Registor.getInstance();
            var container = regist.Get();
            var instance = container.GetInstance<IProtocol>("TCP");
            Assert.IsInstanceOf( typeof(TCPParser), instance);
        }

        [Test]
        public void RegistrationEthernet()
        {
            Registor regist = Registor.getInstance();
            var container = regist.Get();
            var instance = container.GetInstance<IProtocol>("Ethernet");
            Assert.IsInstanceOf(typeof(EthernetParser), instance);
        }

        [Test]
        public void RegistrationUDP()
        {
            Registor regist = Registor.getInstance();
            var container = regist.Get();
            var instance = container.GetInstance<IProtocol>("UDP");
            Assert.IsInstanceOf( typeof(UDPParser), instance);
        }
       
    }
}
