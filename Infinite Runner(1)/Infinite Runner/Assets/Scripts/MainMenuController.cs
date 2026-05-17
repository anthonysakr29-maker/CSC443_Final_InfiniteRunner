using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private TMP_Text highScoreText;

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}