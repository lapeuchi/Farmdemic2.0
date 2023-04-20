using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    Dictionary<string, GameObject> _gameObjects = new Dictionary<string, GameObject>();

    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            if(_gameObjects.TryGetValue(path, out GameObject go))
            {
                return go as T;
            }

            GameObject origin = Resources.Load<GameObject>(path);
            _gameObjects.Add(path, origin);
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parents = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        
        if(prefab == null)
        {
            Debug.Log($"Failed to load prefab {path}");
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
