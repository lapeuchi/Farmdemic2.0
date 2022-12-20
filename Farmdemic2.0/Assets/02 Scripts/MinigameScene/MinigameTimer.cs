using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameTimer : UI_Scene
{
    TMP_Text timer_Text;
    float time;
    public bool isTimerZero;
    
    public override void Init()
    {
        isTimerZero = false;
        
        
        base.Init();
    }
    
    public void Setting(float t)
    {
        time = t;
        timer_Text = GameObject.Find("Timer_Text").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(isTimerZero == false && MinigameManager.isGameOver == false)
        {
            time -= Time.deltaTime;
            timer_Text.text = ((int)time).ToString();
            if(time <= 0)
            {
                EndTimer();
            }
        }    
    }
    
    public void PlusTime(float t)
    {
        if(isTimerZero) return;
        time += t;
    }
    
    public void MinusTime(float t)
    {
        if(isTimerZero) return;
        time -= t;
        if(time < 0) 
        {
            EndTimer();
        }
    
    }

    void EndTimer()
    {
        if(isTimerZero) return;
        isTimerZero = true;
        time = 0;
        timer_Text.text = "0";

        
        MinigameManager.instance.GameOver();
    }


}
