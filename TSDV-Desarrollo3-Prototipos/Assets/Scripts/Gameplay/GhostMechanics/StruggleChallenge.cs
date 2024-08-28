using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StruggleChallenge : Challenge
{
    public StruggleChallenge(int newDamageAmount = 10, float newDamageThreshold = 10, float newDamageTick = 1.0f)
    {
        damageAmount = newDamageAmount; 
        damageThreshold = newDamageThreshold;
        damageTick = newDamageTick; 
    }
    public override void Interact() //chequear la direccion del fantasma comparada con la del input
    {
        
    }
}
