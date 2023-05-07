using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    // Start is called before the first frame update
    Transform transform
    {
        get;
    }
}
