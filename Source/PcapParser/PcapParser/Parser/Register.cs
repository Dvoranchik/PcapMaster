using LightInject;

namespace PcapParser
{
    public class Registor
    {
        ServiceContainer container;
        private static Registor instance;
        public Registor()
        {
            container = new LightInject.ServiceContainer();
            container.Register<IProtocol, TCPParser>("TCP");
            container.Register<IProtocol, EthernetParser>("Ethernet");
            container.Register<IProtocol, UDPParser>("UDP");
            container.Register<IProtocol, ICMPParser>("ICMP");
            container.Register<IProtocol, HTTPParser>("HTTP");
        }
        public static Registor getInstance()
        {
            if (instance == null)
                instance = new Registor();
            return instance;
        }
        public ServiceContainer Get()
        {
            return container;
        }
    }
}
