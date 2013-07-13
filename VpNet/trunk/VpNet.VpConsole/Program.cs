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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VpNet.Abstract;
using VpNet.ManagedApi.System.CommandLine;
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
        static readonly Instance Vp = new Instance();
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
            Console.WriteLine("VPNET Console, copyright (c) 2012-2013 CUBE3 (Cit:36)\n");
            Connect();
        }

        static void _plugins_OnPluginUnloaded(HotSwapPlugins<BaseInstancePlugin> sender, PluginUnloadedArguments<BaseInstancePlugin> args)
        {
            args.NewInstance.InitializePlugin(Vp);
            //args.NewInstance.Vp._avatars = Vp._avatars.Copy();
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
            Vp.UpdateAvatar();
            Cli.GetPromptTarget = WorldPrompt;
            Cli.ParseCommandLine = ProcessCommand;
            Cli.ReadLine();
        }

        static void ProcessCommand(string command)
        {
            // convert string[] args to simulate console input type style
            var args = (from object match in Regex.Matches(command, @"([^\s]*""[^""]+""[^\s]*)|\w+") select match.ToString()).ToArray();
            var result = _context.Cmd.Parse(command);
            if (result == null)
            {
                switch (command.ToLower())
                {
                    case "enter":
                        Vp.Leave();
                        Cli.GetPromptTarget = EnterWorldPrompt;
                        Cli.ParseCommandLine = ProcessEnterWorld;
                        break;
                    case "list plugins":
                        foreach (var plugin in _context.Plugins.Instances)
                        {
                            Cli.WriteLine(ConsoleMessageType.Information,
                                            plugin.Description.Name + "\t\t : " + plugin.Description.Description);
                        }
                        break;
                    case "load plugin":
                        _context.Plugins.Instances[0].Console = Cli;
                        _context.Plugins.Instances[0].InitializePlugin(Vp);
                        _context.Plugins.Activate(_context.Plugins.Instances[0]);
                        break;
                    default:
                        // check if
                        foreach (var plugin in _context.Plugins.ActivePlugins())
                        {
                            if (plugin.HandleConsoleInput(command))
                                break;
                        }
                        break;

                }
            }
            else
            {
                result.Execute(_context);
            }
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
