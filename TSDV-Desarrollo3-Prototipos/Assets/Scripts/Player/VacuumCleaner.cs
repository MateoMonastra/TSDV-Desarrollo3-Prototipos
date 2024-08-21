using System;
using UnityEngine;

namespace Player
{
    public class VacuumCleaner : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private float maxAngle = 45.0f;
        [SerializeField] private float renderDistance = 5.0f;
        [SerializeField] private LayerMask wallLayer;

        private bool _isActive;
        private Vector3? _collision;
        private Ray _ray;
        
        private Quaternion _left ;
        private Quaternion _right ;

        private Vector3 _leftBoundary;
        private Vector3 _rightBoundary;

        private void Start()
        {
            _left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(maxAngle, Vector3.up);
            _leftBoundary = _left * target.forward;
            _rightBoundary = _right * target.forward;
            transform.localScale = new Vector3(Mathf.Abs(((_leftBoundary.x*2) * renderDistance) ), transform.localScale.y, transform.localScale.z);
        }

        public void TurnOn()
        {
            _isActive = true;
        }

        public void TurnOff()
        {
            _isActive = false;
        }

        private void OnTriggerStay(Collider other)
        {
            VacuumObject(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Ghost"))
            {
                other.GetComponent<RandomPatrolling>().StopBeingVacuumed();
            }
        }

        private void VacuumObject(Collider other)
        {
            if (!_isActive) return;
            
            var angleToObject = Vector3.Angle(target.forward, other.transform.position - target.position);
            
            if (!(angleToObject <= maxAngle)) return;
            
            _ray = new Ray(target.position, other.transform.position - target.position);

            if (Physics.Raycast(_ray, out var hit, renderDistance, wallLayer))
            {
                _collision = hit.point;
                return;
            }

            if(other.CompareTag("Ghost"))
            {
                other.GetComponent<RandomPatrolling>().StartBeingVacuumed();
            }

            var rb = other.GetComponent<Rigidbody>();
                
            var direction = (target.position - other.transform.position).normalized;
                
            rb.AddForce(direction * speed, ForceMode.Impulse);
                
            _collision = null;
        }
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            _left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(maxAngle, Vector3.up);
            _leftBoundary = _left * target.forward;
            _rightBoundary = _right * target.forward;
            
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(target.position, target.position + target.forward * renderDistance);

            Gizmos.DrawLine(target.position, target.position + _leftBoundary * renderDistance);

            Gizmos.DrawLine(target.position, target.position + _rightBoundary * renderDistance);
            Gizmos.DrawRay(_ray);
            if (_collision != null)
            {
                Gizmos.DrawSphere(_collision.Value, 0.5f);
            }
        }
#endif
    }
}