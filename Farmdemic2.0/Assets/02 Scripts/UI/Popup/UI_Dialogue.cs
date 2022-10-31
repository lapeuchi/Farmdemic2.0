using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Dialogue : UI_Popup
{
    [SerializeField] List<Dialogue> _dialogueList = new List<Dialogue>();
    int _index = 0;
    float _delay = 0.025f;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");

            if(root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }

            return root;
        }
    }
    enum Texts
    {
        NameText,
        WordText
    }

    enum Images
    {
        ModelImage
    }

    enum Buttons
    {
        NextButton
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        _dialogueList = Managers.Data.dialogueDatas;
        Managers.UI.SetCanvas(gameObject, true);

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.NextButton).onClick.AddListener(OnNextButtonClicked);
        ShowDialogue();
    }

    void ShowDialogue()
    {
        GetText((int)Texts.NameText).text = _dialogueList[_index].name;
        //GetImage((int)Images.ModelImage).sprite = _dialogueList[_index].sprite;
        StartCoroutine(TypingEffect(_dialogueList[_index].word));
    }
    //asdfsd
    IEnumerator TypingEffect(string word)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        
        for(int i = 0; i < word.Length; i++)
        {
            string message = word.Substring(0, i);
            GetText((int)Texts.WordText).text = message;
            yield return wait;
        }

        GetText((int)Texts.WordText).text = word;
    }

    void OnNextButtonClicked()
    {
        _index++;
        if (_index == _dialogueList.Count)
        {
            Managers.UI.ClosePopupUI();
            return;
        }

        ShowDialogue();        
    }
}
