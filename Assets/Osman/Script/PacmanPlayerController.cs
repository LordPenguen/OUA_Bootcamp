using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PacmanPlayerController : MonoBehaviour
{ 
    private Vector3 velocity;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + velocity * Time.deltaTime);
    }
}
