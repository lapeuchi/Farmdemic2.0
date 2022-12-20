using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulBirds : MonoBehaviour, IMinigame
{
    int score;
   
    public void GameStart()
    {
        MinigameManager.instance.StartTimer(30f);
        MinigameManager.instance.StartLife();
        MinigameManager.instance.SetFeedback
        (
            "열심히 좀 해봐요",
            "뭐하냐 (피드백)",
            "ㅋ"
        );
    }

    private void Update()
    {
        // 게임 진행 코드 작성    
        MinigameManager.instance.Score.PlusScore(1);
    }

    public void GameOver()
    {
        if (MinigameManager.instance.Timer.isTimerZero)
        {
            MinigameManager.instance.SetClaer(true);
        }
        
        else if (MinigameManager.instance.Life.isLifeZero)
        {
            MinigameManager.instance.SetClaer(true);
        }

        if(score < 60)
        {
            MinigameManager.instance.SetRank(Define.Rank.C);
        }
        else if (score < 120)
        {
            MinigameManager.instance.SetRank(Define.Rank.B);
        }
        else 
        {
            MinigameManager.instance.SetRank(Define.Rank.A);
        }
    }
}
