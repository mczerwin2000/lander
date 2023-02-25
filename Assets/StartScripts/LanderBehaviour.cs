using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanderBehaviour : MonoBehaviour
{

    [SerializeField] private float speed;

    private Transform lr;
    private Vector3 pos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = lr.position;
        pos.x += speed * Time.deltaTime;
        lr.position = pos;
    }
}
