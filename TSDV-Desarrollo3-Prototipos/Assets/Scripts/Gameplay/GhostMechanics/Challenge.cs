using UnityEngine;

namespace Gameplay.GhostMechanics
{
    public abstract class Challenge : MonoBehaviour 
    {
        public float damageAmount; //Cuanto daño hace
        public float damageThreshold; //Que tan bien debe estar la prueba para considerar daño.
        public float damageTick; //Cada cuanto se ejecuta el daño

        protected float successRatio; //De la totalidad de lo que tenía que hacer, qué tan bien lo hizo.

        public abstract void Interact(Ghost ghost);
        public abstract void OnEnd();
    }
}
