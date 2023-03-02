using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class LanderMovementControllerTest : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRotate;
    [SerializeField] private int score = 0;
    [SerializeField] private float fuel = 1000;
    [SerializeField] private float fuelUsage = 50;
    //[SerializeField] private readonly Vector3 startPosition = new Vector3(-5f,10f, 0f);
    private Rigidbody2D rb2D;
    private Vector2 velocity = new Vector2();
    private Transform tr;
    //private Rotate rt = new Rotate();
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        tr = transform;
        rb2D.constraints = RigidbodyConstraints2D.None;
    }

    /*
    public void OnGroundChanged(bool _onGround)
    {
        Debug.Log(_onGround);
    }
    */
    public void OnGroundChanged(GameObject _onGround)
    {
        int layer = _onGround.layer;
        if (layer == 6) {
            fuel -= 100f + (fuel * 0.2f);
            Debug.Log("FAIL - hit ground");
            Debug.Log("FUEL: " + fuel);
        }
        else if (layer == 7) {
            if (tr.rotation.eulerAngles.z < 15 || tr.rotation.eulerAngles.z > 345)
            {
                if (rb2D.velocity.y > -1.25)
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

    IEnumerator Freeze() {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1;
        tr.position = new Vector3(-5, 10, 0);
        rb2D.velocity = new Vector2(0, 0);
        rb2D.rotation = 0;
    }
    private void Update()
    {
        velocity = rb2D.velocity;

        if (Input.GetKey(KeyCode.D))
        {
            if(tr.rotation.eulerAngles.z > 270 || tr.rotation.eulerAngles.z < 180)
                rb2D.rotation -= speedRotate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(tr.rotation.eulerAngles.z < 90 || tr.rotation.eulerAngles.z > 180)
                rb2D.rotation += speedRotate * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.W) && fuel > 0)
        {
            velocity = speed * tr.up * Time.deltaTime;
            rb2D.velocity += velocity;
       
            //fuel
            fuel -= fuelUsage*Time.deltaTime;
            //Debug.Log(fuel);
        }
    }
}
