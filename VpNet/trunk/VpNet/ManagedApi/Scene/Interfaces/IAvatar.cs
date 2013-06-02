using System.Xml;
using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface IAvatar<TVector3>
        where TVector3 : IVector3, new()
    {
        [XmlAttribute]
        int UserId { get; set; }

        [XmlAttribute]
        string Name { get; set; }

        [XmlIgnore]
        int Session { get; set; }

        [XmlAttribute]
        int AvatarType { get; set; }

        TVector3 Position { get; set; }
        TVector3 Rotation { get; set; }
    }
}