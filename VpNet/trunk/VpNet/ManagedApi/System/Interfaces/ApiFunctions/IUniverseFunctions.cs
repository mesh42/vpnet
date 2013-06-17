namespace VpNet.Interfaces
{
    public interface IUniverseFunctions<out TResult>
        where TResult : class, IRc, new()
    {
        TResult Connect(string host = "universe.virtualparadise.org", ushort port = 57000);
        TResult Login(string username, string password, string botname);
        /// <summary>
        /// Logs in the user, using the preloaded instance configuration.
        /// </summary>
        /// <returns></returns>
        TResult Login();
        /// <summary>
        /// Logs in to the universe and automatically enters the world using the preloaded instance configiguration.
        /// </summary>
        /// <param name="isAnnounceAvatar">if set to <c>true</c> [is announce avatar] then the avatar is updated on the given position as specified within the instance configuration. If the position is not specified, the avatar will appear at Ground Zero.</param>
        /// <returns></returns>
        TResult LoginAndEnter(bool isAnnounceAvatar = true);
    }
}
