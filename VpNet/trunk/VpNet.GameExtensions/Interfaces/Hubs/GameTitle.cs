using System.ComponentModel.DataAnnotations.Schema;
using VpNet.GameExtensions.Abstract;

namespace VpNet.GameExtensions.Interfaces.Hubs
{
    public class GameTitle : OwnerEntity
    {
        [Index]
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public abstract class OwnerEntity : BaseDataEntity
    {
        [Index]
        public string OwnerId { get; set; }
    }
}