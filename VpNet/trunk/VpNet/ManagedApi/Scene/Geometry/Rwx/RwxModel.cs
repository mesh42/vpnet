using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VpNet.Geometry.Rwx
{
    [Serializable]
    [XmlRoot("rwxMmodel", Namespace = Global.XmlNsGeometryRwx)]
    public class RwxModel
    {
        public List<RwxClump> Models;
        public Dictionary<string, RwxClump> Prototypes;
        [XmlAttribute]
        public bool IsDouble = false; // global double material

        // default colors for debugging (each material gets one distinct color):
        // white, red, green, blue, yellow, cyan, magenta
        public int[] DbgColors = new int[] { 0xeeeeee, 0xee0000, 0x00ee00, 0x0000ee, 0xeeee00, 0x00eeee, 0xee00ee };
        private int _i = -1;
        [XmlAttribute]
        public string SourceFile;
        public int GetNextDbgColor()
        {
            _i++;
            if (_i > DbgColors.Length - 1)
                _i = 0;
            return DbgColors[_i];
        }

        public RwxModel()
        {
            Models = new List<RwxClump>();
            Prototypes = new Dictionary<string, RwxClump>();
        }
    }
}