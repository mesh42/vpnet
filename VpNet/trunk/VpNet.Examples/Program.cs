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
using VpNet.Abstract;
using VpNet.PluginFramework;
using VpNet.Extensions;
using VpNet.VpConsole.Gui;

namespace VpNet.VpConsole
{
    /// <summary>
    /// VpNet Examples Console Application.
    /// </summary>
    class Program
    {
        static readonly ConsoleHelpers Cli = new ConsoleHelpers();
        static readonly Instance Vp = new Instance();
        private static string _userName;
        private static string _password;
        private static string _world;
        private static HotSwapPlugins<BaseInstancePlugin> _plugins;
       
        /// <summary>
        /// Mains entry point of the VpNet Examples.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            _plugins = new HotSwapPlugins<BaseInstancePlugin>();
            Console.Title = "Virtual Paradise Console";
            Console.CursorSize = 100;
            Console.SetWindowSize(120,40);
            Console.SetBufferSize(120,40);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
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
            Console.WriteLine("VPNET Console, copyright (c) 2012-2013 CUBE3 (Cit:36)\r\n");
            Connect();
        }

        private static void Connect()
        {
            
            Cli.WriteLine(ConsoleMessageType.Information, "Connecting...");
            try
            {
                Vp.Connect();
                Cli.WriteLine(ConsoleMessageType.Information, "Connected to universe.\r\n");
                Cli.GetPromptTarget = LoginPrompt;
                Cli.ParseCommandLine = ProcessUserName;
                Cli.ReadLine();
            }
            catch (VpException ex)
            {
                Cli.WriteLine(ConsoleMessageType.Error, "Can't connect to universe.");
                Cli.GetPromptTarget = RetryPrompt;
                Cli.ParseCommandLine = RetryuniverseConnect;
                Cli.ReadLine();
            }
        }

        private static void ProcessCommandLine(string commandline)
        {
            Cli.ReadLine();
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
            }
            Cli.WriteLine(ConsoleMessageType.Information, "Logged into universe server");
            Cli.WriteLine(ConsoleMessageType.Information, "Retrieving world list.\r\n");
            Vp.OnWorldList += Vp_OnWorldList;
            Vp.OnAvatarEnter += Vp_OnAvatarEnter;
            Vp.UseAutoWaitTimer = true;
            Vp.ListWorlds();
            Cli.GetPromptTarget = EnterWorldPrompt;
            Cli.ParseCommandLine = ProcessEnterWorld;
            Cli.ReadLine();
        }

        static void Vp_OnAvatarEnter(Instance sender, AvatarEnterEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            Cli.WriteLine(ConsoleMessageType.Information,"   -> " + args.Avatar.Name + " enters.");
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
            switch (command)
            {
                case "list plugins":
                    foreach (var plugin in _plugins.Instances)
                    {
                        Cli.WriteLine(ConsoleMessageType.Information, plugin.Description.Name + "\t\t : " + plugin.Description.Description);
                    }
                    break;
                case "load plugin":
                    
                    _plugins.Instances[0].InitializePlugin(Vp);
                    _plugins.Instances[0].Vp._avatars = Vp._avatars.Copy();
                    break;
            }   
            Cli.ReadLine();
        }

        static void Vp_OnWorldList(Instance sender, WorldListEventArgs args)
        {
            Cli.WriteLine(ConsoleMessageType.Information,"   -> " + args.World.Name + " (" + args.World.UserCount + " users)");
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
