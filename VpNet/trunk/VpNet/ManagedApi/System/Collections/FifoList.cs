using System.Collections.Generic;

namespace VpNet.Collections
{
    internal class FifoList<T>
    {
        private readonly List<T> _list;

        public FifoList()
        {
            _list = new List<T>();
        }

        public int Count()
        {
            return _list.Count;
        }

        public T Next()
        {
            lock(this)
            {
                var o = _list[0];
                _list.RemoveAt(0);
                return o;
            }
        }

        public void Add(T item)
        {
            lock(this)
            {
                _list.Add(item);
            }
        }
    }
}
