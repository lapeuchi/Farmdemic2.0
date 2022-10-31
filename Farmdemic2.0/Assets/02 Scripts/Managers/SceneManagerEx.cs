using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public SceneBase Scene
    {
        get
        {
            GameObject go = GameObject.Find("@Scene");
            return go.GetComponent<SceneBase>();
        }
    }

    public void Load(Define.Scene type)
    {
        SceneManager.LoadScene(type.ToString());
        Scene.Init();
    }
}
