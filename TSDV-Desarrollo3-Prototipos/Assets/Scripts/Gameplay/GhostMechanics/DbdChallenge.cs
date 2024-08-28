using System;

namespace Gameplay.GhostMechanics
{
    public class DbdChallenge : Challenge
    {
        public DbdChallenge(int newDamageAmount = 10, float newDamageThreshold = 10, float newDamageTick = 1.0f)
        {
            damageAmount = newDamageAmount; 
            damageThreshold = newDamageThreshold;
            damageTick = newDamageTick; 
        }

        public override void Interact(Ghost ghost)
        {
            throw new NotImplementedException();
        }

        public override void OnEnd()
        {
            throw new NotImplementedException();
        }
    }
}
