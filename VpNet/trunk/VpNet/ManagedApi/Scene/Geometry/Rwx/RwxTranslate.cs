using System;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxTranslate", Namespace = Global.XmlNsGeometryRwx)]
    public struct RwxTranslate : ICalculate
    {
        public Vector3 Translate;

        public void Transform(RwxClump mesh)
        {
            throw new NotImplementedException();
        }
    }
}