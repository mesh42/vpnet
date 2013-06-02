using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface IUniverse
    {
        [XmlAttribute]
        string Host { get; set; }
        [XmlAttribute]
        int Port { get; set; }
    }
}