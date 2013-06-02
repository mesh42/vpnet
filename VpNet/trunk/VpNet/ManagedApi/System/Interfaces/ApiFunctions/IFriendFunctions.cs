namespace VpNet.Interfaces
{
    public interface IFriendFunctions<out TRc, in TFriend>
        where TRc : class, IRc, new()
        where TFriend : class, IFriend, new()
    {
        TRc GetFriends();
        TRc AddFriendByName(TFriend friend);
        TRc AddFriendByName(string name);
        TRc DeleteFriendById(int friendId);
        TRc DeleteFriendById(TFriend friend);
    }
}
