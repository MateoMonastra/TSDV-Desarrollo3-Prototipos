using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Running : MonoBehaviour
{
    private Rigidbody _rb;

    private Vector3 _dir = Vector3.zero;

    [SerializeField] private float _movementForce;
    [SerializeField] private float _counterMovementForce;
    [SerializeField] private float _rotationSpeed = 5;

    private Vector3 _counterMovement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _counterMovement = new Vector3(-_rb.velocity.x * _counterMovementForce, 0, -_rb.velocity.z * _counterMovementForce);

        _rb.AddForce(_dir.normalized * _movementForce + _counterMovement);

        float angle = Vector3.SignedAngle(transform.forward, _dir, transform.up);

        transform.Rotate(transform.up, angle * Time.deltaTime * _rotationSpeed);
    }

    public void SetDir(Vector3 newDir)
    {
        _dir = newDir;
    }
}
