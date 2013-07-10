using System;

namespace VpNet.PluginFramework.Interfaces
{
    public interface IPlugin : IDisposable
    {
        PluginDescription Description { get; }
        void Unload();
    }
}