using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VpNet.Interfaces;
using VpNet.Extensions;

namespace VpNet.GameExtensions
{
    public class VpObjectInventory<TAvatar, TVpObject, TVector3> 
        where TVector3 : struct, IVector3
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVpObject : class, IVpObject<TVector3>, new()
    {
        private string GetPath(TAvatar avatar)
        {
            return Path.Combine(avatar.UserId.ToString(), "Inventory");
        }

        private string GetPath(TAvatar avatar, TVpObject vpObject)
        {
            return Path.Combine(GetPath(avatar), vpObject.Id.ToString());
        }

        public void Delete(TAvatar avatar)
        {
            Directory.Delete(GetPath(avatar),true);
        }

        public bool Add(TAvatar avatar, TVpObject vpObject)
        {
            if (File.Exists(GetPath(avatar, vpObject)))
                return false;

            vpObject.Serialize(GetPath(avatar,vpObject));
            return true;
        }

        public void Remove(TAvatar avatar, TVpObject vpObject)
        {
            File.Delete(GetPath(avatar, vpObject));
        }

        public TVpObject Get(TAvatar avatar, TVpObject vpObject)
        {
            if (File.Exists(GetPath(avatar,vpObject)))
            {
                return SerializableExtensions.Deserialize<TVpObject>(GetPath(avatar,vpObject));
            }
            return null;
        }
    }
}
