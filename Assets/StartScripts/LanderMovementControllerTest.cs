using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class LanderMovementControllerTest : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRotate;
    [SerializeField] private int score = 0;
    [SerializeField] private float fuel = 1000;
    [SerializeField] private float fuelUsage = 50;
    private Rigidbody2D rb2D;
    private Vector2 velocity = new Vector2();
    private Transform tr;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        tr = transform;
        rb2D.constraints = RigidbodyConstraints2D.None;
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
        StartCoroutine(Freeze());
        Time.timeScale = 0f;
    }

    // Freeze a game for 3 seconds 
    // If it has a fuel,it return the lander to start position
    // Otherwise it's the end of game
    IEnumerator Freeze() {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        if (fuel > 0)
        {
            tr.position = new Vector3(-5, 10, 0); // starting position
            rb2D.velocity = new Vector2(0, 0);    // reset a speed
            rb2D.rotation = 0;                    // reset a rotation
        }
        else {
            Debug.Log("NO FUEL - END OF THE GAME");
        }
    }
    private void Update()
    {
        velocity = rb2D.velocity;

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
        
        if (Input.GetKey(KeyCode.W) && fuel > 0)
        {
            velocity = speed * tr.up * Time.deltaTime;
            rb2D.velocity += velocity;
       
            fuel -= fuelUsage*Time.deltaTime;
        }
    }
}
