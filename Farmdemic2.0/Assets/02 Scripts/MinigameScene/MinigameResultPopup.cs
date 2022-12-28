using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MinigameResultPopup : UI_Popup
{

    [SerializeField] TMP_Text result_Text;
    [SerializeField] TMP_Text scoreValue_Text;
    [SerializeField] Image rankValue_Image;
    [SerializeField] Button restart_Button;
    [SerializeField] Button exit_Button;    


    [SerializeField] TMP_Text[] feedback_Texts = new TMP_Text[3];
    Vector3 rankOriginScale;
    Vector3 rankStartScale = new Vector3(3,3,3);


    public override void Init()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        result_Text = GetText((int)Texts.Result_Text);

        scoreValue_Text = GameObject.Find("ScoreValue_Text").GetComponent<TMP_Text>();

        rankValue_Image = GameObject.Find("RankValue_Image").GetComponent<Image>();
        rankValue_Image.enabled = false;

        restart_Button = GetButton((int)Buttons.Restart_Button);

        exit_Button = GetButton((int)Buttons.Exit_Button);

        for(int i = 0; i < 3; i++)
        {
            feedback_Texts[i] = GameObject.Find($"Feedback_Text_{i}").GetComponent<TMP_Text>();
            feedback_Texts[i].color = new Color(feedback_Texts[i].color.r, feedback_Texts[i].color.g, feedback_Texts[i].color.b, 0);
        }
        
        base.Init();
    }

    void Start()
    {
        scoreValue_Text.text = "0";
        restart_Button.onClick.AddListener(() => ClickedRestart());
        exit_Button.onClick.AddListener(() => ClickedExit());
        rankOriginScale = rankValue_Image.transform.localScale;
    }

    enum Texts
    {
        Result_Text,
        Rank_Text
    }
    
    enum Buttons
    {
        Restart_Button,
        Exit_Button
    }
 
    public void SetResult()
    {
        StartCoroutine(ResultEffect());
    }

    IEnumerator ResultEffect()
    {
        if(MinigameManager.instance.IsClear == false)
        {
            exit_Button.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);   
        
        scoreValue_Text.DOText($"{MinigameManager.instance.Score.Score}", 2f, true, ScrambleMode.Numerals);
        yield return new WaitForSeconds(1.9f);
        Managers.Sound.PlaySFX(Define.SFX.WriteRank);
        yield return new WaitForSeconds(0.5f);
       

        rankValue_Image.sprite = Managers.Resource.Load<Sprite>($"Sprites/Rank_{MinigameManager.instance.Rank.ToString()}");
        rankValue_Image.transform.localScale = rankStartScale;
        rankValue_Image.enabled = true;
        rankValue_Image.transform.DOScale(rankOriginScale, 1f);

        yield return new WaitForSeconds(1f);
        Managers.Sound.PlaySFX(Define.SFX.WriteRank);
        yield return new WaitForSeconds(0.5f);

        Debug.Log(MinigameManager.instance.IsClear);
        if (MinigameManager.instance.IsClear)
        {
            result_Text.DOText("CLEAR!!!", 1.5f, true, ScrambleMode.Uppercase);
        }
        else
        {
            result_Text.DOText("FAILED...", 1.5f, true, ScrambleMode.Uppercase);
        }

        yield return new WaitForSeconds(2.0f);

        if(MinigameManager.instance.IsClear == false)
        {
            for (int i = 0; i < 3; i++)
            {
                if(MinigameManager.instance.feedbacks[i] != null)
                {
                    feedback_Texts[i].text = "● " + MinigameManager.instance.feedbacks[i];
                    feedback_Texts[i].DOFade(1f, 1f);
                }
            }
        }
    }

    private void ClickedExit()
    {
        GameResult.ranks[MinigameManager.instance.Rank] += 1;
        Managers.Scene.LoadSceneAsync(Define.Scene.Game);
        MinigameTrigger.Clear();
    } 

    private void ClickedRestart()
    {
        MinigameTrigger.LoadMiniGame(MinigameTrigger.Minigame);
    }

}
