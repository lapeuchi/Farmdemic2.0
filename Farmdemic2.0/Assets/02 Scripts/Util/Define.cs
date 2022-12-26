using System.Collections;
using UnityEngine;

public class Define
{
    public enum Event
    {
        MiniGame = 1,
        InfoPopup,
        Cutscene,
        Ending
    }

    public enum Minigame
    {  
        None = 0,
        OXQuiz = 1, // ox퀴즈
        MatchingDisinfectant = 2, // 소독약 용도 매칭
        HarmfulBirds = 3, // 해로운 새
        Quarantine = 4, // 의심 가축 격리
    }
    
    public enum Rank
    {
        A,
        B,
        C,
        F
    }

    public enum DataType
    {
        Dialogue,
        OX_Quiz
    }
    
    public enum BGM
    {
        MainBgm,
        MinigameBgm
        
    }

    public enum SFX
    {
        // main
        ClickDialogue,
        Writting,

        // Quiz 
        Collect,
        Worth,

        // bird
        CrowCrying,
        Fire,

        // card
        CardGather,
        CardMix,

        // chick
        Pop,
        Chicken,
        Flapping,

        // minigame result
        WriteRank
    }
    
    public enum Scene
    {
        None,
        Title,
        Game,
        Minigame
    }

    public enum InputEvent
    {
        Click
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