using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace VpNet
{
    public struct TimeSpan : IXmlSerializable, IComparable
    {
        private global::System.TimeSpan _value;

        public static implicit operator TimeSpan(global::System.TimeSpan value)
        {
            return new TimeSpan { _value = value };
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public TimeSpan(int days, int hours, int minutes, int seconds)
        {
            _value = new System.TimeSpan(days, hours, minutes, seconds);
        }

        public static implicit operator global::System.TimeSpan(TimeSpan value)
        {
            return value._value;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            _value = global::System.TimeSpan.Parse(reader.ReadContentAsString());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteValue(_value.ToString());
        }

        public int CompareTo(object other)
        {
            return _value.CompareTo(((TimeSpan)other)._value);
        }
    }
}