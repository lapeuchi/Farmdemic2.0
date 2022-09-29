using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary> 
///      This is a class for running a minigame.
/// 
/// </summary>

public static class MiniGameTrigger
{
    static Define.MiniGame _miniGame = Define.MiniGame.none;
    public static Define.MiniGame MiniGame {private set {_miniGame = value;} get {return _miniGame;}}

    public static void LoadMiniGame(Define.MiniGame miniGame = Define.MiniGame.none)
    {
        if (miniGame == Define.MiniGame.none) 
        {   
            Debug.Log("MiniGame is not Selected");
            return;
        }

        SceneManager.LoadSceneAsync("MiniGameScene");
        MiniGame = miniGame;
        Debug.Log("Load MiniGame " + miniGame);
    }

    public static void Clear()
    {
        MiniGame = Define.MiniGame.none;
    }    
}
