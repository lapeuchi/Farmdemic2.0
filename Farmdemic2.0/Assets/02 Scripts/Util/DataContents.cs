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
    public List<Dialogue> Dialogue = new List<Dialogue>();
    public List<Dialogue> MakeList() { return Dialogue; }
}