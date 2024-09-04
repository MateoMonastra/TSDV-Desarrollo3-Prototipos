using System;
using Gameplay.GhostMechanics;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class VacuumCleaner : MonoBehaviour
    {
        public Action OnVacuumGhost;

        [SerializeField] private Transform target;
        [SerializeField] private float speed;
        [SerializeField] private float maxAngle = 45.0f;
        [SerializeField] private float renderDistance = 5.0f;
        [SerializeField] private LayerMask wallLayer;

        [SerializeField] private ChallengeManager challengeManager;

        private bool _isActive;
        [SerializeField] private GameObject tornado;
        private Vector3? _collision;
        private Ray _ray;

        private Quaternion _left;
        private Quaternion _right;

        private Vector3 _leftBoundary;
        private Vector3 _rightBoundary;

        [SerializeField] private Transform _playerParent;
        public bool isCapturingGhost;

        [SerializeField] private GameObject ADMinigame;
        private bool _wonCapture = false;
        private Collider ghost;

        public Vector2 areaSize; 
        public Vector2 areaCenter;

        private void Start()
        {
            _left = Quaternion.AngleAxis(-maxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(maxAngle, Vector3.up);
            _leftBoundary = _left * target.forward;
            _rightBoundary = _right * target.forward;
            transform.localScale = new Vector3(Mathf.Abs(((_leftBoundary.x * 2) * renderDistance)),
                transform.localScale.y, transform.localScale.z);

            ADMinigame.GetComponentInChildren<ADMinigame>().OnWin += HandleWin;
            ADMinigame.GetComponentInChildren<ADMinigame>().OnLose += HandleLose;
        }

        private void HandleLose()
        {
            _wonCapture = false;
            TeleportCharacter(ghost);
            ghost.transform.SetParent(null);
            TurnOff();
            isCapturingGhost = false;
            ADMinigame.SetActive(false);

            ghost.GetComponent<Ghost>().IsBeingVacuumed = false;
        }

        private void HandleWin()
        {
            _wonCapture = true;

            ADMinigame.SetActive(false);

            ghost.GetComponent<Ghost>().IsBeingVacuumed = true;

            isCapturingGhost = false;
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
            isCapturingGhost = false;

            ADMinigame.GetComponentInChildren<ADMinigame>().ResetMinigame();
            ADMinigame.SetActive(false);

            ghost.GetComponent<Ghost>().IsBeingVacuumed = false;
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
                    other.transform.SetParent(null);
                }

                return;
            }

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

            if (other.gameObject.layer == LayerMask.NameToLayer($"Ghost") && !other.GetComponent<Ghost>().IsBeingVacuumed)
            {
                isCapturingGhost = true;

                other.transform.SetParent(_playerParent);
                
                ADMinigame.SetActive(true);

                ghost = other;

                if (_wonCapture)
                {
                    rb.AddForce(direction * speed, ForceMode.Impulse);
                    isCapturingGhost = false;
                }
            }
            else
            {
                rb.AddForce(direction * speed, ForceMode.Impulse);
            }

            _collision = null;
        }

        void TeleportCharacter(Collider other)
        {
            float randomX = UnityEngine.Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
            float randomY = UnityEngine.Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);

            Vector3 randomPosition = new Vector3(randomX, other.transform.position.y, randomY);

            other.transform.position = randomPosition;
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