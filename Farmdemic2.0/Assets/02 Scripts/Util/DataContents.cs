using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<T>
{
    List<T> MakeList();
}

[System.Serializable]
public struct Dialogue
{
    public int code;
    public Define.Event eventCode;
    public string name;
    public string word;

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
        return Dialogue;
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