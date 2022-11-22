using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : UI_Popup
{
    TMP_Text result_Text;
    TMP_Text score_Text;
    TMP_Text rank_Text;
    Button restart_Button;
    Button exit_Button;

    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        result_Text = GetText((int)Texts.Result_Text);
        score_Text = GetText((int)Texts.Score_Text);
        rank_Text = GetText((int)Texts.Rank_Text);

        restart_Button = GetButton((int)Buttons.Restart_Button);
        exit_Button = GetButton((int)Buttons.Exit_Button);

        restart_Button.onClick.AddListener(() => ClickedRestart());
        exit_Button.onClick.AddListener(() => ClickedExit());
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
