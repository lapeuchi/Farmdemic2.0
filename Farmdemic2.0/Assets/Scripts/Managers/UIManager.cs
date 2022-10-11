using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    public GameObject root;
    Canvas canvas;

    public void Init()
    {
        root = GameObject.Find("@UI");

        if(root == null)
        {
            root = new GameObject { name = "@UI" };
            root.AddComponent<Canvas>();
            canvas = root.GetComponent<Canvas>();
        }

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
}