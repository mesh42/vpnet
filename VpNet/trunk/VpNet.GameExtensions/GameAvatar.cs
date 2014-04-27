using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Xml.Serialization;
using Newtonsoft.Json;
using VpNet.Extensions;

namespace VpNet.GameExtensions
{
    [Serializable]
    public sealed class GameAvatar : VpNet.Abstract.BaseAvatar<Vector3>
    {
        public GamePhysics GamePhysics
        {
            get { return _gamePhysics; }
            set { _gamePhysics = value; }
        }

        public GameFinancials GameFinancials
        {
            get { return _gameFinancials; }
            set { _gameFinancials = value; }
        }

        public GameCharacterAttributes GameCharacterAttributes
        {
            get { return _gameCharacterAttributes; }
            set { _gameCharacterAttributes = value; }
        }

        private bool _isIngame;

        private List<IDisposable> _subscriptions = new List<IDisposable>();
            
        [XmlIgnore]
        [JsonIgnore]
        public bool IsInGame
        {
            get { return _isIngame; }
            set
            {
                if (value == _isIngame)
                    return;
                _isIngame = value;
                if (value)
                {
                    GamePhysics = new GamePhysics();
                    GameFinancials = new GameFinancials();
                    ExtendedData = new List<ExtendedData>();
                    GameCharacterAttributes = new GameCharacterAttributes();

                    // load game settings if they exist.
                    if (File.Exists(Path.Combine(StoragePath, "GameCharacterAttributes.json")))
                    {
                        GameCharacterAttributes = JsonConvert.DeserializeObject<GameCharacterAttributes>(
                            Path.Combine(StoragePath, "GameCharacterAttributes.json").LoadTextFile());
                    }
                    if (File.Exists(Path.Combine(StoragePath, "GamePhysics.json")))
                    {
                        GamePhysics = JsonConvert.DeserializeObject<GamePhysics>(
                            Path.Combine(StoragePath, "GamePhysics.json").LoadTextFile());
                    }
                    if (File.Exists(Path.Combine(StoragePath, "GameFinancials.json")))
                    {
                        GameFinancials = JsonConvert.DeserializeObject<GameFinancials>(
                            Path.Combine(StoragePath, "GameFinancials.json").LoadTextFile());
                    }

                    _subscriptions.Add(GameCharacterAttributes.Changed.Subscribe(p =>
                        JsonConvert.SerializeObject(GameCharacterAttributes)
                            .SaveTextFile(Path.Combine(StoragePath, "GameCharacterAttribtes.json"))));
                    _subscriptions.Add(GameFinancials.Changed.Subscribe(p =>
                        JsonConvert.SerializeObject(GameFinancials)
                            .SaveTextFile(Path.Combine(StoragePath, "GameFinancials.json"))));
                    _subscriptions.Add(GamePhysics.Changed.Subscribe(p =>
                        JsonConvert.SerializeObject(GamePhysics)
                            .SaveTextFile(Path.Combine(StoragePath, "GamePhysics.json"))));

                }
                else
                {
                    _subscriptions.Clear();
                }
                _ingameChanged.OnNext(this);
            }
        }


        
        public List<ExtendedData> ExtendedData { get; set; }

        public GameAvatar()
        {
        }

        private GameAvatarInventory _inventory;

        [NonSerialized] public GameInstance GameInstance;
        [NonSerialized] private GamePhysics _gamePhysics;
        [NonSerialized] private GameFinancials _gameFinancials;
        [NonSerialized] private GameCharacterAttributes _gameCharacterAttributes;

        public string StoragePath
        {
            get { return Path.Combine(GameInstance.GameInstanceConfiguration.StorageDataPath, UserId.ToString()); }
        }

        public GameAvatarInventory Inventory
        {
            get
            {
                if (_inventory != null)
                    return _inventory;
                if (GameInstance == null || !IsInGame)
                    throw new Exception("You can only access a avatars inventory after binding to a game instance and placing the avatar in game using the IsInGame flag.");
                _inventory = new GameAvatarInventory(GameInstance, this);
                return _inventory;
            }
        }

        private readonly Subject<GameAvatar> _ingameChanged = new Subject<GameAvatar>();
        private readonly Subject<GameAvatar> _positionChanged = new Subject<GameAvatar>();
        private readonly Subject<GameAvatar> _rotationChanged = new Subject<GameAvatar>();

        public IObservable<GameAvatar> InGameChanged { get { return _ingameChanged.AsObservable(); } }
        public IObservable<GameAvatar> PositionChanged { get { return _positionChanged.AsObservable(); } }
        public IObservable<GameAvatar> RotationChanged { get { return _rotationChanged.AsObservable(); } }

        public override Vector3 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;
                _positionChanged.OnNext(this);
            }
        }

        public override Vector3 Rotation
        {
            get { return base.Rotation; }
            set
            {
                base.Rotation = value;
                _rotationChanged.OnNext(this);
            }
        }
    }
}
