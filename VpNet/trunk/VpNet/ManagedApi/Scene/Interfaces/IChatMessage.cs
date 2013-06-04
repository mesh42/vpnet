using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Chat Message templated interface specifications.
    /// </summary>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    public interface IChatMessage<TColor>
        where TColor : IColor
    {
        [XmlAttribute]
        ChatMessageTypes Type { get; set; }

        TColor Color { get; set; }

        [XmlAttribute]
        TextEffectTypes TextEffectTypes { get; set; }

        [XmlAttribute]
        string Message { get; set; }
    }
}