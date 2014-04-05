using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using VpNet.Tools;

namespace VpNet.GameExtensions
{
    [Serializable]
    public class GameHud
    {
        private readonly GameInstance _instance;
        private readonly IHubProxy _proxy;
        private readonly string _url;

        public delegate void OnHudInitializedDelegate(GameInstance sender, HudInitializationArgs args);
        public event OnHudInitializedDelegate OnHudIntialized;

        public delegate void OnTopMenuClickDelegate(GameInstance sender, TopMenuClickArgs args);
        public event OnTopMenuClickDelegate OnTopMenuClick;

        private Dictionary<string,int>  _avatarSessionTicets = new Dictionary<string, int>();

        public GameHud(GameInstance instance, IHubProxy proxy, string url)
        {
            _instance = instance;
            _proxy = proxy;
            _url = url;
        }

        /// <summary>
        /// Initializes the hud for the specified avatar. It will create a temporary ticket-id for authentication purposes.
        /// The client browser will send an event back over websockets when it has initialized.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        public void Initialize(GameAvatar avatar)
        {
            var guid = ShortGuid.NewGuid();
            _avatarSessionTicets.Add(guid,avatar.Session);
           _instance.UrlSendOverlay(avatar, _url + "?" + guid);
        }

        /// <summary>
        /// Sends the top menu configuration to an avatar
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="menu">The menu.</param>
        public void TopMenuSet(GameAvatar avatar, IEnumerable<GameHudMenu> menu)
        {
        }

        /// <summary>
        /// Sends the top menu configuration to all avatars
        /// </summary>
        /// <param name="menu">The menu.</param>
        public void TopMenuSet(IEnumerable<GameHudMenu> menu)
        {
        }

        /// <summary>
        /// Set Top menu visibility for specific avatar.
        /// </summary>
        /// <param name="avatar">The avatar.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void TopMenuVisibility(GameAvatar avatar, bool isVisible)
        {
        }

        /// <summary>
        /// Set Top menu visibility for all avatars.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        public void TopMenuVisibility(bool isVisible)
        {
        }
    }

    public class TopMenuClickArgs
    {
        public int MenuId { get; set; }
        public GameAvatar Avatar { get; set; }
    }
}
