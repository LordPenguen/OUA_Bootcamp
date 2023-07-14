using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace BugGameNameSpace
{
    public class PongAndBugGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject pongFrame;
        [SerializeField] private GameObject bugGameInstructions;
        [SerializeField] private TMP_Text pongScore;
        [SerializeField] private Image fadeImage;
        [SerializeField] private GameObject pressSapceText;
        [SerializeField] private TMP_Text bugScore;


        [Header("Health UI")]
        [SerializeField] private Image[] healthbarImages;
        [SerializeField] private Color healthColor;

        int playerScore = 0;
        int aiScore = 0;

        float score = 0;

        private void Start()
        {
            PongGameManager pongManager = FindObjectOfType<PongGameManager>();
            pongManager.OnGameStarted += OnGameStarted;
            pongManager.OnGameChange += OnNextGame;

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
            bugGameInstructions.SetActive(true);
            Invoke("SubscribeMethod", 3.2f);
        }

        void SetHealthBar(int currentHealth)
        {
            int healthbarImageLength = healthbarImages.Length;

            for (int i = 0; i < healthbarImageLength; i++)
            {
                if (i < currentHealth)
                {
                    healthbarImages[i].color = healthColor;
                }
                else healthbarImages[i].color = Color.gray;
            }
        }

        void SubscribeMethod()
        {
            FindAnyObjectByType<PlayerBug>().OnTakeDamage += SetHealthBar;
            FindObjectOfType<Web>().OnTargetDeath += OnTargetDeath;
            SetHealthBar(10);
        }

        void OnGameStarted()
        {
            StartCoroutine(Fade(fadeImage.color, Color.clear, 1.5f, false));
            pressSapceText.SetActive(false);
            FindObjectOfType<Ball>().OnScored += AddScore;
        }

        void OnTargetDeath()
        {
            score++;
            bugScore.text = "ERROR:" + score;

        }

        IEnumerator Fade(Color from, Color to, float time, bool isVisible)
        {
            float speed = 1 / time;
            float percent = 0;

            while (percent < 1)
            {
                percent += Time.deltaTime * speed;
                fadeImage.color = Color.Lerp(from, to, percent);
                yield return null;
            }
            if (!isVisible)
            {
                fadeImage.gameObject.SetActive(isVisible);
            }
        }
    }
}
