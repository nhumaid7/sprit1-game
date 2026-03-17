using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    public AudioSource music;   

    private bool isPaused = false;

    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        MusicManager.Instance.PauseMusic();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        MusicManager.Instance.ResumeMusic();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        MusicManager.Instance.RestartMusic();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
}