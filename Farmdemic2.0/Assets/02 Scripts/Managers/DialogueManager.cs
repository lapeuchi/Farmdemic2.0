using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager
{
    public Dictionary<int, Queue<Dialogue>> DialogueDic { get; private set; } = new Dictionary<int, Queue<Dialogue>>();
    int maxCode = 13;

    public void Init()
    {
        List<Dialogue> dialogueList = Managers.Data.dialogueDatas;

        for (int i = 1; i <= maxCode; i++)
        {
            DialogueDic.Add(i, new Queue<Dialogue>());
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            DialogueDic[dialogue.code].Enqueue(dialogue);
        }
    }

    public Dialogue GetDialogue()
    {
        Dialogue dialogue = null;

        if (DialogueDic[Managers.Game.CurrentChapter].Count == 0)
        {
            Managers.UI.ClosePopupUI();
            Managers.Game.NextChapter();
        }
        else
        {
            dialogue = DialogueDic[Managers.Game.CurrentChapter].Dequeue();
            Debug.Log(Managers.Game.CurrentChapter);
            Managers.Game.CurrentEventCode = dialogue.eventCode;
        }

        return dialogue;
    }
}
