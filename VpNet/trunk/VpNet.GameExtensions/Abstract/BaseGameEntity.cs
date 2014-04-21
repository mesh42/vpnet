using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace VpNet.GameExtensions.Abstract
{
    [Serializable]
    public abstract class BaseGameEntity<T>
    {
        abstract public IObservable<T> Changed { get;  }

        [NonSerialized]
        private readonly GameInstance _gameInstance;

        public abstract string StorageDirectory { get; }

        public GameInstance GameInstance
        {
            get { return _gameInstance; }
        }

        protected BaseGameEntity(GameInstance gameInstance)
        {
            _gameInstance = gameInstance;
        }

        public void DeleteStorageDirectoy()
        {
            Directory.Delete(StorageDirectory, true);
        }
    }
}