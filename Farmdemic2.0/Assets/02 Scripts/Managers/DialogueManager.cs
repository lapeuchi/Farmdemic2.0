using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager
{
    Dictionary<int, Dialogue> _dialogueDic = new Dictionary<int, Dialogue>();
    public int Index { get; private set; } = 0;

    public void Init()
    {
        for (int i = 0; i < Managers.Data.dialogueDatas.Count; i++)
            _dialogueDic.Add(i + 1, Managers.Data.dialogueDatas[i]);
    }

    public Dialogue GetDialogue()
    {
        Dialogue dialogue = _dialogueDic[Index];
        Index++;
        return dialogue;
    }
}
