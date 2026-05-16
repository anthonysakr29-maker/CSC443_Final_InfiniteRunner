using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Pause")]
    [SerializeField] private GameObject pausePanel;

    private bool _gameOverShown;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        int score = Mathf.FloorToInt(GameManager.Instance.Distance);

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (pausePanel != null)
            pausePanel.SetActive(GameManager.Instance.IsPaused);

        if (GameManager.Instance.IsGameOver && !_gameOverShown)
        {
            _gameOverShown = true;

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " + score;

            if (highScoreText != null)
                highScoreText.text = "High Score: " + GameManager.Instance.HighScore;
        }
    }
}