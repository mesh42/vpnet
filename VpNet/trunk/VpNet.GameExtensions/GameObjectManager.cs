using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using CSScriptLibrary;
using VpNet.Extensions;
using VpNet.GameExtensions.Interfaces;

namespace VpNet.GameExtensions
{
    public class GameScript
    {
        private readonly GameInstance _instance;
        public HostApp Host;
        public FileSystemWatcher _fsw;
        private GameVpObject _vpObject;
        private readonly string _csScriptName;
        private IGameScriptVpObject scriptObject = null;

        public GameScript(GameInstance instance, GameVpObject vpObject, string csScriptName)
        {
            _instance = instance;
            _vpObject = vpObject;
            _csScriptName = csScriptName;
            _fsw = new FileSystemWatcher(Path.Combine(instance.GameInstanceConfiguration.StorageDataPath, "CsScript"), _csScriptName);
            _fsw.Changed += _fsw_Changed;
            _fsw.EnableRaisingEvents = true;
            _fsw.NotifyFilter = NotifyFilters.LastWrite;
            Execute(_csScriptName);
        }

        private void Execute(string csScriptName)
        {
            var script1 = Path.Combine(_instance.GameInstanceConfiguration.StorageDataPath, "CsScript", csScriptName).LoadTextFile();
                Host= new HostApp();
                scriptObject = CSScript.LoadCode(script1)
                                .CreateObject("*")
                                .AlignToInterface<IGameScriptVpObject>();

            scriptObject.GameVpObject = _vpObject;
            scriptObject.GameInstance = _instance;
            scriptObject.Initialize();
        }


        void _fsw_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                _fsw.EnableRaisingEvents = false;
                try
                {
                    _instance.Say("changed");
                }

                finally
                {
                    _fsw.EnableRaisingEvents = true;
                }
                scriptObject.Unload();
                
