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
    public string name;
    public string word;
    public Sprite sprite;
    string modelName;

    public Dialogue(string name, string word, string modelName)
    {
        this.name  = name;
        this.word  = word;
        this.modelName = modelName;
        sprite = Managers.Resource.Load<Sprite>($"Sprite/{modelName}");
    }
}
[System.Serializable]
public class DialogueLoader : ILoader<Dialogue>
{
    public List<Dialogue> Dialogue { get; set; } = new List<Dialogue>();

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