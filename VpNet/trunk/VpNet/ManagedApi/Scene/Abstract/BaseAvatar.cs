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
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatar<TVector3> : IAvatar<TVector3>
        where TVector3 : struct, IVector3
    {
        [XmlAttribute]
        virtual public DateTime LastChanged { get; set; }
        [XmlAttribute]
        virtual public int UserId { get; set; }
        [XmlAttribute]
        virtual public string Name { get; set; }
        [XmlIgnore]
        virtual public int Session { get;set; }
        [XmlAttribute]
        virtual public int AvatarType { get; set; }
        virtual public TVector3 Position { get; set; }
        virtual public TVector3 Rotation { get; set; } // X pitch, Y yaw
        [XmlIgnore]
        public virtual bool IsBot
        {
            get
            {
                return Name != null && Name.StartsWith("[");
            }
        }

        protected BaseAvatar(int userId, string name,int session,int avatarType,TVector3 position,TVector3 rotation)
        {
            UserId = userId;
            Name = name;
            Session = session;
            AvatarType = avatarType;
            Position = position;
            Rotation = rotation;
        }

        protected BaseAvatar() { }
    }
}
