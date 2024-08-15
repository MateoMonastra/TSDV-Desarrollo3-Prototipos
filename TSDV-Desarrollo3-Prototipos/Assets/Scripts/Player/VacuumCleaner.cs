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

        private Collider _collider;
        private Vector3? _collision;
        private Ray _ray;
        Quaternion left ;
        Quaternion right ;

        Vector3     leftBoundary;
        Vector3     rightBoundary;
        private void Start()
        {
            _collider = GetComponent<Collider>();
            
            left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            right = Quaternion.AngleAxis(maxAngle, Vector3.up);
            leftBoundary = left * target.forward;
            rightBoundary = right * target.forward;
            transform.localScale = new Vector3(Mathf.Abs(((leftBoundary.x*2) * renderDistance) ), transform.localScale.y, transform.localScale.z);
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
            var angleToObject = Vector3.Angle(target.forward, other.transform.position - target.position);
            if (angleToObject <= maxAngle)
            {
                _ray = new Ray(target.position,  other.transform.position - target.position);

                if (Physics.Raycast(_ray, out RaycastHit hit, renderDistance, wallLayer))
                {
                    _collision = hit.point;
                    return;
                }

                other.transform.position = Vector3.MoveTowards(other.transform.position, target.position,
                    speed * Time.deltaTime);
                _collision = null;
            }
        }
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            right = Quaternion.AngleAxis(maxAngle, Vector3.up);
            leftBoundary = left * target.forward;
            rightBoundary = right * target.forward;
            
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(target.position, target.position + target.forward * renderDistance);

            Gizmos.DrawLine(target.position, target.position + leftBoundary * renderDistance);

            Gizmos.DrawLine(target.position, target.position + rightBoundary * renderDistance);
            Gizmos.DrawRay(_ray);
            if (_collision != null)
            {
                Gizmos.DrawSphere(_collision.Value, 0.5f);
            }
        }
#endif
    }
}