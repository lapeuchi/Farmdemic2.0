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

    public enum DataType
    {
        Dialogue,
        OX_Quiz
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
        public int answer;
        public string explanation;
    }
}
public interface IDataLoader
{
    public void Load<T>(string path) where T : IDataLoader;
}