using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public static class Util
{
    public static T GetOrAddComponent<T> (this GameObject go) where T : Component
    {
        T component = go.GetComponent<T>();

        if (component == null) component = go.AddComponent<T>();
        return component;
    }

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;
        
        if(recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                
                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();

                    if (component != null) return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name) 
                {
                    if(component != null) 
                        return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(this GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        return transform.gameObject;
    }

    public static void BindEvent(this GameObject go, Action<PointerEventData> eventData, Define.InputEvent type = Define.InputEvent.Click)
    {
        UI_EventHandler eventHandler = GetOrAddComponent<UI_EventHandler>(go);

        switch(type)
        {
            case Define.InputEvent.Click:
                eventHandler.OnClickHandler -= eventData;
                eventHandler.OnClickHandler += eventData;
                break;
        }
    }
    
    public static int[] RandomF(int maxCount, int n)
	{
		int[] defaults = new int[maxCount];
		int[] results = new int[n];

		for (int i = 0; i < maxCount; ++i)
		{
			defaults[i] = i;
		}

		for (int i = 0; i < n; ++i)
		{
			int index = UnityEngine.Random.Range(0, maxCount);
			results[i] = defaults[index];
			defaults[index] = defaults[maxCount - 1];
			maxCount--;
		}

		return results;
	}

}