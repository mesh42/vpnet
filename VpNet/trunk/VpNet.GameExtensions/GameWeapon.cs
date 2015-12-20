using System.Collections.Generic;
using VpNet.GameExtensions.Interfaces.Hubs.Entities;

namespace VpNet.GameExtensions
{
    public class GameWeapon : IGameWeapon
    {
        public string Name { get; set; }
        public float Radius { get; set; }
        public float MaxDamage { get; set; }
        public float FalloffPercentage { get; set; }
        public float Range { get; set; }

        public GameWeapon(string name, float radius, float maxDamage, float falloffPercentage, float range)
        {
            Name = name;
            Radius = radius;
            MaxDamage = maxDamage;
            FalloffPercentage = falloffPercentage;
            Range = range;
        }

        /// <summary>
        /// Detonates the specified weapon. Such as a remotely controlled weapons device.
        /// </summary>
        /// <param name="weaponPosition">The weapon position.</param>
        /// <param name="potentialVictims">The potential victims.</param>
        /// <returns></returns>
        public System.Collections.Generic.Dictionary<int,double> Detonate(Vector3 weaponPosition, IEnumerable<GameAvatar> potentialVictims)
        {
            var damageResults = new System.Collections.Generic.Dictionary<int,double>();
            foreach (var potentialVictim in potentialVictims)
            {
                var distance = Vector3.Distance(weaponPosition, potentialVictim.Position);
                if (distance<= Radius)
                {
                    damageResults.Add(
                        potentialVictim.Session,
                        MaxDamage -(MaxDamage*(distance - ((distance/Radius)*FalloffPercentage/100))));
 
                }
            }
            return damageResults;
        }

        /// <summary>
        /// Throws the specified weapon, it is not wise to throw a weapon with a large damage radius if you have low skils you will be part of the damage results.
        /// </summary>
        /// <param name="attackerPosition">The attacker position.</param>
        /// <param name="targettedVictim">The targetted victim.</param>
        /// <param name="attackerSkillPercentage">The attacker skill percentage.</param>
        /// <param name="potentialVictims">The potential victims.</param>
        /// <returns></returns>
        public System.Collections.Generic.Dictionary<int, double> Throw(GameAvatar attackerPosition, GameAvatar targettedVictim,  float attackerSkillPercentage, IEnumerable<GameAvatar> potentialVictims)
        {
            var damageResults = new System.Collections.Generic.Dictionary<int, double>();
            var throwingDistance = Vector3.Distance(targettedVictim.Position, attackerPosition.Position)* (attackerSkillPercentage/100);
            var impact = Vector3.PointAlongLine(attackerPosition.Position,targettedVictim.Position, throwingDistance);
           
            foreach (var potentialVictim in potentialVictims)
            {
                var distance = Vector3.Distance(impact, potentialVictim.Position);
                if (distance <= Radius)
                {
                    damageResults.Add(potentialVictim.Session, MaxDamage * (Radius - distance) / Radius);
                }
            }
            return damageResults;
        }
    }
}
