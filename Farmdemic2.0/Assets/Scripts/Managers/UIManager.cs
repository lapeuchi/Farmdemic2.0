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
            Root.AddComponent<Canvas>();
            Root.AddComponent<CanvasScaler>();
            Root.AddComponent<GraphicRaycaster>();
            Root.transform.parent = GameObject.Find("@Managers").transform;
        }
        
        Canvas canvas = Root.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
}