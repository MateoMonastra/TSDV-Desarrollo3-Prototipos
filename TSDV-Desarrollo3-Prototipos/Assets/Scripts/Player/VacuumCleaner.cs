using System;
using Gameplay;
using Gameplay.GhostMechanics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

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

        public bool isActive;
        [SerializeField] private GameObject tornado;
        private Vector3? _collision;
        private Ray _ray;

        private Quaternion _left;
        private Quaternion _right;

        private Vector3 _leftBoundary;
        private Vector3 _rightBoundary;

        [SerializeField] private Transform _playerParent;
        [SerializeField] public bool isCapturingGhost;

        [SerializeField] private GameObject ADMinigameObj;
        [SerializeField] private GameObject SkillcheckMinigameObj;
        
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

            if (SkillcheckMinigameObj)
            {
                SkillcheckMinigameObj.GetComponent<SkillCheck>().OnWin += HandleSkillcheckWin;
                SkillcheckMinigameObj.GetComponent<SkillCheck>().OnLose += HandleSkillcheckLose;
            }

            isCapturingGhost = false;
        }

        private void HandleSkillcheckLose()
        {
            TeleportCharacter(ghost);
            ghost.transform.SetParent(null);
            TurnOff();
            isCapturingGhost = false;
            SkillcheckMinigameObj.SetActive(false);

            ghost.GetComponent<Ghost>().IsBeingVacuumed = false;
            ghost.GetComponent<RandomPatrolling>().enabled = true;
            ghost.GetComponent<NavMeshAgent>().enabled = true;
            ghost.GetComponent<TrashSpawning>().enabled = true;
            
        }

        private void HandleSkillcheckWin()
        {

            SkillcheckMinigameObj.SetActive(false);

            ghost.transform.SetParent(null);
            ghost.GetComponent<Ghost>().IsBeingVacuumed = true;
            ghost.GetComponent<Ghost>().stunned = true;

            isCapturingGhost = false;
        }

        public void TurnOn()
        {
            isActive = true;
            tornado.SetActive(true);
        }

        public void TurnOff()
        {
            isActive = false;
            tornado.SetActive(false);
            isCapturingGhost = false;

            //ADMinigameObj.GetComponentInChildren<ADMinigame>().ResetMinigame();
            //ADMinigameObj.SetActive(false);

            if (SkillcheckMinigameObj)
            {
                SkillcheckMinigameObj.SetActive(false);
            }

            if (ghost)
            {
                ghost.GetComponent<Ghost>().IsBeingVacuumed = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            VacuumObject(other);
        }

        private void VacuumObject(Collider other)
        {
            if (!isActive)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer($"Ghost"))
                {
                    other.transform.SetParent(null);
                    if (!other.GetComponent<Ghost>().stunned)
                    {
                        other.GetComponent<RandomPatrolling>().enabled = true;
                        other.GetComponent<NavMeshAgent>().enabled = true;
                        other.GetComponent<TrashSpawning>().enabled = true;
                    }
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

            if (other.gameObject.layer == LayerMask.NameToLayer($"Ghost") &&
                !other.GetComponent<Ghost>().IsBeingVacuumed)
            {
                isCapturingGhost = true;

                if (other.GetComponent<Ghost>().stunned)
                {
                    rb.AddForce(direction * speed, ForceMode.Impulse);
                    isCapturingGhost = false;
                }
                else
                {

                    other.GetComponent<RandomPatrolling>().enabled = false;
                    other.GetComponent<NavMeshAgent>().enabled = false;
                    other.GetComponent<TrashSpawning>().enabled = false;
                    
                    other.transform.SetParent(_playerParent);

                    //ADMinigameObj.SetActive(true);
                    if (SkillcheckMinigameObj)
                    {
                        SkillcheckMinigameObj.SetActive(true);
                    }

                    ghost = other;

                    //if (_wonCapture)
                    //{
                    //    rb.AddForce(direction * speed, ForceMode.Impulse);
                    //    isCapturingGhost = false;
                    //}
                }
            }
            else if (other.gameObject.layer != LayerMask.NameToLayer($"NotVacuumable") && !isCapturingGhost)
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