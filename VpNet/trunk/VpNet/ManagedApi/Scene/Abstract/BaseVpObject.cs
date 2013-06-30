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
using System.Xml.Serialization;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseVpObject<TVector3> : IVpObject<TVector3>
        where TVector3 : IVector3,new()
    {
        private TVector3 _position;
        private Cell _cell;

        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public int Type { get; set; }
        [XmlAttribute]
        public DateTime Time { get; set; }
        [XmlAttribute]
        public int Owner { get; set; }

        public TVector3 Position
        {
            get { return _position; }
            set
            {
                _cell = null;
                _position = value;
            }
        }
        public TVector3 Rotation { get; set; }
        [XmlAttribute]
        public float Angle { get; set; }
        [XmlAttribute]
        public string Action { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public int ObjectType { get; set; }
        [XmlAttribute]
        public string Model { get; set; }
        [XmlIgnore]
        public int ReferenceNumber { get; set; }

        protected BaseVpObject(int id, int type, DateTime time, int owner, TVector3 position, TVector3 rotation, float angle, string action, string description, int objectType, string model)
        {
            Id = id;
            Type = type;
            Time = time;
            Owner = owner;
            Position = position;
            Rotation = rotation;
            Angle = angle;
            Action = action;
            Description = description;
            ObjectType = objectType;
            Model = model;
        }

        protected BaseVpObject()
        {
            Angle = float.MaxValue;
            Time = DateTime.UtcNow;
        }

        [XmlIgnore]
        public ICell Cell
        {
            get
            {
                if (_cell == null)
                    if (Position != null)
                    _cell = new Cell((int) (Math.Floor(Position.X)/10), (int) (Math.Floor(Position.Z)/10));
                    else
                        _cell = new Cell(0,0);

                return _cell;
            }
        }
    }
}
