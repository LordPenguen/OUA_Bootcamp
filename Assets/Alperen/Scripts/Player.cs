using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class Player : MonoBehaviour
{
    [Header ("Movement Input")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;
    float verticalLookRatation;

    [Header ("Mouse Input")]
    [SerializeField] private Vector2 mouseSensitivityXY = new Vector2(250f, 250f);
    [SerializeField] private Vector2 verticalLookThreshold = new Vector2(-60, 65);
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;

    [Header ("Jump")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private float msBetweenJumps = .1f;
    bool grounded;
    float nextJumpTime;
    float playerHeight;

    PlayerController playerController;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerHeight = transform.localScale.y;
    }

    private void Update()
    {
        //Debug.DrawLine(transform.position, (transform.position -transform.up * 1.1f), Color.red);
        MovementInput();
        MouseInput();
        JumpInput();
    }

    void MovementInput()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount;

        if (Input.GetKey(KeyCode.LeftShift))
            targetMoveAmount = moveDir * runSpeed;
        else targetMoveAmount = moveDir * walkSpeed;

        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        playerController.SetVelocity(moveAmount);
    }

    void MouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityXY.x * Time.deltaTime;
        verticalLookRatation += Input.GetAxis("Mouse Y") * mouseSensitivityXY.y * Time.deltaTime;
        verticalLookRatation = Mathf.Clamp(verticalLookRatation, verticalLookThreshold.x, verticalLookThreshold.y);

        playerController.UpdateMouseLook(mouseX, verticalLookRatation);
    }

    void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded && Time.time > nextJumpTime)
            {
                nextJumpTime = Time.time + msBetweenJumps / 1000;
                playerController.Jump(jumpForce);
            }

            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, playerHeight + .1f, groundMask))
                grounded = true;
            else grounded = false;
        }  
    }
   
}
    
