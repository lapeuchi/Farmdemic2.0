using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Card : UI_Base
{
    enum Texts
    {
        TitleText,
        ContentText
    }

    enum Images
    {
        ContentImage
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TMP_Text>(typeof(Texts));
    }

    public void SetInfo(Tip tip)
    {
        GetText((int)Texts.TitleText).text = tip.title;
        GetText((int)Texts.ContentText).text = tip.content;
        GetImage((int)Images.ContentImage).sprite = tip.image;
    }
}
