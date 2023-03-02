using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Platform info
 * It's needed when lander touch a platform
 * Return how many points it should get based on platform type
 */
public class PlatformData : MonoBehaviour
{
    [SerializeField] private int BASE_PONITS;
    [SerializeField] private int BONUS_MULTIPLY;

    public int getPoints() {
        return BASE_PONITS * BONUS_MULTIPLY;
    }
}
