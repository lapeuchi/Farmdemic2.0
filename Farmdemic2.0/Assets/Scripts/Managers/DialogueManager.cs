using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager
{
    public GameObject Root { get; set; }
    private int index = 0;
    public List<Dialogue> dialogues = new List<Dialogue>();
    private DialogueUI dialogueUI;
    private float delay = 0.1f;

    public void Init()
    {
        if(Root == null)
        {
            Root = Managers.Resource.Instantiate("UI/UI_Dialogue", GameObject.Find("@Managers").transform); 
            Root.transform.parent = Managers.UI.Root.transform;
            Root.AddComponent<DialogueUI>();
        }

        Root.gameObject.name = "@Dialogue";
        dialogues = Managers.Data.dialogueDatas;
        dialogueUI = Root.GetComponent<DialogueUI>();
        dialogueUI.Init();

        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        if(index == dialogues.Count) return;
        Debug.Log("Next");
        dialogueUI.SetUI(dialogues[index].name, dialogues[index].model);
        dialogueUI.DialogueEffect(dialogues[index].word, delay);
        index++;
    }
}