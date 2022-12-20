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
    
    public static bool isGameOver = false;
    public static bool isGameStart = false;

    public MinigameScore Score;
    public MinigameLife Life;
    public MinigameTimer Timer;
    
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
        Score = Managers.UI.ShowSceneUI<MinigameScore>();
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
        GameStart();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Life.PlusLife();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Life.MinusLife();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            Timer.PlusTime(5f);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Timer.MinusTime(5f);
        }
    }

    public void StartLife()
    {
        Life = Managers.UI.ShowSceneUI<MinigameLife>();
        Life.Setting();
    }
    public void StartTimer(float time)
    {
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

    public void GameOver()
    {   
        Debug.Log($"GameOver()");
        minigameController.GameOver();
        isGameOver = true;
        Debug.Log(MinigameManager.instance.IsClear);
        StartCoroutine(GameOverEffect());
    }

    private IEnumerator GameOverEffect()
    {
        StartCoroutine(gameEvt_UI.GameOverEffect());

        yield return new WaitUntil(()=> gameEvt_UI.isEndGameOverEffect == true);
        
        Managers.UI.ClosePopupUI();

        Score.gameObject.SetActive(false);
        Life.gameObject.SetActive(false);
        Timer.gameObject.SetActive(false);

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