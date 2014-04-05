using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VpNet.Abstract;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameVpObject : BaseVpObject<Vector3> 
    {
        public GamePhysics GamePhysics { get; set; }
        public GameFinancials GameFinancials { get; set; }
        [XmlAttribute]
        public bool IsInventoryObject { get; set; }

        public List<ExtendedData> ExtendedData { get; set; }

        public GameVpObject()
        {
            GamePhysics = new GamePhysics();
            GameFinancials = new GameFinancials();
        }
    }



    public class ExtendedData
    {
        [XmlAttribute]
        public string Type { get; set; }
        public string JsonData { get; set; }

        public T GetObject<T>()
        {
            return JsonConvert.DeserializeObject<T>(JsonData);
        }
    }
}
