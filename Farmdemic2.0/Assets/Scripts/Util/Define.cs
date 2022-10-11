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

    public struct Dialogue : IDataLoader
    {
        public string name;
        public string word;
        public Sprite model;

        public void Load<T>(string path) where T : IDataLoader
        {
             
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

public interface IDataLoader
{
    public void Load<T>(string path) where T : IDataLoader;
}