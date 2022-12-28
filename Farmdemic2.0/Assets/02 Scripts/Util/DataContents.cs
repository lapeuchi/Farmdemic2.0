using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<T>
{
    List<T> MakeList();
}

public interface ILoader<TKey, TValue>
{
    Dictionary<TKey, TValue> MakeDic(); 
}


[System.Serializable]
public class Dialogue
{
    public int code;
    public Define.Event eventCode;
    public string name;
    public string word;
    public Sprite image;

    public void Init()
    {
        if (name.Equals("김영수 할아버지"))
            image = Managers.Resource.Load<Sprite>("Sprites/FaceChip/Youngsu");
        else
            image = Managers.Resource.Load<Sprite>("Sprites/FaceChip/Hyeok");
    }

    public Dialogue(int code, Define.Event eventCode, string name, string word)
    { 
        this.code = code;
        this.eventCode = eventCode;
        this.name  = name;
        this.word  = word;
    }
}

[System.Serializable]
public class DialogueLoader : ILoader<Dialogue>
{
    public List<Dialogue> Dialogue = new List<Dialogue>();

    public List<Dialogue> MakeList()
    {
        foreach (Dialogue dialogue in Dialogue)
            dialogue.Init();

        return Dialogue;
    }
}

[System.Serializable]
public class Tip
{
    public int code;
    public string title;
    public string content;
    public Sprite image;
    public string path;

    public void Init()
    {
        image = Managers.Resource.Load<Sprite>($"Sprites/Popup/{path}");
    }

    public Tip(int code, string title, string content, string path)
    {
        this.code = code;
        this.title = title;
        this.content = content;
        this.path = path;
        image = Managers.Resource.Load<Sprite>($"Sprites/Popup/{path}");
    }
}

[System.Serializable]
public class TipLoader : ILoader<Tip>
{
    public List<Tip> Tip = new List<Tip>();

    public List<Tip> MakeList()
    {
        foreach (Tip tip in Tip)
            tip.Init();

        return Tip;
    }
}

[System.Serializable]
public struct Quiz_OX
{
    public string text;
    public string answer;
    public string explanation;

    public Quiz_OX(string text, string answer, string explanation)
    {
        this.text = text;
        this.answer = answer;
        this.explanation = explanation;
    }
}

[System.Serializable]
public class OXQuizLoader : ILoader<Quiz_OX>
{
    public List<Quiz_OX> OXQuiz = new List<Quiz_OX>();

    public List<Quiz_OX> MakeList()
    {
        return OXQuiz;
    }
}

[System.Serializable]
public struct Matching
{
    public string disinfectant;
    public string []use;

    public Matching(string disinfectant, string[] use)
    {
        this.disinfectant = disinfectant;
        this.use = use;
    }
}

[System.Serializable]
public class MatchingLoader : ILoader<Matching>
{
    public List<Matching> MatchingData = new List<Matching>();

    public List<Matching> MakeList()
    {
        return MatchingData;
    }
}