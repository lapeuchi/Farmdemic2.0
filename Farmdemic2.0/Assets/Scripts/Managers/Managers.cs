using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region //Instance
    static Managers Instance { get { Init(); return _instance; } }
    static Managers _instance = null;

    public static ResourceManager Resource { get { return Instance._resource; } }
    ResourceManager _resource = new ResourceManager();

    public static DialogueManager Dialogue { get { return Instance._dialogue; } }
    DialogueManager _dialogue = new DialogueManager();

    public static GameManager Game { get { return Instance._game; } }
    GameManager _game = new GameManager();
    
    public static DataManager Data { get { return Instance._data; } }
    DataManager _data = new DataManager();
    #endregion

    static void Init()
    {
        if(_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
            }

            if(go.GetComponent<Managers>() == null)
            {
                go.AddComponent<Managers>();
            }

            _instance = go.GetComponent<Managers>();
            DontDestroyOnLoad(go);
            
            Dialogue.Init();
        }
    }

    void Start()
    {
        Init();    
    }
}
