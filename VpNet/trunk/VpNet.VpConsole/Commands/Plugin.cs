﻿#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2013 CUBE3 (Cit:36)

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using VpNet.CommandLine;
using VpNet.CommandLine.Attributes;
using VpNet.PluginFramework;
using VpNet.PluginFramework.Interfaces;

namespace VpNet.VpConsole.Commands
{
    [Command(Literal = "plugin")]
    public class Plugin : IParsableCommand<VpPluginContext>
    {
        [BoolFlag(True = "load", False = "unload",Required= true,ArgumentIndex=1)]
        public bool IsLoad { get; set; }

        [Literal(Required=true,ArgumentIndex=2)]
        public string PluginName { get; set; }

        [NamedFlag(Prefix = "", Literal ="persist",Required= false,ArgumentIndex=3)]
        public bool Persist { get; set; }

        public bool Execute(VpPluginContext ctx)
        {
            if (IsLoad && ctx.Plugins.ActivePlugins().Find(p => p.Description.Name.ToLower() == PluginName.ToLower())!=null)
            {
                ctx.Cli.WriteLine(ConsoleMessageType.Error,string.Format("Plugin {0} already loaded.",PluginName));
                return true;
            } 
            if (IsLoad)
            {
                var plugin = ctx.Plugins.Instances.Find(p => p.Description.Name.ToLower() == PluginName.ToLower());
                if (plugin == null)
                {
                    ctx.Cli.WriteLine(ConsoleMessageType.Error, string.Format("Plugin named {0} not found.", PluginName));
                    return true;
                }
                plugin.Console = ctx.Cli;
                plugin.InitializePlugin(ctx.Vp);
                ctx.Plugins.Activate(plugin);
                ctx.Cli.WriteLine(ConsoleMessageType.Information, string.Format("Plugin {0} initialized.", PluginName));
                return true;
            }
            if (!IsLoad && ctx.Plugins.ActivePlugins().Find(p => p.Description.Name.ToLower() == PluginName.ToLower()) == null)
            {
                ctx.Cli.WriteLine(ConsoleMessageType.Error, string.Format("Plugin {0} is not loaded.", PluginName));
                return true;
            }
            if (!IsLoad)
            {
                var plugin = ctx.Plugins.Instances.Find(p => p.Description.Name.ToLower() == PluginName.ToLower());
                if (plugin == null)
                {
                    ctx.Cli.WriteLine(ConsoleMessageType.Error, string.Format("Plugin named {0} not found.", PluginName));
                    return true;
                }
                ctx.Plugins.Deactivate(plugin);
                ctx.Cli.WriteLine(ConsoleMessageType.Information, string.Format("Plugin {0} unloaded.", PluginName));
                return true;
            }
            return true;
        }
    }
}