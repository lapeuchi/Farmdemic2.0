using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _word;
    private Image _model;
    private Button _nextButton;
    
    [SerializeField] private List<Dialogue> myList = new List<Dialogue>();
    
    public void Init()
    {
        DialogueManager dialogue = Managers.Dialogue;

        _name = dialogue.Root.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        _word = dialogue.Root.transform.Find("Word").GetComponent<TextMeshProUGUI>();
        _model = dialogue.Root.transform.Find("Model").GetComponent<Image>();
        _nextButton = dialogue.Root.transform.Find("NextButton").GetComponent<Button>();
        _nextButton.onClick.AddListener(Managers.Dialogue.UpdateDialogue);

        myList = dialogue.dialogues;
    }

    public void DialogueEffect(string word, float delay) => StartCoroutine(TypingEffect(word, delay));
    public void SetUI(string name, Sprite model) { _name.text = name; _model.sprite = model; }
    
    IEnumerator TypingEffect(string word, float delay)
    {
        WaitForSeconds DELAY = new WaitForSeconds(delay);
        _nextButton.gameObject.SetActive(false);

        for(int i = 0; i < word.Length; i++)
        {
            string text = word.Substring(0, i);
            _word.text = text;
            yield return DELAY;
        }

        _nextButton.gameObject.SetActive(true);
    }
}
