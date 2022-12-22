using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingDisinfectant : MonoBehaviour, IMinigame
{
    private Transform root;
    private Transform useCardParent;
    private Transform gatherPoint; // it's point at which cards gather

    private int lenght;

    [SerializeField]
    private Transform disinfetacntCard;
    [SerializeField]
    private Transform nextDisinfetacntCard;
    [SerializeField]
    private Transform useCard;

    [SerializeField]
    private List<string> types = new List<string>(); // type of use card
    [SerializeField]
    private List<Vector3> useCardsPoint = new List<Vector3>();
    [SerializeField]
    private string disinfetanctSequence;
    [SerializeField]
    private List<Transform> useCards = new List<Transform>();

    void ShuffleUseCards()
    {

    }

    void GatherUseCards()
    {

    }

    public void ChangeUseCardInfo()
    {

    }

    public void SwitchNextDisinfectantCard()
    {

    }

    void ScoreCalculation()
    {

    }

    public void SelectUseCard(Transform tr)
    {
        Debug.Log(tr.name);

        ScoreCalculation();

        GatherUseCards();
    }

    private void Init()
    {
        root = transform.Find("UI");
        useCardParent = root.Find("ParentUseCard");
        gatherPoint = root.Find("GatherPoint");

        useCards.Clear();
        types.Clear();
        useCardsPoint.Clear();

        lenght = useCardParent.childCount;
        for (int i = 0; i < lenght; i++)
        {
            Transform discoveredCard = useCardParent.GetChild(i);

            useCards.Add(discoveredCard);
            types.Add(discoveredCard.name); // type is card name
            useCardsPoint.Add(discoveredCard.position);
        }
    }

    public void GameStart()
    {
        Init();
    }

    public void GameOver()
    {
        
    }   
}
