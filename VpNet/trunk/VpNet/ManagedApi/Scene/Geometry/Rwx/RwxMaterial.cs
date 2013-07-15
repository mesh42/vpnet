using System;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxMaterial", Namespace = Global.XmlNsGeometryRwx)]
    public struct RwxMaterial
    {
        [XmlAttribute]
        public float Diffuse;
        [XmlAttribute]
        public float Ambient;
        [XmlAttribute]
        public float Opacity;
        [XmlAttribute]
        public float Specular;
        public Vector3 Color;
        public Vector3 Surface;
        [XmlAttribute]
        public bool IsDouble;
        [XmlAttribute]
        public string Texture;
        [XmlAttribute]
        public bool IsVertexLighting;
        [XmlAttribute]
        public bool IsTextureLit;
        [XmlAttribute]
        public bool IsTextureForeshorten;
        [XmlAttribute]
        public bool IsTexturFilter;
    }
}