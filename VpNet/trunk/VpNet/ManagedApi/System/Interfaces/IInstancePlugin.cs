using VpNet.Abstract;

namespace VpNet.Interfaces
{
    /// <summary>
    /// Interface for instance plugin. instantiates a plugin for a specified world type using this interface for its 
    /// composition.
    /// </summary>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    public interface IInstancePlugin<TWorld>
        where TWorld: class, IWorld,new()
    {
        void InitializePlugin(BaseInstanceEvents<TWorld> baseInstance);
    }
}
