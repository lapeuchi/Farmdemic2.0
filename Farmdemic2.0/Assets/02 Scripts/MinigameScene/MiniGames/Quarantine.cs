using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarantine : MonoBehaviour, IMinigame
{
    public void GameStart()
    {
        MinigameManager.instance.StartTimer(30);
    }
    
    public void GameOver()
    {
           
    }   
}