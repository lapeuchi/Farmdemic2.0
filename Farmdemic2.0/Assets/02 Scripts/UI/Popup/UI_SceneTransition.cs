using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_SceneTransition : UI_Popup
{
    enum Images
    {
        FadeImage
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        GetImage((int)Images.FadeImage).DOFade(0f, 1.5f);
        Invoke("FadeIn", 1.5f);
    }

    void FadeIn()
    {
        Managers.UI.ClosePopupUI();
        Managers.UI.ShowSceneUI<UI_Dialogue>();
    }
}
