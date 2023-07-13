using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PacmanPlayerController))]
public class PacmanPlayer : MonoBehaviour
{
    public float speed = 1f;
    private PacmanPlayerController pacmanPlayerController;
    private bool isDead;
    private Vector3 moveVelocity;
    void Start()
    {
        pacmanPlayerController = GetComponent<PacmanPlayerController>();
        
    }

    
    void Update()
    {
        if (!isDead)
        {
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
            moveVelocity = (moveInput.normalized) * speed;
            pacmanPlayerController.SetVelocity(moveVelocity);
            LookAtFace(moveVelocity);            
        }
        
    }

    void LookAtFace(Vector3 lookPoint)
    {
        if (lookPoint != Vector3.zero)
        {
            transform.forward = lookPoint;
        }
    }

    public void Die()
    {
        moveVelocity = Vector3.zero;
        pacmanPlayerController.SetVelocity(moveVelocity);
        isDead = true;
    }
}
