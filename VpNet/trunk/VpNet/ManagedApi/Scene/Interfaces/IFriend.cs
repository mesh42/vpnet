using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface IFriend
    {
        [XmlAttribute]
        int Id { get; set; }
        [XmlAttribute]
        string Name { get; set; }
        [XmlAttribute]
        int UserId { get; set; }
        [XmlAttribute]
        bool Online { get; set; }

    }
}