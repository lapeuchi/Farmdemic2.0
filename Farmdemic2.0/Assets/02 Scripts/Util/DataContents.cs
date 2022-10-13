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
    public Sprite model;

    public Dialogue(string name, string word, Sprite model)
    {
        this.name  = name;
        this.word  = word;
        this.model = model;
    }
}
[System.Serializable]
public class DialogueLoader : ILoader<Dialogue>
{
    public List<Dialogue> myList { get; set; } = new List<Dialogue>();

    public List<Dialogue> MakeList()
    {
        return myList;
    }
}