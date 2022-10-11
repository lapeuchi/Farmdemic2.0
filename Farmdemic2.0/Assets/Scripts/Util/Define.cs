using System.Collections;
using UnityEngine;

public class Define
{
    public enum Minigame
    {  
        None,
        OXQuiz,
        Game_0,
        Game_1,
        Game_2,
        Game_3
    }
    
    public enum Rank
    {
        None,
        S,
        A,
        B,
        C,
        D
    }

    public enum DataType
    {
        Dialogue,
        OX_Quiz
    }

    public enum BGM
    {

    }

    public enum SFX
    {
        
    }

    public struct Quiz_OX
    {
        public string text;
        public string answerText;
        public int answer;
        public string explanation;

        public Quiz_OX(string text, string answerText, int answer, string explanation)
        {
            this.text = text;
            this.answerText = answerText;
            this.explanation = explanation;
            this.answer = answer;
        }
    }
}