using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingDisinfectant : MonoBehaviour, IMinigame
{
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
        Invoke("DoneSwitchDisinfectant", 4f);
    }

    void DoneSwitchDisinfectant()
    {
        switchDisinfectanctCard = false;
    }

    void SetCorrectTypes()
    {

    }

    void ScoreCalculation()
    {
        MinigameManager.instance.Score.PlusScore(addScore);
        //Debug.Log(addScore);
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

    public void SelectUseCard(Transform tr)
    {
        if (switchDisinfectanctCard)
            return;
        selectUse = tr.name;

        if (IsCurrectCard())
        {
            addScore = 50;
            Managers.Sound.PlaySFX(Define.SFX.Collect);
        }
        else
        {
            addScore = -50;
            Managers.Sound.PlaySFX(Define.SFX.Worth);
        }

        ScoreCalculation();

        GatherUseCards();
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
        Init();
        CloseFade();
        MinigameManager.instance.StartTimer(60);
    }

    public void GameOver()
    {
        
    }   
}
