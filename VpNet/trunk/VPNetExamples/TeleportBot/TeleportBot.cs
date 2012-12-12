using System;
using System.IO;
using System.Linq;
using VPNetExamples.Common;
using VPNetExamples.Common.ActionInterpreter.Commands;
using VpNet.Core;
using VpNet.Core.Structs;
using System.Collections.Generic;

namespace VPNetExamples.TeleportBot
{
    internal class TeleportBot : BaseExampleBot
    {
        private List<Avatar> _avatars = new List<Avatar>();

        public TeleportBot(){
            Instance.EventAvatarAdd += new VpNet.Core.Instance.AvatarEvent(Instance_EventAvatarAdd);

        }

        public TeleportBot(Instance instance) : base(instance){
            Instance.EventAvatarAdd += new VpNet.Core.Instance.AvatarEvent(Instance_EventAvatarAdd);
        }

        public override void Initialize()
        {
            Instance.EventChat += new VpNet.Core.Instance.ChatEvent(Instance_EventChat);
            Instance.EventAvatarChange += new VpNet.Core.Instance.AvatarEvent(Instance_EventAvatarChange);
        }

        void Instance_EventAvatarChange(Instance sender, Avatar eventData)
        {
            _avatars.RemoveAll(p => p.Session == eventData.Session);
            _avatars.Add(eventData);
        }

        void Instance_EventChat(Instance sender, VpNet.Core.EventData.Chat eventData)
        {
            if ((eventData.Message.ToLower().StartsWith("//search ")))
            {
                var name = eventData.Message.ToLower().Substring(eventData.Message.IndexOf(' '));
                var avatar = _avatars.Find(p=>p.Name.ToLower()==name.Trim());
                if (avatar != null)
                {
                    Instance.TeleportAvatar(eventData.Session, string.Empty, avatar.X, avatar.Y, avatar.Z, avatar.Yaw, avatar.Pitch);
                }
            }
        }

        void Instance_EventAvatarAdd(Instance sender, Avatar eventData)
        {
            _avatars.Add(eventData);
        }
        public override void Disconnect()
        {
            // note that as we added the timer using the add timer method, from the base class we do not need to cleanup the timer.
            // it is handled by that base class.
        }
    }
}
