using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutOfMap : MonoBehaviour
{
    [SerializeField] private bool InOut;

    public UnityEvent<bool> OnTriggerChange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(InOut);
    }
}
