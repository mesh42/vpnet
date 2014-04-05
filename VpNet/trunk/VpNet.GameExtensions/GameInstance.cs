#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2014 CUBE3 (Cit:36)

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
using System.Xml.Serialization;

using VpNet.Abstract;

namespace VpNet.GameExtensions
{
    [Serializable]
    [XmlRoot("Instance", Namespace = Global.XmlNsInstance)]
    public class GameInstance : BaseInstanceT<GameInstance,
        GameAvatar,
        Color,
        Friend,
        RcDefault,
        TerrainCell,
        TerrainNode,
        TerrainTile,
        Vector3,
        GameVpObject,
        World,
        WorldAttributes,
        Cell,
        ChatMessage,
        Terrain,
        Universe,
        Teleport<World, GameAvatar, Vector3>,
        UserAttributes,
        GameAvatarChangeEventArgs,
        GameAvatarEnterEventArgs,
        GameAvatarLeaveEventArgs,
        GameAvatarClickEventArgs,
        QueryCellResultArgsT<GameVpObject, Vector3>,
        QueryCellEndArgs,
        ChatMessageEventArgsT<GameAvatar, ChatMessage, Vector3, Color>,
        FriendAddCallbackEventArgs,
        FriendDeleteCallbackEventArgs,
        FriendsGetCallbackEventArgs,
        TerrainNodeEventArgs,
        UniverseDisconnectEventArgs,
        ObjectChangeArgsT<GameAvatar, GameVpObject, Vector3>,
        ObjectChangeCallbackArgsT<RcDefault, GameVpObject, Vector3>,
        ObjectClickArgsT<GameAvatar, GameVpObject, Vector3>,
        ObjectCreateArgsT<GameAvatar, GameVpObject, Vector3>,
        ObjectCreateCallbackArgsT<RcDefault, GameVpObject, Vector3>,
        ObjectDeleteArgsT<GameAvatar, GameVpObject, Vector3>,
        ObjectDeleteCallbackArgsT<RcDefault, GameVpObject, Vector3>,
        ObjectGetCallbackArgsT<RcDefault, GameVpObject, Vector3>,
        WorldDisconnectEventArgs,
        WorldListEventArgs,
        WorldSettingsChangedEventArgs,
        TeleportEventArgsT<Teleport<World, GameAvatar, Vector3>, World, GameAvatar, Vector3>,
        WorldEnterEventArgsT<World>,
        WorldLeaveEventArgsT<World>,
        UserAttributesEventArgsT<UserAttributes>
        >
    {
        private readonly string _gameCloudServiceUrl;
        private readonly string _hudServiceUrl;
        public GameInstanceConfiguration GameInstanceConfiguration { get; private set; }
        public GameCloudService Cloud { get; private set; }
        public GameObjectManager GameObjectManager { get; private set; }

        public GameInstance(string userName, string password, string botName, string world,
            string gameCloudServiceUrl = "http://localhost/Bootstrap", string storageDataPath = ".\\GameData", 
            string hudServiceUrl = "http://localhost/Bootstrap/VpHud")
        {
            _gameCloudServiceUrl = gameCloudServiceUrl;
            _hudServiceUrl = hudServiceUrl;
            Implementor = this;
            GameInstanceConfiguration = new GameInstanceConfiguration {StorageDataPath = storageDataPath};
            Configuration = new InstanceConfiguration<World>(false)
            {
                BotName = botName,
                Password = password,
                UserName = userName,
                World = new World() { Name = world }
            };
            GameObjectManager = new GameObjectManager(this);
            Cloud = new GameCloudService(this,gameCloudServiceUrl,hudServiceUrl);
            AutoWaitTimerMs = 1;
            UseAutoWaitTimer = true;
        }
        public GameInstance() { }
    }
}
