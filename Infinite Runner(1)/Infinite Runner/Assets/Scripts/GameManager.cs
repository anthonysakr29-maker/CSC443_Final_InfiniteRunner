using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameConfig config;
    [SerializeField] private GameObject deathEffectPrefab;
    public float ScrollSpeed { get; private set; }
    public float Distance { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsPaused { get; private set; }
    public int HighScore { get; private set; }
    public int Coins { get; private set; }

    [SerializeField] private GameObject pauseButton;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (config != null)
            ScrollSpeed = config.startSpeed;

        Time.timeScale = 1f;

        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            Restart();

        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame && !IsGameOver)
            TogglePause();

        if (IsGameOver)
            return;

        if (IsPaused)
            return;
        if (config == null) return;

        if (pauseButton != null)
            pauseButton.SetActive(!GameManager.Instance.IsGameOver);

        ScrollSpeed = Mathf.Min(
            ScrollSpeed + config.speedIncreaseRate * Time.deltaTime,
            config.maxSpeed
        );

        Distance += ScrollSpeed * Time.deltaTime;
    }

    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;
        IsPaused = false;
        Time.timeScale = 1f;
        ScrollSpeed = 0f;

        Animator playerAnimator = FindFirstObjectByType<PlayerController>().GetComponentInChildren<Animator>();

        if (playerAnimator != null)
            playerAnimator.SetTrigger("Die");

        int finalScore = Mathf.FloorToInt(Distance);

        if (finalScore > HighScore)
        {
            HighScore = finalScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDeath();

        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, FindFirstObjectByType<PlayerController>().transform.position, Quaternion.identity);

        Debug.Log("Game Over. Press R to restart. Final score: " + Mathf.FloorToInt(Distance));
    }

    public void TogglePause()
    {
        if (IsGameOver) return;

        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (IsGameOver) return;

        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void AddCoins(int amount)
    {
        Coins += amount;
    }
}