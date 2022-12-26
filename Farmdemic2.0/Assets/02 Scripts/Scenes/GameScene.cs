using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDic;

public class GameScene : SceneBase
{
    [SerializeField] private List<Dialogue> dialogues = new List<Dialogue>();
    Dictionary<int, Queue<Dialogue>> dialogueDic = new Dictionary<int, Queue<Dialogue>>();

    public override void Init()
    {
        base.Init();
        dialogues = Managers.Data.dialogueDatas;
        sceneType = Define.Scene.Game;

        List<Dialogue> dialogueList = Managers.Data.dialogueDatas;

        for (int i = 1; i <= 13; i++)
        {
            dialogueDic.Add(i, new Queue<Dialogue>());
        }

        foreach (Dialogue dialogue in dialogueList)
        {
            dialogueDic[dialogue.code].Enqueue(dialogue);
        }

        //if (Managers.Game.CurrentStory == Define.Story.Intro)
        //    Managers.UI.ShowPopupUI<UI_Intro>();
        //else
        Managers.UI.ShowPopupUI<UI_SceneTransition>();
        Managers.Sound.PlayBGM(Define.BGM.MainBgm);
       
    }


    public IEnumerator LoadMnigameWithEffect(Define.Minigame game)
    {
        Camera.main.GetComponent<MainCameraMovement>().ZoomEffect();
        yield return new WaitForSeconds(1.5f);
        MinigameTrigger.LoadMiniGame(game);
    }
}
