namespace VpNet.Interfaces
{
    public interface IAvatarFunctions<out TRc, in TAvatar, in TVector3>
        where TRc : class, IRc, new()
        where TAvatar : class, IAvatar<TVector3>,new()
        where TVector3 : class, IVector3, new()
    {
        TRc UpdateAvatar(float x = 0.0f, float y = 0.0f, float z = 0.0f, float yaw = 0.0f, float pitch = 0.0f);
        TRc UpdateAvatar(TVector3 position);
        TRc UpdateAvatar(TVector3 position, TVector3 rotation);
        /// <summary>
        /// Send an avatar click event to other users in the world
        /// </summary>
        /// <param name="session">The session id of the clicked avatar.</param>
        /// <returns>Zero when successful, otherwise nonzero.</returns>
        TRc AvatarClick(int session);
        /// <summary>
        /// Send an avatar click event to other users in the world
        /// </summary>
        /// <param name="avatar">The avatar object containing the session id.</param>
        /// <returns>Zero when successful, otherwise nonzero.</returns>
        TRc AvatarClick(TAvatar avatar);
    }
}
