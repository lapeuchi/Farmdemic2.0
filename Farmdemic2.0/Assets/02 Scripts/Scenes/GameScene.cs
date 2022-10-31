using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SceneBase
{
    public override void Init()
    {
        base.Init();

        sceneType = Define.Scene.Game;
    }
}
