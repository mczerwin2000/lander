using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown dropDown;

    public void Start()
    {
        dropDown.value = SaveGame.getProfile() - 1;
    }
    public void PlayGame() {
        Debug.Log("Click");
        SceneManager.LoadScene("Scenes/SampleScene");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void optionProfile(int id) { 
        Debug.Log("Change profile to " + (id+1).ToString());
        SaveGame.setProfile(id+1);
    }
}
