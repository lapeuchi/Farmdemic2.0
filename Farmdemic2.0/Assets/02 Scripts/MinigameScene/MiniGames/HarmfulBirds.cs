using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulBirds : MonoBehaviour, IMinigame
{
    [SerializeField] Transform[] spawnPos = new Transform[3];
    float[] createTime = new float[3] { 1, 2, 3 };
    float currentTime = 0f;

    public void GameStart()
    {
        MinigameManager.instance.StartTimer(40f);
        MinigameManager.instance.StartLife();
        MinigameManager.instance.SetFeedback
        (
            "열심히 좀 해봐요",
            "뭐하냐 (피드백)",
            "ㅋ"
        );

        StartCoroutine(OnRoutine());
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            CrowController crow = hit.transform.GetComponent<CrowController>();

            if (crow != null)
                crow.ShotDown();
        }
    }

    IEnumerator OnRoutine()
    {
        int tmpCreateTimeIdx = Random.Range(0, createTime.Length);

        while (MinigameManager.instance.Timer.isTimerZero == false)
        {
            currentTime += Time.deltaTime;

            if (createTime[tmpCreateTimeIdx] <= currentTime)
            {
                currentTime = 0;
                int index = Random.Range(0, spawnPos.Length);
                Managers.Resource.Instantiate("Minigame/HarmfulBirds/Crow", spawnPos[index].position, Quaternion.identity);
                tmpCreateTimeIdx = Random.Range(0, createTime.Length);
            }

            yield return null;
        }
    }

    public void GameOver()
    {
        if (MinigameManager.instance.Timer.isTimerZero)
        {
            MinigameManager.instance.SetClaer(true);
        }
        else if(MinigameManager.instance.Life.isLifeZero)
        {
            MinigameManager.instance.SetClaer(false);
        }

        if(MinigameManager.instance.Score.Score < 60)
        {
            MinigameManager.instance.SetRank(Define.Rank.C);
        }
        else if (MinigameManager.instance.Score.Score < 120)
        {
            MinigameManager.instance.SetRank(Define.Rank.B);
        }
        else 
        {
            MinigameManager.instance.SetRank(Define.Rank.A);
        }
    }
}
