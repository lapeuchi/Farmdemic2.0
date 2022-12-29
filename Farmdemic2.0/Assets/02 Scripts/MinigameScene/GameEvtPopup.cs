using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEvtPopup : UI_Popup
{
    [SerializeField] TMP_Text evt_Text;
    [SerializeField] Image loading_Image;

    [SerializeField] public bool isEndCountDown = false;
    [SerializeField] public bool isEndGameOverEffect = false;

    [SerializeField] private int count = 3;
    [SerializeField] private float originSize;
    [SerializeField] private float maxSize;

    enum Texts
    {
        Evt_Text
    }

    public override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        
        evt_Text = GetText((int)Texts.Evt_Text);

        base.Init();
    }

    void Start()
    {
        originSize = evt_Text.fontSize;
        maxSize = originSize * 2;
        evt_Text.text = "게임이 곧 시작됩니다.";
    }

    public IEnumerator GameStartEffect()
    {   
        evt_Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Managers.Sound.PlaySFX(Define.SFX.Countdown);
        for (int i = count; i >= 0; i--)
        {
            count = i;
            evt_Text.fontSize = maxSize;
            
            if(i == 0) evt_Text.text = "Start";
            else evt_Text.text = $"{count}";

            //Debug.Log(i);
            float timer = 0;

            while (timer <= 1)
            {    
                evt_Text.fontSize = Mathf.Lerp(evt_Text.fontSize, originSize, 0.07f);
                timer += Time.deltaTime;
                yield return null;
            }

            
        }   

        yield return new WaitForEndOfFrame();
        evt_Text.gameObject.SetActive(false);
        isEndCountDown = true;
    }

    public IEnumerator GameOverEffect()
    {
        evt_Text.gameObject.SetActive(true);
        evt_Text.fontSize = 1f;
        evt_Text.text = "게임 종료";
        
        float tiemr = 0;
        while(true)
        {
            evt_Text.fontSize = Mathf.Lerp(evt_Text.fontSize, originSize, 0.02f); 
            tiemr += Time.deltaTime;

            if(tiemr > 2f)
            {
                break;
            }

            yield return null;
        }
        
        evt_Text.fontSize = originSize;

        yield return new WaitForSeconds(2f);
        isEndGameOverEffect = true;
        evt_Text.gameObject.SetActive(false);

    }

}