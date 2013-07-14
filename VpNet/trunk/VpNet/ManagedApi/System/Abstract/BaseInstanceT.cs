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
using System.Linq;
using System.Threading;
using VpNet.Cache;
using VpNet.Extensions;
using VpNet.Interfaces;
using VpNet.NativeApi;
using Attribute = VpNet.NativeApi.Attributes;

namespace VpNet.Abstract
{
    /// <summary>
    /// Abtract fully teamplated instance class, providing .NET encapsulation strict templated types to the native C wrapper.
    /// </summary>
    /// <typeparam name="T">Type of the abstract implementation</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    /// <typeparam name="TFriend">The type of the friend.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <typeparam name="TTerrainCell">The type of the terrain cell.</typeparam>
    /// <typeparam name="TTerrainNode">The type of the terrain node.</typeparam>
    /// <typeparam name="TTerrainTile">The type of the terrain tile.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    /// <typeparam name="TWorld">The type of the world.</typeparam>
    /// <typeparam name="TWorldAttributes">The type of the world attributes.</typeparam>
    /// <typeparam name="TCell">The type of the cell.</typeparam>
    /// <typeparam name="TChatMessage">The type of the chat message.</typeparam>
    /// <typeparam name="TTerrain">The type of the terrain.</typeparam>
    /// <typeparam name="TUniverse">The type of the universe.</typeparam>
    /// <typeparam name="TTeleport">The type of the teleport.</typeparam>
    /// <typeparam name="TAvatarChangeEventArgs">The type of the avatar change event args.</typeparam>
    /// <typeparam name="TAvatarEnterEventArgs">The type of the avatar enter event args.</typeparam>
    /// <typeparam name="TAvatarLeaveEventArgs">The type of the avatar leave event args.</typeparam>
    /// <typeparam name="TQueryCellResultArgs">The type of the query cell result args.</typeparam>
    /// <typeparam name="TQueryCellEndArgs">The type of the query cell end args.</typeparam>
    /// <typeparam name="TChatMessageEventArgs">The type of the chat message event args.</typeparam>
    /// <typeparam name="TFriendAddCallbackEventArgs">The type of the friend add callback event args.</typeparam>
    /// <typeparam name="TFriendDeleteCallbackEventArgs">The type of the friend delete callback event args.</typeparam>
    /// <typeparam name="TFriendsGetCallbackEventArgs">The type of the friends get callback event args.</typeparam>
    /// <typeparam name="TTerainNodeEventArgs">The type of the terain node event args.</typeparam>
    /// <typeparam name="TUniverseDisconnectEventargs">The type of the universe disconnect eventargs.</typeparam>
    /// <typeparam name="TObjectChangeArgs">The type of the object change args.</typeparam>
    /// <typeparam name="TObjectChangeCallbackArgs">The type of the object change callback args.</typeparam>
    /// <typeparam name="TObjectClickArgs">The type of the object click args.</typeparam>
    /// <typeparam name="TObjectCreateArgs">The type of the object create args.</typeparam>
    /// <typeparam name="TObjectCreateCallbackArgs">The type of the object create callback args.</typeparam>
    /// <typeparam name="TObjectDeleteArgs">The type of the object delete args.</typeparam>
    /// <typeparam name="TObjectDeleteCallbackArgs">The type of the object delete callback args.</typeparam>
    /// <typeparam name="TWorldDisconnectEventArg">The type of the world disconnect event arg.</typeparam>
    /// <typeparam name="TWorldListEventargs">The type of the world list eventargs.</typeparam>
    /// <typeparam name="TWorldSettingsChangedEventArg">The type of the world settings changed event arg.</typeparam>
    /// <typeparam name="TTeleportEventArgs">The type of the teleport event args.</typeparam>
    /// <typeparam name="TWorldEnterEventArgs">The type of the world enter event args.</typeparam>
    /// <typeparam name="TWorldLeaveEventArgs">The type of the world leave event args.</typeparam>
    [Serializable]
    public abstract partial class BaseInstanceT<T,
        /* Scene Type specifications ----------------------------------------------------------------------------------------------------------------------------------------------*/
        TAvatar, TColor, TFriend, TResult, TTerrainCell, TTerrainNode,
        TTerrainTile, TVector3, TVpObject, TWorld, TWorldAttributes,TCell,TChatMessage,TTerrain,TUniverse,TTeleport,

