using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PacmanGame
{
    public class PacmanCollider : MonoBehaviour
    {
        public static int Score { get; private set; }
        public WinMusicController winMusicController;

        private void Start()
        {
            Score = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ghost"))
            {
                GetComponent<PacmanPlayer>().Die();
            }

            if (other.CompareTag("Cherry"))
            {
                Destroy(other.gameObject);
                Score++;

                if (Score >= 82)
                {
                    print("Level completed");
                    FindAnyObjectByType<PacmanGameManager>().LevelComplete();
                    if (winMusicController != null)
                    {
                        winMusicController.PlayWinSound();
                    }
                    
                }
            }
        }
    }
}
