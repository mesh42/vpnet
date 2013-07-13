using VpNet.CommandLine;
using VpNet.CommandLine.Attributes;
using VpNet.PluginFramework;

namespace VpNet.VpConsole.Commands
{
    [Command(Literal = "plugin")]
    public class Plugin : IParsableCommand<VpPluginContext>
    {
        [BoolFlag(False = "load", True = "unload",Required= true)]
        public bool IsLoad { get; set; }

        [Literal(Required=true)]
        public string PluginName { get; set; }

        [NamedFlag(Prefix = "/", Literal ="persist",Required= false)]
        public bool Persist { get; set; }


        public bool Execute(VpPluginContext executionContext)
        {
            return false;
        }
    }
}
