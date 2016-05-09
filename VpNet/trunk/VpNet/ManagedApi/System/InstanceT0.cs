﻿#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2016 CUBE3 (Cit:36)

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
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    [XmlRoot("Instance", Namespace = Global.XmlNsInstance)]
    public class Instance : BaseInstanceT<Instance,
        Avatar<Vector3>, 
        Color, 
        Friend, 
        RcDefault, 
        TerrainCell, 
        TerrainNode, 
        TerrainTile,
        Vector3, 
        VpObject<Vector3>, 
        World, 
        WorldAttributes,
        Cell,
        ChatMessage,
        Terrain,
        Universe,
        Teleport<World,Avatar<Vector3>,Vector3>,
        UserAttributes,
        IHud<Avatar<Vector3>,Vector3>,
        AvatarChangeEventArgsT<Avatar<Vector3>,Vector3>,
        AvatarEnterEventArgsT<Avatar<Vector3>,Vector3>,
        AvatarLeaveEventArgsT<Avatar<Vector3>,Vector3>,
        AvatarClickEventArgsT<Avatar<Vector3>, Vector3>,
        QueryCellResultArgsT<VpObject<Vector3>, Vector3>,
        QueryCellEndArgs,
        ChatMessageEventArgsT<Avatar<Vector3>,ChatMessage,Vector3,Color>,
        FriendAddCallbackEventArgs,
        FriendDeleteCallbackEventArgs,
        FriendsGetCallbackEventArgs,
        TerrainNodeEventArgs,
        UniverseDisconnectEventArgs,
        ObjectChangeArgsT<Avatar<Vector3>,VpObject<Vector3>,Vector3>,
        ObjectChangeCallbackArgsT<RcDefault,VpObject<Vector3>,Vector3>,
        ObjectClickArgsT<Avatar<Vector3>, VpObject<Vector3>, Vector3>,
        ObjectCreateArgsT<Avatar<Vector3>, VpObject<Vector3>, Vector3>,
        ObjectCreateCallbackArgsT<RcDefault, VpObject<Vector3>, Vector3>,
        ObjectDeleteArgsT<Avatar<Vector3>, VpObject<Vector3>, Vector3>,
        ObjectDeleteCallbackArgsT<RcDefault, VpObject<Vector3>, Vector3>,
        ObjectGetCallbackArgsT<RcDefault, VpObject<Vector3>, Vector3>,
        ObjectBumpArgsT<Avatar<Vector3>,VpObject<Vector3>,Vector3>,
        WorldDisconnectEventArgs,
        WorldListEventArgs,
        WorldSettingsChangedEventArgs,
        TeleportEventArgsT<Teleport<World,Avatar<Vector3>,Vector3>,World,Avatar<Vector3>,Vector3>,
        WorldEnterEventArgsT<World>,
        WorldLeaveEventArgsT<World>,
        UserAttributesEventArgsT<UserAttributes>
        >
      
           
    {
        public Instance()
        {
            Implementor = this;
            Avatars();
        }

        public Instance(BaseInstanceEvents<World> parentInstance)
            : base(parentInstance)
        {
            Implementor = this;
        }

        public Instance(InstanceConfiguration<World> instanceConfiguration): base(instanceConfiguration)
        {
            Implementor = this;
        }
    }
}
