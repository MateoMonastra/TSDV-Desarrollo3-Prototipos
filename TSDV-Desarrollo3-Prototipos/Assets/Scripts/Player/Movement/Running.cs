using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Running : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 dir = Vector3.zero;

    [SerializeField] private float movementForce;

    [SerializeField] private float counterMovementForce;

    private Vector3 counterMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        counterMovement = new Vector3(-rb.velocity.x * counterMovementForce, 0, -rb.velocity.z * counterMovementForce);

        transform.forward = Vector3.Lerp(transform.forward, dir, 0.4f);

        rb.AddForce(dir.normalized * movementForce + counterMovement);
    }

    public void SetDir(Vector3 newDir)
    {
        dir = newDir;
    }
}
