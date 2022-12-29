using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> 
///      This is a class for running a minigame.
/// 
/// </summary>

public static class MinigameTrigger
{
    private static Define.Minigame _minigame = Define.Minigame.None;
    public static Define.Minigame Minigame {private set {_minigame = value;} get {return _minigame;}}

    public static void LoadMiniGame(Define.Minigame miniGame)
    {
        if (miniGame == Define.Minigame.None)
        {   
            //Debug.Log("MiniGame is not Selected");
            return;
        }
        
        Managers.Scene.LoadSceneAsync(Define.Scene.Minigame);
        Minigame = miniGame;
        //Debug.Log("Load MiniGame " + miniGame);
    }

    public static void SetMinigame(Define.Minigame game)
    {
        Minigame = game;
    }
    
    public static void Clear()
    {
        Minigame = Define.Minigame.None;
    }    
}
