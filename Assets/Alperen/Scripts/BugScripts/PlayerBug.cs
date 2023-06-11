using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBug : MonoBehaviour
{
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 velocity = inputVector * 1.3f;
        moveAmount = Vector3.SmoothDamp(velocity, moveAmount, ref smoothMoveVelocity, .15f);
        LookAtpoint(velocity);
    }

    void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveAmount * Time.deltaTime);
    }

    void LookAtpoint(Vector3 lookPoint)
    {
        if (lookPoint != Vector3.zero)
        {
            //Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
            transform.forward = lookPoint;
        }
    }
}
