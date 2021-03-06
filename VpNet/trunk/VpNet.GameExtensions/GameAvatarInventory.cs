﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using VpNet.Extensions;
using VpNet.GameExtensions.Abstract;

namespace VpNet.GameExtensions
{
    [Serializable]
    public sealed class GameAvatarInventory : BaseGameEntity<GameAvatarInventory>
    {
        private GameAvatar _avatar;

        public GameAvatarInventory(GameInstance gameInstance, GameAvatar avatar) : base(gameInstance)
        {
            _avatar = avatar;
        }

        public override string StorageDirectory
        {
            get { return Path.Combine(GameInstance.GameInstanceConfiguration.StorageDataPath, _avatar.UserId.ToString(), "Inventory"); } 
        }

        private string GetPath(GameVpObject vpObject)
        {
            return Path.Combine(StorageDirectory, vpObject.Id.ToString());
        }

        public void Clear()
        {
            var di = new DirectoryInfo(StorageDirectory);
            if (di.Exists)
                di.Delete(true);
        }

        public bool Add(GameVpObject vpObject)
        {
            if (File.Exists(GetPath(vpObject)))
                return false;
            JsonConvert.SerializeObject(vpObject,Formatting.Indented).SaveTextFile(GetPath(vpObject));
            return true;
        }

        public void Remove(GameVpObject vpObject)
        {
            File.Delete(GetPath(vpObject));
        }

        public GameVpObject Get(GameVpObject vpObject)
        {
            if (File.Exists(GetPath(vpObject)))
            {
                return JsonConvert.DeserializeObject<GameVpObject>(GetPath(vpObject).LoadTextFile());
                //return SerializableExtensions.Deserialize<GameVpObject>(GetPath(vpObject));
            }
            return null;
        }

        public List<GameVpObject> Items()
        {
            var items = new List<GameVpObject>();
            try
            {
                foreach (var file in new DirectoryInfo(StorageDirectory).GetFiles())
                {
                    items.Add(JsonConvert.DeserializeObject<GameVpObject>(file.FullName.LoadTextFile()));
                    //items.Add(SerializableExtensions.Deserialize<GameVpObject>(file.FullName));
                }
                return items;
            }
            catch (DirectoryNotFoundException ex)
            {
                return items;
            }
        }

        public override IObservable<GameAvatarInventory> Changed
        {
            get { throw new NotImplementedException(); }
        }
    }
}
