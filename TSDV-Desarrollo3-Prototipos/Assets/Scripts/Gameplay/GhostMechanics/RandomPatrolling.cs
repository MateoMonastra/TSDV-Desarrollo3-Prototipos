using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.GhostMechanics
{
    public class RandomPatrolling : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _range;

        [SerializeField] private Transform _centrePoint;

        [SerializeField] private Transform _player;

        [SerializeField] private float _safeDistance = 5f;
        [SerializeField] private float _patrolingSpeed = 3f;
        [SerializeField] private float _fleeSpeed = 5f;

        public bool _isBeingVacuumed = false;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            float distance = Vector3.Distance(transform.position, _player.position);

            if(_isBeingVacuumed)
            {
                return;
            }

            if(distance < _safeDistance)
            {
                Flee();
            }
            else if(_agent.remainingDistance <= _agent.stoppingDistance)
            {
                Patrol();
            }
        }

        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        private void Flee()
        {
            if(_isBeingVacuumed)
                return;
            
            Vector3 directionToPlayer = transform.position - _player.position;
            Vector3 fleeDirection = transform.position + directionToPlayer.normalized * _safeDistance;

            _agent.speed = _fleeSpeed;
            _agent.SetDestination(fleeDirection);
        }

        private void Patrol()
        {
            _agent.speed = _patrolingSpeed;

            if (RandomPoint(_centrePoint.position, _range, out var point))
            {
                Debug.DrawRay(point, Vector3.up, Color.yellow, 1.0f);
                _agent.SetDestination(point);
            }
        }

        public void StartBeingVacuumed()
        {
            if(_isBeingVacuumed)
                return;

            Debug.Log("fantasma aspirandose");
            _isBeingVacuumed = true;
            _agent.updatePosition = false;
        }

        public void StopBeingVacuumed()
        {
            _agent.updatePosition = true;
            _isBeingVacuumed = false;
            Patrol();
        }
    }
}

