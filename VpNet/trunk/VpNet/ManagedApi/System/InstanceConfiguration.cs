using System;
using VpNet.Interfaces;

namespace VpNet
{
    [Serializable]
    public class InstanceConfiguration<TWorld> : Abstract.BaseInstanceConfiguration<TWorld>
        where TWorld : class, IWorld, new()
    {
        #region Overrides of BaseInstanceConfiguration<TWorld>

        private readonly bool _isChildInstance = false;

        public override bool IsChildInstance
        {
            get { return _isChildInstance; }
        }

        public InstanceConfiguration(bool isChildInstance)
        {
            _isChildInstance = true;
        }

        public InstanceConfiguration()
        {
            _isChildInstance = false;
        }


        #endregion
    }
}
