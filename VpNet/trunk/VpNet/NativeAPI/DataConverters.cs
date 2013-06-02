#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2013 CUBE3 (Cit:36)
    Portions herein taken from: Roy Curtis (Cit:182)
    https://github.com/RoyCurtis/VPNet
    30-05-2013 cube3: added Generics

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
using System.IO;
using System.Runtime.InteropServices;
using VpNet.Interfaces;

namespace VpNet.NativeApi
{
    internal static class DataConverters
    {
        /// <summary>
        /// Converts terrain node data to a 2D TerrainCell array
        /// </summary>
        public static TTerrainCell[,] TerrainNodeData<TTerrainCell>(byte[] data)
            where TTerrainCell : class, ITerrainCell, new()
        {
            var cells = new TTerrainCell[8, 8];

            using (var memStream = new MemoryStream(data))
            {
                var array = new byte[8];
                for (var i = 0; i < 64; i++)
                {
                    if (memStream.Read(array, 0, 8) < 8)
                        throw new Exception("Unexpected end of byte array");

                    var pin = GCHandle.Alloc(array, GCHandleType.Pinned);
                    var cell = (TTerrainCell)Marshal.PtrToStructure(pin.AddrOfPinnedObject(), typeof(TerrainCell));
                    pin.Free();

                    var row = i % 8;
                    var col = (i - row) / 8;
                    cells[col, row] = cell;
                }
            }

            return cells;
        }
    }
}
