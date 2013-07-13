using System.IO;
using VpNet.CommandLine;
using VpNet.CommandLine.Attributes;
using VpNet.Extensions;
using VpNet.PluginFramework;

namespace VpNet.VpConsole.Commands
{
    [Command(Literal="autologin")]
    public class AutoLogin : IParsableCommand<VpPluginContext>
    {
        [BoolFlag(False="disabled", True="enabled")]
        public bool Enabled { get; set; }
        public static string LoginconfigurationXmlPath = @"loginConfiguration.xml";

        public bool Execute(VpPluginContext context)
        {
            if (Enabled)
            {
                context.Vp.Configuration.Serialize(LoginconfigurationXmlPath);
            }
            else
            {
                if (File.Exists(LoginconfigurationXmlPath))
                    File.Delete(LoginconfigurationXmlPath);
            }
            return true;
        }
    }
}
