using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveGame
{
    private static int highestScore;
    private static String profile = "profile1";
    [Serializable]
    private class SaveData
    {
       public int highestScore = 0;
       public KeyCode up = KeyCode.W;
       public KeyCode left = KeyCode.A;
       public KeyCode right = KeyCode.D;
    }

    public static void SaveProgress()
    {
        SaveData saveData = new SaveData();
        saveData.highestScore = SaveGame.highestScore;
        saveData.up = ButtonSettings.KeyUp;
        saveData.left = ButtonSettings.KeyLeft;
        saveData.right = ButtonSettings.KeyRight;
        Debug.Log(saveData.highestScore);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + SaveGame.profile + ".dat");
        Debug.Log("Saving game");

        formatter.Serialize(file, saveData);
        file.Close();
    }

    public static void LoadProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SaveGame.profile + ".dat"))
        {
            Debug.Log("Loading Game");
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SaveGame.profile + ".dat", FileMode.Open);
            SaveData saveData = (SaveData)formatter.Deserialize(file);
            Debug.Log(saveData.highestScore);
            SaveGame.highestScore = saveData.highestScore;
            ButtonSettings.KeyUp = saveData.up;
            ButtonSettings.KeyLeft = saveData.left;
            ButtonSettings.KeyRight = saveData.right;
            if (ButtonSettings.KeyUp == KeyCode.None) {
                ButtonSettings.KeyUp = KeyCode.W;
            }
            if (ButtonSettings.KeyLeft == KeyCode.None)
            {
                ButtonSettings.KeyLeft = KeyCode.A;
            }
            if (ButtonSettings.KeyRight == KeyCode.None)
            {
                ButtonSettings.KeyRight = KeyCode.D;
            }
            Debug.Log(ButtonSettings.KeyUp);
            file.Close();
        }
        else { 
            SaveGame.highestScore = 0;
            ButtonSettings.KeyUp = KeyCode.W;
            ButtonSettings.KeyLeft = KeyCode.A;
            ButtonSettings.KeyRight = KeyCode.D;
        }
    }

    public static int getHighestScore() { 
        return SaveGame.highestScore;
    }

    public static void setHighestScore(int value) {
        SaveGame.highestScore = value;
    }

    public static void setProfile(int value) {
        if (value == 1) {
            SaveGame.profile = "profile1";
        }
        else if (value == 2)
        {
            SaveGame.profile = "profile2";
        }
        else if (value == 3)
        {
            SaveGame.profile = "profile3";
        }
        LoadProgress();
    }

    public static int getProfile()
    {
        if (profile == "profile1") {
            return 1;
        }
        if (profile == "profile2")
        {
            return 2;
        }
        if (profile == "profile3")
        {
            return 3;
        }
        return 1;
    }
}
