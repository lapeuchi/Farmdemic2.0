using System.Collections;
using UnityEngine;

public class Define
{
    public enum Minigame
    {  
        None = 0,

        OXQuiz = 1,
        BuildingPosPuzzle = 2,
        CleanList = 3,
        CleanSwipe = 4,
        MatchingDisinfectant = 5,
        MixDisinfectant = 6,
        HarmfulBirds = 7,
        RatDefence = 8,
        CheckComingAndGoing = 9,
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
        MainBgm
    }

    public enum SFX
    {
        ClickDialogue,
        Collect,
        Worth
    }
    
    public enum Scene
    {
        None,
        Title,
        Game,
        Minigame
    }
    
    public enum Story
    {
        None = 0,
        Chater1 = 11,
        Chater2,
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