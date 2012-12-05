using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VpNet.NativeApi
{
    public enum Callbacks
    {
        /**
 *  The attribute #VP_OBJECT_ID is set to the object ID of the new object
 */
        ObjectAdd,
        ObjectChange,
        ObjectDelete,
        GetFriends,
        FriendAdd,
        FriendDelete,
        TerrainQuery,
        TerrainNodeSet,
        HighestCallback
    }
}
