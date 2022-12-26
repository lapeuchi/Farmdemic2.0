using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager
{
    public Dictionary<int, Queue<Dialogue>> _dialogueDic = new Dictionary<int, Queue<Dialogue>>();
    public DialogueEvent EventHandler { get { return _dialogueEvent; } }
    DialogueEvent _dialogueEvent = new DialogueEvent();
    Define.Event CurrentEvent;
    int maxCode = 5;
    
    public void Init()
    {
        List<Dialogue> dialogueList = Managers.Data.dialogueDatas;

        for (int i = 1; i <= maxCode; i++)
        {
            _dialogueDic.Add(i, new Queue<Dialogue>());
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            _dialogueDic[dialogue.code].Enqueue(dialogue);
        }
    }

    public Dialogue GetDialogue()
    {
        Dialogue dialogue = new Dialogue();

        if (_dialogueDic[_dialogueEvent.CurrentChapter].Count == 0)
        {
            _dialogueEvent.NextChapter(EventHandler.CurrentEventCode);
        }
        else
        {
            dialogue = _dialogueDic[_dialogueEvent.CurrentChapter].Dequeue();
            Managers.Dialogue.EventHandler.CurrentEventCode = dialogue.eventCode;
        }

        return dialogue;
    }
}
