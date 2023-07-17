using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanGame
{
    public class PacmanGameManager : MonoBehaviour
    {
        public event System.Action OnGameOver;
        public event System.Action OnPlayerDeath;
        public event System.Action OnGameStarted;
        public event System.Action OnLevelCompleted;

        [SerializeField] GameObject cherries;
        [SerializeField] GameObject powerPellets;
        
        [Header("Ghost")]
        [SerializeField] Ghost ghostPrefab;
        [SerializeField] Transform ghostInitalPositionParent;
        [SerializeField] Material[] ghostMaterials;
        Vector3[] ghostInitialPositions;

        [Header("Player")]
        [SerializeField] Transform playerInitialTransform;
        [SerializeField] PacmanPlayer playerPrefab;

        int len;
        Ghost[] ghosts;
        PacmanPlayer player;

        bool gameStarted;
        bool gameRetry;
        bool gameOver;

        //private void Start()
        //{
        //}

        private void Update()
        {
            if (!gameStarted && !gameOver)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (OnGameStarted != null)
                    {
                        OnGameStarted();
                    }

                    Instantiate(cherries);
                    Instantiate(powerPellets);
                    InstantiatePlayer();
                    InstantiateGhosts();
                    gameStarted = true;
                }
            }

            if (gameStarted && gameRetry)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ResetLocations();
                    if (OnGameStarted != null)
                    {
                        OnGameStarted();
                    }
                }
            }
        }

        void InstantiateGhosts()
        {
            ghostInitialPositions = new Vector3[ghostInitalPositionParent.childCount];
            len = ghostInitialPositions.Length;
            ghosts = new Ghost[len];
            for (int i = 0; i < len; i++)
            {
                ghostInitialPositions[i] = ghostInitalPositionParent.GetChild(i).position;
                ghosts[i] = Instantiate(ghostPrefab, ghostInitialPositions[i], Quaternion.identity);
                ghosts[i].refreshRate = -((len / len - 1.1f - i) + i * .25f);
                ghosts[i].delayTime = 10 - ghosts[i].refreshRate * i;
                ghosts[i].GetComponent<Renderer>().material = ghostMaterials[i];
            }
        }

        void InstantiatePlayer()
        {
            player = Instantiate(playerPrefab, playerInitialTransform.position, playerInitialTransform.rotation);
        }

        void ResetLocations()
        {
            player.transform.position = playerInitialTransform.position;
            player.ResetPlayer();

            for (int i = 0; i < len; i++)
            {
                ghosts[i].transform.position = ghostInitialPositions[i];
                ghosts[i].ResetGhost();
            }
            gameRetry = false;
        }

        public void GameOver()
        {
            gameOver = true;
            gameStarted = false;
            print("game over");

            for (int i = 0; i < len; i++)
            {
                ghosts[i].OnPlayerCaptured();
            }

            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }

        public void PlayerDeath()
        {
            print("player captured");
            gameRetry = true;

            for (int i = 0; i < len; i++)
            {
                ghosts[i].OnPlayerCaptured();
            }

            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
        }

        public void LevelComplete()
        {
            for (int i = 0; i < len; i++)
            {
                ghosts[i].OnPlayerCaptured();
            }
            if (OnLevelCompleted != null)
            {
                OnLevelCompleted();
            }
        }
    }
}
