using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Transform[] CameraPoints { get { return cameraPoints; } set { cameraPoints = value; } }
    public int MaxChaper { get { return maxChater; } }
    public int CurrentChapter { get { return currentChapter; } }
    public int CurrentCutScene { get { return cutIdx; } set { cutIdx = value; } }
    public int MaxPopup { get { return maxPopup; } }
    public Define.Event CurrentEventCode { get { return eventCode; } set { eventCode = value; } }
    
    Define.Event eventCode;
    Transform[] cameraPoints;
    int maxChater;
    int maxPopup = 3;
    int currentChapter = 1; 
    
    int miniGameIdx = 1;
    int popupIdx = 1;
    int cutIdx = 0;

    public void Init()
    {
        maxChater = 13;
        cameraPoints = new Transform[6];
        eventCode = Managers.Dialogue.Dialogues[currentChapter].Peek().eventCode;
    }
    public void NextChapter()
    {
        currentChapter++;
        
        switch (eventCode)
        {
            case Define.Event.MiniGame:
                MinigameTrigger.LoadMiniGame((Define.Minigame)miniGameIdx);
                miniGameIdx++;
                break;
            case Define.Event.InfoPopup:
                Managers.UI.ShowPopupUI<UI_RealTip>().Init(popupIdx);
                popupIdx++;
                break;
            case Define.Event.Cutscene:
                Managers.UI.ShowPopupUI<UI_Cutscene>();
                break;
            case Define.Event.Fade:
                Managers.UI.ShowPopupUI<UI_Fade>();
                break;
            case Define.Event.Ending:
                Managers.UI.ShowPopupUI<UI_Cutscene>();
                break;
        }
        if(eventCode != Define.Event.Ending)
            eventCode = Managers.Dialogue.Dialogues[currentChapter].Peek().eventCode;
    }

    public void SetCamera()
    {
        Camera.main.transform.position = Managers.Game.CameraPoints[Managers.Game.CurrentCutScene].position;
        Camera.main.transform.rotation = Managers.Game.CameraPoints[Managers.Game.CurrentCutScene].rotation;
    }
}