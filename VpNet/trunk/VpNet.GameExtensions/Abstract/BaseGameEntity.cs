using System;
using System.IO;

namespace VpNet.GameExtensions.Abstract
{
    [Serializable]
    public abstract class BaseGameEntity
    {
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