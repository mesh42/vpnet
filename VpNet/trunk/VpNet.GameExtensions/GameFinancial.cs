using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameFinancials
    {
        [XmlAttribute]
        public Decimal Credits
        {
            get { return _credits; }
            set
            {
                _credits = value;
                _changedSubject.OnNext(this);
            }
        }

        public GameFinancials()

        {
            _credits = new Decimal();
        }

        private readonly Subject<GameFinancials> _changedSubject = new Subject<GameFinancials>();
        private decimal _credits;
        [JsonIgnoreAttribute]
        public IObservable<GameFinancials> Changed { get { return _changedSubject.AsObservable(); } }
    }
}
