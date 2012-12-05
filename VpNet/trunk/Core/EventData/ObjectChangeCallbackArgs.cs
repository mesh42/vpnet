using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VpNet.Core.Structs;

namespace VpNet.Core.EventData
{
    public class ObjectChangeCallbackArgs : EventArgs
    {
        public VpObject VpObject { get; set; }
        public int Rc { get; set; }

        public ObjectChangeCallbackArgs(int rc, VpObject vpObject)
        {
            VpObject = vpObject;
            Rc = rc;
        }
    }
}
