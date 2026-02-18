using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool isPaused = false;

    public void Awake()
    {
        pauseMenuUI.SetActive(false);
    }
    public void Resume()
    {
        if (isPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void TogglePause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!isPaused)
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }

            else if (isPaused)
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
