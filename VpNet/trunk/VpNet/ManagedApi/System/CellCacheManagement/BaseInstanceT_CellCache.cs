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
using System.Collections.Generic;
using System.Linq;
using VpNet.Extensions;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    public abstract partial class BaseInstanceT<T,
        /* Scene Type specifications ----------------------------------------------------------------------------------------------------------------------------------------------*/
        TAvatar, TColor, TFriend, TResult, TTerrainCell, TTerrainNode,
        TTerrainTile, TVector3, TVpObject, TWorld, TWorldAttributes,TCell,TChatMessage,TTerrain,TUniverse,TTeleport,
        TUserAttributes,
        /* Event Arg types --------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Avatar Event Args */
        TAvatarChangeEventArgs, TAvatarEnterEventArgs, TAvatarLeaveEventArgs, TAvatarClickEventArgs,
        /* Cell Event Args */
        TQueryCellResultArgs, TQueryCellEndArgs,
        /* Chat Event Args */
        TChatMessageEventArgs,
        /* Friend Event Args */
        TFriendAddCallbackEventArgs, TFriendDeleteCallbackEventArgs, TFriendsGetCallbackEventArgs,
        /* Terrain Event Args */
        TTerainNodeEventArgs,
        /* Universe Event Args */
        TUniverseDisconnectEventargs,
        /* VpObject Event Args */
        TObjectChangeArgs, TObjectChangeCallbackArgs, TObjectClickArgs, TObjectCreateArgs,
        TObjectCreateCallbackArgs, TObjectDeleteArgs, TObjectDeleteCallbackArgs, TObjectGetCallbackArgs,
        /* World Event Args */
            TWorldDisconnectEventArg, TWorldListEventargs, TWorldSettingsChangedEventArg,
          /* Teleport Event Args */
        TTeleportEventArgs,
        TWorldEnterEventArgs,
        TWorldLeaveEventArgs,
        TUserAttributesEventArgs
        >
