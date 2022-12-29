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
    
    private Define.Rank _rank;
    public Define.Rank Rank {get {return _rank;} private set {_rank = value;}}
    
    public string[] feedbacks = new string[3];

    private bool isClear;
    public bool IsClear { get { return isClear; } private set {isClear = value; } }
    
    public bool isGameOver = false;
    public bool isGameStart = false;

    public MinigameScore Score;
    public MinigameLife Life;
    public MinigameTimer Timer;
    
    private void Awake()
    {
        Init();
        FindAndSetGame();

        Managers.Sound.PlayBGM(Define.BGM.MinigameBgm);
    }

    private void Init()
    {
        if(instance == null)
            instance = this;
        else Destroy(gameObject);
        isGameOver = false;
        
        minigameParent = GameObject.Find("MinigameParent").transform;
        
        gameEvt_UI = Managers.UI.ShowPopupUI<GameEvtPopup>();
    }

    private void FindAndSetGame()
    {
        if(MinigameTrigger.Minigame == Define.Minigame.None)
        {
            //Debug.Log("Game is not Selected Are you a tester?");
        }
        else
        {
            curMiniGame = MinigameTrigger.Minigame;
        }

        //Debug.Log(curMiniGame);
        
        GameObject game = minigameParent.Find(curMiniGame.ToString()).gameObject;
        minigameController = game.GetComponent<IMinigame>();
        game.SetActive(true);
    }
    
    private void Start()
    {
        GameStart();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) Life.PlusLife();
        else if (Input.GetKeyDown(KeyCode.O)) Life.MinusLife();

        if(Input.GetKeyDown(KeyCode.L)) Timer.PlusTime(5f);
        else if (Input.GetKeyDown(KeyCode.K)) Timer.MinusTime(5f);

        if (Input.GetKeyDown(KeyCode.N)) Score.PlusScore(50);
        else if (Input.GetKeyDown(KeyCode.M)) Score.PlusScore(-50);
        
        if (Input.GetKeyDown(KeyCode.G)) GameOver();
    }

    public void StartScore()
    {
        Score = Managers.UI.ShowSceneUI<MinigameScore>();
        Score.Setting();
    }

    public void StartLife()
    {
        Life = Managers.UI.ShowSceneUI<MinigameLife>();
        Life.Setting();
    }

    public void StartTimer(float time)
    {
       // Debug.Log($"StartTimer({time})");
        Timer = Managers.UI.ShowSceneUI<MinigameTimer>();

        Timer.Setting(time);
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

    public void SetClaer(bool isClear) { IsClear = isClear; }

    public void CloseGamePanels()
    {
        if(Score != null) Score.gameObject.SetActive(false);
        if(Life != null) Life.gameObject.SetActive(false);
        if(Timer != null) Timer.gameObject.SetActive(false);
    }

    public void GameOver()
    {   
        if(isGameOver == true) return;
        isGameOver = true;
        //Debug.Log($"GameOver()");
        minigameController.GameOver();
        //Debug.Log(MinigameManager.instance.IsClear);
       
        if(isClear == false) 
        {
            SetRank(Define.Rank.F);
        }
        
        StartCoroutine(GameOverEffect());
    }

    private IEnumerator GameOverEffect()
    {
        StartCoroutine(gameEvt_UI.GameOverEffect());
        
        yield return new WaitUntil(()=> gameEvt_UI.isEndGameOverEffect == true);
        
        Managers.UI.ClosePopupUI();
        CloseGamePanels();
        MinigameResultPopup resultPopup = Managers.UI.ShowPopupUI<MinigameResultPopup>();
        resultPopup.SetResult();
    }
    
    public void GameStart()
    {
        StartCoroutine(GameStartEffect());
        //Debug.Log("GameStart()");
    }
    
    private IEnumerator GameStartEffect()
    {
        yield return new WaitForSeconds(1f);
        
        StartCoroutine(gameEvt_UI.GameStartEffect());
        
        yield return new WaitUntil(()=> gameEvt_UI.isEndCountDown == true);
        
        isGameStart = true;
        
        yield return null;

        minigameController.GameStart();
        StartScore();
    }
}