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
    static Define.Minigame _minigame = Define.Minigame.none;
    public static Define.Minigame Minigame {private set {_minigame = value;} get {return _minigame;}}

    public static void LoadMiniGame(Define.Minigame miniGame = Define.Minigame.none)
    {
        if (miniGame == Define.Minigame.none)
        {   
            Debug.Log("MiniGame is not Selected");
            return;
        }

        SceneManager.LoadSceneAsync("MiniGameScene");
        Minigame = miniGame;
        Debug.Log("Load MiniGame " + miniGame);
    }

    public static void SetMiniGame(Define.Minigame game)
    {
        Minigame = game;
    }

    public static void Clear()
    {
        Minigame = Define.Minigame.none;
    }    
}
