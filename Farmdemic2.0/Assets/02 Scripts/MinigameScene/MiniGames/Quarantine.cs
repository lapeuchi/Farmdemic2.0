using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Quarantine : MonoBehaviour, IMinigame
{
    int chickenCount;
    int infectedChickenCount;

    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject chickenPrefab; 
    [SerializeField] GameObject infectedChicken_Prefab;

    public List<GameObject> leftChickens = new List<GameObject>();
    GameObject[] chickens;
    Fence infectedFence;

    public int point = 100; 

    DOTweenAnimation leftFence_Anim;
    DOTweenAnimation rightFence_Anim; 

    void Awake()
    {
        leftFence_Anim = GameObject.Find("LeftFence_Text").GetComponent<DOTweenAnimation>();
        rightFence_Anim = GameObject.Find("RightFence_Text").GetComponent<DOTweenAnimation>();

        Camera.main.gameObject.SetActive(false);
        infectedFence = GameObject.Find("InfectedFence").GetComponent<Fence>();
        infectedFence.quarantineFence = true;
    }

    public void GameStart()
    {            
        chickenCount = 15;
        infectedChickenCount = 15;
        chickenPrefab = Managers.Resource.Load<GameObject>("Prefabs/Minigame/Quarantine/Chicken");

        infectedChicken_Prefab = Managers.Resource.Load<GameObject>("Prefabs/Minigame/Quarantine/InfectedChicken");

        spawnPos = GameObject.Find("SpawnPos").transform;
        
        //Debug.Log("strangeChicken Count: " + infectedChickenCount);
        
        SetChicken();

        MinigameManager.instance.SetFeedback
        (
            "가만히 있는 닭을 최대한 분류해보세요.",
            "울타리와 같은 분류의 닭들 중 최대한 가까운 닭 먼저 분류해보세요"
        );

        MinigameManager.instance.StartTimer(60);

        StartCoroutine(Spwan());
        leftFence_Anim.DOPlay();
        rightFence_Anim.DOPlay();
    }

    void SetChicken()
    {
        int o = chickenCount;
        int s = infectedChickenCount;
        for (int i = 0; i < chickenCount + infectedChickenCount; i++)
        {
            int j = Random.Range(0, 2);

            //일반닭 소환
            if ((j == 0 && o >= 0) || s == 0)
            {
                GameObject go = Instantiate(chickenPrefab, spawnPos.position, Quaternion.identity);
                o--;
            }
            // AI 닭 소환
            else if ((j==1 && s >= 0) || o == 0)
            {
                GameObject go = Instantiate(infectedChicken_Prefab, spawnPos.position, Quaternion.identity);
                go.GetComponent<ChickenAI>().Infection(); 
                s--;
            }
        }
        chickens = GameObject.FindGameObjectsWithTag("Chicken");

        foreach(GameObject go in chickens)
        {
            leftChickens.Add(go);
            go.SetActive(false);
        }
    }

    IEnumerator Spwan()
    {
        int sum = chickenCount + infectedChickenCount;
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < sum; i++)
        {
            chickens[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }    
    }

    public void GameOver()
    {
        int maxScore = (chickenCount+infectedChickenCount) * point;
        int rank_A = maxScore - (point * 3);
        int rank_B = maxScore - (point * 6);
        int rank_C = maxScore - (point * 10);

        if (MinigameManager.instance.Score.Score >= rank_A)
        {
            MinigameManager.instance.SetClaer(true);
            MinigameManager.instance.SetRank(Define.Rank.A);
        }
        else if (MinigameManager.instance.Score.Score >= rank_B)
        {
            MinigameManager.instance.SetClaer(true);
            MinigameManager.instance.SetRank(Define.Rank.B);
        }
        else if (MinigameManager.instance.Score.Score >= rank_C)
        {
            MinigameManager.instance.SetClaer(true);
            MinigameManager.instance.SetRank(Define.Rank.C);
        }
        else
        {
            MinigameManager.instance.SetClaer(false);
            MinigameManager.instance.SetRank(Define.Rank.F);
        }
    }
}