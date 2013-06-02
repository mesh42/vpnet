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

using System;

namespace VpNet.Examples
{
    public class EventsExample
    {
                /// <summary>
        /// Initializes a new instance of the <see cref="HelloWorldExample"/> class.
        /// The events example shows how to receive events from the world and universe server.
        /// </summary>
        /// <param name="user">The user name.</param>
        /// <param name="password">The password.</param>
        /// <param name="botname">The name of the bot.</param>
        /// <param name="world">The world to world to enter.</param>
        public EventsExample(string user, string password, string botname, string world)
        {
            var vp = new Instance();
            vp.Connect();
            vp.Login(user,password,botname);
            vp.Enter(world);
            // announce your avatar so it can receive avatar events.
            vp.UpdateAvatar();
            // Register to VP events
            vp.OnObjectChange += vp_OnObjectChange;

            Console.WriteLine("Press any key to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    vp.Wait();
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
                    

        }

        void vp_OnObjectChange(Instance sender, ObjectChangeArgsT<Avatar<Vector3>, VpObject<Vector3>, Vector3> args)
        {
            Console.WriteLine("object {0} changed by {1}", args.VpObject.Id, args.Avatar.Name);
        }
    }
}
