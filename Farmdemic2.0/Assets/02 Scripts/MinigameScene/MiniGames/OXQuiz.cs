using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OXQuiz : MonoBehaviour, IMinigame
{
    [SerializeField] Button O_Button;
    [SerializeField] Button X_Button;
    [SerializeField] TMP_Text pannel_Text;
    [SerializeField] bool lockButton;

    [SerializeField] public List<Quiz_OX> quiz_List = new List<Quiz_OX>();
    
    [SerializeField] int quizIndex = 0;
    int quizLength = 5;

    int point = 20;

    private void Awake()
    {
        O_Button = GameObject.Find("O_Button").GetComponent<Button>();
        X_Button = GameObject.Find("X_Button").GetComponent<Button>();
        pannel_Text = GameObject.Find("Pannel_Text").GetComponent<TMP_Text>();
        
        O_Button.onClick.AddListener(()=>Input("O"));
        X_Button.onClick.AddListener(()=>Input("X"));
        lockButton = true;
        quizIndex = 0;
        CreateQuiz();
        
        Debug.Log("Init!");
    }

    void Start()
    {
        
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
                MinigameManager.instance.SetRank(Define.Rank.D);
                break;
            case 1:
                MinigameManager.instance.SetRank(Define.Rank.C);
                break;
            case 2:
                MinigameManager.instance.SetRank(Define.Rank.B);
                break;
            case 3:
                MinigameManager.instance.SetRank(Define.Rank.A);
                break;
            case 4:
                MinigameManager.instance.SetRank(Define.Rank.S);
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