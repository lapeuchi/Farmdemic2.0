using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Todo@ make dialogue system (UI)
public class DialogueManager : MonoBehaviour
{
    private int index = 0;
    private List<Define.Dialogue> dialogues = new List<Define.Dialogue>();
    private Text ui_name;
    private Text ui_word;
    private Image ui_model;
    private Button ui_nextButton;
    private GameObject root;
    private float _delay = 0.125f;
    public void Init()
    {
        root = Managers.UI.root.transform.Find("@Dialogue").gameObject;
        ui_name = root.GetComponent<Text>();
        ui_word = root.GetComponent<Text>();
        ui_model = root.GetComponent<Image>();
    }

    public void NextTalk()
    {
        ui_nextButton.gameObject.SetActive(false);
        StartCoroutine(TalkEffect());
        index++;
    }

    IEnumerator TalkEffect()
    {
        string word = dialogues[index].word;
        ui_name.text = dialogues[index].name;
        ui_model.sprite = dialogues[index].model;
        WaitForSeconds delay = new WaitForSeconds(_delay);

        for(int i = 0; i < word.Length; i++)
        {
            string _word = word.Substring(0, i);
            ui_word.text = dialogues[index].word;
            yield return delay;
        }

        ui_nextButton.gameObject.SetActive(true);
    }
}
