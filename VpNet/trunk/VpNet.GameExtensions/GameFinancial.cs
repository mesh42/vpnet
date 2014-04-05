using System;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameFinancials
    {
        [XmlAttribute]
        public Decimal Credits { get; set; }

        public GameFinancials()
        {
            Credits = new Decimal();
        }
    }
}
