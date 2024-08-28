using System;
using Gameplay.GhostMechanics;
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
        [SerializeField] private Transform player;

        private bool _isActive;
        [SerializeField] private GameObject tornado;
        private Vector3? _collision;
        private Ray _ray;

        private Quaternion _left;
        private Quaternion _right;

        private Vector3 _leftBoundary;
        private Vector3 _rightBoundary;

        private bool _isVacuumingGhost;

        public float timeToRotate = 2f;
        private float _timer = 0f;
        public float angleRange = 90f;
        public int hp = 100;

        [SerializeField] private Rigidbody _rb;


        private void Start()
        {
            _left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(maxAngle, Vector3.up);
            _leftBoundary = _left * target.forward;
            _rightBoundary = _right * target.forward;
            transform.localScale = new Vector3(Mathf.Abs(((_leftBoundary.x * 2) * renderDistance)),
                transform.localScale.y, transform.localScale.z);
        }

        void Update()
        {
            if ((_isVacuumingGhost))
            {
                _timer += Time.deltaTime;

                if (_timer > timeToRotate)
                {
                    ChangeRotation();
                    _timer -= timeToRotate;
                }

                player.transform.Translate(transform.forward * Time.deltaTime);
            }
        }

        public void TurnOn()
        {
            _isActive = true;
            //tornado.SetActive(true);
        }

        public void TurnOff()
        {
            _isActive = false;
            //tornado.SetActive(false);
        }

        private void OnTriggerStay(Collider other)
        {
            VacuumObject(other);
        }

        private void VacuumObject(Collider other)
        {
            if (!_isActive)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer($"Ghost"))
                {
                    //other.GetComponent<RandomPatrolling>().StopBeingVacuumed();
                }

                return;
            }

            _isVacuumingGhost = (other.gameObject.layer == LayerMask.NameToLayer($"Ghost"));

            if (!_isVacuumingGhost)
            {
                var angleToObject = Vector3.Angle(target.forward, other.transform.position - target.position);

                if (!(angleToObject <= maxAngle)) return;

                _ray = new Ray(target.position, other.transform.position - target.position);

                if (Physics.Raycast(_ray, out var hit, renderDistance, wallLayer))
                {
                    _collision = hit.point;
                    return;
                }

                var rb = other.GetComponent<Rigidbody>();

                var direction = (target.position - other.transform.position).normalized;

                //if (other.gameObject.layer == LayerMask.NameToLayer($"Ghost"))
                //{
                //    //rb.AddForce(direction * speed, ForceMode.Impulse);
                //    //other.GetComponent<RandomPatrolling>().StartBeingVacuumed();

                //    _isVacuumingGhost = true;
                //}
                //else
                //{

                rb.AddForce(direction * speed, ForceMode.Impulse);

                //}

                _collision = null;
            }
            else
            {
                other.gameObject.transform.SetParent(player);
            }
        }

        void ChangeRotation()
        {
            float randomAngle = UnityEngine.Random.Range(-angleRange / 2, angleRange / 2);

            Vector3 rot = player.transform.rotation.eulerAngles;

            rot.y += randomAngle;

            _rb.transform.rotation = Quaternion.Euler(rot);
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