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
            Debug.Log("Failed : This path is null");
        }

        return data;
    }

    public GameObject Instantiate(string path, Transform parents = null)
    {
        GameObject go = Load<GameObject>($"Prefabs/path");

        if(go == null)
        {
            return null;
        }

        return Object.Instantiate(go, parents);
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation, Transform parents = null)
    {
        GameObject go = Load<GameObject>($"Prefabs/path");

        if (go == null)
        {
            return null;
        }

        return Object.Instantiate(go, position, rotation, parents);
    }

    public void Destroy(GameObject gameObject, float time)
    {
        Object.Destroy(gameObject, time);
    }
}
