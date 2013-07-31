using System;
using System.Xml.Serialization;
using VpNet.Abstract;

namespace VpNet
{
    public abstract class TimedEventArgs : EventArgs, ITimedEventArgs
    {
        private DateTime _creationDate = DateTime.UtcNow;

        [XmlAttribute]
        public DateTime CreationDateUtc
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }
    }
}