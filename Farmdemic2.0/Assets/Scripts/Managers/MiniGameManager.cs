using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance = null;
    
    public Define.MiniGame curMiniGame;

    public ResultUI result_UI;

    public static int _score;
    public static int Score {get {return _score;} set {_score = value;}}

    void Awake()
    {
        Init();
    }

    void Init()
    {
        curMiniGame = MiniGameTrigger.MiniGame;
        result_UI = transform.Find("Result_UI").GetComponent<ResultUI>();
    }

    void Update()
    {

    }


    public void GameOver(bool isClear)
    {   
        result_UI.gameObject.SetActive(true);
        
        result_UI.SetResult(isClear, Score);
    }
}
