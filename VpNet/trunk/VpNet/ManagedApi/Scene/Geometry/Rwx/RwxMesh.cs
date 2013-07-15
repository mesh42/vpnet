using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxClump", Namespace = Global.XmlNsGeometryRwx)]
    public class RwxClump
    {
        public RwxMaterial Material = new RwxMaterial();
        public List<float> Vertices = new List<float>();
        public List<float> Uvs = new List<float>();
        public List<int> Indices = new List<int>();
        [XmlAttribute]
        public string Name = string.Empty;

        [XmlIgnore]
        public List<ICalculate> Transforms = new List<ICalculate>();

        public void AddScale(Vector3 scale)
        {
            Transforms.Add(new RwxScale() { Scale = scale });
        }

        public void AddTransform(Vector3 rotation, float angle)
        {
            Transforms.Add(new RwxRotate() { Angle = angle, Rotate = rotation });
        }

        public void AddTransform(Vector3 translation)
        {
            Transforms.Add(new RwxTranslate() { Translate = translation });
        }

        public RwxClump() { }

    }
}