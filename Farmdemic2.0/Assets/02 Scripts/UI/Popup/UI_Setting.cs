using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    enum Buttons
    {
        BGM_Plus,
        BGM_Minus,
        SFX_Plus,
        SFX_Minus,
        CloseButton,
        ExitButton
    }

    enum Sliders
    {
        BGM_Slider,
        SFX_Slider
    }

    public override void Init()
    {
        base.Init();
        Bind<Slider>(typeof(Sliders));
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.BGM_Plus).onClick.AddListener(OnClickedBGMPlus);
        GetButton((int)Buttons.BGM_Minus).onClick.AddListener(OnClickedBGMMinus);
        GetButton((int)Buttons.SFX_Plus).onClick.AddListener(OnClickedSFXPlus);
        GetButton((int)Buttons.SFX_Minus).onClick.AddListener(OnClickedSFXMinus);
        GetButton((int)Buttons.CloseButton).onClick.AddListener(OnClickedCloseButton);
        GetButton((int)Buttons.ExitButton).onClick.AddListener(OnClickedGameOverButton);

        Get<Slider>((int)Sliders.BGM_Slider).value = 1f;
        Get<Slider>((int)Sliders.SFX_Slider).value = 1f;
    }

    void OnClickedBGMPlus()
    {
        Get<Slider>((int)Sliders.BGM_Slider).value += 0.1f;
        Managers.Sound.SetVolume(Get<Slider>((int)Sliders.BGM_Slider).value, Define.Sound.BGM);
    }

    void OnClickedSFXPlus()
    {
        Get<Slider>((int)Sliders.SFX_Slider).value += 0.1f;
        Managers.Sound.SetVolume(Get<Slider>((int)Sliders.SFX_Slider).value, Define.Sound.SFX);
    }

    void OnClickedSFXMinus()
    {
        Get<Slider>((int)Sliders.SFX_Slider).value -= 0.1f;
        Managers.Sound.SetVolume(Get<Slider>((int)Sliders.SFX_Slider).value, Define.Sound.SFX);
    }

    void OnClickedBGMMinus()
    {
        Get<Slider>((int)Sliders.BGM_Slider).value -= 0.1f;
        Managers.Sound.SetVolume(Get<Slider>((int)Sliders.BGM_Slider).value, Define.Sound.BGM);
    }

    void OnClickedCloseButton()
    {
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickedGameOverButton()
    {
        Application.Quit();
    }
}
