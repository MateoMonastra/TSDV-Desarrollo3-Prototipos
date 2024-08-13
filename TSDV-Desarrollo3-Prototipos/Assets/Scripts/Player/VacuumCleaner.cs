using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class VacuumCleaner : MonoBehaviour
    {
        [SerializeField] private Transform target; 
        [SerializeField] private float speed;
        [SerializeField] private LayerMask layerMask;

        private Collider _collider;

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }

        public void TurnOn()
        {
            _collider.isTrigger = true;
        }
        public void TurnOff()
        {
            _collider.isTrigger = false;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (Physics.Raycast(target.position,other.gameObject.transform.position,layerMask))
            {
                return;
            }
            other.transform.position = Vector3.MoveTowards(other.transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
