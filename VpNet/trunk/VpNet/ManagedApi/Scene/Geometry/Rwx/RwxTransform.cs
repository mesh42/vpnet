using System;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxTransform", Namespace = Global.XmlNsGeometryRwx)]
    public struct RwxTransform : ICalculate
    {
        public Matrix Matrix;

        public void Transform(RwxClump mesh)
        {
            throw new NotImplementedException();
        }
    }
}