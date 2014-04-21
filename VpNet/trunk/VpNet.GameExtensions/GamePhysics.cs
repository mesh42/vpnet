using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GamePhysics
    {
        [XmlAttribute]
        public float Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                _changedSubject.OnNext(this);
            }
        }

        private Subject<GamePhysics> _changedSubject = new Subject<GamePhysics>();
        private float _weight;

        public IObservable<GamePhysics> Changed { get { return _changedSubject.AsObservable(); } }
    }
}
