using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown dropDown;
    [SerializeField] private InputField up;
    [SerializeField] private InputField left;
    [SerializeField] private InputField right;
    [SerializeField] private Animator settingsAnim;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Text highestScore;
    [SerializeField] private Text achievementText;
    private string validNumbers = "0123456789";
    private string validletters = "qwertyuiopasdfghjklzxcvbnm".ToUpper() + "0123456789";

    public void Start()
    {
        SaveGame.LoadProgress();
        dropDown.value = SaveGame.getProfile() - 1;
        highestScore.text = "HIGHEST SCORE: " + SaveGame.getHighestScore();
        achievementText.text = "ACHIEMENT UNLOCKED: " + countAchievements() + "/4";
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

    public void toSettings()
    {
        StartCoroutine(settings1(false,true));
        StartCoroutine(settings2());
        //settingsAnim.Play("Crossfade.CrossFadeSetting", 0, 1f);
        Debug.Log("Animation");
    }

    public void goBack() {
        StartCoroutine(settings1(true,false));
        StartCoroutine(settings2());
    }
    IEnumerator settings1(bool menu,bool settings) {
        settingsAnim.SetTrigger("Settings");
        yield return new WaitForSecondsRealtime(0.5f);
        mainPanel.SetActive(menu);
        settingsPanel.SetActive(settings);
    }

    IEnumerator settings2()
    {
        yield return new WaitForSecondsRealtime(0.5f);
    }

    public void optionProfile(int id) { 
        Debug.Log("Change profile to " + (id+1).ToString());
        SaveGame.setProfile(id+1);
        highestScore.text = "HIGHEST SCORE: " + SaveGame.getHighestScore();
        achievementText.text = "ACHIEMENT UNLOCKED: " + countAchievements() + "/4";
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

    private int countAchievements() {
        int count = 0;
        if (SaveGame.pointsAch) count++;
        if (SaveGame.crashLandingAch) count++;
        if (SaveGame.landing2137Ach) count++;
        if(SaveGame.lostTreasureAch) count++;
        return count;
    }
}
