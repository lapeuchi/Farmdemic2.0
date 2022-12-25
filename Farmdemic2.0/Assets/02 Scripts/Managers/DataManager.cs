using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public List<Dialogue> dialogueDatas { get; private set; } = new List<Dialogue>();
    public List<Quiz_OX> OXQuizDatas { get; private set; } = new List<Quiz_OX>();
    public List<Matching> MatchingDatas { get; private set; } = new List<Matching>();

    public void Init()
    {
        dialogueDatas = Load<DialogueLoader, Dialogue>("Dialogue").MakeList();
        OXQuizDatas = Load<OXQuizLoader, Quiz_OX>("OXQuiz").MakeList();
        MatchingDatas = Load<MatchingLoader, Matching>("Disinfectant").MakeList();
    }

    public Loader Load<Loader, T>(string path) where Loader : ILoader<T>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
