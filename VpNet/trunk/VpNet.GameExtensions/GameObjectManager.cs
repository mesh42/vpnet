using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using VpNet.Extensions;

namespace VpNet.GameExtensions
{
    /// <summary>
    /// Manages objects and tracks changes by applying some filtering on top of the underlaying VPNET callbacks.
    /// </summary>
    [Serializable]
    public class GameObjectManager
    {
        private readonly GameInstance _instance;
        private Dictionary<int,GameVpObject> _list = new Dictionary<int, GameVpObject>();

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

        public RcDefault Add(GameVpObject prototype)
        {
            if (_list.ContainsKey(prototype.Id))
                throw new Exception("This object is already managed.");
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