using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEvtUI : MonoBehaviour
{
    TMP_Text evt_Text;

    public bool isZeroCount = false;
    public bool isGameOver = false;

    int count = 5;
    float originSize;
    float maxSize;

    void Awake()
    {
        evt_Text = GameObject.Find("Evt_Text").GetComponent<TMP_Text>();
        originSize = evt_Text.fontSize;
        maxSize = originSize * 2;
        evt_Text.text = "게임이 곧 시작됩니다.";
    }

    public IEnumerator CountDown()
    {   
        yield return null;

        for (int i = count; i >= 0; i--)
        {
            count = i;
            evt_Text.fontSize = maxSize;
            
            if(i == 0) evt_Text.text = "Start";
            else evt_Text.text = $"{count}";

            Debug.Log(i);
            float timer = 0;

            while (timer <= 1)
            {    
                evt_Text.fontSize = Mathf.Lerp(evt_Text.fontSize, originSize, 0.07f);
                timer += Time.fixedDeltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }   

        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
        isZeroCount = true;
    }

    public IEnumerator GameOver()
    {
        evt_Text.fontSize = 0.1f;
        evt_Text.text = "게임 종료";
        
        float tiemr = 0;
        while(true)
        {
            evt_Text.fontSize = Mathf.Lerp(evt_Text.fontSize, originSize, 0.3f);     
            
            tiemr += Time.deltaTime;
            if(tiemr > 2f)
            {
                break;
            }

            yield return null;
        }
        
        evt_Text.fontSize = originSize;

        yield return new WaitForSeconds(2f);
        isGameOver = true;
        gameObject.SetActive(false);

    }

}
