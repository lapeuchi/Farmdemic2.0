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

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Util.BindEvent(GetButton((int)Buttons.CloseButton).gameObject, OnClickedCloseButton);
    }

    void OnClickedCloseButton(PointerEventData evtData)
    {
        Managers.Game.NextChapter();
        Managers.UI.ClosePopupUI();
    }
}
