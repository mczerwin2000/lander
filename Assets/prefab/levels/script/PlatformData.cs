using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformData : MonoBehaviour
{
    [SerializeField] private int BASE_PONITS;
    [SerializeField] private int BONUS_MULTIPLY;
    private Transform tr;
    private void Awake()
    {
        tr = transform;
    }

    public int getPoints() {
        return BASE_PONITS * BONUS_MULTIPLY;
    }

    public float getRotation() {
        return tr.rotation.eulerAngles.z;
    }
}
