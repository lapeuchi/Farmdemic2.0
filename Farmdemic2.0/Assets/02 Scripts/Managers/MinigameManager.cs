//#define Debug
#define Release

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance = null;

    public Define.Minigame curMiniGame;
    public IMinigame minigameController;

    public Transform minigameParent;

    public ResultUI result_UI;
    public GameEvtUI gameEvt_UI;
    
    private int _score;
    public int Score {get {return _score;} private set {_score = value;}}
    
    private Define.Rank _grade;
    public Define.Rank Grade {get {return _grade;} private set {_grade = value;}}
    
    public static bool isGameOver = false;
    public static bool isGameStart = false;

    private void Awake()
    {
        Init();
        FindAndSetGame();
    }

    private void Init()
    {
        if(instance == null)
            instance = this;
        else Destroy(gameObject);

        minigameParent = GameObject.Find("MinigameParent").transform;
        result_UI = transform.Find("Result_UI").GetComponent<ResultUI>();
        gameEvt_UI = transform.Find("GameEvt_UI").GetComponent<GameEvtUI>();
    }

    private void FindAndSetGame()
    {
#if Release
        curMiniGame = MinigameTrigger.Minigame;
#endif

#if Debug
        if(curMiniGame == Define.Minigame.None)
        {
            Debug.LogError("Game is not Selected");
        }
#endif
        GameObject game = minigameParent.Find(curMiniGame.ToString()).gameObject;
        minigameController = game.GetComponent<IMinigame>();
        game.SetActive(true);
    }
    
    private void Start()
    {
        GameStart();
        Score = 0;
    }

    public void AddScore(int score)
    {
        this.Score += score;
    }
    
    public void MultipleScore(int score, int n)
    {
        this.Score *= score * n;
    }

    public void SetScore(int score)
    {
        this.Score = score;
    }

    public void SetRank(Define.Rank grade)
    {
        this.Grade = grade;
    }

    public void GameOver(bool isClear)
    {   
        isGameOver = true;
        
        StartCoroutine(GameOverEffect(isClear));
    }

    private IEnumerator GameOverEffect(bool isClear)
    {
        gameEvt_UI.gameObject.SetActive(true);
        StartCoroutine(gameEvt_UI.GameOverEffect());
        yield return new WaitUntil(()=> gameEvt_UI.isGameOver == true);
        result_UI.gameObject.SetActive(true);
        result_UI.SetResult(isClear);
    }
    
    public void GameStart()
    {
        StartCoroutine(GameStartEffect());
    }

    private IEnumerator GameStartEffect()
    {
        yield return null;

        gameEvt_UI.gameObject.SetActive(true);
        StartCoroutine(gameEvt_UI.GameStartEffect());

        yield return new WaitUntil(()=> gameEvt_UI.isZeroCount == true);
        
        isGameStart = true;
        
        yield return null;

        minigameController.GameStart();
    }


}
