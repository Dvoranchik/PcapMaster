using System;
using SharpPcap;
using System.IO;
using PcapParser;

namespace PcapOpen.Test
{
    class Fixture
    {
        static public TimeSpan TimeToTimeSpan(SharpPcap.RawCapture rawCapture)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return rawCapture.Timeval.Date - origin;
        }

        static public ICaptureDevice OpenFile(string str)
        {
            return new Open().open(Path.Combine(AppDomain.
                CurrentDomain.BaseDirectory, Convert.ToString("..\\..\\..\\Libs\\TestPcapFiles\\" + str)));
        }

        static public SharpPcap.RawCapture ReturnProtocol(ICaptureDevice device, int number)
        {
            var rawCapture = device.GetNextPacket();
            for (int i = 0; i < number; i++)
            {
                rawCapture = device.GetNextPacket();
            }
            return rawCapture;
        }
    }
}
