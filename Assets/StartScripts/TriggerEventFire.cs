using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventFire : MonoBehaviour
{
    /*
    public UnityEvent<bool> OnTriggerChange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject test = collision.gameObject;
        if (test != null)
            Debug.Log(test.layer);
        OnTriggerChange?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(false);
    }
    */

    public UnityEvent<GameObject> OnTriggerChange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collision.gameObject.GetComponent<PlatformData>();
        OnTriggerChange?.Invoke(collision.gameObject);
    }

   /* private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(collision.gameObject);
    }*/
}