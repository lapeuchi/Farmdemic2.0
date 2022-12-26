using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UI_Title : UI_Scene
{
    enum Buttons
    {
        Button
    }

    enum Texts
    {
        Text
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        GetButton((int)Buttons.Button).onClick.AddListener(() => Managers.Scene.Load(Define.Scene.Game));
        StartCoroutine(TextEffect());
    }

    IEnumerator TextEffect()
    {
        while(true)
        {
            GetText((int)Texts.Text).DOColor(new Color(1, 1, 1, 0.2f), 1f);
            yield return new WaitForSeconds(1f);
            GetText((int)Texts.Text).DOColor(new Color(1, 1, 1, 0.8f), 1f);
            yield return new WaitForSeconds(1f);
        }
    }
}
