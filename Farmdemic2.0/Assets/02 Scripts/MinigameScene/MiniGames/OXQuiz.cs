using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OXQuiz : UI_Popup, IMinigame
{
    [SerializeField] Button O_Button;
    [SerializeField] Button X_Button;
    [SerializeField] TMP_Text pannel_Text;
    [SerializeField] TMP_Text index_Text;
    [SerializeField] Image effect_Image;
    [SerializeField] Sprite collect_Sprite;
    [SerializeField] Sprite worth_Sprite;

    int effect_num = 2;

    bool lockButton;

    public List<Quiz_OX> quiz_List = new List<Quiz_OX>();
    
    int quizIndex = 0;
    int quizLength = 10;
    int point = 10;
    
    float effectTime;
    float blinkTime = 0.5f;

    enum Buttons
    {
        O_Button,
        X_Button
    }

    enum Texts
    {
        Pannel_Text
    }
    
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));

        O_Button = GetButton((int)Buttons.O_Button);
        X_Button = GetButton((int)Buttons.X_Button);
        //pannel_Text = GetText((int)Texts.Pannel_Text);
        pannel_Text = GameObject.Find("Panel_Text").GetComponent<TMP_Text>();
        index_Text = GameObject.Find("Index_Text").GetComponent<TMP_Text>();
        effect_Image = GameObject.Find("Effect_Image").GetComponent<Image>();
        effect_Image.enabled = false;

        collect_Sprite = Managers.Resource.Load<Sprite>("Sprites/Check_Sprite");
        worth_Sprite = Managers.Resource.Load<Sprite>("Sprites/Worth_Sprite");
        MinigameManager.instance.SetFeedback
        (
            "반복 플레이를 통해 암기를 하세요",
            "인터넷에서 관련 자료를 검색해보세요."
        );
        SetIndexText();

        effectTime = blinkTime * effect_num + 1.5f;
    }
    
    void CreateQuiz()
    {
        List<Quiz_OX> jsonList = Managers.Data.OXQuizDatas;
        int[] rands = Util.RandomF(jsonList.Count, quizLength);

        for(int i = 0; i < quizLength; i++)
        {
            quiz_List.Add(jsonList[rands[i]]);
        }
    }

    public void GameStart()
    {
        O_Button.onClick.AddListener(() => Input("O"));
        X_Button.onClick.AddListener(() => Input("X"));
        lockButton = true;
        quizIndex = 0;
        CreateQuiz();

        StartCoroutine(ChangeQuiz());
    }

    public void GameOver()
    {
        lockButton = true;
        float t = MinigameManager.instance.Score.Score / point;

        if (t <= 5)
        {
            MinigameManager.instance.SetClaer(false);
            MinigameManager.instance.SetRank(Define.Rank.F);
        }
        else if (t <= 6)
        {
            MinigameManager.instance.SetRank(Define.Rank.C);
            MinigameManager.instance.SetClaer(true);
        }
        else if (t <= 8)
        {
            MinigameManager.instance.SetRank(Define.Rank.B);
            MinigameManager.instance.SetClaer(true);
        }
        else
        {
            MinigameManager.instance.SetRank(Define.Rank.A);
            MinigameManager.instance.SetClaer(true);
        }       
    }
    
    void Input(string inputAnswer)
    {
        if (lockButton == true) return;
        lockButton = true;
        StartCoroutine(Explanation(inputAnswer));
    }

    void SetIndexText()=> index_Text.text = $"{quizIndex+1}/{quizLength}";

    IEnumerator Explanation(string inputAnswer)
    {
        bool isCollect = inputAnswer == quiz_List[quizIndex].answer;
        StartCoroutine(Effect(isCollect));

        yield return new WaitForSeconds(effectTime);
        if (isCollect)
        {
            pannel_Text.text = "정답입니다!\n";
            MinigameManager.instance.Score.PlusScore(point);
            //Managers.Sound.PlaySFX(Define.SFX.Collect);
        }
        else
        {
            pannel_Text.text = "오답입니다.\n";
            //Managers.Sound.PlaySFX(Define.SFX.Worth);
        }

        pannel_Text.text += $"해설: {quiz_List[quizIndex].explanation}";

        yield return new WaitForSeconds(5f);

        quizIndex++;

        if(quizIndex != quiz_List.Count)
            StartCoroutine(ChangeQuiz());
        else
        {
            MinigameManager.instance.GameOver();
            GameOver();
        }
    }

    IEnumerator Effect(bool isCollect)
    {
        Debug.Log($"Effect({isCollect})");
        if (isCollect)
        {
            effect_Image.sprite = collect_Sprite;
            effect_Image.color = Color.green;
            Managers.Sound.PlaySFX(Define.SFX.Collect);
        }
        else
        {
            effect_Image.sprite = worth_Sprite;
            effect_Image.color = Color.red;
            Managers.Sound.PlaySFX(Define.SFX.Worth);
        }
        effect_Image.enabled = true;
        
        for(int i = 0; i < effect_num; i++)
        {
            effect_Image.enabled = false;
            yield return new WaitForSeconds(blinkTime);
            effect_Image.enabled = true;
            yield return new WaitForSeconds(blinkTime);
        }
        effect_Image.enabled = false;
    }

    IEnumerator ChangeQuiz()
    {               
        if(quizIndex != 0)
        {
            pannel_Text.text = "다음 문제입니다.";
        }
        else if (quizIndex == quiz_List.Count)
        {
            pannel_Text.text = "마지막 문제입니다.";
        }
        else
        {
            pannel_Text.text = "첫 번째 문제입니다.";
        }

        SetIndexText();
        
        yield return new WaitForSeconds(2f);

        pannel_Text.text = quiz_List[quizIndex].text;
        lockButton = false;   
    }

    

}