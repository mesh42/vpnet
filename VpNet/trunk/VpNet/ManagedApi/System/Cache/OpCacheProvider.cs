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
using System.Net;
using System.Threading.Tasks;
using VpNet.Interfaces;

namespace VpNet.Cache
{
    public delegate void ModelDataDelegate(ModelData data);

    public class OpCacheProvider<TWorld>  
        where TWorld : class, IWorld, new()
    {
        private readonly TWorld _world;
        private readonly string _localPath;
        private readonly string _modelPath;
        private readonly string _remoteModelPath;

        public OpCacheProvider(TWorld world, string localPath)
        {
            _world = world;
            _localPath = localPath;
            _modelPath = Path.Combine(localPath, "models");
            _remoteModelPath = _world.RawAttributes["objectpath"] + "/models/";
            if (!Directory.Exists(_modelPath))
            {
                Directory.CreateDirectory(_modelPath);
            }
        }

        public Task GetModelDataAsync(string name, ModelDataDelegate callback)
        {
            var t = new Task(() => Download(name,callback));
            t.Start();
            return t;
        }

        private void Download(string name, ModelDataDelegate callback)
        {
            var model = Path.Combine(_modelPath, Path.GetFileNameWithoutExtension(name) + ".zip");
            if (File.Exists(model))
            {
                var zip = ZipStorer.Open(model, FileAccess.Read);
                var dir = zip.ReadCentralDir();
                if (dir.Count == 0)
                    callback(new ModelData{Data=string.Empty,Exception=new Exception("No such model found."),Name=name});
                using (var mem = new MemoryStream())
                {
                    zip.ExtractFile(dir[0], mem);
                    zip.Close();
                    mem.Position = 0;
                    using (var sr = new StreamReader(mem))
                    {
                        var data = sr.ReadToEnd();
                        callback(new ModelData { Data = data, Name = name });
                    }
                }
                return;
            }

            
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFile(
                                           Path.Combine(_remoteModelPath,
                                                        Path.GetFileNameWithoutExtension(name) + ".zip"),model);
                    // recurse to read the file which was saved to harddisk.
                    Download(name, callback);
                   
                }
                catch (Exception ex)
                {
                    callback(new ModelData{Name=name, Data=string.Empty,Exception=ex});
                }
           }
        }
    }

    public class ModelData
    {
        public string Data { get; internal set; }
        public string Name { get; internal set; }
        public Exception Exception { get; internal set; }

        public bool HasException
        {
            get { return Exception != null; }
        }
    }
}
