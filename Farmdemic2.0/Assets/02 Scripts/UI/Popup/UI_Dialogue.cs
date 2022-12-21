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

    [SerializeField] Queue<Dialogue> _dialogueQueue = new Queue<Dialogue>();
    Define.Story currentChapter = Define.Story.None;
    Sprite _hyeok;
    Sprite _young;
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
        base.Init();

        for(int i = 0; i < (int) currentChapter; i++)
            _dialogueQueue.Enqueue(Managers.Data.dialogueDatas[i]);

        _hyeok = Managers.Resource.Load<Sprite>($"Sprites/Hyeok");
        _young = Managers.Resource.Load<Sprite>($"Sprites/Young");

        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.NextButton).onClick.AddListener(OnNextButtonClicked);
        ShowDialogue();
        base.Init();
    }

    void ShowDialogue()
    {
        Dialogue dialgoue = _dialogueQueue.Dequeue();
        GetText((int)Texts.NameText).text = dialgoue.name;

        if(dialgoue.name == "방혁")
            GetImage((int)Images.ModelImage).sprite = _hyeok;
        else
            GetImage((int)Images.ModelImage).sprite = _young;

        StartCoroutine(TypingEffect(dialgoue.word));
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
        //Managers.Sound.PlaySFX(Define.SFX.ClickDialogue);
        if (_index == (int) Define.Story.Chater1)
        {
            Managers.UI.ClosePopupUI();
            MinigameTrigger.LoadMiniGame(Define.Minigame.OXQuiz);
            //StartCoroutine(GameObject.Find("@Scene").GetComponent<GameScene>().LoadMnigameWithEffect(Define.Minigame.OXQuiz));
            return;
        }

        ShowDialogue();
    }
}
