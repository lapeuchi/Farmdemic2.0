using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    Dragable drag;

    void Start()
    {
        
    }

    void Update()
    {
        if(drag.isClick)
        {
            Destroy(gameObject);
        }
    }
}
