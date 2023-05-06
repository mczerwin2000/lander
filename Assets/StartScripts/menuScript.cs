using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gameIsPaused)
            {
                Resume();
            }
            else { 
                Pause();
            }
        }
    }

    public void Resume() {
        gameIsPaused = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    private void Pause() {
        gameIsPaused = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMenu() {
        Debug.Log("Loading Start Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void QuitGame() {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
