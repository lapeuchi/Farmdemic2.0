using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OXQuiz : MiniGameBase
{
    int input;
    int answer;
    
    bool lockButton;

    [SerializeField] Button O_Button;
    [SerializeField] Button X_Button;

    [SerializeField] Text pannel_Text;

    public List<Define.Quiz_OX> quizs = new List<Define.Quiz_OX>();
    int index = 0;

    private void Awake() 
    {
        Init();
        O_Button.onClick.AddListener(()=>Input(1));
        X_Button.onClick.AddListener(()=>Input(2));
        lockButton = true;
    }

    protected override void Init()
    {
        O_Button = GameObject.Find("O_Button").GetComponent<Button>();
        X_Button = GameObject.Find("X_Button").GetComponent<Button>();
        pannel_Text = GameObject.Find("Pannel_Text").GetComponent<Text>();
        
        for(int i = 0; i < 3; i++)
        {
            int a = Random.Range(-100, 1000);
            int b = Random.Range(-100, 1000);
            int ans = Random.Range(0, 1) == 0 ? a + b : Random.Range(-200, 2000);

            Define.Quiz_OX q;
            q.text = $"({a}) + ({b}) = {ans}";
            q.answer = ans = a+b;
        }
    }

    protected override void GameOver(bool isWin)
    {
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
            pannel_Text.text = "정답입니다!";
        }
        else
        {
            pannel_Text.text = "오답입니다.";
        }
        
        yield return new WaitForSeconds(1f);
        StartCoroutine(ChangeQuiz());
    }

    

    IEnumerator ChangeQuiz()
    {       
        input = 0;
        answer = 0;
        
        if(index != 0)
        {
            pannel_Text.text = "다음 문제입니다.";
        }
        else
        {
            pannel_Text.text = "다음 문제입니다.";
        }
        

        pannel_Text.text = quizs[0].text;
        answer = quizs[0].answer;
        lockButton = false;
        

        yield return new WaitForSeconds(1f);
        
        
    }

    
}