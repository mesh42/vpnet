using System;
using VpNet.Interfaces;
using VpNet.NativeApi;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseInstanceEvents<TWorld>
        where TWorld : class, IWorld, new()
    {
        internal IntPtr _instance;
        //internal InstanceConfiguration<TWorld> _configuration;
        public InstanceConfiguration<TWorld> Configuration { get; set; }

        #region Implementation of IInstanceEvents

        public abstract event EventDelegate OnChatNativeEvent;
        public abstract event EventDelegate OnAvatarAddNativeEvent;
        public abstract event EventDelegate OnAvatarDeleteNativeEvent;
        public abstract event EventDelegate OnAvatarChangeNativeEvent;
        public abstract event EventDelegate OnWorldListNativeEvent;
        public abstract event EventDelegate OnObjectChangeNativeEvent;
        public abstract event EventDelegate OnObjectCreateNativeEvent;
        public abstract event EventDelegate OnObjectDeleteNativeEvent;
        public abstract event EventDelegate OnObjectClickNativeEvent;
        public abstract event EventDelegate OnQueryCellEndNativeEvent;
        public abstract event EventDelegate OnUniverseDisconnectNativeEvent;
        public abstract event EventDelegate OnWorldDisconnectNativeEvent;
        public abstract event EventDelegate OnTeleportNativeEvent;
        public abstract event CallbackDelegate OnObjectCreateCallbackNativeEvent;
        public abstract event CallbackDelegate OnObjectChangeCallbackNativeEvent;
        public abstract event CallbackDelegate OnObjectDeleteCallbackNativeEvent;
        public abstract event CallbackDelegate OnFriendAddCallbackNativeEvent;
        public abstract event CallbackDelegate OnFriendDeleteCallbackNativeEvent;
        public abstract event CallbackDelegate OnGetFriendsCallbackNativeEvent;

        #endregion
    }
}