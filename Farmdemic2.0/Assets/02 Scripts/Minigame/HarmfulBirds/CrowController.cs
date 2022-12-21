using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrowController : MonoBehaviour
{
    float speed = 5f;
    float arrivePos;
    int score = 10;

    Image myImg;
    Animator anim;
    private void Start()
    {
        myImg = gameObject.GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x <= arrivePos)
            Arrive();
    }

    void Arrive()
    {
        Managers.Sound.PlaySFX(Define.SFX.CrowCrying);
        MinigameManager.instance.Life.MinusLife();
    }

    public void ShotDown()
    {
        MinigameManager.instance.Score.PlusScore(score);
        anim.SetTrigger("Die");
        myImg.DOFade(0.4f, 1f);
        Destroy(gameObject, 1f);
    }


}
