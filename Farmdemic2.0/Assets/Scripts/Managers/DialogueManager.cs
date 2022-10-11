using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue
{
    public List<Dialogue> myList = new List<Dialogue>();
    public string name;
    public string word;
    public Sprite model;

    public List<Dialogue> MyDialogue()
    {
        return myList;
    }
}

public class DialogueManager
{
    public GameObject Root { get; set; }
    private int index = 0;
    private List<Dialogue> dialogues = new List<Dialogue>();
    private DialogueUI dialogueUI;
    private float delay = 0.125f;

    public void Init()
    {
        if(Root == null)
        {
            Root = Managers.Resource.Instantiate("UI/DialogueCanvas", GameObject.Find("@Managers").transform); 
            Root.transform.parent = Managers.UI.Root.transform;
            Root.AddComponent<DialogueUI>();
        }

        Root.gameObject.name = "@Dialogue";
        dialogueUI = Root.GetComponent<DialogueUI>();
        dialogueUI.Init();

        NextTalk();
    }

    public void NextTalk()
    {
        if(index == dialogues.Count) return;
        dialogueUI.SetUI(dialogues[index].name, dialogues[index].model);
        dialogueUI.DialogueEffect(dialogues[index].word, delay);
        index++;
    }
}