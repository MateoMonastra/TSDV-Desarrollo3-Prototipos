using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class VacuumCleaner : MonoBehaviour
    {
        [SerializeField] private Transform target; 
        [SerializeField] private float speed;

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
            other.transform.position = Vector3.MoveTowards(other.transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
