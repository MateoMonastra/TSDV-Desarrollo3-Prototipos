using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPatrolling : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _range;

    [SerializeField] private Transform _centrePoint;

    [SerializeField] private Transform _player;

    [SerializeField] private float _safeDistance = 6f;
    [SerializeField] private float _patrolingSpeed = 4f;
    [SerializeField] private float _fleeSpeed = 6f;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);

        if(distance < _safeDistance )
        {
            Vector3 directionToPlayer = transform.position - _player.position;
            Vector3 fleeDirection = transform.position + directionToPlayer.normalized * _safeDistance;

            _agent.speed = _fleeSpeed;
            _agent.SetDestination(fleeDirection);
        }
        else if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            Vector3 point;
            _agent.speed = _patrolingSpeed;

            if(RandomPoint(_centrePoint.position, _range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.yellow, 1.0f);
                _agent.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        NavMeshHit hit;

        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
