using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameCharacterAttributes
    {
        private double _strenght;
        private double _dexterity;
        private double _intelligence;
        private double _health;

        [XmlAttribute]
        public double Strenght
        {
            get { return _strenght; }
            set
            {
                _strenght = value; 
                _changedSubject.OnNext(this);
            }
        }

        [XmlAttribute]
        public double Dexterity
        {
            get { return _dexterity; }
            set
            {
                _dexterity = value;
                _changedSubject.OnNext(this);

            }
        }

        [XmlAttribute]
        public double Intelligence
        {
            get { return _intelligence; }
            set
            {
                _intelligence = value;
                _changedSubject.OnNext(this);

            }
        }

        [XmlAttribute]
        public double Health
        {
            get { return _health; }
            set
            {
                _health = value;
                _changedSubject.OnNext(this);
            }
        }

        private readonly Subject<GameCharacterAttributes> _changedSubject = new Subject<GameCharacterAttributes>();
        public IObservable<GameCharacterAttributes> Changed { get { return _changedSubject.AsObservable(); } }

    }
}