        /* Event Arg types --------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Avatar Event Args */
        TAvatarChangeEventArgs, TAvatarEnterEventArgs, TAvatarLeaveEventArgs,
        /* Cell Event Args */
        TQueryCellResultArgs, TQueryCellEndArgs,
        /* Chat Event Args */
        TChatMessageEventArgs,
        /* Friend Event Args */
        TFriendAddCallbackEventArgs, TFriendDeleteCallbackEventArgs, TFriendsGetCallbackEventArgs,
        /* Terrain Event Args */
        TTerainNodeEventArgs,
        /* Universe Event Args */
        TUniverseDisconnectEventargs,
        /* VpObject Event Args */
        TObjectChangeArgs, TObjectChangeCallbackArgs, TObjectClickArgs, TObjectCreateArgs,
        TObjectCreateCallbackArgs, TObjectDeleteArgs, TObjectDeleteCallbackArgs,
        /* World Event Args */
            TWorldDisconnectEventArg, TWorldListEventargs, TWorldSettingsChangedEventArg,
          /* Teleport Event Args */
        TTeleportEventArgs,
        TWorldEnterEventArgs,
        TWorldLeaveEventArgs
        > :
        /* Interface specifications -----------------------------------------------------------------------------------------------------------------------------------------*/
        /* Functions */
        BaseInstanceEvents<TWorld>,
        IAvatarFunctions<TResult, TAvatar, TVector3>,
        IChatFunctions<TResult, TAvatar, TColor, TVector3>,
        IFriendFunctions<TResult, TFriend>,
        ITeleportFunctions<TResult, TWorld, TAvatar, TVector3>,
        ITerrainFunctions<TResult, TTerrainTile, TTerrainNode, TTerrainCell>,
        IVpObjectFunctions<TResult, TVpObject, TVector3>,
        IWorldFunctions<TResult, TWorld, TWorldAttributes>,
        IUniverseFunctions<TResult>
/* Constraints ----------------------------------------------------------------------------------------------------------------------------------------------------*/
        where TUniverse : class, IUniverse, new()
        where TTerrain : class, ITerrain, new()
        where TCell : class, ICell, new()
        where TChatMessage : class, IChatMessage<TColor>, new()
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode, TTerrainCell>, new()
        where TResult : class, IRc, new()
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar<TVector3>, new()
        where TFriend : class, IFriend, new()
        where TColor : class, IColor, new()
        where TVpObject : class, IVpObject<TVector3>, new()
        where TVector3 : struct, IVector3
        where TWorldAttributes : class, IWorldAttributes, new()
        where TTeleport : class, ITeleport<TWorld,TAvatar,TVector3>, new()
        where T : class, new()
        /* Event Arg types --------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Avatar Event Args */
        where TAvatarChangeEventArgs : class, IAvatarChangeEventArgs<TAvatar,TVector3>, new()
        where TAvatarEnterEventArgs : class, IAvatarEnterEventArgs<TAvatar,TVector3>, new()
        where TAvatarLeaveEventArgs : class, IAvatarLeaveEventArgs<TAvatar,TVector3>, new()
        /* Cell Event Args */
        where TQueryCellResultArgs : class, IQueryCellResultArgs<TVpObject,TVector3>, new()
        where TQueryCellEndArgs : class, IQueryCellEndArgs<TCell>, new()
        /* Chat Event Args */
        where TChatMessageEventArgs : class, IChatMessageEventArgs<TAvatar,TChatMessage,TVector3,TColor>, new()
        /* Friend Event Args */
        where TFriendAddCallbackEventArgs : class,IFriendAddCallbackEventArgs<TFriend>, new()
        where TFriendDeleteCallbackEventArgs : class, IFriendDeleteCallbackEventArgs<TFriend>,  new()
        where TFriendsGetCallbackEventArgs : class, IFriendsGetCallbackEventArgs<TFriend>,  new()
        /* Terrain Event Args */
        where TTerainNodeEventArgs : class, ITerrainNodeEventArgs<TTerrain>,  new()
        /* Universe Event Args */
        where TUniverseDisconnectEventargs : class, IUniverseDisconnectEventArgs<TUniverse>, new()
        /* VpObject Event Args */
        where TObjectChangeArgs : class,IObjectChangeArgs<TAvatar,TVpObject,TVector3>, new()
        where TObjectChangeCallbackArgs : class,IObjectChangeCallbackArgs<TResult,TVpObject,TVector3>, new()
        where TObjectClickArgs : class, IObjectClickArgs<TAvatar,TVpObject,TVector3>,  new()
        where TObjectCreateArgs : class, IObjectCreateArgs<TAvatar,TVpObject,TVector3>, new()
        where TObjectCreateCallbackArgs : class, IObjectCreateCallbackArgs<TResult,TVpObject,TVector3>, new()
        where TObjectDeleteArgs : class, IObjectDeleteArgs<TAvatar,TVpObject,TVector3>,  new()
        where TObjectDeleteCallbackArgs : class,IObjectDeleteCallbackArgs<TResult,TVpObject,TVector3>,  new()
        /* World Event Args */
        where TWorldDisconnectEventArg : class, IWorldDisconnectEventArgs<TWorld>, new()
        where TWorldListEventargs : class, IWorldListEventArgs<TWorld>,new()
        where TWorldSettingsChangedEventArg : class,IWorldSettingsChangedEventArgs<TWorld>, new()
        where TTeleportEventArgs : class, ITeleportEventArgs<TTeleport,TWorld,TAvatar,TVector3>, new()
        where TWorldEnterEventArgs : class, IWorldEnterEventArgs<TWorld>, new()
        where TWorldLeaveEventArgs : class, IWorldLeaveEventArgs<TWorld>, new()
    {
        bool _isInitialized;

        public OpCacheProvider ModelCacheProvider { get; internal set; }
  
        private int _reference = int.MinValue;
        private readonly Dictionary<int, TVpObject> _objectReferences = new Dictionary<int, TVpObject>();
        private Timer _waitTimer;

        private Dictionary<int, TAvatar> _avatars;

        public T Implementor { get; set; }

        Dictionary<string, TWorld> _worlds; 

        private bool _useAutoWaitTimer;
        public int AutoWaitTimerMs = 30;

        public bool UseAutoWaitTimer
        {
            get { return _useAutoWaitTimer; }
            set
            {
                if (value)
                {
                    if (Configuration.IsChildInstance)
                        throw new Exception("Auto wait timer should only be activated from the main instance.");
                    if (!_isInitialized)
                        return;
                    if (_waitTimer != null)
                        _waitTimer.Dispose();
                    _waitTimer = new Timer(WaitTimerCallback,this,0,AutoWaitTimerMs);
              
                }
                _useAutoWaitTimer = value;
            }
        }

        private void WaitTimerCallback(object state)
        {
            lock (this)
            {
                if (_isInitialized)
                {
                    Functions.vp_wait(_instance, 0);
                    return;
                }
                _useAutoWaitTimer = false;
                if (_waitTimer != null)
                    _waitTimer.Dispose();
            }
        }

        private TUniverse Universe { get; set; }
        private TWorld World { get; set; }

        private int GetNextReference()
        {
            lock (this)
            {
                if (_reference < int.MaxValue)
                    _reference++;
                else
                    _reference = int.MinValue;
                return _reference;
            }
        }

        internal void Init()
        {
            Universe = new TUniverse();
            World = new TWorld();
            ((IAvatarFunctions<TResult, TAvatar, TVector3>) this).Avatars = new Dictionary<int, TAvatar>();
            _avatars =  ((IAvatarFunctions<TResult, TAvatar, TVector3>) this).Avatars;
            _worlds = new Dictionary<string, TWorld>();
            _isInitialized = true;
        }

        internal void InitOnce()
        {
            OnChatNativeEvent += OnChatNative;
            OnAvatarAddNativeEvent += OnAvatarAddNative;
            OnAvatarChangeNativeEvent += OnAvatarChangeNative;
            OnAvatarDeleteNativeEvent += OnAvatarDeleteNative;
            OnWorldListNativeEvent += OnWorldListNative;
            OnWorldDisconnectNativeEvent += OnWorldDisconnectNative;

            OnObjectChangeNativeEvent += OnObjectChangeNative;
            OnObjectCreateNativeEvent += OnObjectCreateNative;
            OnObjectClickNativeEvent += OnObjectClickNative;
            OnObjectDeleteNativeEvent += OnObjectDeleteNative;
            OnQueryCellEndNativeEvent += OnQueryCellEndNative;
            OnUniverseDisconnectNativeEvent += OnUniverseDisconnectNative;
            OnTeleportNativeEvent += OnTeleportNative;

            OnObjectCreateCallbackNativeEvent += OnObjectCreateCallbackNative;
            OnObjectChangeCallbackNativeEvent += OnObjectChangeCallbackNative;
            OnObjectDeleteCallbackNativeEvent += OnObjectDeleteCallbackNative;
            OnFriendAddCallbackNativeEvent += OnFriendAddCallbackNative;
            OnFriendDeleteCallbackNativeEvent += OnFriendDeleteCallbackNative;
            OnGetFriendsCallbackNativeEvent += OnGetFriendsCallbackNative;
        }


        internal protected BaseInstanceT(BaseInstanceEvents<TWorld> parentInstance)
        {
            _instance = parentInstance._instance;
            Init();
            _avatars = ((IAvatarFunctions<TResult, TAvatar, TVector3>) parentInstance).Avatars.Copy();
            Configuration = parentInstance.Configuration;
            Configuration.IsChildInstance = true;
            parentInstance.OnChatNativeEvent += OnChatNative;
            parentInstance.OnAvatarAddNativeEvent += OnAvatarAddNative;
            parentInstance.OnAvatarChangeNativeEvent += OnAvatarChangeNative;
            parentInstance.OnAvatarDeleteNativeEvent += OnAvatarDeleteNative;
            parentInstance.OnWorldListNativeEvent += OnWorldListNative;
            parentInstance.OnWorldDisconnectNativeEvent += OnWorldDisconnectNative;

            parentInstance.OnObjectChangeNativeEvent += OnObjectChangeNative;
            parentInstance.OnObjectCreateNativeEvent += OnObjectCreateNative;
            parentInstance.OnObjectClickNativeEvent += OnObjectClickNative;
            parentInstance.OnObjectDeleteNativeEvent += OnObjectDeleteNative;
            parentInstance.OnQueryCellEndNativeEvent += OnQueryCellEndNative;
            parentInstance.OnUniverseDisconnectNativeEvent += OnUniverseDisconnectNative;
            parentInstance.OnTeleportNativeEvent += OnTeleportNative;

            parentInstance.OnObjectCreateCallbackNativeEvent += OnObjectCreateCallbackNative;
            parentInstance.OnObjectChangeCallbackNativeEvent += OnObjectChangeCallbackNative;
            parentInstance.OnObjectDeleteCallbackNativeEvent += OnObjectDeleteCallbackNative;
            parentInstance.OnFriendAddCallbackNativeEvent += OnFriendAddCallbackNative;
            parentInstance.OnFriendDeleteCallbackNativeEvent += OnFriendDeleteCallbackNative;
            parentInstance.OnGetFriendsCallbackNativeEvent += OnGetFriendsCallbackNative;
        }

        protected BaseInstanceT(InstanceConfiguration<TWorld> configuration)
        {
            InitOnce();
            InitVpNative();
            // this can't be a child instance.
            configuration.IsChildInstance = false;
            Configuration = configuration;
        } 

        private void InitVpNative()
        {
            if (!_isInitialized)
            {
                Init();
                Configuration = new InstanceConfiguration<TWorld>(false);
                int rc = Functions.vp_init(1);
                if (rc != 0)
                {
                    if (rc != 3)
                        throw new VpException((ReasonCode)rc);
                    //vp previously initialized. do nothing.
                }

            }
            _instance = Functions.vp_create();

            SetNativeEvent(Events.Chat, OnChatNative1);
            SetNativeEvent(Events.AvatarAdd, OnAvatarAddNative1);
            SetNativeEvent(Events.AvatarChange, OnAvatarChangeNative1);
            SetNativeEvent(Events.AvatarDelete, OnAvatarDeleteNative1);
            SetNativeEvent(Events.WorldList, OnWorldListNative1);
            SetNativeEvent(Events.WorldSetting, OnWorldSettingNative1);
            SetNativeEvent(Events.WorldSettingsChanged, OnWorldSettingsChangedNative1);
            SetNativeEvent(Events.ObjectChange, OnObjectChangeNative1);
            SetNativeEvent(Events.Object, OnObjectCreateNative1);
            SetNativeEvent(Events.ObjectClick, OnObjectClickNative1);
            SetNativeEvent(Events.ObjectDelete, OnObjectDeleteNative1);
            SetNativeEvent(Events.QueryCellEnd, OnQueryCellEndNative1);
            SetNativeEvent(Events.UniverseDisconnect, OnUniverseDisconnectNative1);
            SetNativeEvent(Events.WorldDisconnect, OnWorldDisconnectNative1);
            SetNativeEvent(Events.Teleport, OnTeleportNative1);
            SetNativeCallback(Callbacks.ObjectAdd, OnObjectCreateCallbackNative1);
            SetNativeCallback(Callbacks.ObjectChange, OnObjectChangeCallbackNative1);
            SetNativeCallback(Callbacks.ObjectDelete, OnObjectDeleteCallbackNative1);
            SetNativeCallback(Callbacks.FriendAdd, OnFriendAddCallbackNative1);
            SetNativeCallback(Callbacks.FriendDelete, OnFriendDeleteCallbackNative1);
            SetNativeCallback(Callbacks.GetFriends, OnGetFriendsCallbackNative1);
        }

        protected BaseInstanceT()
        {
            InitOnce();
            InitVpNative();
        }

        internal void OnObjectCreateCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectCreateCallbackNativeEvent(instance, rc, reference); } }
        internal void OnObjectChangeCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectChangeCallbackNativeEvent(instance, rc, reference); } }
        internal void OnObjectDeleteCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnObjectDeleteCallbackNativeEvent(instance, rc, reference); } }
        internal void OnFriendAddCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnFriendAddCallbackNativeEvent(instance, rc, reference); } }
        internal void OnFriendDeleteCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnFriendDeleteCallbackNativeEvent(instance, rc, reference); } }
        internal void OnGetFriendsCallbackNative1(IntPtr instance, int rc, int reference) { lock (this) { OnFriendDeleteCallbackNativeEvent(instance, rc, reference); } } 
        internal void OnChatNative1(IntPtr instance) { lock (this) { OnChatNativeEvent(instance); } }
        internal void OnAvatarAddNative1(IntPtr instance) { lock (this) { OnAvatarAddNativeEvent(instance); } }
        internal void OnAvatarChangeNative1(IntPtr instance) { lock (this) { OnAvatarChangeNativeEvent(instance); } }
        internal void OnAvatarDeleteNative1(IntPtr instance) { lock (this) { OnAvatarDeleteNativeEvent(instance); } }
        internal void OnWorldListNative1(IntPtr instance) { lock (this) { OnWorldListNativeEvent(instance); } }
        internal void OnWorldDisconnectNative1(IntPtr instance) { lock (this) { OnWorldDisconnectNativeEvent(instance); } }
        internal void OnWorldSettingsChangedNative1(IntPtr instance) { lock (this) { OnWorldSettingsChangedNativeEvent(instance); } }
        internal void OnWorldSettingNative1(IntPtr instance) { lock (this) { OnWorldSettingNativeEvent(instance); } }
        internal void OnObjectChangeNative1(IntPtr instance) { lock (this) { OnObjectChangeNativeEvent(instance); } }
        internal void OnObjectCreateNative1(IntPtr instance) { lock (this) { OnObjectCreateNativeEvent(instance); } }
        internal void OnObjectClickNative1(IntPtr instance) { lock (this) { OnObjectClickNativeEvent(instance); } }
        internal void OnObjectDeleteNative1(IntPtr instance) { lock (this) { OnObjectDeleteNativeEvent(instance); } }
        internal void OnQueryCellEndNative1(IntPtr instance) { lock (this) { OnQueryCellEndNativeEvent(instance); } }
        internal void OnUniverseDisconnectNative1(IntPtr instance) { lock (this) { OnUniverseDisconnectNativeEvent(instance); } }
        internal void OnTeleportNative1(IntPtr instance) { lock (this) { OnTeleportNativeEvent(instance); } }

        private bool _isDisposing;

        ~BaseInstanceT()
        {
            if (Configuration.IsChildInstance || _isDisposing)
                return;
            if (_instance == IntPtr.Zero) return;
            lock (this)
            {
                try
                {
                    Functions.vp_destroy(_instance);
                }
                catch
                {
                    // surpress not a valid instance pointer, _instance previously cleaned up.
                }
            }
        }

        #region Methods

        #region IUniverseFunctions Implementations

        virtual public TResult Connect(string host = "universe.virtualparadise.org", ushort port = 57000)
        {
            Universe.Host = host;
            Universe.Port = port;

            lock (this)
            {
                return new TResult
                    {
                        Rc = Functions.vp_connect_universe(_instance, host, port)
                    };
            }
        }

        virtual public TResult LoginAndEnter(bool announceAvatar = true)
        {
            Connect();
            Login();
            if (announceAvatar)
            {
                Enter();
                return UpdateAvatar();
            }
            return Enter();
        }

        virtual public TResult Login()
        {
            if (Configuration == null ||
                string.IsNullOrEmpty(Configuration.BotName) ||
                string.IsNullOrEmpty(Configuration.Password) ||
                string.IsNullOrEmpty(Configuration.UserName)
                )
            {
                throw new ArgumentException("Can't login because of Incomplete login configuration.");
            }
            return Login(Configuration.UserName, Configuration.Password, Configuration.BotName);
        }

        virtual public TResult Login(string username, string password, string botname)
        {
            lock (this)
            {
                Configuration.BotName = botname;
                Configuration.UserName = username;
                Configuration.Password = password;
                return new TResult
                    {
                    Rc = Functions.vp_login(_instance, username, password, botname)
                };
            }
        }

        #endregion

        #region IWorldFunctions Implementations

        virtual public TResult Wait(int milliseconds = 10)
        {
            return new TResult { Rc = Functions.vp_wait(_instance, milliseconds) };
        }

        virtual public TResult Enter(string worldname)
        {
            lock (this)
            {
                Configuration.World = new TWorld{Name=worldname};

                var result = new TResult
                {
                    Rc = Functions.vp_enter(_instance, worldname)
                };
                if (result.Rc == 0 && OnWorldEnter != null)
                    OnWorldEnter(Implementor,new TWorldEnterEventArgs(){World = Configuration.World.Copy()});
                return result;

            }
        }

        virtual public TResult Enter()
        {
            if (Configuration==null || Configuration.World==null || string.IsNullOrEmpty(Configuration.World.Name))
                   throw new ArgumentException("Can't login because of Incomplete instance world configuration.");
            return Enter(Configuration.World);
        }

        virtual public TResult Enter(TWorld world)
        {
            lock (this)
            {
                Configuration.World = world.Copy();
                return new TResult
                {
                     Rc= Functions.vp_enter(_instance, world.Name)
                };
            }
        }

        /// <summary>
        /// Leave the current world
        /// </summary>
        virtual public TResult Leave()
        {
            lock (this)
            {
                var result = new TResult {Rc = Functions.vp_leave(_instance)};
                if (result.Rc == 0 && OnWorldLeave !=null)
                {
                    OnWorldLeave(Implementor,new TWorldLeaveEventArgs {World = Configuration.World.Copy()});
                }
                return result;
            }
        }

        virtual public void Disconnect()
        {
            _avatars.Clear();
            Functions.vp_destroy(_instance);
            _isInitialized = false;
            InitVpNative();
            if (OnUniverseDisconnect != null)
                OnUniverseDisconnect(Implementor, new TUniverseDisconnectEventargs { Universe = Universe,DisconnectType = DisconnectType.UserDisconnected  });
        }

        virtual public TResult ListWorlds()
        {
            lock (this)
            {
                return new TResult {Rc = Functions.vp_world_list(_instance, 0)};
            }
        }

        #endregion

        #region IQueryCellFunctions Implementation

        virtual public TResult QueryCell(int cellX, int cellZ)
        {
            lock (this)
            {
                return new TResult { Rc = Functions.vp_query_cell(_instance, cellX, cellZ) };
            }
        }

        #endregion

        #region IVpObjectFunctions implementations

        public TResult ClickObject(TVpObject vpObject)
        {
            lock (this)
            {
                return ClickObject(vpObject.Id);
            }
        }

        public TResult ClickObject(int objectId)
        {
            lock (this)
            {
                Functions.vp_int_set(_instance, Attributes.ObjectId,objectId);
                return new TResult
                {
                    Rc = Functions.vp_object_click(_instance)
                };
            }
        }

        virtual public TResult DeleteObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = GetNextReference();
            lock (this)
            {
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(_instance, Attribute.ObjectId, vpObject.Id);
                rc = Functions.vp_object_delete(_instance);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        virtual public TResult AddObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = GetNextReference();
            lock (this)
            {
                vpObject.ReferenceNumber = referenceNumber; // calculated a unqiue id for you.
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(_instance, Attribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(_instance, Attribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(_instance, Attribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(_instance, Attribute.ObjectModel, vpObject.Model);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_float_set(_instance, Attribute.ObjectX, vpObject.Position.X);
                Functions.vp_float_set(_instance, Attribute.ObjectY, vpObject.Position.Y);
                Functions.vp_float_set(_instance, Attribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(_instance, Attribute.ObjectType, vpObject.ObjectType);
                rc = Functions.vp_object_add(_instance);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        virtual public TResult ChangeObject(TVpObject vpObject)
        {
            int rc;
            var referenceNumber = GetNextReference();
            lock (this)
            {
                _objectReferences.Add(referenceNumber, vpObject);
                Functions.vp_int_set(_instance, Attribute.ReferenceNumber, referenceNumber);
                Functions.vp_int_set(_instance, Attribute.ObjectId, vpObject.Id);
                Functions.vp_string_set(_instance, Attribute.ObjectAction, vpObject.Action);
                Functions.vp_string_set(_instance, Attribute.ObjectDescription, vpObject.Description);
                Functions.vp_string_set(_instance, Attribute.ObjectModel, vpObject.Model);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationX, vpObject.Rotation.X);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationY, vpObject.Rotation.Y);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationZ, vpObject.Rotation.Z);
                Functions.vp_float_set(_instance, Attribute.ObjectX, vpObject.Position.X);
                Functions.vp_float_set(_instance, Attribute.ObjectY, vpObject.Position.Y);
                Functions.vp_float_set(_instance, Attribute.ObjectZ, vpObject.Position.Z);
                Functions.vp_float_set(_instance, Attribute.ObjectRotationAngle, vpObject.Angle);
                Functions.vp_int_set(_instance, Attribute.ObjectType, vpObject.ObjectType);
                rc = Functions.vp_object_change(_instance);
            }
            if (rc != 0)
            {
                _objectReferences.Remove(referenceNumber);
            }
            return new TResult {Rc = rc};
        }

        #endregion

        #region ITeleportFunctions Implementations

        virtual public TResult TeleportAvatar(TAvatar avatar, string world, float x, float y, float z, float yaw, float pitch)
        {
            return new TResult
                {
                    Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world, x, y, z, yaw, pitch)
                };
        }

        virtual public TResult TeleportAvatar(int targetSession, string world, float x, float y, float z, float yaw, float pitch)
        {
            return new TResult
                {
                    Rc = Functions.vp_teleport_avatar(_instance, targetSession, world, x, y, z, yaw, pitch)
                };
        }

        virtual public TResult TeleportAvatar(TAvatar avatar, string world, TVector3 position, float yaw, float pitch)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world, position.X, position.Y, position.Z, yaw, pitch)
            };
        }

        virtual public TResult TeleportAvatar(int targetSession, string world, TVector3 position, float yaw, float pitch)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, targetSession, world, position.X, position.Y, position.Z, yaw, pitch)
            };

        }

        virtual public TResult TeleportAvatar(TAvatar avatar, string world, TVector3 position, TVector3 rotation)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world, position.X, position.Y, position.Z, rotation.Y, rotation.X)
            };

        }

        public TResult TeleportAvatar(TAvatar avatar, TWorld world, TVector3 position, TVector3 rotation)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, world.Name, position.X, position.Y, position.Z, rotation.Y, rotation.X)
            };
        }

        virtual public TResult TeleportAvatar(TAvatar avatar, TVector3 position, TVector3 rotation)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, string.Empty, position.X, position.Y, position.Z, rotation.Y, rotation.X)
            };
        }

        virtual public TResult TeleportAvatar(TAvatar avatar)
        {
            return new TResult
            {
                Rc = Functions.vp_teleport_avatar(_instance, avatar.Session, string.Empty, avatar.Position.X, avatar.Position.Y, avatar.Position.Z, avatar.Rotation.Y, avatar.Rotation.X)
            };
        }

        #endregion

        #region IAvatarFunctions Implementations.

        virtual public TResult UpdateAvatar(float x = 0.0f, float y = 0.0f, float z = 0.0f,float yaw = 0.0f, float pitch = 0.0f)
        {
            lock (this)
            {
                Functions.vp_float_set(_instance, Attribute.MyX, x);
                Functions.vp_float_set(_instance, Attribute.MyY, y);
                Functions.vp_float_set(_instance, Attribute.MyZ, z);
                Functions.vp_float_set(_instance, Attribute.MyYaw, yaw);
                Functions.vp_float_set(_instance, Attribute.MyPitch, pitch);
                return new TResult
                {
                    Rc = Functions.vp_state_change(_instance)
                };

            }
        }

        public TResult UpdateAvatar(TVector3 position)
        {
            return UpdateAvatar(position.X, position.Y, position.Z);
        }

        public TResult UpdateAvatar(TVector3 position, TVector3 rotation)
        {
            return UpdateAvatar(position.X, position.Y, position.Z,rotation.X,rotation.Y);
        }

        public TResult AvatarClick(int session)
        {
            return new TResult {Rc = Functions.vp_avatar_click(_instance,session)};
        }

        public TResult AvatarClick(TAvatar avatar)
        {
            return new TResult { Rc = Functions.vp_avatar_click(_instance, avatar.Session) };
        }

        #endregion

        #region IChatFunctions Implementations

        virtual public TResult Say(string message)
        {
            lock (this)
            {
                return new TResult {Rc = Functions.vp_say(_instance, message)};
            }
        }

        public TResult ConsoleMessage(int targetSession, string name, string message, TextEffectTypes effects = (TextEffectTypes) 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            return new TResult { Rc = Functions.vp_console_message(_instance, targetSession, name, message, (int)effects, red, green, blue) };
        }

        public TResult ConsoleMessage(TAvatar avatar, string name, string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, avatar.Session, name, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(int targetSession, string name, string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, targetSession, name, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(string name, string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, 0, name, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(string message, TColor color, TextEffectTypes effects = (TextEffectTypes) 0)
        {
            if (color == null)
                color = new TColor();
            return new TResult { Rc = Functions.vp_console_message(_instance, 0, string.Empty, message, (int)effects, color.R, color.G, color.B) };
        }

        public TResult ConsoleMessage(string message)
        {
            return new TResult { Rc = Functions.vp_console_message(_instance, 0, string.Empty, message, 0, 0, 0, 0) };
        }

        virtual public TResult ConsoleMessage(TAvatar avatar, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0)
        {
            return new TResult { Rc = Functions.vp_console_message(_instance, avatar.Session, name, message, (int)effects, red, green, blue) };
        }
        #endregion

        #endregion

        #region Events

        private readonly Dictionary<Events, EventDelegate> _nativeEvents = new Dictionary<Events, EventDelegate>();
        private readonly Dictionary<Callbacks, CallbackDelegate> _nativeCallbacks = new Dictionary<Callbacks, CallbackDelegate>();

      



        private void SetNativeEvent(Events eventType, EventDelegate eventFunction)
        {
            _nativeEvents[eventType] = eventFunction;
            Functions.vp_event_set(_instance, (int)eventType, eventFunction);
        }

        private void SetNativeCallback(Callbacks callbackType, CallbackDelegate callbackFunction)
        {
            _nativeCallbacks[callbackType] = callbackFunction;
            Functions.vp_callback_set(_instance, (int)callbackType, callbackFunction);
        }

        //public delegate void Event(T sender);
        public delegate void ChatMessageDelegate(T sender, TChatMessageEventArgs args);

        public delegate void AvatarChangeDelegate(T sender, TAvatarChangeEventArgs args);
        public delegate void AvatarEnterDelegate(T sender, TAvatarEnterEventArgs args);
        public delegate void AvatarLeaveDelegate(T sender, TAvatarLeaveEventArgs args);

        public delegate void TeleportDelegate(T sender, TTeleportEventArgs args);

        public delegate void WorldListEventDelegate(T sender, TWorldListEventargs args);

        public delegate void ObjectCreateDelegate(T sender, TObjectCreateArgs args);
        public delegate void ObjectChangeDelegate(T sender, TObjectChangeArgs args);
        public delegate void ObjectDeleteDelegate(T sender, TObjectDeleteArgs args);
        public delegate void ObjectClickDelegate(T sender, TObjectClickArgs args);


        public delegate void ObjectCreateCallback(T sender, TObjectCreateCallbackArgs args);
        public delegate void ObjectChangeCallback(T sender, TObjectChangeCallbackArgs args);
        public delegate void ObjectDeleteCallback(T sender, TObjectDeleteCallbackArgs args);

        public delegate void QueryCellResultDelegate(T sender, TQueryCellResultArgs args);
        public delegate void QueryCellEndDelegate(T sender, TQueryCellEndArgs args);

        public delegate void WorldSettingsChangedDelegate(T sender, TWorldSettingsChangedEventArg args);
        public delegate void WorldDisconnectDelegate(T sender, TWorldDisconnectEventArg args);

        public delegate void UniverseDisconnectDelegate(T sender, TUniverseDisconnectEventargs args);

        public delegate void FriendAddCallbackDelegate(T sender, TFriendAddCallbackEventArgs args);
        public delegate void FriendDeleteCallbackDelegate(T sender, TFriendDeleteCallbackEventArgs args);
        public delegate void FriendsGetCallbackDelegate(T sender, TFriendsGetCallbackEventArgs args);

        public event ChatMessageDelegate OnChatMessage;
        public event AvatarEnterDelegate OnAvatarEnter;
        public event AvatarChangeDelegate OnAvatarChange;
        public event AvatarLeaveDelegate OnAvatarLeave;

        public event TeleportDelegate OnTeleport;

        public event ObjectCreateDelegate OnObjectCreate;
        public event ObjectChangeDelegate OnObjectChange;
        public event ObjectDeleteDelegate OnObjectDelete;
        public event ObjectClickDelegate OnObjectClick;

        public event ObjectCreateCallback OnObjectCreateCallback;
        public event ObjectDeleteCallback OnObjectDeleteCallback;
        public event ObjectChangeCallback OnObjectChangeCallback;

        public event WorldListEventDelegate OnWorldList;
        public event WorldSettingsChangedDelegate OnWorldSettingsChanged;
        public event FriendAddCallbackDelegate OnFriendAddCallback;
        public event FriendDeleteCallbackDelegate OnFriendDeleteCallback;
        public event FriendsGetCallbackDelegate OnFriendsGetCallback;

        public event WorldDisconnectDelegate OnWorldDisconnect;
        public event UniverseDisconnectDelegate OnUniverseDisconnect;

        public event QueryCellResultDelegate OnQueryCellResult;
        public event QueryCellEndDelegate OnQueryCellEnd;

        /* Events indirectly assosicated with VP */

        public delegate void WorldEnterDelegate(T sender, TWorldEnterEventArgs args);
        public event WorldEnterDelegate OnWorldEnter;
        public delegate void WorldLeaveDelegate(T sender, TWorldLeaveEventArgs args);
        public event WorldLeaveDelegate OnWorldLeave;

        #endregion

        #region CallbackHandlers

        private void OnObjectCreateCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                var vpObject = _objectReferences[reference];
                _objectReferences.Remove(reference);
                if (OnObjectCreateCallback != null)
                {
                    vpObject.Id = Functions.vp_int(sender, Attribute.ObjectId);
                    OnObjectCreateCallback(Implementor, new TObjectCreateCallbackArgs { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        private void OnObjectChangeCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                var vpObject = _objectReferences[reference];
                _objectReferences.Remove(reference);

                if (OnObjectChangeCallback != null)
                {
                    vpObject.Id = Functions.vp_int(sender, Attribute.ObjectId);
                    OnObjectChangeCallback(Implementor, new TObjectChangeCallbackArgs { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        private void OnObjectDeleteCallbackNative(IntPtr sender, int rc, int reference)
        {
            lock (this)
            {
                var vpObject = _objectReferences[reference];
                _objectReferences.Remove(reference);

                if (OnObjectDeleteCallback != null)
                {
                    OnObjectDeleteCallback(Implementor, new TObjectDeleteCallbackArgs { Result = new TResult { Rc = rc }, VpObject = vpObject });
                }
            }
        }

        #endregion

        #region Event handlers

        private void OnTeleportNative(IntPtr sender)
        {
            if (OnTeleport == null)
                return;
            TTeleport teleport;
            lock (this)
            {
                teleport = new TTeleport
                    {
                        Avatar = GetAvatar(Functions.vp_int(sender, Attribute.AvatarSession)),
                        Position = new TVector3
                            {
                                X = Functions.vp_float(sender, Attribute.TeleportX),
                                Y = Functions.vp_float(sender, Attribute.TeleportY),
                                Z = Functions.vp_float(sender, Attribute.TeleportZ)
                            },
                        Rotation = new TVector3
                            {
                                X = Functions.vp_float(sender, Attribute.TeleportPitch),
                                Y = Functions.vp_float(sender, Attribute.TeleportYaw),
                                Z = 0 /* Roll not implemented yet */
                            },
                            // TODO: maintain user count and world state statistics.
                        World = new TWorld { Name = Functions.vp_string(sender, Attribute.TeleportWorld),State = WorldState.Unknown, UserCount=-1 }

                    };
            }
            OnTeleport(Implementor, new TTeleportEventArgs{Teleport = teleport});
        }

        private void OnGetFriendsCallbackNative(IntPtr sender, int rc, int reference)
        {
            if (OnFriendAddCallback == null)
                return;
            lock (this)
            {
                var friend = new TFriend
                    {
                        UserId = Functions.vp_int(sender,Attributes.UserId),
                        Id = Functions.vp_int(sender,Attributes.FriendId),
                        Name = Functions.vp_string(sender,Attributes.FriendName),
                       Online = Functions.vp_int(sender,Attributes.FriendOnline)==1
                    };
                OnFriendsGetCallback(Implementor,new TFriendsGetCallbackEventArgs{Friend=friend});
            }
        }


        private void OnFriendDeleteCallbackNative(IntPtr sender, int rc, int reference)
        {
            // todo: implement this.
        }

        private void OnFriendAddCallbackNative(IntPtr sender, int rc, int reference)
        {
            // todo: implement this.
        }


        private void OnChatNative(IntPtr sender)
        {
            TChatMessageEventArgs data;
            lock (this)
            {
                if (!_avatars.ContainsKey(Functions.vp_int(sender, Attribute.AvatarSession)))
                {
                    var avatar = new TAvatar
                        {
                            Name = Functions.vp_string(sender, Attribute.AvatarName),
                            Session = Functions.vp_int(sender, Attribute.AvatarSession)
                        };
                    _avatars.Add(avatar.Session, avatar);
                }
                

                if (OnChatMessage == null) return;
                data = new TChatMessageEventArgs
                    {
                        Avatar = _avatars[Functions.vp_int(sender, Attribute.AvatarSession)].Copy(),
                        ChatMessage = new TChatMessage
                            {
                                Type = (ChatMessageTypes)Functions.vp_int(sender, Attribute.ChatType),
                                Message = Functions.vp_string(sender, Attribute.ChatMessage),
                                Name = Functions.vp_string(sender, Attribute.AvatarName),
                                TextEffectTypes = (TextEffectTypes)Functions.vp_int(sender,Attributes.ChatEffects)
                            }
                    };
                if (data.ChatMessage.Type == ChatMessageTypes.Console)
                {
                    data.ChatMessage.Color = new TColor
                                                 {
                                                     R = (byte) Functions.vp_int(sender, Attribute.ChatRolorRed),
                                                     G = (byte) Functions.vp_int(sender, Attribute.ChatColorGreen),
                                                     B = (byte) Functions.vp_int(sender, Attribute.ChatColorBlue)
                                                 };
                }
                else
                {
                    data.ChatMessage.Color = new TColor
                    {
                        R = 0,
                        G = 0,
                        B = 0
                    };
                }
            }
            OnChatMessage(Implementor, data);
        }

        private void OnAvatarAddNative(IntPtr sender)
        {
            TAvatar data;
            lock (this)
            {
                data = new TAvatar {UserId=Functions.vp_int(sender, Attribute.UserId),
                Name = Functions.vp_string(sender, Attribute.AvatarName),
                Session=Functions.vp_int(sender, Attribute.AvatarSession),
                AvatarType=Functions.vp_int(sender, Attribute.AvatarType),
                Position=new TVector3{X=Functions.vp_float(sender, Attribute.AvatarX),
                            Y=Functions.vp_float(sender, Attribute.AvatarY),
                            Z=Functions.vp_float(sender, Attribute.AvatarZ)},
                Rotation=new TVector3{X=Functions.vp_float(sender, Attribute.AvatarPitch),
                            Y=Functions.vp_float(sender, Attribute.AvatarYaw),
                            Z=0 /* roll currently not supported*/}};
                if (!_avatars.ContainsKey(data.Session))
                    _avatars.Add(data.Session, data);
            }
            if (OnAvatarEnter == null) return;
            OnAvatarEnter(Implementor, new TAvatarEnterEventArgs { Avatar = data.Copy() });
        }

        private void OnAvatarChangeNative(IntPtr sender)
        {
            TAvatar data;
            lock (this)
            {
                data = new TAvatar{UserId=_avatars[Functions.vp_int(sender, Attribute.AvatarSession)].UserId, Name=Functions.vp_string(sender, Attribute.AvatarName),
                                  Session=Functions.vp_int(sender, Attribute.AvatarSession),
                                  AvatarType=Functions.vp_int(sender, Attribute.AvatarType),
                                  Position=new TVector3{X=Functions.vp_float(sender, Attribute.AvatarX),
                                  Y=Functions.vp_float(sender, Attribute.AvatarY),
                                  Z=Functions.vp_float(sender, Attribute.AvatarZ)},
               Rotation=new TVector3{X=Functions.vp_float(sender, Attribute.AvatarPitch),
                            Y=Functions.vp_float(sender, Attribute.AvatarYaw),
                            Z=0 /* roll currently not supported*/}};            
                setAvatar(new Avatar<Vector3>().CopyFrom(data));
            }
            if (OnAvatarChange != null)
                OnAvatarChange(Implementor, new TAvatarChangeEventArgs { Avatar = data.Copy() });
        }

        private void OnAvatarDeleteNative(IntPtr sender)
        {
            TAvatar data;
            lock (this)
            {
                data = _avatars[Functions.vp_int(sender, Attribute.AvatarSession)];
                _avatars.Remove(data.Session);
            }
            if (OnAvatarLeave == null) return;
            OnAvatarLeave(Implementor, new TAvatarLeaveEventArgs { Avatar = data.Copy() });
        }

        private void OnObjectClickNative(IntPtr sender)
        {
            if (OnObjectClick == null) return;
            int session;
            int objectId;
            TVector3 world;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                objectId = Functions.vp_int(sender, Attribute.ObjectId);
                world = new TVector3
                    {
                        X = Functions.vp_float(sender, Attribute.ClickHitX),
                        Y = Functions.vp_float(sender, Attribute.ClickHitY),
                        Z = Functions.vp_float(sender, Attribute.ClickHitZ)
                    };
            }

            OnObjectClick(Implementor,
                          new TObjectClickArgs
                              {WorldHit=world, Avatar = _avatars[session].Copy(), VpObject = new TVpObject {Id = objectId}});
        }

        private void OnObjectDeleteNative(IntPtr sender)
        {
            if (OnObjectDelete == null) return;
            int session;
            int objectId;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                objectId = Functions.vp_int(sender, Attribute.ObjectId);
            }
            OnObjectDelete(Implementor, new TObjectDeleteArgs{Avatar=_avatars[session].Copy(),VpObject = new TVpObject { Id = objectId }});
        }

        private void OnObjectCreateNative(IntPtr sender)
        {
            if (OnObjectCreate == null && OnQueryCellResult == null) return;
            TVpObject vpObject;
            int session;
            lock (this)
            {
                session = Functions.vp_int(sender, Attribute.AvatarSession);
                vpObject = new TVpObject

                    {
                        Action = Functions.vp_string(sender, Attribute.ObjectAction),
                        Description = Functions.vp_string(sender, Attribute.ObjectDescription),
                        Id = Functions.vp_int(sender, Attribute.ObjectId),
                        Model = Functions.vp_string(sender, Attribute.ObjectModel),
                        Rotation = new TVector3 { X = Functions.vp_float(sender, Attribute.ObjectRotationX), Y = Functions.vp_float(sender, Attribute.ObjectRotationY), Z = Functions.vp_float(sender, Attribute.ObjectRotationZ) },
                        Time =
                            new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Functions.vp_int(sender,
                                                                                          Attribute.
                                                                                              ObjectTime)),
                        ObjectType = Functions.vp_int(sender, Attribute.ObjectType),
                        Owner = Functions.vp_int(sender, Attribute.ObjectUserId),
                        Position = new TVector3 { X = Functions.vp_float(sender, Attribute.ObjectX), Y = Functions.vp_float(sender, Attribute.ObjectY), Z = Functions.vp_float(sender, Attribute.ObjectZ) },
                        Angle = Functions.vp_float(sender, Attribute.ObjectRotationAngle)
                    };

            }
            if (session == -1 && OnQueryCellResult != null)
                OnQueryCellResult(Implementor, new TQueryCellResultArgs{VpObject=vpObject.Copy()});
            else
                if (OnObjectCreate != null)
                    OnObjectCreate(Implementor, new TObjectCreateArgs { Avatar =  _avatars[session].Copy(), VpObject = vpObject.Copy() });
        }

        public List<TAvatar> Avatars()
        {
            return _avatars.Values.ToList().Copy();
        } 

        public void Commit(TAvatar avatar)
        {
            lock (_avatars)
            {
                _avatars[avatar.Session].CopyFrom(avatar);
            }
        }
    
        public TAvatar GetAvatar(int session)
        {
            if (_avatars.ContainsKey(session))
                return _avatars[session];
            var avatar = new TAvatar { Session = session };
            _avatars.Add(session, avatar);
            return avatar.Copy();
        }

        private void setAvatar(Avatar<Vector3> avatar)
        {
            lock(this)
            {
                if (_avatars.ContainsKey(avatar.Session))
                {
                    _avatars[avatar.Session] = new TAvatar().CopyFrom(avatar);
                }
                else
                {
                    _avatars[avatar.Session] = new TAvatar().CopyFrom(avatar);
                }
            }
        }

        private void OnObjectChangeNative(IntPtr sender)
        {
            if (OnObjectChange == null) return;
            TVpObject vpObject;
            int sessionId;
            lock (this)
            {
                vpObject = new TVpObject
                    {
                        Action = Functions.vp_string(sender, Attribute.ObjectAction),
                        Description = Functions.vp_string(sender, Attribute.ObjectDescription),
                        Id = Functions.vp_int(sender, Attribute.ObjectId),
                        Model = Functions.vp_string(sender, Attribute.ObjectModel),

                        Rotation = new TVector3
                        {
                            X = Functions.vp_float(sender, Attribute.ObjectRotationX),
                            Y = Functions.vp_float(sender, Attribute.ObjectRotationY),
                            Z = Functions.vp_float(sender, Attribute.ObjectRotationZ)
                        },

                        Time =
                            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(
                                Functions.vp_int(sender, Attribute.ObjectTime)),
                        ObjectType = Functions.vp_int(sender, Attribute.ObjectType),
                        Owner = Functions.vp_int(sender, Attribute.ObjectUserId),
                        Position = new TVector3
                            {
                                X = Functions.vp_float(sender, Attribute.ObjectX),
                                Y = Functions.vp_float(sender, Attribute.ObjectY),
                                Z = Functions.vp_float(sender, Attribute.ObjectZ)
                            },
                        Angle = Functions.vp_float(sender, Attribute.ObjectRotationAngle)
                    };
                sessionId = Functions.vp_int(sender, Attribute.AvatarSession);
            }
            OnObjectChange(Implementor, new TObjectChangeArgs { Avatar = GetAvatar(sessionId).Copy(), VpObject = vpObject });
        }

        private void OnQueryCellEndNative(IntPtr sender)
        {
            if (OnQueryCellEnd == null) return;
            int x;
            int z;
            lock (this)
            {
                x = Functions.vp_int(sender, Attribute.CellX);
                z = Functions.vp_int(sender, Attribute.CellZ);
            }
            OnQueryCellEnd(Implementor, new TQueryCellEndArgs{Cell=new TCell{X=x,Z=z}});
        }

        private void OnWorldListNative(IntPtr sender)
        {
            if (OnWorldList == null)
                return;

            TWorld data;
            lock (this)
            {
                string worldName = Functions.vp_string(_instance, Attribute.WorldName);
                data = new TWorld
                {
                    Name = worldName,
                    State = (WorldState)Functions.vp_int(_instance, Attribute.WorldState),
                    UserCount = Functions.vp_int(_instance, Attribute.WorldUsers)
                };
            }
            if (_worlds.ContainsKey(data.Name))
                _worlds.Remove(data.Name);
            _worlds.Add(data.Name,data);
            OnWorldList(Implementor,new TWorldListEventargs{ World=data.Copy()});
        }

        private void OnWorldSettingNativeEvent(IntPtr instance)
        {
            if (!_worlds.ContainsKey(Configuration.World.Name))
            {
                _worlds.Add(Configuration.World.Name,Configuration.World);
            }
            var world = _worlds[Configuration.World.Name];
            var key = Functions.vp_string(instance, Attributes.WorldSettingKey);
            var value = Functions.vp_string(instance, Attributes.WorldSettingValue);
            world.RawAttributes[key] = value;
        }

        private void OnWorldSettingsChangedNativeEvent(IntPtr instance)
        {
            // Initialize World Object Cache if a local object path has been specified and a objectpath is speficied in the world attributes.
            // TODO: some world, such as Test do not specify a objectpath, maybe there's a default search path we dont know of.
            var world = _worlds[Configuration.World.Name];
            if (!string.IsNullOrEmpty(world.LocalCachePath) && world.RawAttributes.ContainsKey("objectpath"))
            {
                ModelCacheProvider = new OpCacheProvider(_worlds[Configuration.World.Name].RawAttributes["objectpath"],world.LocalCachePath);
            }
            if (OnWorldSettingsChanged != null)
                OnWorldSettingsChanged(Implementor, new TWorldSettingsChangedEventArg() { World = _worlds[Configuration.World.Name].Copy()});
        }

        private void OnUniverseDisconnectNative(IntPtr sender)
        {
            if (OnUniverseDisconnect == null) return;
            OnUniverseDisconnect(Implementor,new TUniverseDisconnectEventargs{Universe = Universe});
        }

        private void OnWorldDisconnectNative(IntPtr sender)
        {
            if (OnWorldDisconnect == null) return;
            OnWorldDisconnect(Implementor,new TWorldDisconnectEventArg{World=World});
        }

        #endregion

        #region Cleanup

        public void ReleaseEvents()
        {
            lock (this)
            {
                OnChatMessage = null;
                OnAvatarEnter = null;
                OnAvatarChange = null;
                OnAvatarLeave = null;
                OnObjectCreate = null;
                OnObjectChange = null;
                OnObjectChangeCallback = null;
                OnObjectDelete = null;
                OnObjectClick = null;
                OnWorldList = null;
                OnWorldDisconnect = null;
                OnWorldSettingsChanged = null;
                OnWorldDisconnect = null;
                OnUniverseDisconnect = null;
                //OnUserAttributes = null;
                OnQueryCellResult = null;
                OnQueryCellEnd = null;
                OnFriendAddCallback = null;
                OnFriendDeleteCallback = null;
                OnFriendsGetCallback = null;
            }
        }

        public void Dispose()
        {
            _isDisposing = true;
            if (_instance != IntPtr.Zero)
            {
                if (Configuration.IsChildInstance)
                    return;
                Functions.vp_destroy(_instance);
            }
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Friend Functions

        public TResult GetFriends()
        {
            return new TResult {Rc = Functions.vp_friends_get(_instance)};
        }

        public TResult AddFriendByName(TFriend friend)
        {
            return new TResult { Rc = Functions.vp_friend_add_by_name(_instance,friend.Name) };
        }

        public TResult AddFriendByName(string name)
        {
            return new TResult { Rc = Functions.vp_friend_add_by_name(_instance, name) };
        }

        public TResult DeleteFriendById(int friendId)
        {
            return new TResult { Rc = Functions.vp_friend_delete(_instance,friendId) };
        }

        public TResult DeleteFriendById(TFriend friend)
        {
            return new TResult { Rc = Functions.vp_friend_delete(_instance, friend.Id) };
        }

        #endregion

        #region ITerrainFunctions Implementation

        public TResult TerrianQuery(int tileX, int tileZ, int[,] nodes)
        {
            return new TResult { Rc = Functions.vp_terrain_query(_instance, tileX,tileZ,nodes) };
        }

        public TResult SetTerrainNode(int tileX, int tileZ, int nodeX, int nodeZ, TerrainCell[,] cells)
        {
            return new TResult { Rc = Functions.vp_terrain_node_set(_instance, tileX, tileZ, nodeX, nodeZ, cells) };

        }

        #endregion

        #region Implementation of IInstanceEvents

        override internal event EventDelegate OnChatNativeEvent;
        override internal event EventDelegate OnAvatarAddNativeEvent;
        override internal event EventDelegate OnAvatarDeleteNativeEvent;
        override internal event EventDelegate OnAvatarChangeNativeEvent;
        override internal event EventDelegate OnWorldListNativeEvent;
        override internal event EventDelegate OnObjectChangeNativeEvent;
        override internal event EventDelegate OnObjectCreateNativeEvent;
        override internal event EventDelegate OnObjectDeleteNativeEvent;
        override internal event EventDelegate OnObjectClickNativeEvent;
        override internal event EventDelegate OnQueryCellEndNativeEvent;
        override internal event EventDelegate OnUniverseDisconnectNativeEvent;
        override internal event EventDelegate OnWorldDisconnectNativeEvent;
        override internal event EventDelegate OnTeleportNativeEvent;
        override internal event CallbackDelegate OnObjectCreateCallbackNativeEvent;
        override internal event CallbackDelegate OnObjectChangeCallbackNativeEvent;
        override internal event CallbackDelegate OnObjectDeleteCallbackNativeEvent;
        override internal event CallbackDelegate OnFriendAddCallbackNativeEvent;
        override internal event CallbackDelegate OnFriendDeleteCallbackNativeEvent;
        override internal event CallbackDelegate OnGetFriendsCallbackNativeEvent;

        #endregion



        #region Implementation of IAvatarFunctions<out TResult,TAvatar,in TVector3>

        Dictionary<int, TAvatar> IAvatarFunctions<TResult, TAvatar, TVector3>.Avatars { get; set; }

        #endregion
    }
}
