using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public List<Dialogue> DialogueDatas { get; private set; } = new List<Dialogue>();
    public List<Quiz_OX> OXQuizDatas { get; private set; } = new List<Quiz_OX>();
    public List<Matching> MatchingDatas { get; private set; } = new List<Matching>();
    public Dictionary<int, Queue<Tip>> TipDatas { get; private set; } = new Dictionary<int, Queue<Tip>>();
    
    public void Init()
    {
        DialogueDatas = Load<DialogueLoader, Dialogue>("Dialogue").MakeList();
        OXQuizDatas = Load<OXQuizLoader, Quiz_OX>("OXQuiz").MakeList();
        MatchingDatas = Load<MatchingLoader, Matching>("Disinfectant").MakeList();
        List<Tip> tipList = Load<TipLoader, Tip>("Tip").MakeList();
        
        for(int i = 1; i <= 2; i++)
        {
            TipDatas.Add(i, new Queue<Tip>());
        }

        foreach(Tip tip in tipList)
        {
            TipDatas[tip.code].Enqueue(tip);
        }
    }

    public Loader Load<Loader, T>(string path) where Loader : ILoader<T>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
