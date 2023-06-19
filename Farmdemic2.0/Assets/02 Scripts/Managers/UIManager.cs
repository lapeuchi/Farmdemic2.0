using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{
    GameObject root = null;

    public GameObject Root
    {
        get
        {
            if(root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }

    int _order = 10;
    

    public void SetCanvas(GameObject go, bool sort)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        CanvasScaler canvasScaler = go.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);

        if(sort == true)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name) == true) name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        go.transform.SetParent(Root.transform);
        return popup;
    }

    UI_Scene _sceneUI = null;

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name) == true) name = typeof(T).Name;
        
        GameObject go = Managers.Resource.Instantiate($"UI/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;
        go.transform.SetParent(Root.transform);
        
        return sceneUI;
    }

    public T MakeSubitem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Subitem/{name}");
        T subitem = go.GetOrAddComponent<T>();

        if (parent != null)
            go.transform.SetParent(parent);

        return subitem;
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0) return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        _order--;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() == popup)
            Managers.Resource.Destroy(popup.gameObject);
    }

    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
            ClosePopupUI();
    }
}