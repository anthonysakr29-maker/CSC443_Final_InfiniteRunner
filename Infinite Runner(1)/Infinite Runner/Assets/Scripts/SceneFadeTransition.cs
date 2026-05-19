using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFadeTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private bool fadeFromBlackOnStart = true;

    private bool _isTransitioning;

    private void Start()
    {
        if (fadeImage == null) return;

        if (fadeFromBlackOnStart)
            StartCoroutine(FadeFromBlack());
        else
            SetAlpha(0f);
    }

    public void FadeToScene(string sceneName)
    {
        if (_isTransitioning) return;

        if (fadeImage == null)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
            return;
        }

        StartCoroutine(FadeToSceneRoutine(sceneName));
    }

    private IEnumerator FadeFromBlack()
    {
        fadeImage.gameObject.SetActive(true);
        SetAlpha(1f);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            SetAlpha(1f - timer / fadeDuration);
            yield return null;
        }

        SetAlpha(0f);
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeToSceneRoutine(string sceneName)
    {
        _isTransitioning = true;

        fadeImage.gameObject.SetActive(true);
        SetAlpha(0f);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            SetAlpha(timer / fadeDuration);
            yield return null;
        }

        SetAlpha(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }
}