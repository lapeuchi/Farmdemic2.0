using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public SceneBase Scene { get { return GameObject.FindObjectOfType<SceneBase>(); } }

    public void Load(Define.Scene type)
    {
        SceneManager.LoadScene(type.ToString());
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        char[] letters = name.ToCharArray();

        letters[0] = char.ToUpper(letters[0]);

        return new string(letters);
    }
}
