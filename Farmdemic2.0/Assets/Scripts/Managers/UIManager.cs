using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    public GameObject Root;
    
    public void Init()
    {
        Root = GameObject.Find("@UI");

        if(Root == null)
        {
            Root = new GameObject { name = "@UI_Root" };
            Root.transform.parent = GameObject.Find("@Managers").transform;
        }
    }
}