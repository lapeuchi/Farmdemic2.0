using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSaver
{
    public static Dictionary<Define.Rank, int> ranks = new Dictionary<Define.Rank, int>();
    public static Dictionary<Define.Rank, int> rankScore = new Dictionary<Define.Rank, int>();
    public static void Init()
    {
        SetRank();
    }

    private static void SetRank()
    {
        rankScore.Add(Define.Rank.A, 25);
        rankScore.Add(Define.Rank.B, 20);
        rankScore.Add(Define.Rank.C, 15);
        
        for(int i = 0; i < (int)Define.Rank.F; i++)
        {
            Debug.Log((Define.Rank)i);
            ranks.Add((Define.Rank)i, 0);
            Debug.Log(ranks.ContainsKey((Define.Rank)i));
        }
    }
}
