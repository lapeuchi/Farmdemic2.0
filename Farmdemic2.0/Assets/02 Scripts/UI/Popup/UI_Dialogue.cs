using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
        FaceChipImage,
        BG
    }
    float _delay = 0.025f;
    bool typing = false;

    public override void Init()
    {
        base.Init();
        
        Bind<TMP_Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        GetImage((int)Images.BG).gameObject.BindEvent(OnNextButtonClicked);
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Dialogue dialgoue = Managers.Dialogue.GetDialogue();

        if(dialgoue != null)
        {
            GetText((int)Texts.NameText).text = dialgoue.name;
            GetImage((int)Images.FaceChipImage).sprite = dialgoue.image;

            StartCoroutine(TypingEffect(dialgoue.word));
        }
    }

    IEnumerator TypingEffect(string word)
    {
        WaitForSeconds wait = new WaitForSeconds(_delay);
        typing = true;
        Managers.Sound.PlaySFX(Define.SFX.Writting);
        for(int i = 0; i < word.Length; i++)
        {
            GetText((int)Texts.WordText).text = word.Substring(0, i);
            yield return wait;
        }

        GetText((int)Texts.WordText).text = word;
        yield return new WaitForSeconds(0.5f);
        typing = false;
    }

    void OnNextButtonClicked(PointerEventData evtData)
    {
        if (typing)
            return;

        Managers.Sound.PlaySFX(Define.SFX.ClickDialogue);
        ShowDialogue();
    }
}
