using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent
{
    public int CurrentChapter { get; private set; } = 1;
    public Define.DialogueEvent CurrentEventCode { get { return eventCode; } set { eventCode = value; } }
    Define.DialogueEvent eventCode;
    int miniGameIdx = 0;
    int popupIdx = 0;

    public void NextChapter(Define.DialogueEvent type)
    {
        switch(type)
        {
            case Define.DialogueEvent.MiniGame:
                MinigameTrigger.LoadMiniGame((Define.Minigame) miniGameIdx);
                miniGameIdx++;
                break;
            case Define.DialogueEvent.InfoPopup:
                //Managers.UI.ShowPopupUI<UI_Info>();
                popupIdx++;
                break;
        }

        CurrentChapter++;
    }
}
