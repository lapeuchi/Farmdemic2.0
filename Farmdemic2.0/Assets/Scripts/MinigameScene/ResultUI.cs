using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultUI : MonoBehaviour
{
    [SerializeField] TMP_Text result_Text;
    [SerializeField] TMP_Text score_Text;
    [SerializeField] TMP_Text rank_Text;
    [SerializeField] Button restart_Button;
    [SerializeField] Button exit_Button;

    void Awake()
    {
        result_Text = GameObject.Find("Result_Text").GetComponent<TMP_Text>();
        score_Text = GameObject.Find("Score_Text").GetComponent<TMP_Text>();
        rank_Text = GameObject.Find("Rank_Text").GetComponent<TMP_Text>();

        restart_Button = GameObject.Find("Restart_Button").GetComponent<Button>();
        exit_Button = GameObject.Find("Exit_Button").GetComponent<Button>();

        restart_Button.onClick.AddListener(()=>ClickedRestart());
        exit_Button.onClick.AddListener(()=>ClickedExit());
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
