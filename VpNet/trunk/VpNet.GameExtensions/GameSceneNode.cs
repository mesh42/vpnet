namespace VpNet.GameExtensions
{
    public class GameSceneNode
    {
        private readonly GameInstance _gameInstance;
        private GameVpObject _gameVpObject;

        public GameVpObject GameVpObject
        {
            get { return _gameVpObject; }
            internal set
            {
                _gameInstance.GameObjectManager.Add(value);
                _gameVpObject = value;
            }
        }

        public GameSceneNode ParentNode { get; set; }
        private Dictionary<int,GameSceneNode> ChildNodes { get; set; }

        public GameSceneNode(GameInstance gameInstance)
        {
            _gameInstance = gameInstance;
            ChildNodes = new Dictionary<int, GameSceneNode>();
        }

        public GameSceneNode AddChild(GameVpObject gameVpObject)
        {
            var node = new GameSceneNode(_gameInstance)
            {
                GameVpObject = gameVpObject,
                ParentNode = this
            };
            ChildNodes.Add(gameVpObject.Id,new GameSceneNode(_gameInstance));
            return node;
        }
    }
}
