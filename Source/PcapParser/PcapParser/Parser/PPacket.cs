
namespace PcapParser
{
    public class PPacket
    {
        
        string Name, Time, Lenght, SourseIp, Destination,
            SourcePort, DestinationPort, Information;
        public string GetPacket(int i)
        {
            switch (i)
            {
                case 0:
                    return Name;
                case 1:
                    return Time;
                case 2:
                    return Lenght;
                case 3:
                    return SourseIp;
                case 4:
                    return Destination;
                case 5:
                    return SourcePort;
                case 6:
                    return DestinationPort;
                case 7:
                    return Information;
            }
            return "";
        }

        public PPacket(string name, string time, string lenght,
          string sourceIp, string destination, string sourcePort,
          string destinationPort, string information) {
                Name = name;
                Time = time;
                Lenght = lenght;
                SourseIp = sourceIp;
                Destination = destination;
                SourcePort = sourcePort;
                DestinationPort = destinationPort;
                Information = information;
            }
    }
}