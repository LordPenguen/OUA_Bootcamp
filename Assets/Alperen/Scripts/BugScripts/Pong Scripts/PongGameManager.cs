using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BugGameNameSpace
{
    public class PongGameManager : MonoBehaviour
    {
        public event System.Action OnGameChange;
        public event System.Action OnGameStarted;

        [Header("Screen Points")]
        [SerializeField] Transform topLeft;
        [SerializeField] Transform topRight;
        [SerializeField] Transform bottomLeft;
        [SerializeField] Transform bottomRight;

        [Header("Gameobjects")]
        [SerializeField] private Ball ball;
        [SerializeField] private Paddle paddle;
        [SerializeField] private Paddle aiPaddle;

        [Header("Game Transition Delay Fields")]
        [SerializeField] private float newGameDelay = 1f;
        [SerializeField] private float nextGameTransitionDelayTime = 5f;

        public static Vector3 topLeftPos;
        public static Vector3 topRightPos;
        public static Vector3 bottomLeftPos;
        public static Vector3 bottomRightPos;

        Vector3 ballPos = Vector3.zero;
        Vector3 aiPos = Vector3.zero;
        Vector3 playerPos= Vector3.zero;
        Ball newBall;
        Paddle playerPaddle;
        Paddle newAiPaddle;
        bool nextGameTransition = false;
        bool pongGameStarted;

        void Awake()
        {
            topLeftPos = topLeft.position;
            topRightPos = topRight.position;
            bottomLeftPos = bottomLeft.position;
            bottomRightPos = bottomRight.position;

            //InitializeBallAndPaddles();
        }

        void InitializeBallAndPaddles()
        {
            ballPos = new Vector3((topLeftPos.x + topRightPos.x) / 2, (topLeftPos.y + bottomLeftPos.y) / 2, topLeftPos.z);
            newBall = Instantiate(ball, ballPos, Quaternion.identity);
            newBall.OnScored += OnScorePoint;

            playerPos = new Vector3(topLeftPos.x + .05f, (topLeftPos.y + bottomLeftPos.y) / 2, bottomLeftPos.z);
            aiPos = new Vector3(topRightPos.x - .05f, (topRightPos.y + bottomRightPos.y) / 2, bottomRightPos.z);

            playerPaddle = Instantiate(paddle, playerPos, Quaternion.identity);
            newAiPaddle = Instantiate(aiPaddle, aiPos, Quaternion.identity);
            //Paddle secondPaddle = Instantiate(paddle) as Paddle;
        }

        void OnScorePoint(bool isPlayerScored)
        {
            newBall.transform.position = ballPos;
            playerPaddle.transform.position = playerPos;
            newAiPaddle.transform.position = aiPos;
            Invoke("StartNewGame", newGameDelay);
        }

        void StartNewGame()
        {
            newBall.BallInitialize();

            if (!nextGameTransition)
            {
                nextGameTransition = true;
                Invoke("NextGame", nextGameTransitionDelayTime);
            }
        }

        void NextGame()
        {
            Destroy(newBall.gameObject);
            Destroy(playerPaddle.gameObject);
            Destroy(newAiPaddle.gameObject);

            if (OnGameChange != null)
            {
                OnGameChange();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                TesterNextGame();
            }
            if (!pongGameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    InitializeBallAndPaddles();
                    pongGameStarted = true;
                    if (OnGameStarted != null)
                    {
                        OnGameStarted();
                    }
                }
            }
        }

        void TesterNextGame()
        {
            OnScorePoint(false);
        }
    }
}