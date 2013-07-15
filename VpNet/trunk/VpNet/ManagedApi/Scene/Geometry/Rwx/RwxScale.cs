using System;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxScale", Namespace = Global.XmlNsGeometryRwx)]
    public struct RwxScale : ICalculate
    {
        public Vector3 Scale;

        public void Transform(RwxClump mesh)
        {
            throw new NotImplementedException();
        }
    }
}