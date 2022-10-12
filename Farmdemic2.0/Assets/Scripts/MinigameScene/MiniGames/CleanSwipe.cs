using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanSwipe : MonoBehaviour, IMinigame
{
    void Start()
    {
        Camera.main.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        
    }

    public void GameOver(bool isClear)
    {
        
    }   
}
