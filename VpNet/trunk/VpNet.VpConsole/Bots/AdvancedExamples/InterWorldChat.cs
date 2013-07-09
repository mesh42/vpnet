using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace VpNet.VpConsole
{
    public class InterWorldChat{
        private readonly string _user;
        private readonly string _password;
        private readonly string _botname;

        private List<Instance> _instances;
        private Timer _t;
        private Timer _t2;
        private List<World> _worlds; 

        public InterWorldChat(string user, string password, string botname)
        {
            _user = user;
            _password = password;
            _botname = botname;
            _instances = new List<Instance>();
            _instances.Insert(0, new Instance());
            _instances[0].Connect();
            _instances[0].Login(user, password, botname);
            _instances[0].OnWorldList += InterWorldChat_OnWorldList;


            _worlds = new List<World>();
            _instances[0].ListWorlds();
            _t = new Timer(Wait,this,0,30);
        }

        void InterWorldChat_OnWorldList(Instance sender, WorldListEventArgs args)
        {
            if (_t2 != null)
                _t2.Dispose();
            _t2 = new Timer(WorldListTimer,null,1000,0);
            //if (args.World.Name == "Blizzard")
            //    return;
            _worlds.Add(args.World);
        }

        private void WorldListTimer(object state)
        {
            _t.Dispose();
            Debug.WriteLine("end of list");
            _t2.Dispose();
            if (_worlds.Count == 0)
                return;
            _instances[0].OnWorldList -= InterWorldChat_OnWorldList;
            _instances[0].OnChatMessage += vp_OnChatMessage;
            _instances[0].OnAvatarEnter += InterWorldChat_OnAvatarEnter;
            _instances[0].OnAvatarLeave += InterWorldChat_OnAvatarLeave;
            _instances[0].Enter(_worlds[0]);
            _instances[0].ConsoleMessage("interchat", "inter world chat is now online", new Color(192, 0, 0));
            _instances[0].UpdateAvatar();
            foreach (var world in _worlds.Skip(1))
            {
                lock (this)
                {
                    _instances.Insert(0, new Instance());
                    _instances[0].OnChatMessage += vp_OnChatMessage;
                    _instances[0].Connect();
                    _instances[0].Login(_user, _password, _botname);
                    _instances[0].Enter(world.Name);
                    _instances[0].ConsoleMessage("interchat", "inter world chat is now online", new Color(192, 0, 0));
                    _instances[0].UpdateAvatar();
                    _instances[0].OnAvatarEnter += InterWorldChat_OnAvatarEnter;
                    _instances[0].OnAvatarLeave += InterWorldChat_OnAvatarLeave;
                  
                }
            }
            _t = new Timer(Wait,this,30,30);
        }

        void InterWorldChat_OnAvatarLeave(Instance sender, AvatarLeaveEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            foreach (var vp in _instances)
            {
                if (vp.Configuration.World.Name != sender.Configuration.World.Name)
                {
                    vp.ConsoleMessage(_botname,
                        string.Format("[{0}] has left {1}",args.Avatar.Name, sender.Configuration.World.Name), new Color(128, 32, 128));
                  
                }
            }
        }

        void InterWorldChat_OnAvatarEnter(Instance sender, AvatarEnterEventArgsT<Avatar<Vector3>, Vector3> args)
        {
            foreach (var vp in _instances)
            {
                if (vp.Configuration.World.Name != sender.Configuration.World.Name)
                {
                    vp.ConsoleMessage(_botname,
                        string.Format("[{0}] has entered {1}", args.Avatar.Name, sender.Configuration.World.Name), new Color(128,32,128));
                  
                }
            }
        }

        private void Wait(object state)
        {
            foreach (var vp in _instances)
            {
                vp.Wait(0);
            }
        }

        void vp_OnChatMessage(Instance sender, ChatMessageEventArgsT<Avatar<Vector3>, ChatMessage, Vector3, Color> args)
        {
            if ((args.ChatMessage.Type == ChatMessageTypes.Console && args.Avatar.Name=="interchat"))
                return;
            foreach (var vp in _instances)
            {
                if (vp.Configuration.World.Name != sender.Configuration.World.Name)
                {
                    vp.ConsoleMessage(args.ChatMessage.Name, 
                        string.Format("[{0}] {1}",sender.Configuration.World.Name,args.ChatMessage.Message), new Color(64, 64, 64));
                    vp.Wait();
                }    
            }
           
        }
    }
}
