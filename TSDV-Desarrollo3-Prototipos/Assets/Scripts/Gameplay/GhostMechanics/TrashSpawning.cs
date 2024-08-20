using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawning : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            Instantiate(_prefab, transform.position, Quaternion.identity);
            _timer = 0f;
        }
    }
}
