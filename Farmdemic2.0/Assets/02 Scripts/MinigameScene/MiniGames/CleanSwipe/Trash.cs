using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    Interactable drag;

    public bool isDie = false;
    void Start()
    {
        isDie = false;
        drag = GetComponent<Interactable>();
    }

    void Update()
    {
        if(drag.isClick)
        {
            isDie = false;
        }
    }
}
