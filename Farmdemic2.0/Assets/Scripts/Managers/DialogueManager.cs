using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Todo@ make dialogue system (UI)
public class DialogueManager
{
    public int Chapter = 0;
    private int _index = 0;
    private const int maxChapter = 5;

    private Dictionary<int, List<Define.Dialogue>> dialogues = new Dictionary<int, List<Define.Dialogue>>();
    private Define.Dialogue []dialogue = null;
    
    public void Init()
    {
        NextChapter();   
    }

    public void NextTalk()
    {
        string name = dialogue[_index].name;
        string message = dialogue[_index].word;
        _index++;
    }

    public void NextChapter()
    {
        if (Chapter == maxChapter) return;

        dialogue = dialogues[Chapter].ToArray();
        Chapter++;
    }
}
