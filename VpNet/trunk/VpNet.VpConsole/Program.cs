#region Copyright notice
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

using System;
using System.IO;
using VpNet.Abstract;
using VpNet.ManagedApi.System.PluginFramework;
using VpNet.NativeApi;
using VpNet.PluginFramework;
using VpNet.Extensions;
using VpNet.PluginFramework.Interfaces;
using VpNet.VpConsole.Commands;
using VpNet.VpConsole.Gui;

namespace VpNet.VpConsole
{
    /// <summary>
    /// VpNet Examples Console Application.
    /// </summary>
    class Program
    {
        static VpPluginContext _context = new VpPluginContext();
        private static Instance Vp;
        static ConsoleHelpers Cli = new ConsoleHelpers();
        private static string _userName;
        private static string _password;
        private static string _world;
       

        /// <summary>
        /// Mains entry point of the VpNet Examples.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            Vp = new Instance();
            _context.Cli = Cli;
            _context.Plugins = new HotSwapPlugins<BaseInstancePlugin>();
            _context.Plugins.OnPluginUnloaded += _plugins_OnPluginUnloaded;
            Console.Title = "Virtual Paradise Console";
            Console.CursorSize = 100;
            Console.SetWindowSize(120,40);
            //Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"
____   ____.__         __               .__    __________                          .___.__               
\   \ /   /|__|_______/  |_ __ _______  |  |   \______   \_____ ____________     __| _/|__| ______ ____  
 \   Y   / |  \_  __ \   __\  |  \__  \ |  |    |     ___/\__  \\_  __ \__  \   / __ | |  |/  ___// __ \ 
  \     /  |  ||  | \/|  | |  |  // __ \|  |__  |    |     / __ \|  | \// __ \_/ /_/ | |  |\___ \\  ___/ 
   \___/   |__||__|   |__| |____/(____  /____/  |____|    (____  /__|  (____  /\____ | |__/____  >\___  >
                                      \/                       \/           \/      \/         \/     \/ 
");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("VP SDK Version: {0}", System.Reflection.Assembly.GetAssembly(typeof(Instance)).GetName().Version.ToString());
            Console.WriteLine("VP Console Version: {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Console.WriteLine("Copyright (c) 2012-2013 CUBE3 (Cit:36) under LGPL license\n");
            Connect();
        }

        /// <summary>
        /// System wide exception handling.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        static void RcDefault_OnVpException(Interfaces.IRc sender, EventArgs args)
        {
            Cli.WriteLine(ConsoleMessageType.Error,sender.Exception.Message);
            switch (sender.Exception.Reason)
            {
                case ReasonCode.NotInUniverse:
                case ReasonCode.ConnectionError:
                    // ignore any other exceptions (system wide)
                    RcDefault.IgnoreExceptions = true;
                    Vp.UseAutoWaitTimer = false;
                    // turn of system wide exception handling.
                    RcDefault.OnVpException -= RcDefault_OnVpException;
                    // unload all active plugins
                    foreach (var plugin in _context.Plugins.ActivePlugins())
                    {
                        _context.Plugins.Deactivate(plugin);
                    }
                    RcDefault.IgnoreExceptions = false;
                    // attempt to reconnect
                    _isLoadedFromConfiguration = false;
                    Vp.Configuration.IsChildInstance = false;
                    Vp.Dispose();
                    Vp = new Instance();
                    Connect();
                    break;
                default:
                    // no further action taken.
                    break;
            }
        }

        static void _plugins_OnPluginUnloaded(HotSwapPlugins<BaseInstancePlugin> sender, PluginUnloadedArguments<BaseInstancePlugin> args)
        {
            args.NewInstance.InitializePlugin(Vp);
            Cli.WriteLine(ConsoleMessageType.Information, string.Format("Plugin {0} reinitialized by dll replacement.", args.NewInstance.Description.Name));
            _context.Plugins.Activate(args.NewInstance);
        }

        private static void Connect()
        {
            Cli.WriteLine(ConsoleMessageType.Information, "Connecting...");
            try
            {
                 Vp.Connect();
                Cli.WriteLine(ConsoleMessageType.Information, "Connected to universe.\n");
                if (File.Exists(AutoLogin.LoginconfigurationXmlPath))
                {
                    Cli.WriteLine(ConsoleMessageType.Information, "Autologin configuration enabled, attempting auto logon.");
                    var config = SerializableExtensions.Deserialize<InstanceConfiguration<World>>(AutoLogin.LoginconfigurationXmlPath);
                    try
                    {
                        Vp.Login(config.UserName, config.Password, config.BotName);
                        Vp.Enter(config.World.Name);
                        _world = config.World.Name;
                        Vp.UpdateAvatar();
                        ProceedAfterLogin(true);

                    }
                    catch (VpException ex)
                    {
                        if (ex.Reason == ReasonCode.NotInUniverse)
                        {
                            // strange native vpsdk bug, after reating a new native instance, upon first time 
                            // the sdk does not seem to connect properly.
                            Connect();
                        }
                        Cli.WriteLine(ConsoleMessageType.Information, "Autologin failed, please login manually.");
                    }
                }
                else
                {
                    Cli.GetPromptTarget = LoginPrompt;
                    Cli.ParseCommandLine = ProcessUserName;
                    Cli.ReadLine();
                }
            }
            catch (VpException ex)
            {
                Cli.WriteLine(ConsoleMessageType.Error, "Can't connect to universe.");
                Cli.GetPromptTarget = RetryPrompt;
                Cli.ParseCommandLine = RetryuniverseConnect;
                Cli.ReadLine();
            }
        }

        private static void ProcessUserName(string userName)
        {
            _userName = userName;
            Cli.GetPromptTarget = PasswordPrompt;
            Cli.ParseCommandLine = ProcessPassword;
            Cli.IsMaskedInput = true;
            Cli.ReadLine();
        }

        private static void ProcessPassword(string password)
        {
            _password = password;
            Cli.IsMaskedInput = false;
            try
            {
                Vp.Login(_userName, password,"vpnetconsole");
            }
            catch (VpException ex)
            {
                Cli.WriteLine(ConsoleMessageType.Error, ex.Message);
                Cli.GetPromptTarget = LoginPrompt;
                Cli.ParseCommandLine = ProcessUserName;
                Cli.ReadLine();
                return;
            }
            ProceedAfterLogin(false);
        }

        static void ProceedAfterLogin(bool enteredWorld)
        {
            Cli.WriteLine(ConsoleMessageType.Information, "Logged into universe server");
            Cli.WriteLine(ConsoleMessageType.Information, "Retrieving world list.\r\n");
            Vp.OnWorldList += Vp_OnWorldList;
            Vp.OnAvatarEnter += Vp_OnAvatarEnter;
            Vp.OnAvatarLeave += Vp_OnAvatarLeave;
            Vp.UseAutoWaitTimer = true;
            Vp.ListWorlds();
            if (enteredWorld)
            {
                Cli.GetPromptTarget = WorldPrompt;
                Cli.ParseCommandLine = ProcessCommand;
                // once logged in enable system wide exception handling.
                RcDefault.OnVpException += RcDefault_OnVpException;
                LoadPlugins();
            }
            else
            {
                Cli.GetPromptTarget = EnterWorldPrompt;
                Cli.ParseCommandLine = ProcessEnterWorld;
            }
            Cli.ReadLine();
        }

        static void Vp_OnAvatarLeave(Instance sender, AvatarLeaveEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            Cli.WriteLine(ConsoleMessageType.Event, "   *** " + args.Avatar.Name + " left.");
        }

        static void Vp_OnAvatarEnter(Instance sender, AvatarEnterEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            Cli.WriteLine(ConsoleMessageType.Event, "   *** " + args.Avatar.Name + " enters.");
        }

        static string EnterWorldPrompt()
        {
            return "[" + DateTime.Now.ToShortTimeString() + " Enter World>: ";
        }

        static void ProcessEnterWorld(string world)
        {
            _world = world;
            try
            {
                Vp.Enter(world);
            }
            catch (VpException ex)
            {
                Cli.WriteLine(ConsoleMessageType.Error, ex.Message);
                Cli.ReadLine();
                return;
            }
            // once logged in enable system wide exception handling.
            RcDefault.OnVpException += RcDefault_OnVpException;

            Vp.UpdateAvatar();
            Cli.GetPromptTarget = WorldPrompt;
            Cli.ParseCommandLine = ProcessCommand;
            LoadPlugins();
            Cli.ReadLine();
        }

        private static  bool _isLoadedFromConfiguration = false;

        static void LoadPlugins()
        {
            if (_isLoadedFromConfiguration)
                return;

            foreach (var item in _context.Plugins.LoadConfiguration(@"pluginConfiguration.xml"))
            {
                var plugin = _context.Plugins.Instances.Find(p => p.Description.Name.ToLower() == item.Name.ToLower());
                if (plugin == null)
                {
                    Cli.WriteLine(ConsoleMessageType.Error, string.Format("Plugin named {0} not found. Can't load.", item.Name));

                }
                else
                {
                    plugin.Console = Cli;
                    plugin.InitializePlugin(Vp);
                    _context.Plugins.Activate(plugin);
                    Cli.WriteLine(ConsoleMessageType.Information, string.Format("Plugin named {0} initialized from configuration.", item.Name));
                }
            }

            _isLoadedFromConfiguration = true;
        }

        static void ProcessCommand(string command)
        {
            // vp instance context is switchable (multiple instances in different worlds), therefore we need to provide it.
            bool isHandled = false;
            _context.Vp = Vp;
            var result = _context.Cmd.Parse(command);
            if (result == null)
            {
                switch (command.ToLower())
                {
                    case "enter":
                        Vp.Leave();
                        Cli.GetPromptTarget = EnterWorldPrompt;
                        Cli.ParseCommandLine = ProcessEnterWorld;
                        isHandled = true;
                        break;
                    case "list plugins":
                        foreach (var plugin in _context.Plugins.Instances)
                        {
                            Cli.WriteLine(ConsoleMessageType.Information,
                                            plugin.Description.Name.PadRight(20) + " : " + plugin.Description.Description);
                        }
                        isHandled = true;
                        break;
                    default:
                        // check if a plugin can handle the command.
                        foreach (var plugin in _context.Plugins.ActivePlugins())
                        {
                            if (plugin.HandleConsoleInput(command))
                            {
                                isHandled = true;
                                break;
                            }
                        }
                        break;

                }
            }
            else
            {
                isHandled = result.Execute(_context);
            }
            if (!isHandled)
                Cli.WriteLine(ConsoleMessageType.Error,"?Unkonwn Syntax Error.");
            Cli.ReadLine();
        }

        static void Vp_OnWorldList(Instance sender, WorldListEventArgs args)
        {
            Cli.WriteLine(ConsoleMessageType.Event,"   -> " + args.World.Name + " (" + args.World.UserCount + " users)");
        }

        private static void RetryuniverseConnect(string yesno)
        {
            if (yesno.ToLower() == "y")
            {
                Connect();
            }
        }

        private static string WorldPrompt()
        {
            return "[" + DateTime.Now.ToShortTimeString() + " " + _world + ">: ";
        }

        private static string Prompt()
        {
             return "[" + DateTime.Now.ToShortTimeString() + ">: ";
        }

        private static string LoginPrompt()
        {
            return "Login: ";
        }

        private static string PasswordPrompt()
        {
            return "Password: ";
        }

        private static string RetryPrompt()
        {
            return "Retry (Y/N): ";
        }
    }
}
