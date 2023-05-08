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
    [SerializeField] private InputField up;
    [SerializeField] private InputField left;
    [SerializeField] private InputField right;
    private string validNumbers = "0123456789";
    private string validletters = "qwertyuiopasdfghjklzxcvbnm".ToUpper() + "0123456789";

    public void Start()
    {
        SaveGame.LoadProgress();
        dropDown.value = SaveGame.getProfile() - 1;
        printInputField();
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
        printInputField();
    }

    public void changeUpKey(string key)
    {
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
    private void printInputField()
    {
        up.text = ButtonSettings.KeyUp.ToString()[ButtonSettings.KeyUp.ToString().Length - 1].ToString();
        left.text = ButtonSettings.KeyLeft.ToString()[ButtonSettings.KeyLeft.ToString().Length - 1].ToString();
        right.text = ButtonSettings.KeyRight.ToString()[ButtonSettings.KeyRight.ToString().Length - 1].ToString();
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
                printInputField();
            }
        }
    }
}
