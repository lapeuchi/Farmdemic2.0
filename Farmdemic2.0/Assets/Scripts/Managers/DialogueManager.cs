using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Todo@ make dialogue system (UI)
public class DialogueManager : MonoBehaviour
{
    private int index = 0;
    private List<Define.Dialogue> dialogues = new List<Define.Dialogue>();
    private GameObject root;
    private DialogueUI dialogueUI;
    private float delay = 0.125f;

    public void Init()
    {
        root = Managers.UI.root.transform.Find("@Dialogue").gameObject;

        if(root == null)
        {
            root = new GameObject { name = "@Dialogue" };
            root.transform.parent = Managers.UI.root.transform;
            root.AddComponent<DialogueUI>();
        }
        
        dialogueUI = root.GetComponent<DialogueUI>();
        dialogueUI.ui_name = root.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        dialogueUI.ui_word = root.transform.Find("Word").GetComponent<TextMeshProUGUI>();
        dialogueUI.ui_model = root.transform.Find("Model").GetComponent<Image>();
    }

    public void NextTalk()
    {
        if(index == dialogues.Count) return;

        dialogueUI.ui_nextButton.gameObject.SetActive(false);
        dialogueUI.ui_name.text = dialogues[index].name;
        dialogueUI.ui_word.text = dialogues[index].word;
        dialogueUI.ui_model.sprite = dialogues[index].model;
        dialogueUI.DialogueEffect(dialogues[index].word, delay);
        index++;
    }
}
