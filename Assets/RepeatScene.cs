using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepeatScene : MonoBehaviour
{
    [SerializeField] private int LeftRight = 1;
    [SerializeField] private float HowMuch;
    private GameObject toCopy;
    private GameObject copied;
    private bool wasUsed = false;

    private void Start()
    {
        toCopy = this.transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && !wasUsed) {
            copied = GameObject.Instantiate(toCopy);
            copied.transform.position += new Vector3(HowMuch*LeftRight, 0, 0);
            wasUsed = true;
        }
    }

}
