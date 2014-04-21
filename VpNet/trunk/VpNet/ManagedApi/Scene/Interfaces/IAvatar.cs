using System;
using System.Xml.Serialization;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Avatar templated interface specifications.
    /// </summary>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    public interface IAvatar<TVector3>
        where TVector3 : struct, IVector3
    {
        [XmlAttribute]
        DateTime LastChanged { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        [XmlAttribute]
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        [XmlIgnore]
        int Session { get; set; }

        [XmlAttribute]
        int AvatarType { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        TVector3 Position { get; set; }
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation im Pitch, Yaw, Roll (Roll is currently not supported in Vp)
        /// </value>
        TVector3 Rotation { get; set; }

        bool IsBot { get; }
    }
}