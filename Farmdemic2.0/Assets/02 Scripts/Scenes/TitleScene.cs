using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    public override void Init()
    {
        base.Init();
        Managers.UI.ShowSceneUI<UI_Title>();
        sceneType = Define.Scene.Title;

        Managers.Sound.PlayBGM(Define.BGM.TitleBgm);
    }
}
