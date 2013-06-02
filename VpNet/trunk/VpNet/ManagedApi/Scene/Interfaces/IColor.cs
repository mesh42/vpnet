using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface IColor
    {
        [XmlAttribute]
        byte R { get; }

        [XmlAttribute]
        byte G { get; }

        [XmlAttribute]
        byte B { get; }
    }
}