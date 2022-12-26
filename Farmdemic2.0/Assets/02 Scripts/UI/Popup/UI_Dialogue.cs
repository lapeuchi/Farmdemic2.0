using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Dialogue : UI_Scene
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

    int _index = 0;
    float _delay = 0.025f;
    bool typing = false;

    public override void Init()
    {
        base.Init();
        
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnNextButtonClicked);
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Dialogue dialgoue = Managers.Dialogue.GetDialogue();
        GetText((int)Texts.NameText).text = dialgoue.name;
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
        
        ShowDialogue();
    }
}
