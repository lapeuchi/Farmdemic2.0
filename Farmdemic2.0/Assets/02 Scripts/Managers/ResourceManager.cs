using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        T data = Resources.Load<T>(path);

        if(data == null)
        {
            Debug.Log($"Failed : This path is null {path}");
        }

        return data;
    }

    public GameObject Instantiate(string path, Transform parents = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        
        if(prefab == null)
        {
            return null;
        }
        
        GameObject go = Object.Instantiate(prefab, parents);

        int index = go.name.IndexOf("(Clone)");

        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parents = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            return null;
        }

        GameObject go = Object.Instantiate(prefab, position, rotation, parents);
        int index = go.name.IndexOf("(Clone)");

        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public void Destroy(GameObject gameObject, float time = 0)
    {
        Object.Destroy(gameObject, time);
    }
}
