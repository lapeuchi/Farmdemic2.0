using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   
public class DialogueManager
{
    public Dictionary<int, Queue<Dialogue>> DialogueDic { get; private set; } = new Dictionary<int, Queue<Dialogue>>();
    int maxCode = 13;

    public void Init()
    {
        List<Dialogue> dialogueList = Managers.Data.DialogueDatas;

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
            Managers.UI.CloseAllPopupUI();
            Managers.Game.NextChapter();
        }
        else
        {
            dialogue = DialogueDic[Managers.Game.CurrentChapter].Dequeue();
            Managers.Game.CurrentEventCode = dialogue.eventCode;
        }

        return dialogue;
    }
}
