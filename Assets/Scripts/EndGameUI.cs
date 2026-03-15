using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public GameObject shoppingListPanel;
    public GameObject timerText;

    public void ShowEndScreen(bool won, int collected, int total, float timeUsed)
    {
        endGamePanel.SetActive(true);
        shoppingListPanel.SetActive(false);
        timerText.SetActive(false);

        resultText.text = won ? "You Win!" : "You Lose!";
        scoreText.text = "Score: " + collected + " / " + total;
        timeText.text = "Remaining time: " + Mathf.Ceil(timeUsed) + "s";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}