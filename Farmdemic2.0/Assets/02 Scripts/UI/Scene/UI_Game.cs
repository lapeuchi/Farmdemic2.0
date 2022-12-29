using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : UI_Scene
{
    enum Buttons
    {
        SettingButton
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.SettingButton).onClick.AddListener(OnClieckedSettingButton);

    }

    void OnClieckedSettingButton()
    {
        Managers.UI.ShowPopupUI<UI_Setting>();
    }
}
