using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ResultUI : UI_Popup
{
    [SerializeField] Image result_Image;

    [SerializeField] TMP_Text result_Text;
    [SerializeField] TMP_Text score_Text;
    [SerializeField] TMP_Text rank_Text;
    [SerializeField] Button restart_Button;
    [SerializeField] Button exit_Button;

    GameObject[] objects = new GameObject[5];

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        
        result_Image = GetImage((int)Images.Result_Image);
        result_Image.gameObject.SetActive(false);

        result_Text = GetText((int)Texts.Result_Text);
        result_Text.gameObject.SetActive(false);
        objects[0] = result_Text.gameObject;

        score_Text = GetText((int)Texts.Score_Text);
        score_Text.gameObject.SetActive(false);
        objects[1] = score_Text.gameObject;

        rank_Text = GetText((int)Texts.Rank_Text);
        rank_Text.gameObject.SetActive(false);
        objects[2] = rank_Text.gameObject;

        restart_Button = GetButton((int)Buttons.Restart_Button);
        restart_Button.gameObject.SetActive(false);
        objects[3] = restart_Button.gameObject;

        exit_Button = GetButton((int)Buttons.Exit_Button);
        exit_Button.gameObject.SetActive(false);
        objects[4] = exit_Button.gameObject;

        base.Init();
    }

    void Start()
    {
        restart_Button.onClick.AddListener(() => ClickedRestart());
        exit_Button.onClick.AddListener(() => ClickedExit());
    }
    
    enum Images
    {
        Result_Image,
    }

    enum Texts
    {
        Result_Text,
        Score_Text,
        Rank_Text
    }
    
    enum Buttons
    {
        Restart_Button,
        Exit_Button
    }

    public void SetResult(bool isClear)
    {
        StartCoroutine(ResultEffect(isClear));
    }

    IEnumerator ResultEffect(bool isClear)
    {
        yield return new WaitForSeconds(3f);
        result_Image.gameObject.SetActive(true);
        objects[0].SetActive(true);
        objects[1].SetActive(true);
        objects[2].SetActive(true);
        objects[3].SetActive(true);
        objects[4].SetActive(true);
        if (isClear)
        {
            result_Text.text = "Clear !!!";
        }
        else
        {
            result_Text.text = "Falied ...";
        }
        
        score_Text.text = $"Score: {MinigameManager.instance.Score}";
        
        rank_Text.text = $"Rank: {MinigameManager.instance.Grade}";
    }

    private void ClickedExit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LobbyScene");
        MinigameTrigger.Clear();
    } 

    private void ClickedRestart()
    {
        MinigameTrigger.LoadMiniGame(MinigameTrigger.Minigame);
    }

}
