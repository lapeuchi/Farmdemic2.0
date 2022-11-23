using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulBirds : MonoBehaviour, IMinigame
{
    int score;
    public void GameStart()
    {
        MinigameManager.instance.SetFeedback
        (
            "열심히 좀 해봐요",
            "뭐하냐 (피드백)",
            "ㅋ"
        );
    }

    void GameProgress()
    {
        MinigameManager.instance.GameOver();
    }

    public void GameOver()
    {
        MinigameManager.instance.SetScore(score);
        if(score < 50)
        {
            MinigameManager.instance.SetRank(Define.Rank.C);
            MinigameManager.instance.SetClaer(false);
        }
        else if (score > 500)
        {
            MinigameManager.instance.SetRank(Define.Rank.A);
            MinigameManager.instance.SetClaer(true);
        }
    }
}
