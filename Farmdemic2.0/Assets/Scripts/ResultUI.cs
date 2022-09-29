using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] Text result_text;
    [SerializeField] Text score_text;
    [SerializeField] Button restart_Button;
    [SerializeField] Button exit_Button;

    void Awake()
    {
        
        result_text = GameObject.Find("Result_Text").GetComponent<Text>();
        score_text = GameObject.Find("Score_Text").GetComponent<Text>();

        restart_Button = GameObject.Find("Restart_Button").GetComponent<Button>();
        exit_Button = GameObject.Find("Exit_Button").GetComponent<Button>();

        restart_Button.onClick.AddListener(()=>ClickedRestart());
        exit_Button.onClick.AddListener(()=>ClickedExit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResult(bool isClear, int score = 0)
    {
        if (isClear)
        {
            result_text.text = "Clear !!!";
        }
        else
        {
            result_text.text = "Falied ...";
        }
        score_text.text = $"Score: {score}";
    }

    void ClickedExit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LobbyScene");
        MiniGameTrigger.Clear();
    } 

    void ClickedRestart()
    {
        MiniGameTrigger.LoadMiniGame(MiniGameTrigger.MiniGame);
    }
}
