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

using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    /// <summary>
    /// Called when an object is changed in the world by another user.
    /// </summary>
    /// <typeparam name="TAvatar">The type of the avatar.</typeparam>
    /// <typeparam name="TVpObject">The type of the vp object.</typeparam>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [Serializable]
    public abstract class BaseObjectChangeArgs<TAvatar,TVpObject,TVector3> : EventArgs, IObjectChangeArgs<TAvatar, TVpObject, TVector3> where TVector3 : struct,IVector3
        where TAvatar : class, IAvatar<TVector3>, new()
        where TVpObject : class, IVpObject<TVector3>, new()
    {
        public TVpObject VpObject { get; set; }
        public TAvatar Avatar { get; set; }

        protected BaseObjectChangeArgs(TAvatar avatar, TVpObject vpObject)
        {
            VpObject = vpObject;
            Avatar = avatar;
        }

        protected BaseObjectChangeArgs() { }
    }
}
