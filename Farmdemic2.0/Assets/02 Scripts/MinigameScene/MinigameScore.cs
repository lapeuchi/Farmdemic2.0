using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinigameScore : UI_Scene
{
    private int _score;
    public int Score {get {return _score;} private set {_score = value; if (_score < 0) _score = 0; }}

    TMP_Text score_Text;

    public override void Init()
    {
        base.Init();
    }

    public void Setting()
    {
        score_Text = GameObject.Find("Score_Text").GetComponent<TMP_Text>();
        Score = 0;
        score_Text.text = "0";
    }

    public void PlusScore(int score)
    {
        this.Score += score;
        SetText();
    }
    
    public void MultipleScore(int score, int n)
    {
        this.Score *= score * n;
        SetText();
    }

    public void SetScore(int score)
    {
        this.Score = score;
        SetText();
    }

    void SetText() => score_Text.text = Score.ToString();
}
