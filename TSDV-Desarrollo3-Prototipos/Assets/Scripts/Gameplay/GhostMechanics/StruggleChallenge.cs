using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.GhostMechanics
{
    public class StruggleChallenge : Challenge
    {
        public StruggleChallenge(int newDamageAmount = 10, float newDamageThreshold = 10, float newDamageTick = 1.0f)
        {
            damageAmount = newDamageAmount;
            damageThreshold = newDamageThreshold;
            damageTick = newDamageTick;
        }

        public override void Interact(Ghost ghost)
        {
            
        }

        public override void OnEnd()
        {
        }
    }
}