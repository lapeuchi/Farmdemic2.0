using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public List<Define.Dialogue> dialogueData = new List<Define.Dialogue>();

    public void Init()
    {
        
    }

    T Load<T> (string path)
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
