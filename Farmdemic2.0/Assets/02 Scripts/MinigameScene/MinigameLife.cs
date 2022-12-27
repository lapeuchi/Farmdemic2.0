using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MinigameLife : UI_Scene
{
    Image[] life_Images;
    DOTweenAnimation[] life_Anims;
    int maxLife = 3;
    int index;

    public bool isLifeZero;

    public override void Init()
    {
        base.Init();
    }

    public void Setting()
    {
        maxLife = 3;
        index = maxLife - 1;

        isLifeZero = false;
        life_Images = new Image[maxLife];
        life_Anims = new DOTweenAnimation[maxLife];

        Debug.Log(index);

        for(int i = 0; i < maxLife; i++)
        {
            life_Images[i] = GameObject.Find($"Life_Image_{i}").GetComponent<Image>();
            life_Anims[i] = life_Images[i].GetComponent<DOTweenAnimation>();
            int destroyIndex = i;
            life_Anims[i].onComplete.AddListener(()=>DestroyLife(destroyIndex));
        }
    }

    void DestroyLife(int i) 
    {
        Debug.Log(i);
        life_Images[i].gameObject.SetActive(false);
    } 

    public void PlusLife()
    {
        if(index >= maxLife - 1 || isLifeZero) 
        {
            return;
        }
        else
            life_Images[++index].gameObject.SetActive(true);
    }
    public void MinusLife()
    {
        if(index < 0) return;
        StartCoroutine(MinusLifeEffect(index));
    }

    IEnumerator MinusLifeEffect(int index)
    {
        life_Anims[index].DOPlay();
        Managers.Sound.PlaySFX(Define.SFX.RemoveLife);
        this.index -= 1;
        yield return null;
        if (this.index < 0) 
        {
            isLifeZero = true;
            MinigameManager.instance.GameOver();
            this.index = -1;
        }
    }
}
