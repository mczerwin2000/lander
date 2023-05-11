using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody2D))]
public class LanderMovementControllerTest : MonoBehaviour, IEntity
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRotate;
    [SerializeField] private int score = 0;
    [SerializeField] private float fuel = 1000;
    [SerializeField] private float fuelUsage = 50;
    [SerializeField] private GameObject DeathMenu;
    [SerializeField] private GameObject lostConnectionMenu;
    private Rigidbody2D rb2D;
    //private Vector2 velocity = new Vector2();
    private Transform tr;
    private int lastScore = 0;
    private float lastFuel = 1000;
    private bool outOfMap = false;
    private bool hitGround = false;

    //public static event System.Action<string> NoFuelEvent;
    public static event System.Action<string> PointsEvent;
    public static event System.Action<string> Landing2137Event;
    public static event System.Action<string> CrashLandingEvent;
    public static event System.Action<string> LostTreasureEvent;

    [SerializeField] private Text fuelText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highestScoreText;

    private InputHandler inputHandler = new InputHandler();
    private RotationCommand left;
    private RotationCommand right;
    private UpCommand up;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        tr = transform;
        rb2D.constraints = RigidbodyConstraints2D.None;
        left = new RotationCommand(this, rb2D, speedRotate, 1);
        right = new RotationCommand(this, rb2D, speedRotate, -1);
        up = new UpCommand(this, rb2D, speed, fuel, fuelUsage);
        //animationFire = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        fuelText.text += ((int)fuel).ToString();
        scoreText.text += score.ToString();
        SaveGame.LoadProgress();
        highestScoreText.text += SaveGame.getHighestScore().ToString();
    }

    public void OnGroundChanged(GameObject _onGround)
    {
        int layer = _onGround.layer;
        if (layer == 6) { // layer 6 - terrain
            fuel -= 100f + (fuel * 0.2f);
            hitGround = true;
            Debug.Log("FAIL - hit ground");
            Debug.Log("FUEL: " + fuel);
        }
        else if (layer == 7) { // layer 7 - platforms
            if (tr.rotation.eulerAngles.z < 15 || tr.rotation.eulerAngles.z > 345) // Check acceptable angle for a landing (+/- 15°)
            {
                if (rb2D.velocity.y > -1.5) // Check acceptable speed for landing (> -1.5/s)
                {
                    PlatformData info = _onGround.GetComponent<PlatformData>();
                    score += info.getPoints();
                    if (score > 1499) PointsEvent?.Invoke("1500 Points");
                    if (score > 2136 && !hitGround) Landing2137Event?.Invoke("2137 landing");
                    if (fuel <= 0) CrashLandingEvent?.Invoke("Crash landing");
                    Debug.Log("SUCCESS");
                    Debug.Log("Score: " + score);
                    Debug.Log("FUEL: " + fuel);
                }
                else {
                    fuel -= 100f + (fuel * 0.15f);
                    Debug.Log("FAIL - landed too fast");
                    Debug.Log("SPEED: " + rb2D.velocity.y);
                    Debug.Log("FUEL: " + fuel);
                }
            }
            else {
                fuel -= 100f + (fuel * 0.15f);
                Debug.Log("FAIL - landed too badly");
                Debug.Log("FUEL: " + fuel);
            }
        }
        up.setFuel(fuel);
        if(layer == 6 || layer == 7)
        {
            StartCoroutine(Freeze());
            Time.timeScale = 0f;
        }
    }

    // Freeze a game for 3 seconds 
    // If it has a fuel,it return the lander to start position
    // Otherwise it's the end of game
    IEnumerator Freeze() {
        scoreText.text = "SCORE: " + score.ToString();
        lastFuel= fuel;
        lastScore = score;
        if (score > SaveGame.getHighestScore()) {
            highestScoreText.text = "HIGHEST SCORE: " + score.ToString();
            SaveGame.setHighestScore(score);
            SaveGame.SaveProgress();
        }
        if (fuel > 0)
        {
            tr.position = new Vector3(-5, 10, 0); // starting position
            rb2D.velocity = new Vector2(0, 0);    // reset a speed
            rb2D.rotation = 0;                    // reset a rotation
            yield return new WaitForSecondsRealtime(3f);
            menuScript paused = FindFirstObjectByType(typeof(menuScript)).GetComponent<menuScript>();
            Debug.Log(!paused.isGamePaused());
            if (!paused.isGamePaused())
            {
                Time.timeScale = 1;
            }
        }
        else
        {
            Debug.Log("NO FUEL - END OF THE GAME");
            Death();
        }
    }
    private void Update()
    {
        if (tr.rotation.eulerAngles.z > 270 || tr.rotation.eulerAngles.z < 180)
        {
            inputHandler.Rotation(ButtonSettings.KeyRight, right);
        }
        if (tr.rotation.eulerAngles.z < 90 || tr.rotation.eulerAngles.z > 180)
        {
            inputHandler.Rotation(ButtonSettings.KeyLeft, left);
        }
        inputHandler.Up(ButtonSettings.KeyUp, up);
        fuel = up.GetFuel();
        fuelText.text = "FUEL: " + ((int)fuel).ToString();
    }

    private void Death()
    {
        Time.timeScale = 0f;
        StopCoroutine("lostConnection");
        DeathMenu.SetActive(true);
    }

    public void Rewind() {
        tr.position = new Vector3(-5, 10, 0); // starting position
        rb2D.velocity = new Vector2(0, 0);    // reset a speed
        rb2D.rotation = 0;
        score = lastScore;
        scoreText.text = "SCORE: " + score.ToString();
        fuel = lastFuel;
        outOfMap = false;
        StopCoroutine("lostConnection");
        lostConnectionMenu.SetActive(false);
        up.setFuel(fuel);
    }

    public void Restart()
    {
        tr.position = new Vector3(-5, 10, 0); // starting position
        rb2D.velocity = new Vector2(0, 0);    // reset a speed
        rb2D.rotation = 0;
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();
        fuel = 1000;
        outOfMap = false;
        StopCoroutine("lostConnection");
        lostConnectionMenu.SetActive(false);
        up.setFuel(fuel);
    }
    public void InOutOfMap(bool inOut) {
        outOfMap = inOut;
        Debug.Log("InOut: " + outOfMap);
        if (inOut)
        {
            lostConnectionMenu.SetActive(true);
            StartCoroutine("lostConnection");
        }
        else {
            lostConnectionMenu.SetActive(false);
            StopCoroutine("lostConnection");
        }
    }

    IEnumerator lostConnection() {
        yield return new WaitForSeconds(5f);
        lostConnectionMenu.SetActive(false);
        LostTreasureEvent?.Invoke("Lost Treasure");
        Death();
    }


}
