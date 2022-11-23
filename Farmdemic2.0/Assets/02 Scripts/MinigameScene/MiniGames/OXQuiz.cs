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
    bool lockButton;

    public List<Quiz_OX> quiz_List = new List<Quiz_OX>();
    
    int quizIndex = 0;
    int quizLength = 5;
    int point = 20;

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
        pannel_Text = GameObject.Find("Pannel_Text").GetComponent<TMP_Text>();
        MinigameManager.instance.SetFeedback
        (
            "반복 플레이를 통해 암기를 하세요",
            "인터넷에서 관련 자료를 검색해보세요."
        );
    }

    void Start()
    {
        O_Button.onClick.AddListener(() => Input("O"));
        X_Button.onClick.AddListener(() => Input("X"));
        lockButton = true;
        quizIndex = 0;
        CreateQuiz();
    }

    void CreateQuiz()
    {
        List<Quiz_OX> jsonList = Managers.Data.OXQuizDatas;
        int[] rands = RandomF(jsonList.Count, quizLength);

        for(int i = 0; i < quizLength; i++)
        {
            quiz_List.Add(jsonList[rands[i]]);
        }
        
    }

    public void GameStart()
    {
        StartCoroutine(ChangeQuiz());
    }

    public void GameOver(bool isClear)
    {
        lockButton = true;
        
        switch(MinigameManager.instance.Score % point)
        {
            case 0:
                MinigameManager.instance.SetRank(Define.Rank.C);
                break;
            case 1:
                MinigameManager.instance.SetRank(Define.Rank.C);
                break;
            case 2:
                MinigameManager.instance.SetRank(Define.Rank.B);
                break;
            case 3:
                MinigameManager.instance.SetRank(Define.Rank.B);
                break;
            case 4:
                MinigameManager.instance.SetRank(Define.Rank.A);
                break;
        }
        
        MinigameManager.instance.GameOver(isClear);
    }
    
    void Input(string inputAnswer)
    {
        if (lockButton == true) return;

        lockButton = true;

        StartCoroutine(Explanation(inputAnswer));
    }

    IEnumerator Explanation(string inputAnswer)
    {
        if (inputAnswer == quiz_List[quizIndex].answer)
        {
            pannel_Text.text = "정답입니다!\n";
            MinigameManager.instance.AddScore(point);
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
            GameOver(true);
        }
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
        
        yield return new WaitForSeconds(2f);

        pannel_Text.text = quiz_List[quizIndex].text;
        lockButton = false;   
    }

    public int[] RandomF(int maxCount, int n)
	{
		int[] defaults = new int[maxCount];
		int[] results = new int[n];

		for (int i = 0; i < maxCount; ++i)
		{
			defaults[i] = i;
		}

		for (int i = 0; i < n; ++i)
		{
			int index = Random.Range(0, maxCount);
			results[i] = defaults[index];
			defaults[index] = defaults[maxCount - 1];
			maxCount--;
		}

		return results;
	}

}