using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Challenge : MonoBehaviour 
{
    public int damageAmount; //Cuanto da�o hace
    public float damageThreshold; //Que tan bien debe estar la prueba para considerar da�o.
    public float damageTick; //Cada cuanto se ejecuta el da�o

    protected float successRatio; //De la totalidad de lo que ten�a que hacer, qu� tan bien lo hizo.

    public abstract void Interact();
}
