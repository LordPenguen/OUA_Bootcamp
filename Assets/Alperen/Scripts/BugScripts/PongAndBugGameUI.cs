using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BugGameNameSpace
{
    public class PongAndBugGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject pongFrame;
        [SerializeField] private GameObject bugGameInstructions;
        [SerializeField] private TMP_Text pongScore;

        int playerScore = 0;
        int aiScore = 0;

        private void Start()
        {
            FindObjectOfType<Ball>().OnScored += AddScore;
            FindObjectOfType<PongGameManager>().OnGameChange += OnNextGame;
        }

        void AddScore(bool isPlayerScored)
        {
            string scoreText = isPlayerScored ? ++playerScore + "X" + aiScore : playerScore + "X" + ++aiScore;
            pongScore.text = scoreText;
        }

        void OnNextGame()
        {
            pongScore.gameObject.SetActive(false);
            pongFrame.SetActive(false);
        }
    }
}
