﻿using System;
using VPNetExamples.Common;
using VpNet.Core;
using VpNet.Core.EventData;

namespace VPNetExamples
{
    internal class EventDisplayBot : BaseExampleBot
    {
        public EventDisplayBot()
        {
            
        }

        public EventDisplayBot(Instance instance): base(instance){}

        public override void Initialize()
        {
            Instance.EventAvatarAdd += EventAvatarAdd;
            Instance.EventAvatarDelete += EventAvatarDelete;
            Instance.EventChat += EventChat;
            Instance.EventFriend += EventFriend;
            Instance.EventObjectClick += EventObjectClick;
            Instance.EventObjectDelete += EventObjectDelete;
            Instance.EventObjectChange += EventObjectChange;
            Instance.EventObjectCreate += EventObjectCreate;
            Instance.EventWorldList += EventWorldList;
            Instance.EventUniverseDisconnect += EventUniverseDisconnect;
            Instance.EventWorldDisconnect += EventWorldDisconnect;
        }

        void EventObjectCreate(Instance sender, VpObject objectData)
        {
            Console.WriteLine("Created Object {0}", objectData.Id);
        }

        void EventObjectChange(Instance sender, VpObject objectData)
        {
            Console.WriteLine("Changed Object {0}", objectData.Id);
        }

        void EventWorldList(Instance sender, World eventData)
        {
            Console.WriteLine("World List {0}, {1} users.",eventData.Name,eventData.UserCount);
        }

        void EventObjectDelete(Instance sender, int id)
        {
            Console.WriteLine("Delete Object. {0}",id);
        }

        void EventObjectClick(Instance sender, int sessionId, int objectId)
        {
            Console.WriteLine("Avatar with session ID {0} clicked on object {1}.",sessionId, objectId);
        }

        void EventFriend(Instance sender)
        {
            // currently not supported.
            Console.WriteLine("Friend event.");
        }

        void EventChat(Instance sender, Chat eventData)
        {
            Console.WriteLine("{0} says {1}",eventData.Username,eventData.Message);
        }

        void EventAvatarDelete(Instance sender, Avatar eventData)
        {
            Console.WriteLine("{0} left world.", eventData.Name);
        }


        void EventAvatarAdd(Instance sender, VpNet.Core.EventData.Avatar eventData)
        {
            Console.WriteLine("{0} entered world.",eventData.Name);
        }

        void EventUniverseDisconnect(Instance sender)
        {
            Console.WriteLine("Universe disconnected.");
        }
        
        void EventWorldDisconnect(Instance sender)
        {
            Console.WriteLine("World disconnected.");
        }
    }
}
