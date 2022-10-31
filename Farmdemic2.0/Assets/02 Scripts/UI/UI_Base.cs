using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = System.Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject)) objects[i] = Util.FindChild(gameObject, names[i], true);
            else objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object 
    {
        UnityEngine.Object[] objects = null;

        if (_objects.TryGetValue(typeof(T), out objects) == false) return null;
        return objects[index] as T;
    }

    protected TextMeshProUGUI GetText(int index) { return Get<TextMeshProUGUI>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }

    public abstract void Init();
}
