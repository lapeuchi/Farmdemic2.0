using System.Collections;
using UnityEngine;

public class Define
{
    public enum DialogueEvent
    {
        MiniGame = 1,
        InfoPopup,
        Dialogue
    }

    public enum Minigame
    {  
        None = 0,
        OXQuiz = 1, // ox퀴즈
        MatchingDisinfectant = 2, // 소독약 용도 매칭
        HarmfulBirds = 3, // 해로운 새
        Disinfectant = 4, // 의심 가축 격리
    }
    
    public enum Rank
    {
        A,
        B,
        C,
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
        Worth,
        CrowCrying,
    }
    
    public enum Scene
    {
        None,
        Title,
        Game,
        Minigame
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