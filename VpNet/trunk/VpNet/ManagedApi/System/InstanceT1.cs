﻿using VpNet.Abstract;
using VpNet.Interfaces;

namespace VpNet
{
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

        AvatarChangeEventArgsT<Avatar<TVector3>,TVector3>,
        AvatarEnterEventArgsT<Avatar<TVector3>,TVector3>,
        AvatarLeaveEventArgsT<Avatar<TVector3>,TVector3>,
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
        WorldDisconnectEventArgs,
        WorldListEventArgs,
        WorldSettingsChangedEventArgs,
        TeleportEventArgsT<Teleport<World,Avatar<TVector3>,TVector3>,World,Avatar<TVector3>,TVector3>>
         where TVector3 : class, IVector3, new()
         where TResult : class, IRc, new()
    {
        public Instance()
        {
            Implementor = this;
        }
    }
}