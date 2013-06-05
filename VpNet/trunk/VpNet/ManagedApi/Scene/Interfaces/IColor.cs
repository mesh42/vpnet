using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Color Specification
    /// </summary>
    public interface IColor
    {
        /// <summary>
        /// Gets or sets the Red component of RGB.
        /// </summary>
        /// <value>
        /// Level of Red (0-255_
        /// </value>
        [XmlAttribute]
        byte R { get; set; }

        /// <summary>
        /// Gets or sets the Green component of RGB.
        /// </summary>
        /// <value>
        /// Level of Green (0-255)
        /// </value>
        [XmlAttribute]
        byte G { get; set; }

        /// <summary>
        /// Gets or sets the Blue component of RGB.
        /// </summary>
        /// <value>
        /// Level of Blue (0-255)
        /// </value>
        [XmlAttribute]
        byte B { get; set; }
    }
}