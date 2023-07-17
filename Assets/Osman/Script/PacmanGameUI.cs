using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace PacmanGame
{
    public class PacmanGameUI : MonoBehaviour
    {
        [SerializeField] private GameObject pressSpaceText;
        [SerializeField] private Image fadeImage;
        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private TMP_Text gameOverScoreText;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject winUI;

        PacmanGameManager gameManager;
        Color initialFadeImageColor;

        private void Start()
        {
            initialFadeImageColor = fadeImage.color;
            gameManager = FindObjectOfType<PacmanGameManager>();
            gameManager.OnGameStarted += OnGameStarted;
            gameManager.OnPlayerDeath += OnPlayerDeath;
            gameManager.OnGameOver += OnGameOver;
        }

        private void Update()
        {
            scoreText.text = "Score: " + PacmanCollider.Score; 
        }

        void OnGameStarted()
        {
            StartCoroutine(Fade(initialFadeImageColor, Color.clear, 1, false));
            pressSpaceText.SetActive(false);
        }

        void OnPlayerDeath()
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(Fade(Color.clear, initialFadeImageColor, 1, true));
            pressSpaceText.SetActive(true);
        }

        void OnGameOver()
        {
            fadeImage.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            gameOverScoreText.text = "Score: " + PacmanCollider.Score;
            StartCoroutine(Fade(Color.clear, initialFadeImageColor, 2, true));
            gameOverUI.SetActive(true);
        }

        public void StartNewGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitToMallScene()
        {
            //print("going to mall...");
            SceneManager.LoadScene("ElifScene");
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
