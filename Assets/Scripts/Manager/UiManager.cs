using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UiManager : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private Text scorePanel;

        [SerializeField] private Image livesDisplay;

        [SerializeField] private List<Sprite> livesSprites;

        [SerializeField] private Text gameOver;

        [SerializeField] private Text restartTip;
#pragma warning restore 0649

        private const string ScorePrefix = "Score: ";
        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (gameManager is null) Debug.LogError("GameManager not found!");
            
            UpdateScore(0);
        }


        public void UpdateScore(int newScore)
        {
            scorePanel.text = ScorePrefix + newScore;
        }

        public void UpdateLivesDisplay(int numLives)
        {
            livesDisplay.sprite = livesSprites[numLives];
            if (numLives > 0) return;
            
            gameManager.GameOver();
            restartTip.gameObject.SetActive(true);
            StartCoroutine(FlickerGameOver());
        }

        private IEnumerator FlickerGameOver()
        {
            var show = true;
            while (true)
            {
                gameOver.gameObject.SetActive(show);
                show = !show;
                yield return new WaitForSeconds(.333f);
            }
        }

        public void StartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}