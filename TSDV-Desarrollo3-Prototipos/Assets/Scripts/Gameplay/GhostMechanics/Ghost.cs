using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float timeToRotate = 2f; 
    private float _timer = 0f;
    public float angleRange = 90f;
    private Rigidbody _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Movimiento del fantasma

        _timer += Time.deltaTime;

        if (_timer > timeToRotate)
        {
            ChangeRotation();
            _timer -= timeToRotate;
        }

        transform.Translate(transform.forward * Time.deltaTime);
    }

    void ChangeRotation()
    {
        float randomAngle = Random.Range(-angleRange/2, angleRange/2);

        Vector3 rot = transform.rotation.eulerAngles;

        rot.y += randomAngle;

        _rb.transform.rotation = Quaternion.Euler(rot);

    }
}
