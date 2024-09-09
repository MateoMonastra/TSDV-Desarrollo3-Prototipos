using UnityEngine;

namespace Gameplay.GhostMechanics
{
    public class TrashSpawning : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float _spawnInterval = 3f;
        private float _timer;

        void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _spawnInterval && !GetComponent<RandomPatrolling>().isBeingVacuumed)
            {
                Instantiate(_prefab, spawnPoint.position, Quaternion.identity);
                _timer = 0f;
            }
        }
    }
}
