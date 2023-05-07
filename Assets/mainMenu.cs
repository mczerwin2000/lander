using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown dropDown;
    private string validNumbers = "0123456789";
    private string validletters = "qwertyuiopasdfghjklzxcvbnm".ToUpper() + "0123456789";
    

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

    private void changeButton(string key, string which) {
        key = key.ToUpper();
        if (key != "")
        {
            if (validletters.Contains(key))
            {
                if (validNumbers.Contains(key))
                {
                    key = "Alpha" + key;
                }
                KeyCode newButton = (KeyCode)Enum.Parse(typeof(KeyCode), key);
                if (which == "up")
                {
                    ButtonSettings.KeyUp = newButton;
                }
                else if (which == "left") {
                    ButtonSettings.KeyLeft = newButton;
                }
                else if (which == "right")
                {
                    ButtonSettings.KeyRight = newButton;
                }
                SaveGame.SaveProgress();
            }
            else
            {
                Debug.Log("NOT ALLOWED CHAR");
            }
        }
    }

    public void changeUpKey(string key) {
        changeButton(key, "up");
    }

    public void changeLeftKey(string key)
    {
        changeButton(key, "left");
    }

    public void changeRightKey(string key)
    {
        changeButton(key, "right");
    }
}
