using System;
using System.Xml.Serialization;

namespace VpNet.GameExtensions
{
    [Serializable]
    public sealed class GameAvatar : VpNet.Abstract.BaseAvatar<Vector3>
    {
        public GamePhysics GamePhysics { get; set; }
        public GameFinancials GameFinancials { get; set; }
        public GameTraits GameTraits { get; set; }
        [XmlAttribute]
        public bool IsInGame { get; set; }

        public GameAvatar()
        {
            GamePhysics = new GamePhysics();
            GameFinancials = new GameFinancials();
            GameTraits = new GameTraits();
        }

        private GameAvatarInventory _inventory;

        //private GameInstance _gameinstance;


        [NonSerialized] public GameInstance GameInstance;

        public GameAvatarInventory Inventory
        {
            get
            {
                if (_inventory != null)
                    return _inventory;
                if (GameInstance == null)
                    throw new Exception("You can only access a avatars inventory after binding to a game instance.");
                _inventory = new GameAvatarInventory(GameInstance, this);
                return _inventory;
            }
        }
    }
}
