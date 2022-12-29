using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulBirds : MonoBehaviour, IMinigame
{
    [SerializeField] Transform[] spawnPos = new Transform[3];
    float[] createTime = new float[3] { 3f, 2.5f, 1.5f };
    float currentTime = 0f;
    [SerializeField] Camera gameCamera = null;
    [SerializeField] Transform root;

    private void Awake()
    {
        gameCamera = transform.Find("MinigameCamera").GetComponent<Camera>();
        Transform spawnPositions = transform.Find("SpawnPositions");
        Camera.main.gameObject.SetActive(false);
        root = new GameObject { name = "CrowRoot" }.transform;
        for (int i = 0; i < spawnPositions.childCount; i++)
            spawnPos[i] = spawnPositions.GetChild(i);
    }

    public void GameStart()
    {
        MinigameManager.instance.StartTimer(30f);
        MinigameManager.instance.StartLife();
        MinigameManager.instance.SetFeedback
        (
            "검지손가락으로 침착하게 새를 터치해 보세요."
        );

        int index = Random.Range(0, spawnPos.Length);
        StartCoroutine(OnRoutine());
    }

    private void Update()
    {   
        if(Input.GetMouseButtonDown(0) && !MinigameManager.instance.isGameOver)
        {
            Managers.Sound.PlaySFX(Define.SFX.Fire);
            Vector2 mousePos = Input.mousePosition;
            Ray ray = gameCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject go1 = Managers.Resource.Instantiate("Minigame/HarmfulBirds/Fire", hit.point, Quaternion.identity);
                CrowController crow = hit.transform.GetComponent<CrowController>();
                if (crow != null)
                {
                    GameObject go2 = Managers.Resource.Instantiate("Minigame/HarmfulBirds/Explosion", hit.point, Quaternion.identity);
                    crow.ShotDown();
                    Managers.Sound.PlaySFX(Define.SFX.CrawDying);
                }
            }
        }
    }

    IEnumerator OnRoutine()
    {
        int timeIdx = 0;
        float time = 0;
        
        while (MinigameManager.instance.Timer.isTimerZero == false && !MinigameManager.instance.isGameOver)
        {
            currentTime += Time.deltaTime;
            time += Time.deltaTime;

            if(time >= 10)
            {
                time = 0;
                timeIdx++;

                if (timeIdx >= createTime.Length)
                    break;
            }

            if (createTime[timeIdx] <= currentTime)
            {
                currentTime = 0;
                int index = Random.Range(0, spawnPos.Length);
                GameObject go = Managers.Resource.Instantiate("Minigame/HarmfulBirds/Crow", spawnPos[index].position + Vector3.back, Quaternion.identity, root);
            }

            yield return null;
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < root.transform.childCount; i++)
        {
            Destroy(root.transform.GetChild(i).gameObject);
        }

        Managers.Sound.StopAllSfx();

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
