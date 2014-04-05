using System;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameInstanceConfiguration
    {
        [XmlAttribute]
        public string StorageDataPath { get; set; }
        public InstanceConfiguration<World> VpInstanceConfiguration { get; set; }         

        public GameInstanceConfiguration()
        {
            StorageDataPath = ".\\GameData";
        }
    }
}
