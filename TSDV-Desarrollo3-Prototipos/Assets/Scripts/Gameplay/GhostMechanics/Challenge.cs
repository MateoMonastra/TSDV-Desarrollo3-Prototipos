using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Challenge : MonoBehaviour
{
    public int damageAmount; //Cuanto daño hace
    public float damageThreshold; //Que tan bien debe estar la prueba para considerar daño.
    public float damageTick; //Cada cuanto se ejecuta el daño

    protected float successRatio; //De la totalidad de lo que tenía que hacer, qué tan bien lo hizo.

    public Ghost ghost; //El fantasma al que se le va a aplicar el daño.

    public abstract void Interact();
}
