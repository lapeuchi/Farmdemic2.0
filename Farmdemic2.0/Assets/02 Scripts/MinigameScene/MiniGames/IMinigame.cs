using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public interface IMinigame
{
    // MinigameManager.GameStartEffect()에서 호출되는 게임 시작함수
    void GameStart();
    // MinigameManager.GameOver()에서 호출되는 게임 시작함수
    void GameOver();    
}