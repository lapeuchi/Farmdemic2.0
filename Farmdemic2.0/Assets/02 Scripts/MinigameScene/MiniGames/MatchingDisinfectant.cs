using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingDisinfectant : MonoBehaviour, IMinigame
{
    private enum Matching_Disfectant
    {
        CitricAcid,                 //구연산: Citric acid
        AceticAacidPeroxide,        //과산화초산: acetic acid peroxide
        HypochlorousAcid,           //차아염소산: Hypochlorous acid
        SodiumIsocyanate,           //이소시안산 나트륨: sodium isocyanate
        PotassiumSulfatePeroxide,   //삼종염: potassium sulfate peroxide
        GlutarAldehyde              //글루타르 알데하이드: Glutar aldehyde
    }

    private Transform root;
    private Transform useCardParent;
    private Transform gatherPoint; // it's point at which cards gather

    private int cardCount = 0;
    private int doneSomethingCards = 0;

    [Space(10)]
    [Header("[Disinfectacnt Card Table]")]
    [SerializeField]
    private Transform disinfectacntCard;
    [SerializeField]
    private Transform nextDisinfectacntCard;
    [SerializeField]
    private List<string> disinfectanctSequence = new List<string>();

    [Space(10)]
    [Header("[Use Card Table]")]
    [SerializeField]
    private List<string> types = new List<string>(); // type of use card
    [SerializeField]
    private List<Vector3> useCardsPoint = new List<Vector3>();
    [SerializeField]
    private List<Transform> useCards = new List<Transform>();

    #region Function Trigger Table
    private bool gatherUseCards;
    private bool shuffleUseCards;
    #endregion

    [Header("[Function Test Table]")]
    public bool gather;
    public bool shuffle;

    private void Update()
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
    }

    void ShuffleUseCards()
    {
        shuffleUseCards = true;
        CommandToUseCards();

        int[] posIndex = RandomF(cardCount, cardCount);

        for (int i = 0; i < cardCount; i++)
        {
            useCards[i].GetComponent<UseCardController>().Shuffle(useCardsPoint[posIndex[i]]);
        }
    }

    void GatherUseCards()
    {
        gatherUseCards = true;
        CommandToUseCards();
        for (int i = 0; i < cardCount; i++)
        {
            useCards[i].GetComponent<UseCardController>().Gather(gatherPoint.position);
        }
    }

    public int[] RandomF(int maxCount, int n)
    {
        int[] defaults = new int[maxCount];
        int[] results = new int[n];

        for (int i = 0; i < maxCount; ++i)
        {
            defaults[i] = i;
        }

        for (int i = 0; i < n; ++i)
        {
            int index = Random.Range(0, maxCount);
            results[i] = defaults[index];
            defaults[index] = defaults[maxCount - 1];
            maxCount--;
        }

        return results;
    }

    public void ChangeUseCardInfo()
    {
        int[] posIndex = RandomF(cardCount, cardCount);

        for(int i = 0; i < cardCount; i++)
        {

        }

        ShuffleUseCards();
    }

    public void CommandToUseCards()
    {
        doneSomethingCards = 0;
    }

    public void AddDoneCard()
    {
        doneSomethingCards++;
        if(doneSomethingCards == cardCount)
        {
            DoneSomthing();
        }
    }

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
        Debug.Log("Switch Next Disinfectant Card");

        if(disinfectanctSequence.Count == 0)
        {
            SetDisinfectentSequence();
        }
    }

    void SetCorrectTypes()
    {

    }

    void ScoreCalculation()
    {

        //MinigameManager.instance.Score.PlusScore(0);
    }

    public void SelectUseCard(Transform tr)
    {
        Debug.Log(tr.name + "is clicked");

        ScoreCalculation();

        GatherUseCards();
    }

    void SetDisinfectentSequence()
    {
        disinfectanctSequence.Clear();

        /*int[] sequenceIndexs = RandomF(cardCount, cardCount);
        for(int i = 0; i < cardCount; i++)
        {
            int index = sequenceIndexs[i];
            disinfectanctSequence.Add(types[index]);
        }*/
    }

    private void Init()
    {
        root = transform.Find("UI");
        useCardParent = root.Find("ParentUseCard");
        gatherPoint = root.Find("GatherPoint");

        useCards.Clear();
        types.Clear();
        useCardsPoint.Clear();
        disinfectanctSequence.Clear();

        cardCount = useCardParent.childCount;
        for (int i = 0; i < cardCount; i++)
        {
            Transform discoveredCard = useCardParent.GetChild(i);

            useCards.Add(discoveredCard);
            types.Add(discoveredCard.name); // type is card name
            useCardsPoint.Add(discoveredCard.position);
            discoveredCard.GetComponent<UseCardController>().Init(types[i], this);
        }

        SetDisinfectentSequence();
    }

    public void GameStart()
    {
        Init();
    }

    public void GameOver()
    {
        
    }   
}
