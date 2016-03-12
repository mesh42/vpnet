#region Copyright notice
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

namespace VpNet.Interfaces
{
    /// <summary>
    /// VpObject templated interface specifications.
    /// </summary>
    /// <typeparam name="TVector3">The type of the vector3.</typeparam>
    [XmlRoot("vpObject", Namespace = Global.XmlNsScene)]
    public interface IVpObject<TVector3>
        where TVector3 : struct, IVector3
    {
        [XmlAttribute]
        int Id { get; set; }

        [XmlAttribute]
        int Type { get; set; }

        [XmlAttribute]
        DateTime Time { get; set; }

        [XmlAttribute]
        int Owner { get; set; }

        TVector3 Position { get; set; }
        TVector3 Rotation { get; set; }

        [XmlAttribute]
        double Angle { get; set; }

        [XmlAttribute]
        string Action { get; set; }

        [XmlAttribute]
        string Description { get; set; }

        [XmlAttribute]
        int ObjectType { get; set; }

        [XmlAttribute]
        string Model { get; set; }
        
        byte[] Data { get; set; }

        [XmlIgnore]
        int ReferenceNumber { get; set; }

        [XmlIgnore]
        ICell Cell { get; }
    }
}