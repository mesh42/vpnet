using System.Collections.Generic;

namespace VpNet.GameExtensions.Interfaces
{
    public interface IHubQueryable<T>
    {
        HubQueryableResult<T> Query(int page, int pageSize);
    }

    public class HubQueryableResult<T>
    {
        public List<T> Records { get; set; }
        public int TotalCount { get; set; } 
    }
}
