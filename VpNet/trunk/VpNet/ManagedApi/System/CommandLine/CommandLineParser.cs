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
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using VpNet.CommandLine;
using VpNet.CommandLine.Attributes;

namespace VpNet.ManagedApi.System.CommandLine
{
    public class CommandLineParser
    {
        private string[] _args;

        public IParsableCommand Parse(string commandLine)
        {
            IParsableCommand cmd = null;
            // convert string[] args to simulate console input type style
            _args = (from object match in Regex.Matches(commandLine, @"([^\s]*""[^""]+""[^\s]*)|\w+") select match.ToString()).ToArray();
            var types = Assembly.GetCallingAssembly().GetTypes().Where(x => x.GetInterface(typeof(IParsableCommand).ToString()) != null);
            foreach (var type in types)
            {
                var b = type.GetCustomAttributes(typeof (CommandAttribute), false);
                if (b.Length == 1)
                {
                    var a = (CommandAttribute) b[0];
                    if (a.Literal == _args[0].ToLower())
                    {
                        // process the command.
                        cmd = (IParsableCommand) Activator.CreateInstance(type);
                        foreach (var prop in type.GetProperties())
                        {
                            var p = prop.GetCustomAttributes(typeof (CommandLineAttribute), true);
                            if (p.Length == 1)
                            {
                                if ((p[0].GetType() == typeof (BoolFlagAttribute)))
                                {
                                    if (_args.Contains(((BoolFlagAttribute) p[0]).True))
                                    {
                                        prop.SetValue(cmd, true, null);
                                    }
                                    else if (_args.Contains(((BoolFlagAttribute) p[0]).False))
                                    {

                                    }
                                    else
                                    {
                                        // exception if this boolean flag was mandatory.
                                    }
                                }

                            }
                        }
                    }
                }
                break;
            }

            return cmd;
        }

        public bool Parse(string[] args)
        {
            _args = args;
            return true;
        }
    }
}
