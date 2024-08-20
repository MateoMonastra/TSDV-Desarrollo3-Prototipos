using System;
using UnityEngine;

namespace PrototipoExtra
{
   public class ButtonCheck : MonoBehaviour
   {
      [SerializeField] private GameObject door;

      private void Click()
      {
         door.SetActive(false);
      }

      private void OnTriggerEnter(Collider other)
      {
         Click();
      }
   }
}
