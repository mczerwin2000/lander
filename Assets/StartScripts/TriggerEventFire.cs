using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventFire : MonoBehaviour
{
    public UnityEvent<GameObject> OnTriggerChange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(collision.gameObject);
    }

   /* private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(collision.gameObject);
    }*/
}