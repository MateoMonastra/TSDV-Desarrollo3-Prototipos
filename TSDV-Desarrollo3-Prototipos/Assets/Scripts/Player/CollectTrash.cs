using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class CollectTrash : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.SetActive(false);
            Debug.Log("Trash was Collected");
        }
    }
}
