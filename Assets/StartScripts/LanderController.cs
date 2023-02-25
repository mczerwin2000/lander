using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class LanderController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private Rigidbody2D rb2D;
    //[SerializeField] private readonly Vector3 rotate = new Vector3(0f, 0f, 1f);
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*velocity = rb2D.velocity;

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
            var impulse = (1 * Mathf.Deg2Rad) * rb2D.inertia;
            rb2D.AddTorque(-impulse, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            var impulse = (1 * Mathf.Deg2Rad) * rb2D.inertia;
            rb2D.AddTorque(impulse, ForceMode2D.Impulse);
        }
    }
}