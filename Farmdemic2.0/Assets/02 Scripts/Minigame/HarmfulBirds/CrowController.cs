using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CrowController : MonoBehaviour
{
    float speed = 5f;
    float arrivePos = -9;
    int score = 10;
    bool isDead;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(isDead == false)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            
            if (transform.position.x <= arrivePos)
                Arrive();
        }
    }

    void Arrive()
    {
        Managers.Sound.PlaySFX(Define.SFX.CrawCrying);
        GameObject go = Managers.Resource.Instantiate("Minigame/HarmfulBirds/Miss", transform.position + new Vector3(1f,0,0), Quaternion.identity);
        MinigameManager.instance.Life.MinusLife();
        Destroy(gameObject);
    }

    public void ShotDown()
    {
        isDead = true;
        MinigameManager.instance.Score.PlusScore(score);
        anim.SetTrigger("Die");
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Collider col = GetComponent<Collider>();
        sp.DOColor(new Color(1, 1, 1, 0.4f), 1f);
        col.enabled = false;
        Destroy(gameObject, 1f);
    }
}
