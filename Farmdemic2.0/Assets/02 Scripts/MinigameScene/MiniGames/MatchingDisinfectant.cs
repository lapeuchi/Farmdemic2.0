using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingDisinfectant : MonoBehaviour, IMinigame
{
    private bool isGameEnd;

    private Transform root;
    private Transform useCardParent;
    private Transform disfectantParent;
    private Transform gatherPoint; // it's point at which cards gather
    private GameObject fadePanel;

    private int useCardCount = 0;
    private int disinfectantCardCount = 0;
    private int doneSomethingCards = 0;

    private List<Matching> matchingData = new List<Matching>();

    private MatchingFadeController fadeController;

    [Space(10)]
    [Header("[Disinfectacnt Card Table]")]
    private Transform disinfectantCard;
    private DisinfectantCardController disinfectantController;
    private List<int> disinfectanctSequence = new List<int>();
    private int dataIndex;
    private string selectUse;
    private int addScore = 50;

    [Space(10)]
    [Header("[Use Card Table]")]
    //private List<string> types = new List<string>(); // type of use card
    private List<Vector3> useCardsPoint = new List<Vector3>();
    private List<Transform> useCards = new List<Transform>();

    #region Function Trigger Table
    private bool gatherUseCards;
    private bool shuffleUseCards;
    private bool switchDisinfectanctCard;
    #endregion

    #region Function Test Table
    [Header("[Function Test Table]")]
    public bool gather;
    public bool shuffle;
    public bool switchCard;
    #endregion

    #region ETC Functions

    Image[] effect_Image = new Image[10];
    [SerializeField] Sprite collect_Sprite;
    [SerializeField] Sprite worth_Sprite;

    private void update()//Update()
    {
        //test update
        if (gather)
        {
            GatherUseCards();
            gather = false;
        }

        if (shuffle)
        {
            ShuffleUseCards();
            shuffle = false;
        }

        if(switchCard)
        {
            SwitchNextDisinfectantCard();
            switchCard = false;
        }
    }
    #endregion

    #region gather and shufful
    void ShuffleUseCards()
    {
        shuffleUseCards = true;
        CommandToUseCards();

        int[] posIndex = Util.RandomF(useCardCount, useCardCount);

        for (int i = 0; i < useCardCount; i++)
        {
            useCards[i].GetComponent<UseCardController>().Shuffle(useCardsPoint[posIndex[i]]);
        }

        Managers.Sound.PlaySFX(Define.SFX.CardGather);
    }

    void GatherUseCards()
    {
        gatherUseCards = true;
        CommandToUseCards();
        for (int i = 0; i < useCardCount; i++)
        {
            useCards[i].GetComponent<UseCardController>().Gather(gatherPoint.position);
        }
        Managers.Sound.PlaySFX(Define.SFX.CardGather);
    }

    public void CommandToUseCards()
    {
        doneSomethingCards = 0;
    }

    public void AddDoneCard()
    {
        doneSomethingCards++;
        if(doneSomethingCards == useCardCount)
        {
            DoneSomthing();
        }
    }
    #endregion

    void DoneSomthing()
    {
        if (gatherUseCards)
        {
            ShuffleUseCards();
            gatherUseCards = false;
        }

        if (shuffleUseCards)
        {
            SwitchNextDisinfectantCard();
            shuffleUseCards = false;
        }
    }

    void SwitchNextDisinfectantCard()
    {
        switchDisinfectanctCard = true;
        if (disinfectanctSequence.Count == 0)
        {
            SetDisinfectantSequence();
        }

        dataIndex = disinfectanctSequence[0];
        disinfectantController.Switch(matchingData[dataIndex].disinfectant);
        disinfectanctSequence.RemoveAt(0);
        Invoke("DoneSwitchDisinfectant", 2f);
    }

    void DoneSwitchDisinfectant()
    {
        switchDisinfectanctCard = false;
    }

    void ScoreCalculation()
    {
        MinigameManager.instance.Score.PlusScore(addScore);
    }

    bool IsCurrectCard()
    {
        foreach(string use in matchingData[dataIndex].use)
        {
            if(use.Equals(selectUse))
            {
                return true;
            }
        }
        return false;
    }
    
    // Button Event
    public void SelectUseCard(Transform tr)
    {
        if (isGameEnd || switchDisinfectanctCard || gatherUseCards || shuffleUseCards)
            return;
        selectUse = tr.name;

        if (IsCurrectCard())
        {
            StartCoroutine(CheckEffect(true, tr));
            addScore += 50;
            Managers.Sound.PlaySFX(Define.SFX.Collect);

            Invoke("GatherUseCards", 1f);
        }
        else
        {
            StartCoroutine(CheckEffect(false, tr));
            addScore = 0;
            Managers.Sound.PlaySFX(Define.SFX.Worth);
            StartCoroutine(ShowCorrectCards());
            Invoke("GatherUseCards", 3.2f);
        }

        ScoreCalculation();
        //GatherUseCards();
    }

    IEnumerator CheckEffect(bool isCollect, Transform tr)
    {
        effect_Image[0].transform.position = tr.transform.position;
        if (isCollect)
        {
            effect_Image[0].sprite = collect_Sprite;
            effect_Image[0].color = Color.green;
        }
        else
        {
            effect_Image[0].sprite = worth_Sprite;
            effect_Image[0].color = Color.red;

        }
        effect_Image[0].enabled = true;
        yield return new WaitForSeconds(0.8f);
        effect_Image[0].enabled = false;
    }

    IEnumerator ShowCorrectCards()
    {
        yield return new WaitForSeconds(1);
        int index = 0;
        foreach (Matching data in matchingData)
        {
            if(data.disinfectant.Equals(matchingData[dataIndex].disinfectant))
            {
                foreach (string use in matchingData[dataIndex].use)
                {
                    //Debug.Log(use);
                    for(int i = 0; i < useCardCount; i++)
                    {
                        if(useCards[i].name.Equals(use))
                        {
                            index++;
                            effect_Image[index].transform.position = useCards[i].position;
                            effect_Image[index].sprite = collect_Sprite;
                            effect_Image[index].color = Color.green;
                        }
                    }
                }
            }
        }

        for(int i = 1; i < index; i++)
        {
            effect_Image[i].enabled = true;
        }
        yield return new WaitForSeconds(1f);

        for (int i = 1; i < index; i++)
        {
            effect_Image[i].enabled = false;
        }
    }

    #region fade
    void ShowFade()
    {
        fadePanel.SetActive(true);
    }

    void CloseFade()
    {
        fadePanel.SetActive(false);
    }

    #endregion

    #region Initialize Function
    void SetDisinfectantSequence()
    {
        disinfectanctSequence.Clear();

        int[] sequenceIndexs = Util.RandomF(disinfectantCardCount, disinfectantCardCount);
        for(int i = 0; i < disinfectantCardCount; i++)
        {
            disinfectanctSequence.Add(sequenceIndexs[i]);
        }
    }

    void ClearAllList()
    {
        useCards.Clear();
        //types.Clear();
        useCardsPoint.Clear();
        disinfectanctSequence.Clear();
    }

    void FindBasicObject()
    {
        root = transform.Find("UI");
        useCardParent = root.Find("ParentUseCard");
        gatherPoint = root.Find("GatherPoint");
        disfectantParent = root.Find("DisfectantParent");
        fadePanel = root.Find("FadePanel").gameObject;

        disinfectantController = disfectantParent.GetComponent<DisinfectantCardController>();
        disinfectantController.Init();

        fadeController = fadePanel.GetComponent<MatchingFadeController>();
        fadeController.Init();
    }

    void SetUseCardInfo()
    {
        useCardCount = useCardParent.childCount;

        for (int i = 0; i < useCardCount; i++)
        {
            Transform discoveredCard = useCardParent.GetChild(i);

            //type.add(discoveredCard.name); 
            useCards.Add(discoveredCard);
            useCardsPoint.Add(discoveredCard.position);
            discoveredCard.GetComponent<UseCardController>().Init(this);
        }
    }

    void SetMatchingData()
    {
        List<Matching> datas = Managers.Data.MatchingDatas;
        matchingData = datas;
        disinfectantCardCount = matchingData.Count;
    }

    void Init()
    {
        FindBasicObject();
        ClearAllList();

        SetUseCardInfo();

        SetMatchingData();
        SwitchNextDisinfectantCard();
    }
    #endregion

    public void GameStart()
    {
        isGameEnd = false;
        Init();
        CloseFade();
        MinigameManager.instance.StartTimer(60);
        MinigameManager.instance.SetFeedback("1", "2", "3");
        effect_Image[0] = root.Find($"Effect_Image1").GetComponent<UnityEngine.UI.Image>();
        effect_Image[1] = root.Find($"Effect_Image2").GetComponent<UnityEngine.UI.Image>();
        effect_Image[2] = root.Find($"Effect_Image3").GetComponent<UnityEngine.UI.Image>();
        effect_Image[3] = root.Find($"Effect_Image4").GetComponent<UnityEngine.UI.Image>();
        effect_Image[4] = root.Find($"Effect_Image5").GetComponent<UnityEngine.UI.Image>();
        effect_Image[5] = root.Find($"Effect_Image6").GetComponent<UnityEngine.UI.Image>();
        effect_Image[6] = root.Find($"Effect_Image7").GetComponent<UnityEngine.UI.Image>();
        effect_Image[7] = root.Find($"Effect_Image8").GetComponent<UnityEngine.UI.Image>();
        effect_Image[8] = root.Find($"Effect_Image9").GetComponent<UnityEngine.UI.Image>();
        effect_Image[9] = root.Find($"Effect_Image10").GetComponent<UnityEngine.UI.Image>();
        for (int j = 1; j <= 10; j++)
        {
            //effect_Image[j - 1] = root.Find($"Effect_Image{j}").GetComponent<UnityEngine.UI.Image>();
        }
        collect_Sprite = Managers.Resource.Load<Sprite>("Sprites/Circle_Sprite");
        worth_Sprite = Managers.Resource.Load<Sprite>("Sprites/Worth_Sprite");
        MinigameManager.instance.SetFeedback("왼쪽의 성분 카드가 어디를 소독할때 쓰이는지 찾아봐요.",
        "연속으로 정답을 맞추면 고득점을 할 수 있습니다.");

    }

    public void GameOver()
    {
        isGameEnd = true;
        ShowFade();

        if (MinigameManager.instance.Score.Score >= 500)
        {
            MinigameManager.instance.SetRank(Define.Rank.A);
            MinigameManager.instance.SetClaer(true);
        }
        else if (MinigameManager.instance.Score.Score >= 300)
        {
            MinigameManager.instance.SetRank(Define.Rank.B);
            MinigameManager.instance.SetClaer(true);
        }
        else if (MinigameManager.instance.Score.Score >= 150)
        {
            MinigameManager.instance.SetRank(Define.Rank.C);
            MinigameManager.instance.SetClaer(true);
        }
        else
        {
            MinigameManager.instance.SetRank(Define.Rank.F);
            MinigameManager.instance.SetClaer(false);
        }
    }
}
