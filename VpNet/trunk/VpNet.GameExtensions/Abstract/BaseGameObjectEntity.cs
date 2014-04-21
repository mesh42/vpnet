namespace VpNet.GameExtensions.Abstract
{
    public abstract class BaseGameObjectEntity<T> : BaseGameEntity<T>
    {
        protected BaseGameObjectEntity(GameInstance gameInstance) : base(gameInstance)
        {
        }
    }
}
