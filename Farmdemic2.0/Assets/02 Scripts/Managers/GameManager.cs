using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Transform[] CameraPoints { get { return cameraPoints; } set { cameraPoints = value; } }
    public int MaxChaper { get { return maxChater; } }
    public int CurrentChapter { get { return currentChapter; } }
    public Define.Event CurrentEventCode { get { return eventCode; } set { eventCode = value; } }
    
    Define.Event eventCode;
    Transform[] cameraPoints;
    int maxChater;
    int currentChapter = 1; 
    int miniGameIdx = 1;
    int popupIdx = 0;

    public void Init()
    {
        maxChater = 13;
        cameraPoints = new Transform[maxChater];
        eventCode = Managers.Dialogue.DialogueDic[currentChapter].Peek().eventCode;
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
                Managers.UI.ShowPopupUI<UI_RealTip>();
                popupIdx++;
                break;
            case Define.Event.Cutscene:
                Managers.UI.ShowPopupUI<UI_Dialogue>();
                //Camera.main.transform.position = cameraPoints[currentChapter].position;
                //Camera.main.transform.rotation = cameraPoints[currentChapter].rotation;
                break;
        }

        eventCode = Managers.Dialogue.DialogueDic[currentChapter].Peek().eventCode;
    }
}