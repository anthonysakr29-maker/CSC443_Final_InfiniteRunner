using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text finalScoreText;

    private bool _gameOverShown;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        int score = Mathf.FloorToInt(GameManager.Instance.Distance);

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (GameManager.Instance.IsGameOver && !_gameOverShown)
        {
            _gameOverShown = true;

            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);

            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " + score;
        }
    }
}