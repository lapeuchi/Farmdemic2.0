using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public abstract class SceneBase : MonoBehaviour
{
    public Define.Scene sceneType { get; protected set; } = Define.Scene.None;
    
    protected virtual void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        Object obj = FindObjectOfType(typeof(EventSystem));

        if(obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }
}
