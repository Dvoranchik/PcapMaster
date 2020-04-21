using SharpPcap;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PcapOpen.Test")]
[assembly: InternalsVisibleTo("PcapMaster")]

namespace PcapParser
{
    internal class Controller
    {
        List<PPacket> listOfProtocols;
        Parser parser;
        ICaptureDevice device = null;
        private static Controller instance;
        internal Controller()
        {
            listOfProtocols = new List<PPacket>();
            Registor regist = Registor.getInstance();
        }
        public List<PPacket> GetList()
        {
            return listOfProtocols;
        }
        public static Controller getInstance()
        {
            if (instance == null)
                instance = new Controller();
            return instance;
        }
        public void Add(PPacket protocol)
        {
            listOfProtocols.Add(protocol);
        }

        public ICaptureDevice Start(string filename, int count)
        {
            parser = new Parser(count);
            Close();
            device = parser.NewFile(filename);
            return device;
        }

        public ICaptureDevice Load()
        {
            parser.LoadProtocols(device);
            return device;
        }

        public void Clear()
        {
            listOfProtocols.Clear();
        }

        public ICaptureDevice Close()
        {
            if (device != null)
                device.Close();
            return device;
        }

        internal ICaptureDevice GetDevice()
        {
            return device;
        }

        ~Controller()
        {
            Close();
            Clear();
        }
    }
}
