using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float startTime = 60f;
    public float timeRemaining = 60f;
    public TextMeshProUGUI timerText;
    public EndGameUI endGameUI;

    private bool gameEnded = false;

    void Start()
    {
        timeRemaining = startTime;
    }

    void Update()
    {
        if (gameEnded) return;

        // If player collected everything before time ends
        if (GameManager.Instance != null && GameManager.Instance.HasWon())
        {
            EndGame(true);
            return;
        }

        // Normal countdown
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0)
                timeRemaining = 0;

            if (timerText != null)
            {
                timerText.text = "Time: " + Mathf.Ceil(timeRemaining) + "s";
            }
        }
        else
        {
            // Timer finished before player collected all items
            EndGame(false);
        }
    }

    void EndGame(bool won)
    {
        if (gameEnded) return;
        gameEnded = true;

        if (timerText != null)
        {
            timerText.text = "Remaining time: " + Mathf.Ceil(timeRemaining) + "s";
        }

        endGameUI.ShowEndScreen(
            won,
            GameManager.Instance.CollectedCorrectCount,
            GameManager.Instance.TotalRequiredItems,
            timeRemaining
        );
    }
}