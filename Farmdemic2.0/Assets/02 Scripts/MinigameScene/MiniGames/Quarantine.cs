using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarantine : MonoBehaviour, IMinigame
{
    int chickenCount;
    int infectedChickenCount;

    [SerializeField] int outCount = 0;
    [SerializeField] int inCount = 0;

    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject chickenPrefab; 
    [SerializeField] GameObject infectedChicken_Prefab;

    List<ChickenAI> chickens = new List<ChickenAI>();

    void Awake()
    {
        Camera.main.gameObject.SetActive(false);
    }

    public void GameStart()
    {            
        chickenCount = 15;
        outCount = 0;
        inCount = 0;

        chickenPrefab = Managers.Resource.Load<GameObject>("Prefabs/Minigame/Quarantine/Chicken");
        infectedChicken_Prefab = Managers.Resource.Load<GameObject>("Prefabs/Minigame/Quarantine/InfectedChicken");

        spawnPos = GameObject.Find("SpawnPos").transform;
        infectedChickenCount = Random.Range(1, chickenCount - (int)(chickenCount / 3));
        Debug.Log("strangeChicken Count: " + infectedChickenCount);
        outCount = chickenCount;
        inCount = 0;
        
        SetChicken();

        MinigameManager.instance.StartTimer(60);

        StartCoroutine(Spwan());
    }
    
    void SetChicken()
    {
        // 일반 닭 소환
        for(int i = 0; i < chickenCount - infectedChickenCount; i++)
        {
            GameObject go = Instantiate(chickenPrefab, spawnPos.position, Quaternion.identity);
            chickens.Add(go.GetComponent<ChickenAI>());

            go.SetActive(false);
        }

        // 감염된 닭 소환
        for (int i = 0; i < infectedChickenCount; i++)
        {
            GameObject go = Instantiate(infectedChicken_Prefab, spawnPos.position, Quaternion.identity);
            ChickenAI chick = go.GetComponent<ChickenAI>();
            chick.Infection();
            chickens.Add(go.GetComponent<ChickenAI>());
            go.SetActive(false);
        }
    }

    IEnumerator Spwan()
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < chickenCount; i++)
        {
            chickens[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }    
    }

    public void GameOver()
    {

    }
}