/* Constraints ----------------------------------------------------------------------------------------------------------------------------------------------------*/
        where TUniverse : class, IUniverse, new()
        where TTerrain : class, ITerrain, new()
        where TCell : class, ICell, new()
        where TChatMessage : class, IChatMessage<TColor>, new()
        where TTerrainCell : class, ITerrainCell, new()
        where TTerrainNode : class, ITerrainNode<TTerrainTile,TTerrainNode,TTerrainCell>, new()
        where TTerrainTile : class, ITerrainTile<TTerrainTile,TTerrainNode, TTerrainCell>, new()
        where TResult : class, IRc, new()
        where TWorld : class, IWorld, new()
        where TAvatar : class, IAvatar<TVector3>, new()
        where TFriend : class, IFriend, new()
        where TColor : class, IColor, new()
        where TVpObject : class, IVpObject<TVector3>, new()
        where TVector3 : struct, IVector3
        where TWorldAttributes : class, IWorldAttributes, new()
        where TTeleport : class, ITeleport<TWorld,TAvatar,TVector3>, new()
        where TUserAttributes : class, IUserAttributes, new()
        where T : class, new()
        /* Event Arg types --------------------------------------------------------------------------------------------------------------------------------------------------------*/
        /* Avatar Event Args */
        where TAvatarChangeEventArgs : class, IAvatarChangeEventArgs<TAvatar,TVector3>, new()
        where TAvatarEnterEventArgs : class, IAvatarEnterEventArgs<TAvatar,TVector3>, new()
        where TAvatarLeaveEventArgs : class, IAvatarLeaveEventArgs<TAvatar,TVector3>, new()
        where TAvatarClickEventArgs : class, IAvatarClickEventArgs<TAvatar, TVector3>, new()
        /* Cell Event Args */
        where TQueryCellResultArgs : class, IQueryCellResultArgs<TVpObject,TVector3>, new()
        where TQueryCellEndArgs : class, IQueryCellEndArgs<TCell>, new()
        /* Chat Event Args */
        where TChatMessageEventArgs : class, IChatMessageEventArgs<TAvatar,TChatMessage,TVector3,TColor>, new()
        /* Friend Event Args */
        where TFriendAddCallbackEventArgs : class,IFriendAddCallbackEventArgs<TFriend>, new()
        where TFriendDeleteCallbackEventArgs : class, IFriendDeleteCallbackEventArgs<TFriend>,  new()
        where TFriendsGetCallbackEventArgs : class, IFriendsGetCallbackEventArgs<TFriend>,  new()
        /* Terrain Event Args */
        where TTerainNodeEventArgs : class, ITerrainNodeEventArgs<TTerrain>,  new()
        /* Universe Event Args */
        where TUniverseDisconnectEventargs : class, IUniverseDisconnectEventArgs<TUniverse>, new()
        /* VpObject Event Args */
        where TObjectChangeArgs : class,IObjectChangeArgs<TAvatar,TVpObject,TVector3>, new()
        where TObjectChangeCallbackArgs : class,IObjectChangeCallbackArgs<TResult,TVpObject,TVector3>, new()
        where TObjectClickArgs : class, IObjectClickArgs<TAvatar,TVpObject,TVector3>,  new()
        where TObjectCreateArgs : class, IObjectCreateArgs<TAvatar,TVpObject,TVector3>, new()
        where TObjectCreateCallbackArgs : class, IObjectCreateCallbackArgs<TResult,TVpObject,TVector3>, new()
        where TObjectDeleteArgs : class, IObjectDeleteArgs<TAvatar,TVpObject,TVector3>,  new()
        where TObjectDeleteCallbackArgs : class,IObjectDeleteCallbackArgs<TResult,TVpObject,TVector3>,  new()
        where TObjectGetCallbackArgs : class,IObjectGetCallbackArgs<TResult, TVpObject, TVector3>, new()
        /* World Event Args */
        where TWorldDisconnectEventArg : class, IWorldDisconnectEventArgs<TWorld>, new()
        where TWorldListEventargs : class, IWorldListEventArgs<TWorld>,new()
        where TWorldSettingsChangedEventArg : class,IWorldSettingsChangedEventArgs<TWorld>, new()
        where TTeleportEventArgs : class, ITeleportEventArgs<TTeleport,TWorld,TAvatar,TVector3>, new()
        where TWorldEnterEventArgs : class, IWorldEnterEventArgs<TWorld>, new()
        where TWorldLeaveEventArgs : class, IWorldLeaveEventArgs<TWorld>, new()
        where TUserAttributesEventArgs : class, IUserAttributesEventArgs<TUserAttributes>, new()
    {
        public delegate void CellRangeQueryCompletedDelegate(T sender, CellRangeQueryCompletedArgs<TVpObject,TVector3> args);
        public delegate void CellRangeObjectChangedDelegate(T sender, TObjectChangeArgs args);

        public event CellRangeQueryCompletedDelegate OnQueryCellRangeEnd;
        public event CellRangeObjectChangedDelegate OnObjectCellRangeChange;
        
        private List<TVpObject> _objects; 
        private List<TCell> _cache;
        private bool _isScanning;

        private List<TCell> _cacheScanned;
        private bool _useCellCache;
        private bool _autowaitWasUsed;
        private List<TCell> _cacheScanning;

        public int Cells { 
            get { return _cache.Count(); }
        }

        public bool UseCellCache
        {
            get { return _useCellCache; }
            set
            {
                if (value == _useCellCache)
                    return;
                if (_isScanning)
                    throw new Exception("Please do not change the use cache boolean flag while previous scan has not been completed.");
                if (value)
                {
                    _objects = new List<TVpObject>();
                    _cache = new List<TCell>();
                    _cacheScanned = new List<TCell>();
                    _cacheScanning = new List<TCell>();

                    OnQueryCellResult += BaseInstanceT_CellCache_OnQueryCellResult;
                    OnQueryCellEnd += BaseInstanceT_CellCache_OnQueryCellEnd;
                    OnObjectChange += BaseInstanceT_CellCache_OnObjectChange;
                }
                else
                {
                    OnQueryCellResult -= BaseInstanceT_CellCache_OnQueryCellResult;
                    OnQueryCellEnd -= BaseInstanceT_CellCache_OnQueryCellEnd;
                    OnObjectChange -= BaseInstanceT_CellCache_OnObjectChange;
                }
                _useCellCache = value;
            }
        }

        void BaseInstanceT_CellCache_OnObjectChange(T sender, TObjectChangeArgs args)
        {
            // TODO: remove object from cache when it moves out of the cell range currently managed by this cache.
            lock (this)
            {
                var cell = _cacheScanned.Find(p => p.X == args.VpObject.Cell.X && p.Z == args.VpObject.Cell.Z);
                if (cell == null) return;
                var o = _objects.Find(p => p.Id == args.VpObject.Id);
                _objects.Remove(o);
                _objects.Add(args.VpObject);
                if (OnObjectCellRangeChange != null)
                    OnObjectCellRangeChange(Implementor, args);
            }
        }

        void BaseInstanceT_CellCache_OnQueryCellEnd(T sender, TQueryCellEndArgs args)
        {
            lock (this)
            {
                _cacheScanning.RemoveAll(p => p.X == args.Cell.X && p.Z == args.Cell.Z);
                _cacheScanned.Add(args.Cell);
                if (_cache.Count == 0)
                {
                    _isScanning = false;
                    UseAutoWaitTimer = _autowaitWasUsed;
                    if (OnQueryCellRangeEnd != null)
                        OnQueryCellRangeEnd(Implementor, new CellRangeQueryCompletedArgs<TVpObject,TVector3> { VpObjects = _objects.Copy() });
                }
                else
                {
                    _cacheScanning.Add(_cache[0]);
                    QueryCell(_cache[0].X, _cache[0].Z);
                    _cache.RemoveAt(0);
                }
            }
        }

        void BaseInstanceT_CellCache_OnQueryCellResult(T sender, TQueryCellResultArgs args)
        {
            lock (this)
            {
                _objects.Add(args.VpObject);
            }
        }


        private bool IsCellInList(TCell cell)
        {
            return ((_cache.Find(p => p.X == cell.X && p.Z == cell.Z) != null) ||
                    (_cacheScanned.Find(p => p.X == cell.X && p.Z == cell.Z) != null));
        }

        public int AddCellRange(TCell start, TCell end)
        {
            if (_isScanning)
                throw new Exception("Can not issue a cell range query before the other range query has ended.");
            if (!UseCellCache)
                UseCellCache = true;
            lock (this)
            {
                _autowaitWasUsed = UseAutoWaitTimer.Copy();
                var l = CreateQueryList(start, end);
                foreach (TCell cell in l)
                {

                    if (!IsCellInList(cell))
                    {
                        _cache.Add(cell);
                    }
                }
                if (l.Count>0)
                {
                    if (!_isScanning)
                    {
                        _isScanning = true;
                        for (int i = 0; i < 64; i++)
                        {
                            if (_cache.Count == 0)
                                break;
                            _cacheScanning.Add(_cache[0]);
                            QueryCell(_cache[0].X, _cache[0].Z);
                            _cache.Remove(_cache[0]);
                        }
                        UseAutoWaitTimer = false;
                    }
                }
                return l.Count();
            }
        }

        private List<TCell> CreateQueryList(TCell start, TCell end)
        {
            var ret = new List<TCell>();
            var listX = new List<TCell> { start, end }.OrderBy(p => p.X);
            var listY = new List<TCell> { start, end }.OrderBy(p => p.Z);
            var p1 = new Cell(listX.ElementAt(0).X, listY.ElementAt(0).Z);
            var p2 = new Cell(listX.ElementAt(1).X, listY.ElementAt(1).Z);
            for (var x = p1.X; x < p2.X; x++)
            {
                for (var z = p1.Z; z < p2.Z; z++)
                {
                    ret.Add(new TCell{X=x, Z= z});
                }
            }

            return ret;
        }


        public void AddCell(TCell cell)
        {
            if (_isScanning)
                throw new Exception("Can not issue a cell query before the other range query has ended.");
            if (!UseCellCache)
                UseCellCache = true;
            _autowaitWasUsed = UseAutoWaitTimer.Copy();

            lock (this)
            {
                if (!IsCellInList(cell))
                {
                    _cache.Add(cell);
                    if (!_isScanning)
                    {
                        _isScanning = true;
                        UseAutoWaitTimer = false;
                        QueryCell(cell.X, cell.Z);
                    }
                }
            }
        }
    }
}
