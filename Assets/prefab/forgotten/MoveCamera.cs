using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private int LeftRight;
    private CameraManagment managment;
    void Start()
    {
        Camera.main.enabled = true;
        managment = this.transform.parent.gameObject.GetComponent<CameraManagment>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && managment.whichSide != LeftRight)
        {
            Debug.Log("JEJ");
            Camera.main.transform.position += new Vector3(LeftRight* 20, 0, 0);
            managment.changeSide();
        }
    }
}
