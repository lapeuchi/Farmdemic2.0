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
    public IMinigame minigameController;

    public Transform minigameParent;

    public GameEvtPopup gameEvt_UI;
    
    private int _score;
    public int Score {get {return _score;} private set {_score = value;}}
    
    private Define.Rank _rank;
    public Define.Rank Rank {get {return _rank;} private set {_rank = value;}}

    public string[] feedbacks = new string[3];

    private bool isClear;
    public bool IsClear { get { return isClear; } private set {isClear = value; } }

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
        
        gameEvt_UI = Managers.UI.ShowPopupUI<GameEvtPopup>();  
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
        Score = 0;
        GameStart();
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
        this.Rank = grade;
    }

    public void SetFeedback(string str0 = null, string str1 = null, string str2 = null)
    {
        feedbacks[0] = str0;
        feedbacks[1] = str1;
        feedbacks[2] = str2;
    }

    public void SetClaer(bool isClear) {IsClear = isClear; }

    public void GameOver()
    {   
        minigameController.GameOver();
        Debug.Log($"GameOver()");
        isGameOver = true;
        StartCoroutine(GameOverEffect());
    }

    private IEnumerator GameOverEffect()
    {
        StartCoroutine(gameEvt_UI.GameOverEffect());

        yield return new WaitUntil(()=> gameEvt_UI.isEndGameOverEffect == true);
        
        Managers.UI.ClosePopupUI();

        ResultPopup resultPopup = Managers.UI.ShowPopupUI<ResultPopup>();
        resultPopup.SetResult();
    }
    
    public void GameStart()
    {
        
        StartCoroutine(GameStartEffect());
        Debug.Log("GameStart()");
    }

    private IEnumerator GameStartEffect()
    {
        yield return new WaitForSeconds(1f);
        
        StartCoroutine(gameEvt_UI.GameStartEffect());

        yield return new WaitUntil(()=> gameEvt_UI.isEndCountDown == true);
        
        isGameStart = true;
        
        yield return null;

        minigameController.GameStart();
    }
}