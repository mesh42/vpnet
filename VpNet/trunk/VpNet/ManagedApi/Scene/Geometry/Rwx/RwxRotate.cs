using System;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxRotate", Namespace = Global.XmlNsGeometryRwx)]
    public struct RwxRotate : ICalculate
    {
        public Vector3 Rotate;
        [XmlAttribute]
        public float Angle;
        public void Transform(RwxClump mesh)
        {
            throw new NotImplementedException();
        }
    }
}