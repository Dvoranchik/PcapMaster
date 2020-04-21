
using SharpPcap;
using SharpPcap.LibPcap;


namespace PcapParser
{
     class Open
    {
        public ICaptureDevice open(string fileName)
        {
            string ver = SharpPcap.Version.VersionString;  
            ICaptureDevice device = null;
            device = new CaptureFileReaderDevice(fileName);
            device.Open();

            return device;
        }
    }
}
