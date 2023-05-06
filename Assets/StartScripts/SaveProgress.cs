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
       private int highestScore = 0;

        public void setHighestScore(int score) { 
            this.highestScore= score;
        }

        public int getHighestScore()
        {
            return this.highestScore;
        }
    }

    public static void SaveProgress()
    {
        SaveData saveData = new SaveData();
        saveData.setHighestScore(SaveGame.highestScore);
        Debug.Log(saveData.getHighestScore());
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
            Debug.Log(saveData.getHighestScore());
            SaveGame.highestScore = saveData.getHighestScore();
            file.Close();
        }
        else { 
            SaveGame.highestScore = 0;
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
