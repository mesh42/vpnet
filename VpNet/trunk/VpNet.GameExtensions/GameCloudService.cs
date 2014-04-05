using System;
using Microsoft.AspNet.SignalR.Client;
using VpNet.GameExtensions.Interfaces;
using VpNet.GameExtensions.Interfaces.Hubs;

namespace VpNet.GameExtensions
{
    /// <summary>
    /// hookup to the Game Cloud Service via SignalR.
    /// </summary>
    [Serializable]
    public class GameCloudService
    {
        private readonly GameInstance _implementor;
        private readonly string _url;
        private readonly string _hudServiceUrl;
        private HubConnection _connection;
        private IHubProxy _hudProxy;

        public delegate void OnConnectedDelegate(GameInstance sender, GameCloudServiceConnectionArgs args);
        public delegate void OnConfigurationReceived<T>(GameInstance sender, GameCloudConfiguration<T> args);
        public event OnConnectedDelegate OnConnected;
        public event OnConfigurationReceived<GameWeapon> OnReceivedGameWeapons;
        public event OnConfigurationReceived<GameAchievement> OnReceivedGameAchievements;

        public GameHud Hud { get; private set; }

        public GameCloudService(GameInstance implementor, string url, string hudServiceUrl)
        {
            _implementor = implementor;
            _url = url;
            _hudServiceUrl = hudServiceUrl;
        }

        public void StartService(string apiPrivateKey, string gamePublicKey)
        {
            _connection = new HubConnection(_url, true);
            _hudProxy = _connection.CreateHubProxy("hudControllerHub");
            var proxy = _connection.CreateHubProxy("gameWeaponsHub");
            var proxy2 = _connection.CreateHubProxy("gameAchievementsHub");
            Hud = new GameHud(_implementor, _hudProxy,_url);
            _connection.Start().ContinueWith(continuation =>
            {
                if (OnConnected != null)
                    OnConnected(_implementor, new GameCloudServiceConnectionArgs() { IsSuccess = !continuation.IsFaulted });
                _hudProxy.Invoke<bool>("Connect", new object[] { apiPrivateKey, gamePublicKey }).ContinueWith(task1 =>
                {
                    if (OnHudIntialized != null)
                        OnHudIntialized(_implementor, new HudInitializationArgs(){IsSuccess = task1.Result});
                });
                proxy.Invoke<HubQueryableResult<GameWeapon>>("Query", new object[] { "1", "2" }).ContinueWith(task2 =>
                {
                    if (OnReceivedGameWeapons != null)
                        OnReceivedGameWeapons(_implementor, new GameCloudConfiguration<GameWeapon>() { IsSuccess = !task2.IsFaulted, Records = task2.Result });
                });
                proxy2.Invoke<HubQueryableResult<GameWeapon>>("Query", new object[] { "1", "2" }).ContinueWith(task3 =>
                {
                    if (OnReceivedGameAchievements != null)
                        OnReceivedGameAchievements(_implementor, new GameCloudConfiguration<GameAchievement>() { IsSuccess = !task3.IsFaulted, Records = task3.Result });
                });

            });
        }

        public event GameHud.OnHudInitializedDelegate OnHudIntialized;
    }

    public class HudInitializationArgs
    {
        public bool IsSuccess { get; internal set; }
        public GameAvatar Avatar { get; internal set; }

    }

    public class GameCloudConfiguration<T>
    {
        public bool IsSuccess { get; internal set; }
        public HubQueryableResult<GameWeapon> Records;
    }
}
       