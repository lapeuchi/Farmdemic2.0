using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingDisinfectant : MonoBehaviour, IMinigame
{
    [SerializeField]
    private GameObject[] disinfectant = new GameObject[6];

    [SerializeField]
    private GameObject[] way = new GameObject[6];

    [SerializeField]
    private GameObject disinfectantParent;

    [SerializeField]
    private GameObject wayParent;

    [SerializeField]
    private GameObject canvas;

    private int diffrentDisinfectantHeight;
    private int diffrentWayHeight;

    /**     
    [소독제 종류와 용도]

    비누/세정제      : 소독효과를 감소시키는 유기물질,먼지, 기름 제거
    염기제제        : 저렴하게 대단위 소독
    산성세제        : 세정제와 함께 쓰면 바이러스 사멸 효과 증진
    알데하이드제제   : 축사/차량 내부 소독에 탁월
    산화제          : 동절기에 사용할 경우 적합
    생석회          : 사체 및 토양 소독제로 주로 이용
    **/

    private void Awake()
    {
        diffrentDisinfectantHeight = 70;
        diffrentWayHeight = 270;

        canvas = transform.Find("UI").gameObject;
        disinfectantParent = canvas.transform.Find("DisinfectantParent").gameObject;
        wayParent = canvas.transform.Find("WayParent").gameObject;

        for (int i = 0; i < 6; i++)
        {
            disinfectant[i] = disinfectantParent.transform.Find($"Disinfectant{i + 1}").gameObject;
        }

        for (int i = 0; i < 6; i++)
        {
            way[i] = wayParent.transform.Find($"Way{i + 1}").gameObject;
        }

        //lockButton = true;
        //index = 0;

        Debug.Log("Init!");
    }

    private void SetObjects()
    {

        for (int i = 0; i < 6; i++)
        {
            disinfectant[i] = disinfectantParent.transform.Find($"Disinfectant{i + 1}").gameObject;
        }

        for (int i = 0; i < 6; i++)
        {
            way[i] = wayParent.transform.Find($"Way{i + 1}").gameObject;
        }
    }

    public void GameStart()
    {
        SetObjects();
    }

    public void GameOver(bool isClear)
    {
        
    }   
}
