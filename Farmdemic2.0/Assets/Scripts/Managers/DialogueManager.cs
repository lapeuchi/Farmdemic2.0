using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Todo@ make dialogue system (UI)
public class DialogueManager
{
    public int Chapter = 0;
    private int _index = 0;
    private const int maxChapter = 5;

    private List<Define.Dialogue> dialogues = new List<Define.Dialogue>();
    
    public void Init()
    {
           
    }

    public void NextTalk()
    {
        string name = dialogues[_index].name;
        string message = dialogues[_index].word;
        _index++;
    }
}
