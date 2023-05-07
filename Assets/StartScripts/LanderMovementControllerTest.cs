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
    private Rigidbody2D rb2D;
    //private Vector2 velocity = new Vector2();
    private Transform tr;
    private int lastScore = 0;
    private float lastFuel = 1000;

    private InputHandler inputHandler = new InputHandler();
    private RotationCommand left;
    private RotationCommand right;
    private UpCommand up;

    [SerializeField] private Text fuelText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highestScoreText;

    //[SerializeField] private GameObject MenuPause;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        tr = transform;
        rb2D.constraints = RigidbodyConstraints2D.None;
        left = new RotationCommand(this, rb2D, speedRotate, 1);
        right = new RotationCommand(this, rb2D, speedRotate, -1);
        up = new UpCommand(this, rb2D, tr, speed, fuel, fuelUsage);
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
            Debug.Log("FAIL - hit ground");
            Debug.Log("FUEL: " + fuel);
        }
        else if (layer == 7) { // layer 7 - platforms
            if (tr.rotation.eulerAngles.z < 15 || tr.rotation.eulerAngles.z > 345) // Check acceptable angle for a landing (+/- 15°)
            {
                if (rb2D.velocity.y > -1.25) // Check acceptable speed for landing (> -1.25m/s)
                {
                    PlatformData info = _onGround.GetComponent<PlatformData>();
                    score += info.getPoints();
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
        scoreText.text = "Score: " + score.ToString();
        lastFuel= fuel;
        lastScore = score;
        if (score > SaveGame.getHighestScore()) {
            highestScoreText.text = "Highest Score: " + score.ToString();
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
        /*velocity = rb2D.velocity;

        if (Input.GetKey(KeyCode.D))
        {
            if(tr.rotation.eulerAngles.z > 270 || tr.rotation.eulerAngles.z < 180) // Check acceptable angle for a right rotation
                rb2D.rotation -= speedRotate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(tr.rotation.eulerAngles.z < 90 || tr.rotation.eulerAngles.z > 180) // Check acceptable angle for a left rotation
                rb2D.rotation += speedRotate * Time.deltaTime;
        }
        */
        /*if (Input.GetKey(KeyCode.W) && fuel > 0)
        {
            velocity = speed * tr.up * Time.deltaTime;
            rb2D.velocity += velocity;
       
            fuel -= fuelUsage*Time.deltaTime;
        }*/
        


        fuelText.text = "Fuel: " + ((int)fuel).ToString();
    }

    private void Death()
    {
        Time.timeScale = 0f;
        DeathMenu.SetActive(true);
    }

    public void Rewind() {
        tr.position = new Vector3(-5, 10, 0); // starting position
        rb2D.velocity = new Vector2(0, 0);    // reset a speed
        rb2D.rotation = 0;
        score = lastScore;
        fuel = lastFuel;
        up.setFuel(fuel);
    }

    public void Restart()
    {
        tr.position = new Vector3(-5, 10, 0); // starting position
        rb2D.velocity = new Vector2(0, 0);    // reset a speed
        rb2D.rotation = 0;
        score = 0;
        fuel = 1000;
        up.setFuel(fuel);
    }
}
