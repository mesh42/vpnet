using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface IWorld
    {
        [XmlAttribute]
        string Name { get; set; }
        [XmlAttribute]
        int UserCount { get; set; }
        [XmlAttribute]
        WorldState State { get; set; }
    }
}