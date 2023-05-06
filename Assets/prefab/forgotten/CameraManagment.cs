using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagment : MonoBehaviour
{
    public int whichSide;

    public void changeSide() {
        whichSide *= -1;
    }
}
