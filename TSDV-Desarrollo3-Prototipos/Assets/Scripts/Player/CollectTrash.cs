using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class CollectTrash : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != LayerMask.NameToLayer($"NotVacuumable"))
            {
                other.gameObject.SetActive(false);
                Debug.Log("Trash was Collected");
            }
        }
    }
}
