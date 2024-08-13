using UnityEngine;

namespace Player
{
    public class VacuumCleaner : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private float maxAngle = 45.0f;

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
                var angleToObject = Vector3.Angle(target.forward, other.transform.position - target.position);
                if (angleToObject <= maxAngle)
                {
                    other.transform.position = Vector3.MoveTowards(other.transform.position, target.position,
                        speed * Time.deltaTime);
                }
        }
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(target.position, target.position + target.forward * 5);
        
            Quaternion Left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            Quaternion Right = Quaternion.AngleAxis(maxAngle, Vector3.up);
        
            Vector3 leftBoundary = Left * target.forward;
            Vector3 rightBoundary = Right * target.forward;
        
            Gizmos.DrawLine(target.position, target.position + leftBoundary * 5);
        
            Gizmos.DrawLine(target.position, target.position + rightBoundary * 5);
        }
#endif
    }
}