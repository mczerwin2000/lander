using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    Queue<string> achievementQueue = new Queue<string>();
    [SerializeField] private GameObject panel;
    [SerializeField] private Text showText;
    private bool isDoing = false;

    private void OnEnable()
    {
        LanderMovementControllerTest.PointsEvent += Lander_PointsEvent;
        LanderMovementControllerTest.Landing2137Event += Lander_Landing2137;
        LanderMovementControllerTest.CrashLandingEvent += Lander_CrashLanding;
        LanderMovementControllerTest.LostTreasureEvent += Lander_LostTreasure;
    }

    private void OnDisable()
    {
        LanderMovementControllerTest.PointsEvent -= Lander_PointsEvent;
        LanderMovementControllerTest.Landing2137Event -= Lander_Landing2137;
        LanderMovementControllerTest.CrashLandingEvent -= Lander_CrashLanding;
        LanderMovementControllerTest.LostTreasureEvent -= Lander_LostTreasure;
    }

    private void Lander_PointsEvent(string achievement)
    {
        if (!SaveGame.pointsAch)
        {
            SaveGame.pointsAch = true;
            achievementQueue.Enqueue(achievement);
            SaveGame.SaveProgress();
        }
    }

    private void Lander_Landing2137(string achievement)
    {
        if (!SaveGame.landing2137Ach)
        {
            SaveGame.landing2137Ach = true;
            achievementQueue.Enqueue(achievement);
            SaveGame.SaveProgress();
        }
    }

    private void Lander_CrashLanding(string achievement) {
        if (!SaveGame.crashLandingAch)
        {
            SaveGame.crashLandingAch = true;
            achievementQueue.Enqueue(achievement);
            SaveGame.SaveProgress();
        }
    }

    private void Lander_LostTreasure(string achievement)
    {
        if (!SaveGame.lostTreasureAch)
        {
            SaveGame.lostTreasureAch = true;
            achievementQueue.Enqueue(achievement);
            SaveGame.SaveProgress();
        }
    }

    private IEnumerator ProcessAchievements()
    {

        while (achievementQueue.Count > 0 & isDoing == false)
        {
            panel.SetActive(true);
            isDoing = true;
            string achievement = achievementQueue.Dequeue();
            Debug.Log(achievement);
            showText.text = "Achiement Unlock: " + achievement;
            yield return new WaitForSecondsRealtime(5f); // Wait for 5 seconds before processing the next achievement
            panel.SetActive(false);
            isDoing = false;
        }
    }

    private void Update()
    {
        if (isDoing == false)
        {
            StartCoroutine(ProcessAchievements());
        }
    }
}
