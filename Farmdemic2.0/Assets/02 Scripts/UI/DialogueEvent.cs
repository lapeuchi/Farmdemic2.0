using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent
{
    public int CurrentChapter { get; private set; } = 1;
    public Define.Event CurrentEventCode { get { return eventCode; } set { eventCode = value; } }
    Define.Event eventCode;
    int miniGameIdx = 0;
    int popupIdx = 0;

    public void NextChapter(Define.Event type)
    {
        switch(type)
        {
            case Define.Event.MiniGame:
                MinigameTrigger.LoadMiniGame((Define.Minigame) miniGameIdx);
                miniGameIdx++;
                break;
            case Define.Event.InfoPopup:
                //Managers.UI.ShowPopupUI<UI_Info>();
                popupIdx++;
                break;
        }

        CurrentChapter++;
    }
}
