using VpNet.Interfaces;

namespace VpNet.Abstract
{

    public abstract class BaseInstancePluginT<TWorld> : IInstancePlugin<TWorld>
        where TWorld: class, IWorld, new()
    {
        public abstract void InitializePlugin(BaseInstanceEvents<TWorld> baseInstance);
    }

}
