using VpNet.GameExtensions.Interfaces.Hubs.Entities;

namespace VpNet.GameExtensions.Interfaces.Hubs
{
    public interface IGameWeaponsHub : IHubQueryable<GameWeapon>
    {
    }

    public interface IGameAchievementsHub : IHubQueryable<GameAchievement>
    {
    }

    public interface IGameTitlessHub : IHubQueryable<GameTitle>
    {
    }

    public class GameAchievement : IGameAchievement 
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
