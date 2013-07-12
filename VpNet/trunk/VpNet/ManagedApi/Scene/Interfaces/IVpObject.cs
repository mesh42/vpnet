using System;
using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// VpObject templated interface specifications.
    /// </summary>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [XmlRoot("vpObject", Namespace = Global.XmlNsScene)]
    public interface IVpObject<TVector3>
        where TVector3 : struct, IVector3
    {
        [XmlAttribute]
        int Id { get; set; }

        [XmlAttribute]
        int Type { get; set; }

        [XmlAttribute]
        DateTime Time { get; set; }

        [XmlAttribute]
        int Owner { get; set; }

        TVector3 Position { get; set; }
        TVector3 Rotation { get; set; }

        [XmlAttribute]
        float Angle { get; set; }

        [XmlAttribute]
        string Action { get; set; }

        [XmlAttribute]
        string Description { get; set; }

        [XmlAttribute]
        int ObjectType { get; set; }

        [XmlAttribute]
        string Model { get; set; }

        [XmlIgnore]
        int ReferenceNumber { get; set; }

        [XmlIgnore]
        ICell Cell { get; }
    }
}