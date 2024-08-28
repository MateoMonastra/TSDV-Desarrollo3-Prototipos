using UnityEngine;

namespace Gameplay.GhostMechanics
{
    public abstract class Challenge : MonoBehaviour 
    {
        public float damageAmount; //Cuanto da�o hace
        public float damageThreshold; //Que tan bien debe estar la prueba para considerar da�o.
        public float damageTick; //Cada cuanto se ejecuta el da�o

        protected float successRatio; //De la totalidad de lo que ten�a que hacer, qu� tan bien lo hizo.

        public abstract void Interact(Ghost ghost);
        public abstract void OnEnd();
    }
}
