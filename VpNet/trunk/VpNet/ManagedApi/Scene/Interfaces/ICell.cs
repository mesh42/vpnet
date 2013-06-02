using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface ICell
    {
        [XmlAttribute]
        int X { get; set; }

        [XmlAttribute]
        int Z { get; set; }
    }
}