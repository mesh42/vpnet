using System;
using System.Collections.Generic;
using VpNet.Interfaces;

namespace VpNet
{
    public class CellRangeQueryCompletedArgs<TVpObject,TVector3> : EventArgs 
        where TVector3 : class, IVector3, new()
        where TVpObject: class, IVpObject<TVector3>, new()
    {
        public List<TVpObject> VpObjects { get; set; }
        public CellRangeQueryCompletedArgs(){}

        public CellRangeQueryCompletedArgs(List<TVpObject> vpObjects)
        {
            VpObjects = vpObjects;
        }
    }
}