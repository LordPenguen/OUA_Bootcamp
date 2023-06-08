using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Transform mainCameraTransform;
    Vector3 velocity;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 localMoveVelocity = transform.TransformDirection(velocity) * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + localMoveVelocity);
    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Jump(float jumpForce)
    {
        playerRigidbody.AddForce(transform.up * jumpForce);
    }

    public void UpdateMouseLook(float cameraHorizontalRotation, float cameraVerticalRotation)
    {
        transform.Rotate(Vector3.up * cameraHorizontalRotation);
        mainCameraTransform.localEulerAngles = Vector3.left * cameraVerticalRotation;
    }
}
