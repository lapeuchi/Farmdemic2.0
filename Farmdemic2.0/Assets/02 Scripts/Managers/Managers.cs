using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region //Instance
    static Managers Instance { get { Init(); return _instance; } }
    static Managers _instance = null;

    DataManager _data = new DataManager();
    ResourceManager _resource = new ResourceManager();
    GameManager _game = new GameManager();
    UIManager _ui = new UIManager();
    SoundManager _sound = new SoundManager();
    SceneManagerEx _scene = new SceneManagerEx();
    DialogueManager _dialogue = new DialogueManager();

    public static ResourceManager Resource { get { return Instance._resource; } }
    public static GameManager Game { get { return Instance._game; } }
    public static DataManager Data { get { return Instance._data; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static DialogueManager Dialogue { get { return Instance._dialogue; } }
    #endregion
    
    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
            }

            _instance = Util.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);
            Sound.Init();
            Data.Init();
        }
    }
}
