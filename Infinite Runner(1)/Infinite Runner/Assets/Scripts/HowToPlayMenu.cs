using UnityEngine;

public class HowToPlayMenu : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject panelToHideWhileOpen;

    private void Start()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        if (panelToHideWhileOpen != null)
            panelToHideWhileOpen.SetActive(false);

        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);

        if (panelToHideWhileOpen != null)
            panelToHideWhileOpen.SetActive(true);
    }
}