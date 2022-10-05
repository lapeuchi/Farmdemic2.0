#define Debug
//#define Release

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance = null;

    public Define.Minigame curMiniGame;

    public Transform minigameParent;

    public ResultUI result_UI;

    public static int _score;
    public static int Score {get {return _score;} set {_score = value;}}

    public static bool isGameOver = false;
    public static bool isGameStart = false;

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        minigameParent = GameObject.Find("MinigameParent").transform;

#if Debug  
        if(curMiniGame == Define.Minigame.none)
        {
            MinigameTrigger.SetMiniGame(Define.Minigame.OXQuiz);
        }
#endif
 
        curMiniGame = MinigameTrigger.Minigame;
        result_UI = transform.Find("Result_UI").GetComponent<ResultUI>();

        GameObject game = minigameParent.Find(curMiniGame.ToString()).gameObject;
        game.SetActive(true);
        
    }

    void Update()
    {

    }

    public void GameOver(bool isClear)
    {   
        isGameOver = true;
        result_UI.gameObject.SetActive(true);
        
        result_UI.SetResult(isClear, Score);

    }

    public IEnumerator GameStart(float startTime)
    {
        isGameStart = true;
        
        yield return null;
    }
}
