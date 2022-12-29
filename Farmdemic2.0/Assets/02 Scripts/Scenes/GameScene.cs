using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDic;

public class GameScene : SceneBase
{
    static bool isFirst;
    public override void Init()
    {
        base.Init();
        sceneType = Define.Scene.Game;

        Managers.UI.ShowPopupUI<UI_SceneTransition>();
        Managers.Sound.PlayBGM(Define.BGM.MainBgm);
        
        if(Managers.Game.CameraPoints[0] == null)
        {
            GameObject cam_root = GameObject.Find("CamPos");

            for(int i = 0; i < cam_root.transform.childCount; i++)
            {
                Managers.Game.CameraPoints[i] = cam_root.transform.GetChild(i);
            }
        }
        
        Managers.Game.SetCamera();
        Managers.UI.ShowSceneUI<UI_Game>();
        
        if(isFirst == false)
        {
            isFirst = true;
        
            for(int i = 0; i < (int)Define.Rank.F; i++)
            {
                GameResult.ranks.Add((Define.Rank)i, 0);
            }
        }
    }

    public IEnumerator LoadMnigameWithEffect(Define.Minigame game)
    {
        Camera.main.GetComponent<CameraController>().ZoomEffect();
        yield return new WaitForSeconds(1.5f);
        MinigameTrigger.LoadMiniGame(game);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Managers.UI.ShowPopupUI<UI_Ending>();
        }
    }
}