                scriptObject = null;
                Host = null;
                Execute(_csScriptName);

            }
        }
    }

    /// <summary>
    /// Manages objects and tracks changes by applying some filtering on top of the underlaying VPNET callbacks.
    /// </summary>
    [Serializable]
    public class GameObjectManager
    {
        private readonly GameInstance _instance;
        private Dictionary<int,GameVpObject> _list = new Dictionary<int, GameVpObject>();
        private Dictionary<int, GameScript> _csScripts = new Dictionary<int, GameScript>();
         

        public delegate void OnManagedObjectChangedCallbackDelegate(GameInstance instance, ObjectChangeCallbackArgsT<RcDefault, GameVpObject, Vector3> args);
        public delegate void OnManagedObjectChangedDelegate(GameInstance instance, ObjectChangeArgsT<GameAvatar, GameVpObject, Vector3> args);
        public delegate void OnManagedObjectGetCallbackDelegate(GameInstance instance, ObjectGetCallbackArgsT<RcDefault, GameVpObject, Vector3> args);
        public delegate void OnManagedObjectClickDelegate(GameInstance instance, ObjectClickArgsT<GameAvatar, GameVpObject, Vector3> args);

        public event OnManagedObjectChangedDelegate OnManagedObjectChanged;
        public event OnManagedObjectChangedCallbackDelegate OnManagedObjectChangedCallback;
        public event OnManagedObjectGetCallbackDelegate OnManagedObjectGetCallback;
        public event OnManagedObjectClickDelegate OnManagedObjectClick;

        public IObservable<EventPattern<GameInstance,ObjectClickArgsT<GameAvatar, GameVpObject, Vector3>>> ObjectClickAsObservable
        {
            get
            {
                return Observable
                    .FromEventPattern<OnManagedObjectClickDelegate,GameInstance,ObjectClickArgsT<GameAvatar, GameVpObject, Vector3>>(
                        ev => OnManagedObjectClick += ev,
                        ev => OnManagedObjectClick -= ev);
            }
        }
        public IObservable<EventPattern<GameInstance, ObjectChangeArgsT<GameAvatar, GameVpObject, Vector3>>> ObjectChangeAsObservable
        {
            get
            {
                return Observable
                    .FromEventPattern<OnManagedObjectChangedDelegate, GameInstance, ObjectChangeArgsT<GameAvatar, GameVpObject, Vector3>>(
                        ev => OnManagedObjectChanged += ev,
                        ev => OnManagedObjectChanged -= ev);
            }
        }
        public IObservable<EventPattern<GameInstance, ObjectGetCallbackArgsT<RcDefault, GameVpObject, Vector3>>> ObjectGetCallbackAsOvObservable
        {
            get
            {
                return Observable
                    .FromEventPattern<OnManagedObjectGetCallbackDelegate, GameInstance, ObjectGetCallbackArgsT<RcDefault, GameVpObject, Vector3>>(
                        ev => OnManagedObjectGetCallback += ev,
                        ev => OnManagedObjectGetCallback -= ev);
            }
        }
        public IObservable<EventPattern<GameInstance, ObjectChangeCallbackArgsT<RcDefault, GameVpObject, Vector3>>> ObjectChangedCallbackAsObservable
        {
            get
            {
                return Observable
                    .FromEventPattern<OnManagedObjectChangedCallbackDelegate, GameInstance, ObjectChangeCallbackArgsT<RcDefault, GameVpObject, Vector3>>(
                        ev => OnManagedObjectChangedCallback += ev,
                        ev => OnManagedObjectChangedCallback -= ev);
            }
        }

        public GameObjectManager(GameInstance instance)
        {
           
            

            _instance = instance;
            instance.OnObjectChange += instance_OnObjectChange;
            instance.OnObjectChangeCallback += instance_OnObjectChangeCallback;
            instance.OnObjectGetCallback += instance_OnObjectGetCallback;
            instance.OnObjectClick += instance_OnObjectClick;
        }

        void instance_OnObjectClick(GameInstance sender, ObjectClickArgsT<GameAvatar, GameVpObject, Vector3> args)
        {
            if (!_list.ContainsKey(args.VpObject.Id)) return;
            //_list[args.VpObject.Id].CopyFrom(args.VpObject, true);
            args.VpObject = _list[args.VpObject.Id];
            if (OnManagedObjectChanged != null)
                OnManagedObjectClick(_instance, args);
        }

        void instance_OnObjectGetCallback(GameInstance sender, ObjectGetCallbackArgsT<RcDefault, GameVpObject, Vector3> args)
        {
            if (!_list.ContainsKey(args.VpObject.Id)) return;
            _list[args.VpObject.Id].CopyFrom(args.VpObject, true);
            args.VpObject = _list[args.VpObject.Id];
            if (OnManagedObjectChanged != null)
                OnManagedObjectGetCallback(_instance, args);
        }

        public void AddRange(IEnumerable<GameVpObject> prototypes)
        {
            foreach (var prototype in prototypes)
                Add(prototype);
        }

        public void AttachCsScript(GameVpObject gameVpObject, string csScriptName)
        {
            if (!_list.ContainsKey(gameVpObject.Id))
            {
                var script = Path.Combine(_instance.GameInstanceConfiguration.StorageDataPath, "CsScript", csScriptName).LoadTextFile();
                Add(gameVpObject, script);
            }
            _csScripts.Add(gameVpObject.Id, new GameScript(_instance, gameVpObject, csScriptName));
        }


        public void RemoveCsScript(GameVpObject gameVpObject)
        {
            if (_csScripts.ContainsKey(gameVpObject.Id))
            {
                var host = _csScripts[gameVpObject.Id];
                host = null;
                _csScripts.Remove(gameVpObject.Id);
            }
        }

        public RcDefault Add(GameVpObject prototype, string attachCsScript)
        {
            if (_list.ContainsKey(prototype.Id))
                return new RcDefault(0);
            _list.Add(prototype.Id, prototype);
            // refresh the object.
            return _instance.GetObject(prototype.Id);
        }

        public RcDefault Add(GameVpObject prototype)
        {
            if (_list.ContainsKey(prototype.Id))
                return new RcDefault(0);
            _list.Add(prototype.Id,prototype);
            // refresh the object.
            return _instance.GetObject(prototype.Id);
        }

        public void Remove(int id)
        {
            _list.Remove(id);
        }

        void instance_OnObjectChangeCallback(GameInstance sender, ObjectChangeCallbackArgsT<RcDefault, GameVpObject, Vector3> args)
        {
            if (!_list.ContainsKey(args.VpObject.Id)) return;
            _list[args.VpObject.Id].CopyFrom(args.VpObject, true);
            args.VpObject = _list[args.VpObject.Id];
            if (OnManagedObjectChanged != null)
                OnManagedObjectChangedCallback(_instance, args);
        }

        void instance_OnObjectChange(GameInstance sender, ObjectChangeArgsT<GameAvatar, GameVpObject, Vector3> args)
        {
            if (!_list.ContainsKey(args.VpObject.Id)) return;
            _list[args.VpObject.Id].CopyFrom(args.VpObject, true);
            args.VpObject = _list[args.VpObject.Id];
            if (OnManagedObjectChanged != null)
                OnManagedObjectChanged(_instance, args);
        }
    }
}