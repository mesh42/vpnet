using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseInstanceConfiguration<TWorld>
        where TWorld : class, IWorld, new()
    {
        public TWorld World { get; set; }
    }
}
