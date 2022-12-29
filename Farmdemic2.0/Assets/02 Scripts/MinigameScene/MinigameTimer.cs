using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinigameTimer : UI_Scene
{
    TMP_Text timer_Text;
    float time;
    float startTime;
    public bool isTimerZero;
    Image stopwatch_Image;
    Image curTime_Image;

    bool iswarning;
    
    public override void Init()
    {
        iswarning = false;
        isTimerZero = false;
        base.Init();
    }
    
    public void Setting(float t)
    {
        time = t;
        startTime = t;
        isTimerZero = false;
        timer_Text = GameObject.Find("Timer_Text").GetComponent<TMP_Text>();
        stopwatch_Image = GameObject.Find("Stopwatch_Image").GetComponent<Image>();
        curTime_Image = GameObject.Find("CurTime_Image").GetComponent<Image>();

        timer_Text.text = ((int)time).ToString();

        // Debug.Log($"Timer.Setting({time})");
        // Debug.Log(timer_Text);
        // Debug.Log(isTimerZero = false);
    }

    void Update()
    {   
        // Debug.Log("isTimerZero: "+isTimerZero);
        // Debug.Log("isGameOver: "+MinigameManager.isGameOver);
        if(isTimerZero == false)
        {
            time -= Time.deltaTime;
            if(time <= startTime / 3) Warning();
            timer_Text.text = ((int)time).ToString();
            curTime_Image.fillAmount = time / startTime;
            if(time <= 0)
            {
                EndTimer();
            }
        }    
    }
    
    void Warning()
    {
        if(iswarning == true) return;
        iswarning = true;

         Managers.Sound.PlaySFX(Define.SFX.Hurryup);
        stopwatch_Image.color = Color.red;
        curTime_Image.color = Color.red;
        timer_Text.color = Color.red;   
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
