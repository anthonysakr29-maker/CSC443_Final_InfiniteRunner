using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "SampleScene";
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private SceneFadeTransition sceneFadeTransition;
    public void PlayGame()
    {
        Time.timeScale = 1f;
        if (sceneFadeTransition != null)
            sceneFadeTransition.FadeToScene(gameSceneName);
        else
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