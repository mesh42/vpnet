using System;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GamePhysics
    {
        [XmlAttribute]
        public float Weight { get; set; }
    }
}
