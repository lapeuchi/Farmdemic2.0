using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Dialogue : UI_Popup
{
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

    enum Sprite
    {
        ModelSprite
    }

    [SerializeField] List<Dialogue> _dialogueList = new List<Dialogue>();
    Define.Story currentChapter = Define.Story.None;

    int _index = 0;
    float _delay = 0.025f;
    bool typing = false;

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

    IEnumerator TypingEffect(string word)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        typing = true;
        for(int i = 0; i < word.Length; i++)
        {
            GetText((int)Texts.WordText).text = word.Substring(0, i);
            yield return wait;
        }

        GetText((int)Texts.WordText).text = word;
        typing = false;
    }

    void OnNextButtonClicked()
    {
        if (typing)
            return;

        _index++;
        if (_index == (int) Define.Story.Chater1)
        {
            Managers.UI.ClosePopupUI();
            MinigameTrigger.LoadMiniGame(Define.Minigame.OXQuiz);
            return;
        }

        ShowDialogue();
    }
}
