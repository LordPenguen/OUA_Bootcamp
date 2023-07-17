using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanGame
{
    [RequireComponent(typeof(PacmanPlayerController))]
    public class PacmanPlayer : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        PacmanPlayerController playerController;
        bool dead;
        int playerLifeCount = 3;
        Vector3 moveVelocity;
        PacmanGameManager gameManager;

        void Start()
        {
            playerController = GetComponent<PacmanPlayerController>();
            gameManager = FindObjectOfType<PacmanGameManager>();
        }

        void Update()
        {
            if (!dead)
            {
                Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); 
                moveVelocity = (moveInput.normalized) * speed;
                playerController.SetVelocity(moveVelocity);
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

        public void ResetPlayer()
        {
            dead = false;
        }

        public void Die()
        {
            moveVelocity = Vector3.zero;
            playerController.SetVelocity(moveVelocity);
            dead = true;

            playerLifeCount--;

            if (playerLifeCount <= 0)
            {
                gameManager.GameOver();
                // restart game
                //if (OnGameOver != null)
                //{
                //    OnGameOver();
                //}
            }
            else
            {
                gameManager.PlayerDeath();
                // restart without losing progress
                //if (OnPlayerDeath != null)
                //{
                //    OnPlayerDeath();
                //}
            }
        }
    }
}