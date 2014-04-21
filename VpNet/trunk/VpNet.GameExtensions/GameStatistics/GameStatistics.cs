using System;
using System.Collections.Generic;
using VpNet.GameExtensions.Abstract;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameStatistics : BaseGameEntity<GameStatistics>
    {
        private readonly string _storageDirectory;
        public IList<GameStatisticsAttribute> Attributes { get; set; }

        public GameStatistics(GameInstance instance, string storageDirectory) : base(instance)
        {
            _storageDirectory = storageDirectory;
            Attributes = new List<GameStatisticsAttribute>();
        }

        public override string StorageDirectory
        {
            get { return _storageDirectory; }
        }

        public override IObservable<GameStatistics> Changed
        {
            get { throw new NotImplementedException(); }
        }
    }
}
