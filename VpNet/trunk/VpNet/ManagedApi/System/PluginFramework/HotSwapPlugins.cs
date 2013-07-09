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
using System.Reflection;
using VpNet.PluginFramework.Interfaces;

namespace VpNet.PluginFramework
{
    /// <summary>
    /// Hot swappable plugin framework.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HotSwapPlugins<T>
        where T : class
    {
        private readonly string _pluginPath;
        private List<Assembly> _assemblies;
        private List<T> _instances; 

        public HotSwapPlugins(string pluginPath = @".\")
        {
            _pluginPath = pluginPath;
            _instances = new List<T>();
            _assemblies = new List<Assembly>();
            Discover();
        }

        public List<T> Instances
        {
             get { return _instances; }
        }

        private void Discover()
        {
            Assembly assembly;
            var files = Directory.GetFiles(_pluginPath, "*.dll");
            foreach (string file in files)
            {
                byte[] bytes = File.ReadAllBytes(file);
                try
                {
                    assembly = Assembly.Load(bytes);
                }
                catch
                {
                    continue;
                }
                bool isAdded = false;
                foreach (var type in assembly.GetTypes())
                {
                    if (!type.IsClass || type.IsNotPublic) continue;
                    if (type.BaseType == typeof(T))
                    {
                        if (!isAdded)
                        {
                            _assemblies.Add(assembly);
                        }
                        _instances.Add(Activator.CreateInstance(type) as T);
                        isAdded = true;
                    }
                }
            }
        }
    }
}
