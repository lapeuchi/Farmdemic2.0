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
    string model;

    public Dialogue(string name, string word, string model)
    {
        this.name  = name;
        this.word  = word;
        this.model = model;
        sprite = Managers.Resource.Load<Sprite>($"Sprite/{model}");
    }
}
[System.Serializable]
public class DialogueLoader : ILoader<Dialogue>
{
    public List<Dialogue> Dialogue = new List<Dialogue>();
    public List<Dialogue> MakeList() { return Dialogue; }
}