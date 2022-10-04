using System.Collections;
using UnityEngine;
public class Define
{
    public enum MiniGame
    {  
        none,
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
        public int answer;
        public string explanation;
    }


}