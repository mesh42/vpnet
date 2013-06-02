using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    public interface IChatMessage<TColor>
        where TColor : IColor
    {
        [XmlAttribute]
        ChatMessageTypes Type { get; set; }

        [XmlAttribute]
        TColor Color { get; set; }

        [XmlAttribute]
        TextEffectTypes TextEffectTypes { get; set; }

        [XmlAttribute]
        string Message { get; set; }
    }
}