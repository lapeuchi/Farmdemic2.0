using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OXQuiz : MinigameBase
{
    [SerializeField] int answer;
    
    [SerializeField] bool lockButton;

    [SerializeField] Button O_Button;
    [SerializeField] Button X_Button;

    [SerializeField] Text pannel_Text;

    [SerializeField] public List<Define.Quiz_OX> quizs = new List<Define.Quiz_OX>();
    [SerializeField] int index = 0;
    
    private void Awake()
    {
        
    }

    protected override void Init()
    {
        O_Button = GameObject.Find("O_Button").GetComponent<Button>();
        X_Button = GameObject.Find("X_Button").GetComponent<Button>();
        pannel_Text = GameObject.Find("Pannel_Text").GetComponent<Text>();

        O_Button.onClick.AddListener(()=>Input(1));
        X_Button.onClick.AddListener(()=>Input(2));
        lockButton = true;
        index = 0;

        CreateQuiz();
    }

    void Start()
    {
        StartCoroutine(ChangeQuiz());
    }

    void CreateQuiz()
    {
        for(int i = 0; i < 3; i++)
        {
            int a = Random.Range(-100, 1000);
            int b = Random.Range(-100, 1000);

            int rand = Random.Range(0, 1);
            int ans = rand == 0 ? a + b : Random.Range(-200, 2000);
            if (rand == 0)
            {
                ans = a + b;
            }
            else if (rand == 1)
            {
                ans = Random.Range(-20000, 20000);
            }

            string text = $"({a}) + ({b}) = {ans}";
            string answerText = (a + b).ToString();

            if(rand == 0)
            {
                answer = 1;
            }
            else 
            {
                answer = 2;
            }

            string explanation = "해설 내용";

            Define.Quiz_OX q = new Define.Quiz_OX(text, answerText, answer, explanation);

            quizs.Add(q);
        }
    }

    protected override void GameOver(bool isWin)
    {
        lockButton = true;
        base.GameOver(isWin);
    }
    
    void Input(int input)
    {
        if (lockButton == true) return;

        lockButton = true;

        StartCoroutine(Explanation(input));
    }

    IEnumerator Explanation(int input)
    {
        if (input == answer)
        {
            pannel_Text.text = "정답입니다!\n";
            MinigameManager.Score += 30;
        }
        else
        {
            pannel_Text.text = "오답입니다.\n";

        }
        pannel_Text.text += $"해설: {quizs[index].explanation}";

        yield return new WaitForSeconds(5f);

        index++;

        if(index != quizs.Count)
            StartCoroutine(ChangeQuiz());
        else
        {
            GameOver(true);
        }
    }

    IEnumerator ChangeQuiz()
    {       
        answer = 0;
        
        if(index != 0)
        {
            pannel_Text.text = "다음 문제입니다.";
        }
        else if (index == quizs.Count)
        {
            pannel_Text.text = "마지막 문제입니다.";
        }
        else
        {
            pannel_Text.text = "첫 번째 문제입니다.";
        }
        
        yield return new WaitForSeconds(2f);

        pannel_Text.text = quizs[index].text;
        answer = quizs[index].answer;
        lockButton = false;   
    }
}