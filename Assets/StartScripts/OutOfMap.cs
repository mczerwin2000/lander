using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutOfMap : MonoBehaviour
{
    [SerializeField] private bool InOut;
    [SerializeField] private bool leftRight; //true - left, false , right
    [SerializeField] private bool isNotChildern;
    [SerializeField] private bool isUpBorder;

    public void Awake()
    {
        if (isNotChildern && !isUpBorder)
        {
            float scaleW = (float)Screen.width / Screen.height;
            float posX;
            if (leftRight)
            {
                posX = Camera.main.transform.position.x - (Camera.main.orthographicSize * scaleW);
            }
            else
            {
                posX = Camera.main.transform.position.x + (Camera.main.orthographicSize * scaleW);
            }
            this.transform.position += new Vector3(posX, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        else if (isNotChildern && isUpBorder)
        {
            this.transform.localScale = new Vector3((Screen.width/10.3f), transform.localScale.y, transform.localScale.z);        
        }
    }

    public UnityEvent<bool> OnTriggerChange;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerChange?.Invoke(InOut);
    }
}
