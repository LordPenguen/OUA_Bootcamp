using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace PacmanGame
{
    [RequireComponent(typeof(Rigidbody))]
    public class PacmanPlayerController : MonoBehaviour
    { 
        private Vector3 velocity;
        private Rigidbody myRigidbody;

        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        public void SetVelocity(Vector3 _velocity)
        {
            velocity = _velocity;
        }

        private void FixedUpdate()
        {
            myRigidbody.MovePosition(myRigidbody.position + velocity * Time.deltaTime);
        }
    }
}
