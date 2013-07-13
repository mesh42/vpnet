using VpNet.Abstract;
using VpNet.ManagedApi.System.CommandLine;
using VpNet.PluginFramework.Interfaces;

namespace VpNet.PluginFramework
{
    public class VpPluginContext
    {
        public HotSwapPlugins<BaseInstancePlugin> Plugins { get; set; }
        public CommandLineParser<VpPluginContext> Cmd = new CommandLineParser<VpPluginContext>();
        public IConsole Cli;
        public Instance Vp;
    }
}
