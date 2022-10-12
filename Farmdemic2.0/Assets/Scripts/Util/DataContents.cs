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

    public Dialogue(string _name, string _word, Sprite _model)
    {
        name = _name;
        word = _word;
        model = _model;
    }
}

public class DialogueLoader : ILoader<Dialogue>
{
    List<Dialogue> myList = new List<Dialogue>();

    public List<Dialogue> MakeList()
    {
        return myList;
    }
}