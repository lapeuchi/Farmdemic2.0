using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_Ending : UI_Popup
{
    private Image[] rank_Images;
    private TMP_Text[] rank_Texts;
    private Button exit_Button;

    private TMP_Text finalScore_Text;
    private DOTweenAnimation score_Anim;

    int score;

    public override void Init()
    {
        base.Init();
        Debug.Log(DataSaver.ranks.Count);
        rank_Images = new Image[DataSaver.ranks.Count];
        rank_Texts = new TMP_Text[DataSaver.ranks.Count];  
        for(int i = 0; i < DataSaver.ranks.Count; i++)
        {
            rank_Images[i] = GameObject.Find($"Rank_Image_{i}").GetComponent<Image>();
            rank_Texts[i] = GameObject.Find($"Rank_Text_{i}").GetComponent<TMP_Text>();
        }
        finalScore_Text = GameObject.Find("FinalScore_Text").GetComponent<TMP_Text>();
        score_Anim = finalScore_Text.GetComponent<DOTweenAnimation>();
        
        exit_Button = GameObject.Find("Exit_Button").GetComponent<Button>();
        exit_Button.onClick.AddListener(()=>Managers.Scene.LoadSceneAsync(Define.Scene.Title));

        // 테스트용
        // GameResult.ranks[Define.Rank.A] = 4;
        // GameResult.ranks[Define.Rank.B] = 0;
        // GameResult.ranks[Define.Rank.C] = 0;

        finalScore_Text.text = "0";
        
    }

    void Start()
    {
        StartCoroutine(PrintMinigameGrades());   
    }

    void Update()
    {
        
    }

    IEnumerator PrintMinigameGrades()
    {
        yield return new WaitForSeconds(1.5f);
        for(int i = 0; i < DataSaver.ranks.Count; i++)
        {
            if (DataSaver.ranks[(Define.Rank)i] == 0)
            {
                rank_Texts[i].gameObject.SetActive(false);
                rank_Images[i].gameObject.SetActive(false);
                continue;
            }
            for(int j = 0; j < DataSaver.ranks[(Define.Rank)i]; j++)
            {
                if (j==0)
                {
                    rank_Images[i].enabled = true;
                    rank_Texts[i].enabled = true;
                    rank_Images[i].sprite = Managers.Resource.Load<Sprite>($"Sprites/Rank_{(Define.Rank)i}");
                    rank_Texts[i].text = $"X {j+1}";
                    Managers.Sound.PlaySFX(Define.SFX.WriteRank);
                }
                else
                {
                    rank_Texts[i].text = $"X {j+1}";
                    Managers.Sound.PlaySFX(Define.SFX.WriteRank);
                }
                score += DataSaver.rankScore[(Define.Rank)i];
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1f);
        }
        
        yield return new WaitForSeconds(1f);
        finalScore_Text.enabled = true;
        finalScore_Text.text = score.ToString() + " / 100";
        Managers.Sound.PlaySFX(Define.SFX.WriteRank);
    }
}
