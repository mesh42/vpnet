namespace VpNet.GameExtensions.Interfaces.Hubs.Entities
{
    public interface IGameWeapon
    {
        float FalloffPercentage { get; set; }
        float MaxDamage { get; set; }
        string Name { get; set; }
        float Radius { get; set; }
        float Range { get; set; }
    }
}
