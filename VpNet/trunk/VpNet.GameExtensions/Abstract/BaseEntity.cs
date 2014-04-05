using System;
using System.ComponentModel.DataAnnotations;

namespace VpNet.GameExtensions.Abstract
{
    public abstract class BaseDataEntity
    {
        private Guid _key;

        [Key]
        public Guid Id { 
            get
            {
                if (_key==Guid.Empty) _key = Guid.NewGuid();
                return _key;
            }

            set { _key = value; }} 
    }
}