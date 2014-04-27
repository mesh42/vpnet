namespace VpNet.GameExtensions
{
    public class GameScene
    {
        public GameInstance GameInstance { get; set; }
        public GameSceneNode Root;

        public GameScene(GameInstance gameInstance)
        {
            GameInstance = gameInstance;
            Root = new GameSceneNode(gameInstance);
        }
    }
}
