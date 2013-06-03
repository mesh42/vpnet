﻿#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2013 CUBE3 (Cit:36)

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

namespace VpNet.Interfaces
{
    /// <summary>
    /// Templated Avatar functions.
    /// </summary>
    /// <typeparam name="TRc">The type of the result code object.</typeparam>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVector3">The type of vector3.</typeparam>
    public interface IAvatarFunctions<out TRc, in TAvatar, in TVector3>
        where TRc : class, IRc, new()
        where TAvatar : class, IAvatar<TVector3>,new()
        where TVector3 : class, IVector3, new()
    {
        /// <summary>
        /// Announce your bot at a given location.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordiante.</param>
        /// <param name="yaw">Yaw.</param>
        /// <param name="pitch">Pitch.</param>
        /// <returns>Result object</returns>
        TRc UpdateAvatar(float x = 0.0f, float y = 0.0f, float z = 0.0f, float yaw = 0.0f, float pitch = 0.0f);
        /// <summary>
        /// Announce your bot at a given location with default pitch and yaw.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Result object</returns>
        TRc UpdateAvatar(TVector3 position);
        /// <summary>
        /// Announce your bot at a given location.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns></returns>
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
