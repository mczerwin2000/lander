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
    public void OnGroundChanged(int _onGround)
    {
        if(_onGround > -1)
            Debug.Log(_onGround);
        if (_onGround == 6) {
            //tr.position.x = -5;
            fuel -= 100f + (fuel * 0.15f);
            Debug.Log(fuel);
        }
        else if (_onGround == 7) {
            Debug.Log(rb2D.velocity.y);
        }
        tr.position = new Vector3(-5, 10, 0);
        rb2D.velocity = new Vector2(0, 0);
        rb2D.rotation = 0;
    }

    private void Update()
    {
        velocity = rb2D.velocity;
        /*
        float currentSpped = 0f;
        if (Input.GetKey(KeyCode.D)) {
            currentSpped += speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentSpped += -1f*speed;
        }
        velocity.x = currentSpped;
        rb2D.velocity = velocity;*/


        if (Input.GetKey(KeyCode.D))
        {
            if(tr.rotation.eulerAngles.z > 270 || tr.rotation.eulerAngles.z < 180)
                rb2D.rotation -= speedRotate * Time.deltaTime;
            
            //    tr.Rotate(rotate,-speedRotate * Time.deltaTime );
            //Debug.Log(tr.rotation.eulerAngles.z);
            //Debug.Log(velocity.y);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(tr.rotation.eulerAngles.z < 90 || tr.rotation.eulerAngles.z > 180)
                rb2D.rotation += speedRotate * Time.deltaTime;
            
            //    tr.Rotate(rotate, speedRotate * Time.deltaTime);
           // Debug.Log(tr.rotation.eulerAngles.z);
        }
        
        if (Input.GetKey(KeyCode.W) && fuel > 0)
        {
            velocity = speed * tr.up * Time.deltaTime;
            rb2D.velocity += velocity;
       
            //fuel
            fuel -= fuelUsage*Time.deltaTime;
            Debug.Log(fuel);
        }
    }
}
