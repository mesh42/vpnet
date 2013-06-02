using VpNet.Abstract;
using VpNet.Interfaces;

namespace VpNet
{
    public class Instance<TResult, TVector3, TAvatar> : BaseInstanceT<Instance<TResult, TVector3,TAvatar>,
        TAvatar,
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

        AvatarChangeEventArgsT<TAvatar, TVector3>,
        AvatarEnterEventArgsT<TAvatar, TVector3>,
        AvatarLeaveEventArgsT<TAvatar, TVector3>,
        QueryCellResultArgsT<VpObject<TVector3>, TVector3>,
        QueryCellEndArgs,
        ChatMessageEventArgsT<TAvatar, ChatMessage, TVector3, Color>,
        FriendAddCallbackEventArgs,
        FriendDeleteCallbackEventArgs,
        FriendsGetCallbackEventArgs,
        TerrainNodeEventArgs,
        UniverseDisconnectEventArgs,
        ObjectChangeArgsT<TAvatar, VpObject<TVector3>, TVector3>,
        ObjectChangeCallbackArgsT<TResult, VpObject<TVector3>, TVector3>,
        ObjectClickArgsT<TAvatar, VpObject<TVector3>, TVector3>,
        ObjectCreateArgsT<TAvatar, VpObject<TVector3>, TVector3>,
        ObjectCreateCallbackArgsT<TResult, VpObject<TVector3>, TVector3>,
        ObjectDeleteArgsT<TAvatar, VpObject<TVector3>, TVector3>,
        ObjectDeleteCallbackArgsT<TResult, VpObject<TVector3>, TVector3>,
        WorldDisconnectEventArgs,
        WorldListEventArgs,
        WorldSettingsChangedEventArgs
        >

        where TVector3 : class, IVector3, new()
        where TResult : class, IRc, new()
        where TAvatar : class, IAvatar<TVector3>, new()
    {
        public Instance()
        {
            Implementor = this;
        }
    }
}
