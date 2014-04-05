﻿#region Copyright notice
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
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable] 
    [XmlRoot("Instance", Namespace = Global.XmlNsInstance)]
    public class Instance<TResult, TVector3> : BaseInstanceT<Instance<TResult, TVector3>,
        Avatar<TVector3>, 
        Color, 
        Friend, 
        TResult, 
        TerrainCell, 
        TerrainNode, 
        TerrainTile,
        TVector3, 
        VpObject<TVector3>, 
        World, 
        WorldAttributes,
        Cell,
        ChatMessage,
        Terrain,
        Universe,
        Teleport<World,Avatar<TVector3>,TVector3>, 
        UserAttributes,

        AvatarChangeEventArgsT<Avatar<TVector3>,TVector3>,
        AvatarEnterEventArgsT<Avatar<TVector3>,TVector3>,
        AvatarLeaveEventArgsT<Avatar<TVector3>,TVector3>,
        AvatarClickEventArgsT<Avatar<TVector3>, TVector3>,
        QueryCellResultArgsT<VpObject<TVector3>, TVector3>,
        QueryCellEndArgs,
        ChatMessageEventArgsT<Avatar<TVector3>,ChatMessage,TVector3,Color>,
        FriendAddCallbackEventArgs,
        FriendDeleteCallbackEventArgs,
        FriendsGetCallbackEventArgs,
        TerrainNodeEventArgs,
        UniverseDisconnectEventArgs,
        ObjectChangeArgsT<Avatar<TVector3>,VpObject<TVector3>,TVector3>,
        ObjectChangeCallbackArgsT<TResult,VpObject<TVector3>,TVector3>,
        ObjectClickArgsT<Avatar<TVector3>, VpObject<TVector3>, TVector3>,
        ObjectCreateArgsT<Avatar<TVector3>, VpObject<TVector3>, TVector3>,
        ObjectCreateCallbackArgsT<TResult, VpObject<TVector3>, TVector3>,
        ObjectDeleteArgsT<Avatar<TVector3>, VpObject<TVector3>, TVector3>,
        ObjectDeleteCallbackArgsT<TResult, VpObject<TVector3>, TVector3>,
        ObjectGetCallbackArgsT<TResult, VpObject<TVector3>, TVector3>,
        WorldDisconnectEventArgs,
        WorldListEventArgs,
        WorldSettingsChangedEventArgs,
        TeleportEventArgsT<Teleport<World,Avatar<TVector3>,TVector3>,World,Avatar<TVector3>,TVector3>,
        WorldEnterEventArgsT<World>,
        WorldLeaveEventArgsT<World>,
        UserAttributesEventArgsT<UserAttributes>
        >

         where TVector3 : struct, IVector3
         where TResult : class, IRc, new()
    {
        public Instance()
        {
            Implementor = this;
        }

        public Instance(BaseInstanceEvents<World> parentInstance)
            : base(parentInstance)
        {
            Implementor = this;
        }

        public Instance(InstanceConfiguration<World> instanceConfiguration)
            : base(instanceConfiguration)
        {
            Implementor = this;
        }

    }
}
