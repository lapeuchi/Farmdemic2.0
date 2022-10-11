using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public T Load<T>(string path) where T : ILoader
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data{path}");
        return JsonUtility.FromJson<T>(textAsset.text);
    }
}
