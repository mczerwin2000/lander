using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    private static bool gameIsPaused = false;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject DeathMenu;
    [SerializeField] private GameObject Lander;
    [SerializeField] private Animator end;


    public bool isGamePaused() {
        return gameIsPaused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ButtonSettings.KeyPause)) {
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
        StartCoroutine(loadMenu());
    }

    IEnumerator loadMenu()
    {
        end.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void QuitGame() {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void Rewind() {
        Lander.GetComponent<LanderMovementControllerTest>().Rewind();
        Resume();
    }

    public void NewGame() {
        DeathMenu.SetActive(false);
        Time.timeScale = 1f;
        Lander.GetComponent<LanderMovementControllerTest>().Restart();
    }
}
