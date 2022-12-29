using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Cutscene : UI_Popup
{
    enum Images
    {
        FadeImage
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        StartCoroutine(CutScene());
    }

    IEnumerator CutScene()
    {
        GetImage((int)Images.FadeImage).DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.5f);
        Managers.Game.CurrentCutScene++;
        Managers.Game.SetCamera();
        GetImage((int)Images.FadeImage).DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.5f);

        Managers.UI.ClosePopupUI();

        if (Managers.Game.CurrentEventCode == Define.Event.Ending)
            Managers.UI.ShowPopupUI<UI_Ending>();
        else
            Managers.UI.ShowPopupUI<UI_Dialogue>();
    }
}
