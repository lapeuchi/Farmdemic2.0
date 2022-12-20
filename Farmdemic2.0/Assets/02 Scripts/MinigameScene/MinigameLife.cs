using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameLife : UI_Scene
{
    Image[] life_Images;
    int maxLife = 3;
    int index;

    public bool isLifeZero;

    public override void Init()
    {
        base.Init();
        isLifeZero = false;
        life_Images = new Image[maxLife];
        index = maxLife - 1;
    }

    public void Setting()
    {
        for(int i = 0; i < maxLife; i++)
        {
            life_Images[i] = GameObject.Find($"Life_Image_{i}").GetComponent<Image>();
        }
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
        life_Images[index--].gameObject.SetActive(false);

        if (index < 0) 
        {
            isLifeZero = true;
            MinigameManager.instance.GameOver();
            index = -1;
        }
    }
}
