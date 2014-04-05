using System;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameHudMenu
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Label { get; set; }
        public GameHudMenu[] SubMenu { get; set; }
    }
}
