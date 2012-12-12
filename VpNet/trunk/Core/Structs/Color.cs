using System;

namespace VpNet.Core.EventData
{
    public struct Color
    {
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
        
        public static Color FromName(string name)
        {
            return new Color(); ;
        }

        public static Color FromHtml(string hex)
        {
            return new Color();        
        }
    }
}
