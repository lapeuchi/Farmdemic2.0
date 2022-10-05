using System.Collections;
using UnityEngine;
public class Define
{
    public enum Minigame
    {  
        none,
        OXQuiz,
        Game_0,
        Game_1,
        Game_2,
        Game_3
    }

    public struct Dialogue
    {
        public string name;
        public string word;
        public Sprite model;

        public Dialogue(string name, string word, Sprite model)
        {
            this.name = name;
            this.word = word;
            this.model = model;
        }
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