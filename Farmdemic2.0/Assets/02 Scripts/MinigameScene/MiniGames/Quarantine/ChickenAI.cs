using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum State
{
    Idle,
    Moving,
    Dragging
}

public class ChickenAI : MonoBehaviour
{
    public bool isInfected;
    
    Vector2 moveDir;
    float speed;
    
    float actionTime;
    float actionTimer;

    [SerializeField] State state;
    Animator anim;
    Quarantine quarantine;
    Rigidbody2D rigid;
    Dragable drag;
    DOTweenAnimation dotAnim;

    [SerializeField] Fence curFence;
    [SerializeField] bool onFence;
    [SerializeField] bool inFence;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        drag = Util.GetOrAddComponent<Dragable>(gameObject);
        drag = GetComponent<Dragable>();
        quarantine = GameObject.Find("Quarantine").GetComponent<Quarantine>();
        dotAnim = GetComponent<DOTweenAnimation>();
        SetState(State.Moving);
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Moving:
                MovingUpdate();
                break;
                Managers.Sound.PlaySFX(Define.SFX.Flapping);
            case State.Dragging:
                DraggingUpdate();
                break;
        }

        if (state != State.Dragging && drag.isDrag)
        {
            SetState(State.Dragging);    
        }
        else if (MinigameManager.instance.isGameOver)
        {
            drag.isDragable = false;
        }
    }

    void SetState(State state)
    {
        actionTimer = 0;
        this.state = state;
        switch (state)
        {
            case State.Idle:
                actionTime = Random.Range(1f, 2f);
                anim.CrossFade("Idle", 0.5f);
                break;
            case State.Moving:
                actionTime = Random.Range(2f, 4f);
                moveDir.x = Random.Range(-1f, 1f);
                moveDir.y = Random.Range(-1f, 1f);
                if(moveDir.x < 0) 
                
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1); 
                else 
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
                speed = Random.Range(3f, 5f);
                rigid.velocity = moveDir * speed;
                anim.CrossFade("Moving", 0.5f);
                break;
            case State.Dragging:
                anim.CrossFade("Fly", 0.5f);
                dotAnim.DOPlay();
                break;
        }
        
    }

    void MovingUpdate()
    {   
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        transform.position = Camera.main.ViewportToWorldPoint(pos);

        actionTimer += Time.deltaTime;
        if(actionTimer >= actionTime)
        {
            rigid.velocity = Vector2.zero;
            ChangeRandomState();
        }
        if (drag.isDrag)
            SetState(State.Dragging);
    }
    
    void IdleUpdate()
    {
        actionTimer += Time.deltaTime;
        if (actionTimer >= actionTime)
        {
            ChangeRandomState();
        }
        if (drag.isDrag)
        {
            SetState(State.Dragging);
        }    
    }

    void DraggingUpdate()
    {
        if(drag.isDrag == false)
        { 
            if (onFence)
            {
                CheckFence();
            }
            ChangeRandomState();
            dotAnim.DORewind();
            Managers.Sound.PlaySFX(Define.SFX.Pop);
            return;
        }
    }

    void ChangeRandomState()
    {
        int r = Random.Range(0, (int)State.Dragging);
        if (r == 0) SetState(State.Idle);
        else SetState(State.Moving);
    }

    public void Infection()
    {
        isInfected = true;
    }

    void CheckFence()
    {
        inFence = true;
        drag.isDragable = false;
        transform.position = curFence.transform.position;

        if(curFence.quarantineFence && isInfected)
        {
            GameObject go = Managers.Resource.Instantiate("MiniGame/Quarantine/CollectEffect", transform.position, Quaternion.identity);
            Managers.Sound.PlaySFX(Define.SFX.Collect);
            MinigameManager.instance.Score.PlusScore(quarantine.point);
        }
        else if (!curFence.quarantineFence && !isInfected)
        {
            GameObject go = Managers.Resource.Instantiate("MiniGame/Quarantine/CollectEffect", transform.position, Quaternion.identity);
            Managers.Sound.PlaySFX(Define.SFX.Collect);
            MinigameManager.instance.Score.PlusScore(quarantine.point);
        }
        else
        {
            GameObject go = Managers.Resource.Instantiate("MiniGame/Quarantine/VirusEffect", transform.position, Quaternion.identity);
            Managers.Sound.PlaySFX(Define.SFX.Worth);
            MinigameManager.instance.Score.PlusScore(-quarantine.point * 2);
        }

        quarantine.leftChickens.Remove(gameObject);
        if(quarantine.leftChickens.Count == 0)
        {
            MinigameManager.instance.GameOver();
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GameController"))
        {
            curFence = collision.GetComponent<Fence>();
            onFence = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameController"))
        {
            curFence = null;
            onFence = false;
        }
    }
}
