using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_RealTip : UI_Popup
{
    enum Buttons
    {
        CloseButton
    }

    enum GameObjects
    {
        Group
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickedCloseButton);
        Init(1);
    }

    public void Init(int idx)
    {
        GameObject root = Get<GameObject>((int)GameObjects.Group);

        Queue<Tip> tipQueue = Managers.Data.TipDatas[idx];
;
        while (tipQueue.Count > 0)
        {
            Managers.UI.MakeSubitem<UI_Card>(root.transform).SetInfo(tipQueue.Dequeue());
        }
    }

    void OnClickedCloseButton(PointerEventData evtData)
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowPopupUI<UI_Dialogue>();
    }
}
