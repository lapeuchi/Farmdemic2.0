using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    private void Start()
    {
        OnLoad();
        //Managers.Sound.PlayBGM(Define.BGM.MainBgm);
    }

    public override void Init()
    {
        base.Init();

        sceneType = Define.Scene.Game;
    }

    public override void OnLoad()
    {
        Managers.UI.ShowPopupUI<UI_Dialogue>();
    }
}
