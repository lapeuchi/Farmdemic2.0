using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public List<Dialogue> dialogueDatas { get; private set; } = new List<Dialogue>();

    public void Init()
    {
        dialogueDatas = Load<DialogueLoader, Dialogue>("Dialogue").MakeList();
        Debug.Log(dialogueDatas[0]);
    }

    public Loader Load<Loader, T>(string path) where Loader : ILoader<T>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        Debug.Log($"Succed data load {textAsset.name}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
