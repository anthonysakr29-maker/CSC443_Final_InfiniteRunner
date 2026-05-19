using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject panelToHideWhileOpen;

    private void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (AudioManager.Instance == null) return;

        if (musicSlider != null)
        {
            musicSlider.value = AudioManager.Instance.GetMusicVolume();
            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = AudioManager.Instance.GetSFXVolume();
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }

    public void OpenOptions()
    {
        if (panelToHideWhileOpen != null)
            panelToHideWhileOpen.SetActive(false);

        if (optionsPanel != null)
            optionsPanel.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        if (panelToHideWhileOpen != null)
            panelToHideWhileOpen.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayButtonClick();
    }

    public bool IsOpen => optionsPanel != null && optionsPanel.activeSelf;
}