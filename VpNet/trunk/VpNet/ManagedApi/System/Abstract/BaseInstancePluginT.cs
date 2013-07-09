using VpNet.Interfaces;
using VpNet.PluginFramework;
using VpNet.PluginFramework.Interfaces;

namespace VpNet.Abstract
{

    public abstract class BaseInstancePluginT<TWorld> : IInstancePlugin<TWorld>, IPluginDescription
        where TWorld: class, IWorld, new()
    {
        public abstract void InitializePlugin(BaseInstanceEvents<TWorld> baseInstance);

        public abstract PluginDescription Description {get;}

        public abstract void Unload();
    }

}
