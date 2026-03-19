using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject dimBackground;

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        dimBackground.SetActive(true);
    }

    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        dimBackground.SetActive(false);
    }
}