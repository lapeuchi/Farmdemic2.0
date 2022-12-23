using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        Camera.main.gameObject.SetActive(false);
        infectedFence = GameObject.Find("InfectedFence").GetComponent<Fence>();
        infectedFence.quarantineFence = true;
    }

    public void GameStart()
    {            
        chickenCount = 20;
        infectedChickenCount = 20;
        chickenPrefab = Managers.Resource.Load<GameObject>("Prefabs/Minigame/Quarantine/Chicken");
       
        infectedChicken_Prefab = Managers.Resource.Load<GameObject>("Prefabs/Minigame/Quarantine/InfectedChicken");

        spawnPos = GameObject.Find("SpawnPos").transform;
        
        Debug.Log("strangeChicken Count: " + infectedChickenCount);
        
        SetChicken();

        MinigameManager.instance.StartTimer(60);

        StartCoroutine(Spwan());
    }
    
    void SetChicken()
    {
        // 일반 닭 소환
        for(int i = 0; i < chickenCount; i++)
        {
            GameObject go = Instantiate(chickenPrefab, spawnPos.position, Quaternion.identity);
        }

        // 감염된 닭 소환
        for (int i = 0; i < infectedChickenCount; i++)
        {
            GameObject go = Instantiate(infectedChicken_Prefab, spawnPos.position, Quaternion.identity);
            go.GetComponent<ChickenAI>().Infection();
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